using Authentication_Authorization.BL;
using Authentication_Authorization.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Authentication_Authorization.Basic_Auth
{
    /// <summary>
    /// provide authentication attributes 
    /// use basic token
    /// base64 encoded
    /// </summary>
    public class AuthenticationAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        ///     authenticate the user using basic token 
        ///     use base64 encoded method
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                // Check for the presence of the Authorization header
                if (actionContext.Request.Headers.Authorization == null)
                {
                    // No Authorization header found - user is not authorized.
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You are not allowed to enter");
                }
                else
                {
                    // Extract the authentication token from the header:
                    string authToken = actionContext.Request.Headers.Authorization.Parameter;

                    // 3. Validate the format of the token (expected to be Base64-encoded username:password):
                    if (string.IsNullOrEmpty(authToken) || !authToken.Contains(':'))
                    {
                        actionContext.Response = actionContext.Request.CreateErrorResponse(
                            HttpStatusCode.Unauthorized, "Invalid authentication token format.");
                        return;
                    }



                    // Decode the Base64-encoded token to retrieve username and password
                    string decodeAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                    string[] user = decodeAuthToken.Split(':');

                    if (user.Length != 2)
                    {
                        actionContext.Response = actionContext.Request.CreateErrorResponse(
                            HttpStatusCode.Unauthorized, "Invalid authentication token format.");
                        return;
                    }
                    string username = user[0];   
                    string password = user[1];


                    BLUsers objBLUsers = new BLUsers();
                    // validate the user's username and password
                    if (objBLUsers.Login(username, password))
                    {
                        //build a user identity and claims
                        Users userDetails = objBLUsers.GetUserDetails(username, password);
                        var identity = new GenericIdentity(username);
                        identity.AddClaim(new Claim(ClaimTypes.Name,userDetails.Username));
                        identity.AddClaim(new Claim("Id",(userDetails.Id).ToString()));     // Custom claim for user ID

                        //Create a principal object representing the authenticated user
                        IPrincipal principal = new GenericPrincipal(identity,userDetails.Role.Split(','));  // roles are comma-separated
                        Thread.CurrentPrincipal = principal;

                        // Set the current principal in the thread and (optionally) in the HttpContext
                        if (HttpContext.Current != null)
                        {
                            HttpContext.Current.User = principal;
                        }
                        else
                        {
                            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,"Authorization Denied");
                        }
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You are not allowed to enter");
                    }
                }
            }
            catch (Exception)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
            
        }
    }
}