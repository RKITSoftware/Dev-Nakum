using System.Collections.Generic;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http;
using Bank_Management_System.Business_Logic;
using System.Security.Principal;
using System.Security.Claims;
using System.Threading;

namespace Bank_Management_System.Attributes
{
    /// <summary>
    /// Custom authorization attribute for validating JWT tokens and setting the user's identity.
    /// </summary>
    public class JwtAuthorizationAttribute : AuthorizeAttribute
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
                GenericIdentity objGenericIdentity = new GenericIdentity(user["Id"]);
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
