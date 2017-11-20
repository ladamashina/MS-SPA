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
           
            config.EnableCors();
          
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
          
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
