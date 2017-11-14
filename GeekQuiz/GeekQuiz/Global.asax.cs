﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GeekQuiz.Di;
using GeekQuiz.DI;
using GeekQuiz.Models;
using Microsoft.AspNet.Identity.Owin;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;


namespace GeekQuiz
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
           

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           
        }
    }
}
