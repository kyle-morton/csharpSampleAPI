using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AngularJSAuthentication.API
{
    class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" }); //Allow CORS (cross-site requests)

            using (AuthRepository _repo = new AuthRepository())
            {
                IdentityUser user = await _repo.FindUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
            }

            //return identity object based on their user,role in db
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));
            //can add more claims, but token size will increase

            context.Validated(identity); //generates the token based on identity


        }

    }
}
