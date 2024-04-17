using CustomHeaderVersioning.BL;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace CustomHeaderVersioning
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.Services.Replace(typeof(IHttpControllerSelector), new BLCustomHeader(config));
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
