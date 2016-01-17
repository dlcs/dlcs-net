using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Configuration;
using System.Net;
using System.Text;
using DLCS.Client.Images;
using DLCS.Client.PDFs;
using DLCS.Client.Util;

namespace DLCS.Client.Dlcs
{
    public class Dlcs : ILibraryCloudServices
    {
        // http://api.dlcs.io/{cust-id}/image-json.aspx
        private readonly Uri _imageRegistrationUri, _dlcsImageQueryUri, _dlcsImageStateQueryUri;

        private readonly string _basicAuthUsername, _basicAuthPassword;

        private readonly JsonSerializerSettings _settings;
        private readonly WebClientConfig _webClientConfig;

        public Dlcs(
            string imageRegistrationUri,
            string dlcsImageQueryUri,
            string dlcsImageStateQueryUri,
            string basicAuthUsername,
            string basicAuthPassword)
        {
            _imageRegistrationUri = new Uri(imageRegistrationUri);
            _dlcsImageQueryUri = new Uri(dlcsImageQueryUri);
            _dlcsImageStateQueryUri = new Uri(dlcsImageStateQueryUri);
            _basicAuthUsername = basicAuthUsername;
            _basicAuthPassword = basicAuthPassword;

            // http://stackoverflow.com/questions/23170918/is-the-jsonserializersettings-thread-safe
            // TODO - is this (and its CamelCasePropertyNamesContractResolver) thread safe?
            _settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            
            var proxyConfig = new ProxyConfig
            {
                Address = ConfigurationManager.AppSettings["ProxyAddress"],
                Domain = ConfigurationManager.AppSettings["ProxyDomain"],
                Username = ConfigurationManager.AppSettings["ProxyUsername"],
                Password = ConfigurationManager.AppSettings["ProxyPassword"]
            };
            int proxyPort;
            int.TryParse(ConfigurationManager.AppSettings["ProxyPort"], out proxyPort);
            proxyConfig.Port = proxyPort;

            _webClientConfig = new WebClientConfig
            {
                ProxyConfig = proxyConfig,
                Expect100ContinueIfProxy = true
            };
        }

        private string _basicAuthHeader;

        private string GetBasicAuthHeader()
        {
            if(_basicAuthHeader != null)
            {
                return _basicAuthHeader;
            }
            var b64 = Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(_basicAuthUsername + ":" + _basicAuthPassword));
            _basicAuthHeader = string.Format("Basic {0}", b64);
            return _basicAuthHeader;
        }

        private void AddHeaders(WebClient wc)
        {
            wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            wc.Headers.Add(HttpRequestHeader.Authorization, GetBasicAuthHeader());
        }


        private Operation<TRequest, TResponse> DoOperation<TRequest, TResponse>(TRequest requestObject, Uri uri) 
        {
            var operation = new Operation<TRequest, TResponse>();
            using (var wc = WebClientProvider.GetWebClient(_webClientConfig, uri, 360000))
            {
                try
                {
                    operation.RequestObject = requestObject;
                    operation.RequestJson = JsonConvert.SerializeObject(operation.RequestObject, Formatting.Indented, _settings);
                    AddHeaders(wc);

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


        public Operation<Image[], ImageBatchRegistration> RegisterImages(Image[] images)
        {
            return DoOperation<Image[], ImageBatchRegistration>(images, _imageRegistrationUri);
        }


        public Operation<ImageQuery, ImageStateQueryResponse> GetImageStates(ImageQuery query)
        {
            return DoOperation<ImageQuery, ImageStateQueryResponse>(query, _dlcsImageStateQueryUri);
        }

        public Operation<ImageQuery, Image[]> GetImages(ImageQuery query)
        {
            return DoOperation<ImageQuery, Image[]>(query, _dlcsImageQueryUri);
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
