using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Mvc;
using DLCS.Mock.ApiApp;
using Newtonsoft.Json;

namespace DLCS.Mock
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.EnableSystemDiagnosticsTracing();

            config.Filters.Add(new AddHydraApiHeaderFilter());
            config.MessageHandlers.Add(new HeadHandler());

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
                routeTemplate: "vocab/{format}",
                defaults: new { controller = "Documentation", action = "Vocab", format = RouteParameter.Optional }
            );

            // Routing for customer and its operations
            config.Routes.MapHttpRoute(
                name: "Customers",
                routeTemplate: "customers/{customerId}",
                defaults: new { controller = "Customers", action = "Index", customerId = RouteParameter.Optional }
            );

            // Routing for customer and its operations
            config.Routes.MapHttpRoute(
                name: "Queue",
                routeTemplate: "customers/{customerId}/queue/{action}",
                defaults: new { controller = "Queue", action = "Index" }
                );

            config.Routes.MapHttpRoute(
                name: "SpaceImages",
                routeTemplate: "customers/{customerId}/spaces/{spaceId}/images/{id}",
                defaults: new {controller = "SpaceImages", action = "Image", id = UrlParameter.Optional}
                );

            // Routing for customer and its operations
            config.Routes.MapHttpRoute(
                name: "CustomerProperties",
                routeTemplate: "customers/{customerId}/{action}/{propertyId}",
                defaults: new
                    {
                        controller = "Customers",
                        propertyId = RouteParameter.Optional
                    }
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
