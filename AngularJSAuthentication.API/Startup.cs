

using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;
[assembly: OwinStartup(typeof(AngularJSAuthentication.API.Startup))] //init once server starts (OWIN)
namespace AngularJSAuthentication.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration(); //use to configure API routes
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll); //Allow CORS requests
            app.UseWebApi(config); //Wires API to Server Pipeline
            System.Diagnostics.Debug.WriteLine("Starting up!");
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            Microsoft.Owin.Security.OAuth.OAuthAuthorizationServerOptions OAuthServerOptions = new Microsoft.Owin.Security.OAuth.OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"), //path for gen tokens
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1), //token lives for 1 day
                Provider =  new SimpleAuthorizationServerProvider() //custom SASP that extends the microsoft version

            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }

    }
}