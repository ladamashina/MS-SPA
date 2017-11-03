using System.Web.Http;
using GeekQuiz.Models;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace GeekQuiz.App_Start
{
    public class SimpleInjectiorWebAPIInitialaizer
    {
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            InitializeContainer(container);
            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);


            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            //    new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void InitializeContainer(Container container)
        {
            container.Register<TriviaContext>(Lifestyle.Scoped);
            container.Register<TriviaQuestion>(Lifestyle.Scoped);
            container.Register<TriviaOption>(Lifestyle.Scoped);
            container.Register<TriviaAnswer>(Lifestyle.Scoped);
        }
    }
}