using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Bank_Management_System.Attributes
{
    /// <summary>
    /// Custom authorization attribute for handling unauthorized requests.
    /// </summary>
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        #region Protected Method
        /// <summary>
        /// Overrides the HandleUnauthorizedRequest method to handle unauthorized requests.
        /// </summary>
        /// <param name="actionContext">HttpActionContext for the current request.</param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                // Set response to Forbidden if the user is not authenticated
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }
        #endregion
    }
}
