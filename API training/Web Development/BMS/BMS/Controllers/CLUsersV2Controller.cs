using BMS.Basic_Auth;
using BMS.BL;
using BMS.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Web.Http;

namespace BMS.Controllers
{
    /// <summary>
    /// handle the user's related operation - version 1
    /// </summary>
    [RoutePrefix("api/v2/users")]
    public class CLUsersV2Controller : ApiController
    {
        #region Private Member
        /// <summary>
        /// Initialize the object of the user's services
        /// </summary>
        private readonly BLUsersV2 _objBLUsersV2;

       
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
        public CLUsersV2Controller()
        {
            _objBLUsersV2 = new BLUsersV2();
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
        /// <param name="objUsersV2">object of the user</param>
        /// <returns>response message</returns>
        [HttpPost]
        [Route("signup")]
        public Response SignUp(UsersV2 objUsersV2)
        {
            //_objResponse = new Response();
            _objBLUsersV2.OperationTypes = EnmOperationTypes.A;
            _objBLUsersV2.PreSave(objUsersV2);

            objResponse = _objBLUsersV2.ValidationOnSave();

            if (!objResponse.IsError)
            {
                objResponse = _objBLUsersV2.Save();
            }

            return objResponse;
        }

        /// <summary>
        /// login the user based on username and password
        /// </summary>
        /// <param name="objUsersV2">object of the user contains - username and password</param>
        /// <returns>response message or token and role</returns>
        [HttpPost]
        [Route("login")]
        public Response Login(UsersV2 objUsersV2)
        {
            //_objResponse = new Response();
            objResponse = _objBLUsersV2.Login(objUsersV2);

            return objResponse;
        }

        /// <summary>
        /// get the current logged user's details
        /// </summary>
        /// <returns>details of the user</returns>
        [Authentication]
        [HttpGet]
        [Route("me")]
        public Response UserDetails()
        {
            //_objResponse = new Response();
            int id = GetCurrentUser();
            objResponse = _objBLUsersV2.GetUser(id);

            return objResponse;
        }

        /// <summary>
        /// Get all the users details
        /// </summary>
        /// <returns>list of the users</returns>
        [Authentication]
        [Authorization(Roles = "A")]
        [HttpGet]
        [Route("all")]
        public Response GetAllUser()
        {
            //_objResponse = new Response();

            objResponse = _objBLUsersV2.GetAllUser();

            return objResponse;
        }

        /// <summary>
        /// update the user - current logged user
        /// </summary>
        /// <param name="objUsersV2">object of the user</param>
        /// <returns>response message</returns>
        [Authentication]
        [Authorization(Roles = "U")]
        [HttpPut]
        public Response UpdateUser(UsersV2 objUsersV2)
        {
            int id = GetCurrentUser();
            //_objResponse = new Response();
            _objBLUsersV2.OperationTypes = EnmOperationTypes.E;
            _objBLUsersV2.PreSave(objUsersV2,id);

            objResponse = _objBLUsersV2.ValidationOnSave();

            if (!objResponse.IsError)
            {
                objResponse = _objBLUsersV2.Save();
            }

            return objResponse;
        }

        /// <summary>
        /// Delete the user - current logged user
        /// </summary>
        /// <returns>response message</returns>
        [Authentication]
        [Authorization(Roles = "U")]
        [HttpDelete]
        public Response DeleteUser()
        {
            int id = GetCurrentUser();
            //_objResponse = new Response();
            _objBLUsersV2.OperationTypes = EnmOperationTypes.D;

            objResponse = _objBLUsersV2.ValidationOnDelete(id);
            if (!objResponse.IsError)
            {
                objResponse = _objBLUsersV2.DeleteUser();
            }

            return objResponse;
        }
        #endregion
    }
}