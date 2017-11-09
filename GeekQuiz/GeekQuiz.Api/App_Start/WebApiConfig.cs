using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GeekQuiz.Api
{
    public static class WebApiConfig
    {
        
          
        public static void Register(HttpConfiguration config)
        {
            
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            // Web API configuration and services
            config.EnableCors();
            var formatters = GlobalConfiguration.Configuration.Formatters;

            formatters.Remove(formatters.XmlFormatter);


            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.UseDataContractJsonSerializer = false;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
