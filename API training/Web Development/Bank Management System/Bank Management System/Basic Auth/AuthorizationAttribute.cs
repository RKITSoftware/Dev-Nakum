using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Bank_Management_System.Basic_Auth
{
    /// <summary>
    /// handle the unauthorized request
    /// </summary>
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// It is called when the authorization check fails.
        /// handle the unauthorized request to check if user is authenticated or not 
        /// </summary>
        /// <param name="actionContext"></param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }
    }
}