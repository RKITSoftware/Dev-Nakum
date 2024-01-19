using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JWT.Provider
{
    /// <summary>
    /// Its provide basic token based authentication
    /// </summary>
    public class AppAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// method to validate client authentication
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        /// <summary>
        /// provide the resources to the valid user
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using(BLUserValidate userValidate = new BLUserValidate())
            {
                // validate the user based on username and password
                var user = userValidate.ValidateUser(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError("invalid_grant", "Username or Password is incorrect !!");
                    return;
                }

                // create the identity
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));


                foreach(string role in user.Role.Split(','))
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role.Trim()));    
                }

                //valdiate the identity
                context.Validated(identity);
            }
            
        }
    }

}