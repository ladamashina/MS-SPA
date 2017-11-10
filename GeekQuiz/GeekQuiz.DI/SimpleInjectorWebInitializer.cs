
using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

using GeekQuiz.Core;
using System.Web;
using Microsoft.Owin;
using SimpleInjector.Advanced;
using System.Collections.Generic;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Owin;


[assembly: WebActivator.PostApplicationStartMethod(typeof(GeekQuiz.Di.SimpleInjectorInitializer), "Initialize")]

namespace GeekQuiz.Di
{
    

    public static class SimpleInjectorInitializer
    {

        public static Container GetContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();
            return container;
        }
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();


            System.Web.Mvc.DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {


            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<ApplicationDbContext>(Lifestyle.Scoped);
            container.Register<IAuthenticationManager>(() => container.GetInstance<IOwinContext>().Authentication, Lifestyle.Scoped);
            container.Register<IOwinContext>(() =>
                container.IsVerifying()
                    ? new OwinContext(new Dictionary<string, object>())
                    : HttpContext.Current.GetOwinContext(), Lifestyle.Scoped);

            container.Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(container.GetInstance<ApplicationDbContext>()), Lifestyle.Scoped);

            container.Register<TriviaContext>(Lifestyle.Scoped);
        }


     

       

    }

    
    
   
}