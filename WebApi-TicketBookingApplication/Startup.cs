using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using WebApi_TicketBookingApplication.Providers;
using System.Web.Http;

[assembly: OwinStartup(typeof(WebApi_TicketBookingApplication.Startup))]

namespace WebApi_TicketBookingApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //enable cors origin requests            
            // app.UseCors(Cors.CorsOptions.AllowAll);
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            var myProvider = new AuthorizationServiceProvider();
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
               // AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(1),
                Provider = myProvider
            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
           // ConfigureAuth(app);
        }
    }
}
