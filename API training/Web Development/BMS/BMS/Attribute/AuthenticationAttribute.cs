using BMS.BL;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace BMS.Basic_Auth
{
    /// <summary>
    /// provide authentication attributes 
    /// use basic token
    /// base64 encoded
    /// </summary>
    public class AuthenticationAttribute : AuthorizeAttribute
    {
        #region Protected Method
        /// <summary>
        /// Overrides the IsAuthorized method to perform JWT token validation and set the user's identity.
        /// </summary>
        /// <param name="actionContext">HttpActionContext for the current request.</param>
        /// <returns>True if the user is authorized, false otherwise.</returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var authorizationHeader = actionContext.Request.Headers.Authorization;
            if (authorizationHeader == null || authorizationHeader.Scheme != "Bearer")
            {
                return false;
            }
            string token = authorizationHeader.Parameter;

            BLAuth objBLAuth = new BLAuth();
            Dictionary<string, string> user = objBLAuth.VerifyToken(token);
            if (user != null)
            {
                // Create the claims and add identity to principal

                //The name of the user on whose behalf the code is being run.
                GenericIdentity objGenericIdentity = new GenericIdentity(user["Id"]);


                //Adds a single claim to this claims identity.
                objGenericIdentity.AddClaim(new Claim("Id", user["Id"].ToString()));
                objGenericIdentity.AddClaim(new Claim(ClaimTypes.Name, user["Name"]));
                objGenericIdentity.AddClaim(new Claim(ClaimTypes.Email, user["Email"]));

                IPrincipal principal = new GenericPrincipal(objGenericIdentity, user["Role"].Split(','));
                Thread.CurrentPrincipal = principal;

                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = principal;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        #endregion
    }
}