using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http.Filters;
using DLCS.HydraModel.Config;

namespace DLCS.Mock.ApiApp
{
    public class AddHydraApiHeaderFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var headers = actionExecutedContext.Response.Headers;
            AddIfMissing(headers, "Link", "<" + Constants.BaseUrl + "/vocab#>; rel=\"http://www.w3.org/ns/hydra/core#apiDocumentation\"");
            AddIfMissing(headers, "Access-Control-Allow-Origin", "*");
            AddIfMissing(headers, "Access-Control-Expose-Headers", "Link");
            var contHeaders = actionExecutedContext.Response.Content.Headers;
            if (contHeaders.ContentType != null)
            {
                if (contHeaders.ContentType.MediaType.StartsWith("application/json"))
                {
                    contHeaders.ContentType = new MediaTypeHeaderValue("application/ld+json");
                }
            }
        }

        private void AddIfMissing(HttpResponseHeaders headers, string header, string value)
        {
            if (!headers.Contains(header))
            {
                headers.Add(header, value);
            }
        }
    }
}