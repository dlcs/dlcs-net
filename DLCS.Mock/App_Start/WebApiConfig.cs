using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using Newtonsoft.Json;

namespace DLCS.Mock
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Contexts",
                routeTemplate: "contexts/{typeName}.jsonld",
                defaults: new { controller = "Contexts", action = "Index" }
            );

            config.Routes.MapHttpRoute(
                name: "Documentation",
                routeTemplate: "vocab",
                defaults: new { controller = "Documentation", action = "Vocab" }
            );

            config.Routes.MapHttpRoute(
                name: "DlcsApiRoot",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { controller = "DlcsApi", action = "EntryPoint", id = RouteParameter.Optional }
            );


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
