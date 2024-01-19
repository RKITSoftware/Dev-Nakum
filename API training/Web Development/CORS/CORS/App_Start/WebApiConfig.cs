using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CORS
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // for entire project - cors is enabled 
            //EnableCorsAttribute cors = new EnableCorsAttribute("*","*","*");
            //config.EnableCors(cors);


            // for only specific controller - cors is enabled
            config.EnableCors();
        }
    }
}
