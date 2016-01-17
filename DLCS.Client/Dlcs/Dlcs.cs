using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using DLCS.Client.Config;
using DLCS.Client.Images;
using DLCS.Client.PDFs;
using DLCS.Client.Util;

namespace DLCS.Client.Dlcs
{
    public class Dlcs : IDlcs
    {
        private readonly DlcsConfig _dlcsConfig;
        private readonly WebClientConfig _webClientConfig;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public Dlcs(
            DlcsConfig dlcsConfig,
            WebClientConfig webClientConfig)
        {
            _dlcsConfig = dlcsConfig;
            if (!_dlcsConfig.ApiEntryPoint.EndsWith("/"))
            {
                _dlcsConfig.ApiEntryPoint += "/";
            }
            _webClientConfig = webClientConfig;
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                // http://stackoverflow.com/questions/23170918/is-the-jsonserializersettings-thread-safe
                // TODO - is this (and its CamelCasePropertyNamesContractResolver) thread safe?
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        private string _basicAuthHeader;

        private string GetBasicAuthHeader()
        {
            if(_basicAuthHeader != null)
            {
                return _basicAuthHeader;
            }
            var credentials = _dlcsConfig.ApiKey + ":" + _dlcsConfig.ApiSecret;
            var b64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials));
            _basicAuthHeader = "Basic " + b64;
            return _basicAuthHeader;
        }

        private void AddHeaders(WebClient wc)
        {
            wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            wc.Headers.Add(HttpRequestHeader.Authorization, GetBasicAuthHeader());
        }

        private Operation<TRequest, TResponse> PostOperation<TRequest, TResponse>(TRequest requestObject, Uri uri)
        {
            var operation = new Operation<TRequest, TResponse> { HttpMethod = "POST" };
            return DoOperation(requestObject, uri, operation);
        }
        private Operation<TRequest, TResponse> GetOperation<TRequest, TResponse>(TRequest requestObject, Uri uri)
        {
            var operation = new Operation<TRequest, TResponse> { HttpMethod = "GET" };
            return DoOperation(requestObject, uri, operation);
        }

        private Operation<TRequest, TResponse> DoOperation<TRequest, TResponse>(
            TRequest requestObject, Uri uri, Operation<TRequest, TResponse> operation) 
        {
            // TODO - we may need to switch to System.Net.HttpClient for this lot.
            using (var wc = WebClientProvider.GetWebClient(_webClientConfig, uri, 360000))
            {
                try
                {
                    operation.RequestObject = requestObject;
                    operation.RequestJson = JsonConvert.SerializeObject(
                        operation.RequestObject, Formatting.Indented, _jsonSerializerSettings);
                    AddHeaders(wc);
                    switch (operation.HttpMethod)
                    {
                        case "POST":
                            operation.ResponseJson = wc.UploadString(uri, "POST", operation.RequestJson);
                            break;
                        case "GET":
                            wc.QueryString = new NameValueCollection { { "q", operation.RequestJson } };
                            operation.ResponseJson = wc.DownloadString(uri);
                            break;
                        case "PUT":
                            throw new NotImplementedException("PUT - do this with HttpClient");
                        case "DELETE":
                            // TODO: this REALLY needs HttpClient!!
                            var bytes = wc.UploadData(uri, "DELETE", new byte[0]);
                            operation.ResponseJson = wc.Encoding.GetString(bytes);
                            break;
                        default:
                            throw new NotImplementedException("Unknown HTTP Method " + operation.HttpMethod);
                    }
                    operation.ResponseJson = wc.UploadString(uri, "POST", operation.RequestJson);
                    operation.ResponseObject = JsonConvert.DeserializeObject<TResponse>(operation.ResponseJson);
                }
                catch (Exception ex)
                {
                    operation.Error = GetError(ex);
                }
            }
            return operation;
        }

        private static Uri _imageQueueUri;
        private void InitQueue()
        {
            if (_imageQueueUri == null)
            {
                // TODO: At this point we would work out RESTfully where the queue for this customer is
                // and cache that for a bit - we assume the API stays reasonably stable.
                _imageQueueUri = new Uri(_dlcsConfig.ApiEntryPoint + _dlcsConfig.CustomerId + "/queue");
            }
        }


        public Operation<Image[], ImageBatchRegistration> RegisterImages(Image[] images)
        {
            InitQueue();
            return PostOperation<Image[], ImageBatchRegistration>(images, _imageQueueUri);
        }


        public Operation<ImageQuery, ImageStateQueryResponse> GetImageStates(ImageQuery query)
        {
            InitQueue();
            return GetOperation<ImageQuery, ImageStateQueryResponse>(query, _imageQueueUri);
        }

        public Operation<ImageQuery, Image[]> GetImages(ImageQuery query)
        {
            // can you do a query across spaces? Should be able to.
            // If so, where do you submit the GET to?
            // GET /customer returns a customer model; should it return the first page of images from across the spaces?
            // doesn't feel right - but what do you send the GET to?
            // GET /customer/allImages maybe (another reserved word that spaces can't be called)

            // If space is specified then it's just GET /customer/space?q={...imageQuery...}
            // TODO- this is temporary, talk over with Adam
            var imageQueryUri = _dlcsConfig.ApiEntryPoint + _dlcsConfig.CustomerId;
            if (query.Space.HasValue)
            {
                imageQueryUri += "/" + query.Space.Value;
            }
            return GetOperation<ImageQuery, Image[]>(query, new Uri(imageQueryUri));
        }

        private static Error GetError(Exception ex)
        {
            if (ex is WebException)
            {
                var we = (WebException)ex;
                var badResponse = (HttpWebResponse)we.Response;
                if (badResponse != null)
                {
                    return new Error
                    {
                        Status = (int) badResponse.StatusCode,
                        Message = we.Message
                    };
                }
                return new Error
                {
                    Status = 0,
                    Message = we.Message
                };
            }
            return new Error
            {
                Status = 0,
                Message = ex.Message
            };
        }




        public MakePdfResponse MakePdf(MakePdfRequest pdfRequest)
        {
            throw new NotImplementedException();
        }

    }
}
