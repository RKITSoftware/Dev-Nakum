using Bank_Management_System.Models;
using Bank_Management_System.BusinessLogic;
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

namespace Bank_Management_System.Basic_Auth
{
    /// <summary>
    /// provide authentication attributes 
    /// use basic token
    /// base64 encoded
    /// </summary>
    public class AuthenticationAttribute : AuthorizationFilterAttribute
    {
        #region Private Member
        private readonly Type _userType = typeof(UsersV1);
        #endregion

        #region Contructor
        /// <summary>
        /// Initializes a new instance of the AuthenticationAttribute class.
        /// </summary>
        public AuthenticationAttribute(Type userType)
        {
            _userType = userType;
        }
        public AuthenticationAttribute()
        {
            _userType = typeof(UsersV1);
        }
        #endregion

        #region Public Method
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

                    // validate the user's username and password
                    // to check version types
                    object objUser;
                    if (_userType == typeof(UsersV1))
                    {
                        objUser = BLValidateUser.ValidateUser(username, password,1);
                    }
                    else
                    {
                        objUser = BLValidateUser.ValidateUser(username, password,2);
                    }

                    if (objUser != null)
                    {
                        //add identity
                    
                        var identity = new GenericIdentity(username);
                        //identity.AddClaim(new Claim(ClaimTypes.Name, objUser.UserName));
                        //identity.AddClaim(new Claim(ClaimTypes.Email, objUser.Email));

                        identity.AddClaim(new Claim(ClaimTypes.Name, _userType.GetProperty("UserName").GetValue(objUser).ToString()));
                        identity.AddClaim(new Claim("Id", _userType.GetProperty("Id").GetValue(objUser).ToString()));
                        identity.AddClaim(new Claim(ClaimTypes.Email, _userType.GetProperty("Email").GetValue(objUser).ToString()));


                        //IPrincipal principal = new GenericPrincipal(identity, objUser.Role.Split(','));
                        IPrincipal principal = new GenericPrincipal(identity, _userType.GetProperty("Role").GetValue(objUser).ToString().Split(','));

                        Thread.CurrentPrincipal = principal;

                        // if current context is exist assign the principal
                        if (HttpContext.Current != null)
                        {
                            HttpContext.Current.User = principal;
                        }
                        else
                        {
                            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Authorization Denied");
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

        #endregion
    }
}