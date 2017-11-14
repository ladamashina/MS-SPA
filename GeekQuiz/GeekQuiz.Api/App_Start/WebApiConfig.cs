using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using GeekQuiz.Di;
using Newtonsoft.Json.Serialization;
using Owin;
using SimpleInjector.Integration.WebApi;

namespace GeekQuiz.Api
{
    public static class WebApiConfig
    {
        
          
        public static void Register(IAppBuilder app)
        {
           var container =  SimpleInjectorInitializer.GetContainer(); //simple injector
            var config = new HttpConfiguration();
            app.UseWebApi(config);
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            // Web API configuration and services
            config.EnableCors();
            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);


            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.UseDataContractJsonSerializer = false;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
