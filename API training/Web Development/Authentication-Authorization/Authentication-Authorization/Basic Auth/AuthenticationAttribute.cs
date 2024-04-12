using Authentication_Authorization.BL;
using Authentication_Authorization.Models;
using System;
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
                // to check authorization header is exist or not 
                if (actionContext.Request.Headers.Authorization == null)
                {
                    // header is not exists - unauthorized user
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You are not allowed to enter");
                }
                else
                {
                    // get the auth token from header
                    string authToken = actionContext.Request.Headers.Authorization.Parameter;
                    //username : password  -- base64 encoded

                    // decode auth token using base64 method
                    string decodeAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                    string[] user = decodeAuthToken.Split(':');

                    string username = user[0];   
                    string password = user[1];

                    BLUsers objBLUsers = new BLUsers();
                    // validate the user's username and password
                    if (objBLUsers.Login(username, password))
                    {
                        //add identity
                        Users userDetails = objBLUsers.GetUserDetails(username, password);
                        var identity = new GenericIdentity(username);
                        identity.AddClaim(new Claim(ClaimTypes.Name,userDetails.Username));
                        identity.AddClaim(new Claim("Id",(userDetails.Id).ToString()));

                        IPrincipal principal = new GenericPrincipal(identity,userDetails.Role.Split(','));
                        Thread.CurrentPrincipal = principal;    

                        // if current context is exist assign the principal
                        if(HttpContext.Current != null)
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