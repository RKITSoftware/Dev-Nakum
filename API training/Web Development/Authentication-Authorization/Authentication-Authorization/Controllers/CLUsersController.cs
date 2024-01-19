using Authentication_Authorization.Basic_Auth;
using Authentication_Authorization.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Authentication_Authorization.Controllers
{
    /// <summary>
    /// Manage all the user 
    /// Based on roles - executes the request 
    /// </summary>
    [Authentication]
    public class CLUsersController : ApiController
    {
        [HttpGet]
        [Route("api/users")]
        [AuthorizationAttribute(Roles = "admin")]
        public HttpResponseMessage GetAllUser()
        {
            return Request.CreateResponse(HttpStatusCode.OK, Users.GetAllUser());
        }

    }
}
