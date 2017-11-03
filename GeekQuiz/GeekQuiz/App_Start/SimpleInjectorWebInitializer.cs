using System;
using System.Reflection;
using GeekQuiz.Controllers;

[assembly: WebActivator.PostApplicationStartMethod(typeof(GeekQuiz.App_Start.SimpleInjectorWebInitializer), "Initialize")]

namespace GeekQuiz.App_Start
{
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using Microsoft.Owin.Security;
    using Microsoft.Owin;
    using SimpleInjector.Advanced;
    using System.Collections.Generic;
    using System.Web;
    using GeekQuiz.Models;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using System.Web.Mvc;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;

    public static class SimpleInjectorWebInitializer
    {
        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            //container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
       
            container.Verify();
            
            //GlobalConfiguration.Configuration.DependencyResolver =
            //    new SimpleInjectorWebApiDependencyResolver(container);

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<IdentityFactoryOptions<ApplicationUserManager>>(Lifestyle.Scoped);
            container.Register<ApplicationDbContext>(Lifestyle.Scoped);

            container.Register<IUserStore<ApplicationUser>>(() =>
                new UserStore<ApplicationUser>(container.GetInstance<ApplicationDbContext>()), Lifestyle.Scoped);

            container.Register(() => container.IsVerifying()
                ? new OwinContext(new Dictionary<string, object>()).Authentication
                : HttpContext.Current.GetOwinContext().Authentication, Lifestyle.Scoped);
        }

    }
    
}