using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DLCS.Mock.ApiApp
{
    public class HeadHandler : DelegatingHandler
    {
        // http://www.strathweb.com/2013/03/adding-http-head-support-to-asp-net-web-api/
        private const string Head = "IsHead";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Head)
            {
                request.Method = HttpMethod.Get;
                request.Properties.Add(Head, true);
            }

            var response = await base.SendAsync(request, cancellationToken);

            object isHead;
            response.RequestMessage.Properties.TryGetValue(Head, out isHead);

            if (isHead != null && ((bool)isHead))
            {
                var oldContent = await response.Content.ReadAsByteArrayAsync();
                var content = new StringContent(string.Empty);
                content.Headers.Clear();

                foreach (var header in response.Content.Headers)
                {
                    content.Headers.Add(header.Key, header.Value);
                }

                content.Headers.ContentLength = oldContent.Length;
                response.Content = content;
            }

            return response;
        }
    }
}
