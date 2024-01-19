using JWT.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

[assembly: OwinStartup(typeof(JWT.Startup))]

namespace JWT
{
    public class Startup
    {
        /// <summary>
        /// configuration for generating token 
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            //allows all the header and method from anywhere
            app.UseCors(CorsOptions.AllowAll);

            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions()
            {
                // its allow both HTTP and HTTPS
                AllowInsecureHttp = true,

                // declare the end points of token
                TokenEndpointPath = new PathString("/token"),
                
                //declare the expire time of token - 7 days
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60*24*7),

                //declare provider
                Provider = new AppAuthorizationServerProvider()
            };

            //pass the all option to authorization server 
            app.UseOAuthAuthorizationServer(options);

            // use the bearer token for validation
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            // create the HTTP Configuration
            HttpConfiguration config = new HttpConfiguration();

            //register config to webApi
            WebApiConfig.Register(config);
        }
    }
}
