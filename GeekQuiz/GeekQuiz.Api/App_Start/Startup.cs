using System;
using System.Threading.Tasks;
using System.Web.Http;
using GeekQuiz.DI;
using Microsoft.Owin;
using Owin;
   
[assembly: OwinStartup(typeof(GeekQuiz.Api.Startup))]

namespace GeekQuiz.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            System.Data.Entity.Database.SetInitializer(new TriviaDatabaseInitializer());
            WebApiConfig.Register(app);
           
        }
    }
}
