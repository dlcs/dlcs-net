using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using DLCS.WebClient.Config;
using DLCS.WebClient.Model;
using DLCS.WebClient.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Image = DLCS.WebClient.Model.Image;

namespace DLCS.WebClient.Dlcs
{
    public class Dlcs : IDlcs
    {
        internal readonly DlcsConfig DlcsConfig;
        internal readonly WebClientConfig WebClientConfig;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

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

        private void AddHeaders(System.Net.WebClient wc)
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
                    if (requestObject != null)
                    {
                        operation.RequestObject = requestObject;
                        operation.RequestJson = JsonConvert.SerializeObject(
                            operation.RequestObject, Formatting.Indented, _jsonSerializerSettings);
                    }
                    AddHeaders(wc);
                    switch (operation.HttpMethod)
                    {
                        case "POST":
                            operation.ResponseJson = wc.UploadString(operation.Uri, "POST", operation.RequestJson);
                            break;
                        case "GET":
                            if (requestObject != null)
                            {
                                wc.QueryString = new NameValueCollection {{"q", operation.RequestJson}};
                            }
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
                    operation.ResponseObject = JsonConvert.DeserializeObject<TResponse>(operation.ResponseJson);
                }
                catch (Exception ex)
                {
                    operation.Error = GetError(ex);
                }
            }
            return operation;
        }



        public Operation<ImageQuery, HydraImageCollection> GetImages(ImageQuery query, int defaultSpace)
        {
            int space = defaultSpace;
            if (query.Space.HasValue) space = query.Space.Value;
            var imageQueryUri = string.Format("{0}customers/{1}/spaces/{2}/images", 
                DlcsConfig.ApiEntryPoint, DlcsConfig.CustomerId, space);
            return GetOperation<ImageQuery, HydraImageCollection>(query, new Uri(imageQueryUri));
        }

        public Operation<ImageQuery, HydraImageCollection> GetImages(string nextUri)
        {
            return GetOperation<ImageQuery, HydraImageCollection>(null, new Uri(nextUri));
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


        private static Uri _imageQueueUri;
        private void InitQueue()
        {
            if (_imageQueueUri == null)
            {
                // TODO: At this point we would work out RESTfully where the queue for this customer is
                // and cache that for a bit - we assume the API stays reasonably stable.
                _imageQueueUri = new Uri(string.Format("{0}customers/{1}/queue", DlcsConfig.ApiEntryPoint, DlcsConfig.CustomerId));
            }
        }



        public Operation<HydraImageCollection, Batch> RegisterImages(HydraImageCollection images)
        {
            InitQueue();
            return PostOperation<HydraImageCollection, Batch>(images, _imageQueueUri);
        }

        
        public string GetRoleUri(string accessCondition)
        {
            // https://api.dlcs.io/customers/1/roles/requiresRegistration
            return string.Format("{0}customers/{1}/roles/{2}", DlcsConfig.ApiEntryPoint, DlcsConfig.CustomerId, ToCamelCase(accessCondition.Trim()));
        }


        /// <summary>
        /// converts "Some list of strings" to "someListOfStrings"
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToCamelCase(string s)
        {
            var sb = new StringBuilder();
            bool previousWasSpace = false;
            foreach (char c in s.Trim())
            {
                if (Char.IsLetterOrDigit(c))
                {
                    sb.Append(previousWasSpace ? Char.ToUpperInvariant(c) : c);
                }
                previousWasSpace = Char.IsWhiteSpace(c);
            }
            return sb.ToString();
        }
    }
}
