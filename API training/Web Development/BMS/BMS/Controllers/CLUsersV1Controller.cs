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
        public Response SignUp(UsersV1 objUsersV1)
        {
            //_objResponse = new Response();
            _objBLUsersV1.OperationTypes = EnmOperationTypes.A;
            _objBLUsersV1.PreSave(objUsersV1);

            objResponse = _objBLUsersV1.ValidationOnSave();
            
            if(!objResponse.IsError)
            {
                objResponse = _objBLUsersV1.Save();
            }

            return objResponse;
        }

        /// <summary>
        /// login the user based on username and password
        /// </summary>
        /// <param name="objUsersV1">object of the user contains - username and password</param>
        /// <returns>response model</returns>
        [HttpPost]
        [Route("login")]
        public Response Login(UsersV1 objUsersV1)
        {
            //_objResponse = new Response();
            objResponse = _objBLUsersV1.Login(objUsersV1);

            return objResponse;
        }

        /// <summary>
        /// get the current logged user's details
        /// </summary>
        /// <returns>response model</returns>
        [Authentication]
        [HttpGet]
        [Route("me")]
        public Response UserDetails()
        {
            //_objResponse = new Response();
            int id = GetCurrentUser();
            objResponse = _objBLUsersV1.GetUser(id);

            return objResponse;
        }

        /// <summary>
        /// Get all the users details
        /// </summary>
        /// <returns>response model</returns>
        [Authentication]
        [Authorization(Roles = "A")]
        [HttpGet]
        [Route("all")]
        public Response GetAllUser()
        {
            //_objResponse = new Response();

            objResponse = _objBLUsersV1.GetAllUser();
            
            return objResponse;
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
        public Response UpdateUser(UsersV1 objUsersV1)
        {
            int id = GetCurrentUser();
            //_objResponse = new Response();
            _objBLUsersV1.OperationTypes = EnmOperationTypes.E;
            _objBLUsersV1.PreSave(objUsersV1,id);

            objResponse = _objBLUsersV1.ValidationOnSave();

            if (!objResponse.IsError)
            {
                objResponse = _objBLUsersV1.Save();
            }

            return objResponse;
        }

        /// <summary>
        /// Delete the user - current logged user
        /// </summary>
        /// <returns>response model</returns>
        [Authentication]
        [Authorization(Roles = "U")]
        [HttpDelete]
        [Route("")]
        public Response DeleteUser()
        {
            int id = GetCurrentUser();
            //_objResponse = new Response();
            _objBLUsersV1.OperationTypes = EnmOperationTypes.D;

            objResponse = _objBLUsersV1.ValidationOnDelete(id);
            if (!objResponse.IsError)
            {
                objResponse = _objBLUsersV1.DeleteUser();
            }

            return objResponse;
        }
        #endregion
    }
}