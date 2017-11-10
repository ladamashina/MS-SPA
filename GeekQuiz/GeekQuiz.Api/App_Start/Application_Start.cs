using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace GeekQuiz.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public class Startup
        {
            public void Configuration(IAppBuilder app)
            {
                // token generation
                app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
                {
                    AllowInsecureHttp = false,

                    TokenEndpointPath = new PathString("/token"),
                    AccessTokenExpireTimeSpan = TimeSpan.FromHours(8),

                    Provider = new SimpleAuthorizationServerProvider()
                });

                // token consumption
                app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

                app.UseWebApi(WebApiConfig.Register());
            }
        }
    }
}