using BMS.Basic_Auth;
using BMS.BL;
using BMS.Models;
using System;
using System.Net;
using System.Security.Claims;
using System.Web.Http;

namespace BMS.Controllers
{
    /// <summary>
    /// handle the user's related operation - version 1
    /// </summary>
    [RoutePrefix("api/v1/users")]
    public class CLUsersV1Controller : ApiController
    {
        #region Private Member
        /// <summary>
        /// Initialize the object of the user's services
        /// </summary>
        private readonly BLUsersV1 _objBLUsersV1;
        #endregion


        #region Public Member
        /// <summary>
        /// Object of the response model
        /// </summary>
        public Response objResponse;
        #endregion

        #region Constructor
        /// <summary>
        /// create the object
        /// </summary>
        public CLUsersV1Controller()
        {
            _objBLUsersV1 = new BLUsersV1();
            objResponse = new Response();
        }
        #endregion

        #region Private Method
        /// <summary>
        /// get the user id of logged user
        /// </summary>
        /// <returns>user id</returns>
        private int GetCurrentUser()
        {
            ClaimsPrincipal currentUser = User as ClaimsPrincipal;
            if (currentUser != null)
            {
                string userId = currentUser.FindFirst("Id")?.Value;

                return Convert.ToInt32(userId);
            }
            return 0;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// register the user into list
        /// </summary>
        /// <param name="objUsersV1">object of the user</param>
        /// <returns>response model</returns>
        [HttpPost]
        [Route("signup")]
        public IHttpActionResult SignUp(UsersV1 objUsersV1)
        {
            _objBLUsersV1.OperationTypes = enmOperationTypes.A;
            _objBLUsersV1.PreSave(objUsersV1);

            objResponse = _objBLUsersV1.ValidationOnSave();
            
            if(!objResponse.IsError)
            {
                objResponse = _objBLUsersV1.Save();
            }

            return Ok(objResponse);
        }

        /// <summary>
        /// login the user based on username and password
        /// </summary>
        /// <param name="objUsersV1">object of the user contains - username and password</param>
        /// <returns>response model</returns>
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(UsersV1 objUsersV1)
        {
            objResponse = _objBLUsersV1.Login(objUsersV1);

            return Ok(objResponse);
        }

        /// <summary>
        /// get the current logged user's details
        /// </summary>
        /// <returns>response model</returns>
        [Authentication]
        [HttpGet]
        [Route("details")]
        public IHttpActionResult UserDetails()
        {
            int id = GetCurrentUser();
            objResponse = _objBLUsersV1.GetUser(id);
            return Ok(objResponse);
        }

        /// <summary>
        /// Get all the users details
        /// </summary>
        /// <returns>response model</returns>
        [Authentication]
        [Authorization(Roles = "A")]
        [HttpGet]
        [Route("allUsers")]
        public IHttpActionResult GetAllUser()
        {
            objResponse = _objBLUsersV1.GetAllUser();
            return Ok(objResponse);
        }

        /// <summary>
        /// update the user - current logged user
        /// </summary>
        /// <param name="objUsersV1">object of the user</param>
        /// <returns>response model</returns>
        [Authentication]
        [Authorization(Roles = "U")]
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateUser(UsersV1 objUsersV1)
        {
            int id = GetCurrentUser();
            _objBLUsersV1.OperationTypes = enmOperationTypes.E;
            _objBLUsersV1.PreSave(objUsersV1,id);

            objResponse = _objBLUsersV1.ValidationOnSave();

            if (!objResponse.IsError)
            {
                objResponse = _objBLUsersV1.Save();
            }

            return Ok(objResponse);
        }

        /// <summary>
        /// Delete the user - current logged user
        /// </summary>
        /// <returns>response model</returns>
        [Authentication]
        [Authorization(Roles = "U")]
        [HttpDelete]
        [Route("")]
        public IHttpActionResult DeleteUser()
        {
            int id = GetCurrentUser();
            _objBLUsersV1.OperationTypes = enmOperationTypes.D;

            objResponse = _objBLUsersV1.ValidationOnDelete(id);
            if (!objResponse.IsError)
            {
                objResponse = _objBLUsersV1.DeleteUser();
            }

            return Ok(objResponse);
        }
        #endregion
    }
}