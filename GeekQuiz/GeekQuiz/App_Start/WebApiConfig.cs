
using System.Linq;
using System.Web.Http;
using GeekQuiz.Auth;

using GeekQuiz.Di;
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using SimpleInjector.Integration.WebApi;
[assembly: OwinStartup(typeof(Startup))]
namespace GeekQuiz
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = SimpleInjectorInitializer.GetContainer();
            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );
            // Remove xml default serializer
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
