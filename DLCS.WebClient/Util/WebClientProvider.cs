using System;
using System.Net;
using DLCS.WebClient.Config;

namespace DLCS.WebClient.Util
{
    public class WebClientProvider
    {
        public static System.Net.WebClient GetWebClient(WebClientConfig config, Uri uri, int timeout = 60000, bool withProxy = true)
        {
            var pxCfg = config.ProxyConfig;
            if (string.IsNullOrWhiteSpace(pxCfg.Address))
            {
                withProxy = false;
            }
            if (!withProxy || uri.DnsSafeHost.EndsWith(".local")) // TODO - !!
            {
                return new WebClientWithTimeout(timeout);
            }
            string proxyUri = pxCfg.Address.Trim();
            if (pxCfg.Port > 0) proxyUri += ":" + pxCfg.Port;
            var proxy = new WebProxy { Address = new Uri(proxyUri) };
            if (!string.IsNullOrWhiteSpace(pxCfg.Username))
            {
                if (!string.IsNullOrWhiteSpace(pxCfg.Domain))
                {
                    proxy.Credentials = new NetworkCredential(
                        pxCfg.Username.Trim(), pxCfg.Password, pxCfg.Domain.Trim());
                }
                else
                {
                    proxy.Credentials = new NetworkCredential(
                        pxCfg.Username.Trim(), pxCfg.Password);
                }
            }

            if (config.Expect100ContinueIfProxy)
            {
                ServicePoint servicePoint = ServicePointManager.FindServicePoint(uri, proxy);
                servicePoint.Expect100Continue = false;
            }

            return new WebClientWithTimeout(timeout) {Proxy = proxy};
        }

        private class WebClientWithTimeout : System.Net.WebClient
        {
            /// <summary>
            /// Time in milliseconds
            /// </summary>
            private int Timeout { get; }

            public WebClientWithTimeout() : this(60000) { }

            public WebClientWithTimeout(int timeout)
            {
                Timeout = timeout;
            }

            protected override WebRequest GetWebRequest(Uri address)
            {
                var request = base.GetWebRequest(address);
                if (request != null)
                {
                    request.Timeout = Timeout;
                }
                return request;
            }
        }
    }
}