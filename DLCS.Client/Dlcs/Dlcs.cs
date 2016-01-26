using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using DLCS.Client.Config;
using DLCS.Client.Interface;
using DLCS.Client.Model;
using DLCS.Client.Model.Images;
using DLCS.Client.PDFs;
using DLCS.Client.Util;
using Image = DLCS.Client.Model.Images.Image;

namespace DLCS.Client.Dlcs
{
    public class Dlcs : IDlcs
    {
        internal readonly DlcsConfig DlcsConfig;
        internal readonly WebClientConfig WebClientConfig;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        private readonly Queue _queue;
        private readonly Customer _customer;

        public Dlcs(
            DlcsConfig dlcsConfig,
            WebClientConfig webClientConfig)
        {
            DlcsConfig = dlcsConfig;
            if (!DlcsConfig.ApiEntryPoint.EndsWith("/"))
            {
                DlcsConfig.ApiEntryPoint += "/";
            }
            WebClientConfig = webClientConfig;
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                // http://stackoverflow.com/questions/23170918/is-the-jsonserializersettings-thread-safe
                // TODO - is this (and its CamelCasePropertyNamesContractResolver) thread safe?
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            _queue = new Queue(this);
            _customer = new Customer(this);
        }

        public IQueue Queue
        {
            get { return _queue; }
        }

        public ICustomer Customer
        {
            get { return _customer; }
        }

        private string _basicAuthHeader;

        private string GetBasicAuthHeader()
        {
            if(_basicAuthHeader != null)
            {
                return _basicAuthHeader;
            }
            var credentials = DlcsConfig.ApiKey + ":" + DlcsConfig.ApiSecret;
            var b64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials));
            _basicAuthHeader = "Basic " + b64;
            return _basicAuthHeader;
        }

        private void AddHeaders(WebClient wc)
        {
            wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            wc.Headers.Add(HttpRequestHeader.Authorization, GetBasicAuthHeader());
        }

        internal Operation<TRequest, TResponse> PostOperation<TRequest, TResponse>(TRequest requestObject, Uri uri)
        {
            var operation = new Operation<TRequest, TResponse>
            {
                HttpMethod = "POST", Uri = uri
            };
            return DoOperation(requestObject, operation);
        }

        internal Operation<TRequest, TResponse> GetOperation<TRequest, TResponse>(TRequest requestObject, Uri uri)
        {
            var operation = new Operation<TRequest, TResponse>
            {
                HttpMethod = "GET", Uri = uri
            };
            return DoOperation(requestObject, operation);
        }

        private Operation<TRequest, TResponse> DoOperation<TRequest, TResponse>(
            TRequest requestObject, Operation<TRequest, TResponse> operation) 
        {
            // TODO - we may need to switch to System.Net.HttpClient for this lot.
            using (var wc = WebClientProvider.GetWebClient(WebClientConfig, operation.Uri, 360000))
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
                            operation.ResponseJson = wc.UploadString(operation.Uri, "POST", operation.RequestJson);
                            break;
                        case "GET":
                            wc.QueryString = new NameValueCollection { { "q", operation.RequestJson } };
                            operation.ResponseJson = wc.DownloadString(operation.Uri);
                            break;
                        case "PUT":
                            throw new NotImplementedException("PUT - do this with HttpClient");
                        case "DELETE":
                            // TODO: this REALLY needs HttpClient!!
                            var bytes = wc.UploadData(operation.Uri, "DELETE", new byte[0]);
                            operation.ResponseJson = wc.Encoding.GetString(bytes);
                            break;
                        default:
                            throw new NotImplementedException("Unknown HTTP Method " + operation.HttpMethod);
                    }
                    operation.ResponseJson = wc.UploadString(operation.Uri, "POST", operation.RequestJson);
                    operation.ResponseObject = JsonConvert.DeserializeObject<TResponse>(operation.ResponseJson);
                }
                catch (Exception ex)
                {
                    operation.Error = GetError(ex);
                }
            }
            return operation;
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
            var imageQueryUri = DlcsConfig.ApiEntryPoint + DlcsConfig.CustomerId;
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
