using Authentication_Authorization.Models;
using System;
using System.Collections.Generic;
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
    public class AuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                if (actionContext.Request.Headers.Authorization == null)
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You are not allowed to enter");
                }
                else
                {
                    string authToken = actionContext.Request.Headers.Authorization.Parameter;
                    //username : password  -- base64 encoded

                    string decodeAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                    string[] user = decodeAuthToken.Split(':');

                    string username = user[0];
                    string password = user[1];

                    if (BLValidateUser.Login(username, password))
                    {
                        Users userDetails = BLValidateUser.GetUserDetails(username, password);
                        var identity = new GenericIdentity(username);
                        identity.AddClaim(new Claim(ClaimTypes.Name,userDetails.Username));
                        identity.AddClaim(new Claim("Id",(userDetails.Id).ToString()));

                        IPrincipal principal = new GenericPrincipal(identity,userDetails.Role.Split(','));
                        Thread.CurrentPrincipal = principal;    

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