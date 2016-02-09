using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace iiifly.Dlcs
{
    public static class HttpClientBuilder
    {
        private static HttpClient _httpClient;
        private static readonly object ClientLock = new object();

        public static HttpClient GetHttpClient()
        {
            if (_httpClient == null)
            {
                lock (ClientLock)
                {
                    if (_httpClient == null)
                    {
                        _httpClient = new HttpClient();
                        if (ConfigurationManager.AppSettings["dlcs-use-auth"].ToLowerInvariant() == "true")
                        {
                            var key = ConfigurationManager.AppSettings["dlcs-key"];
                            var secret = ConfigurationManager.AppSettings["dlcs-secret"];
                            var authHeaderPlain = string.Format("{0}:{1}", key, secret);
                            var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(authHeaderPlain));
                            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                        }
                        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/ld+json"));
                    }
                }
            }
            return _httpClient;
        }
    }
}
