using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WebApi_TicketBookingApplication.Repository;
using static WebApi_TicketBookingApplication.Models.LoginRegisterModel;

namespace WebApi_TicketBookingApplication.Providers
{
    public class AuthorizationServiceProvider : OAuthAuthorizationServerProvider
    {
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if ((context.UserName == "" || context.UserName == null) || (context.Password == "" || context.Password == null))
            {
                context.SetError("invalid_grant", "Incorrect Email/Phone/Password");
                return;
            }

            Register userDetails = null;
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            userDetails = await LoginRepository.FindUser(context.UserName, context.Password);
            if (userDetails == null)
            {
                context.SetError("invalid_grant", "Incorrect Email/Phone/Password");
                return;
            }


            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //identity.AddClaim(new Claim(ClaimTypes.Name, userDetails.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userDetails.Id.ToString()));
            //identity.AddClaim(new Claim("role", "user"));
            //identity.AddClaim(new Claim("sub", context.UserName));

            //AuthenticationProperties properties = CreateProperties(userDetails.Id);
            //var ticket = new AuthenticationTicket(identity, properties);
            //context.Validated(ticket);

            context.Validated(identity);

        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);

        }

        public static AuthenticationProperties CreateProperties(string userId)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userId", userId }
            };
            return new AuthenticationProperties(data);
        }
    }
}