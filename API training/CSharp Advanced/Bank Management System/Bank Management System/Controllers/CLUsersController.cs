using Bank_Management_System.Attributes;
using Bank_Management_System.Business_Logic;
using Bank_Management_System.Enums;
using Bank_Management_System.Models;
using Bank_Management_System.Models.DTO;
using System;
using System.Security.Claims;
using System.Web.Http;

namespace Bank_Management_System.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    [JwtAuthorization]
    public class CLUsersController : ApiController
    {
        #region Private Member
        /// <summary>
        /// Create the object of user service
        /// </summary>
        private readonly BLUsers _objBLUsers;
        #endregion

        #region Public Member

        /// <summary>
        /// Create the object of response model
        /// </summary>
        public Response objResponse;
        #endregion


        #region Constructor
        /// <summary>
        /// Initializes a new instance of the CLUsersController class.
        /// </summary>
        public CLUsersController()
        {
            _objBLUsers = new BLUsers();
            objResponse = new Response();
        }
        #endregion

        #region Private Method
        /// <summary>
        /// Gets the current user's ID from the claims.
        /// </summary>
        /// <returns>The current user's ID or 0 if not found.</returns>
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
        /// Handles user sign-up.
        /// </summary>
        /// <param name="objDtoUse01">User details for sign-up.</param>
        /// <returns>Response model</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("api/users/signup")]
        public IHttpActionResult SignUp([FromBody] DtoUse01 objDtoUse01)
        {
            _objBLUsers.OperationType = enmOperationTypes.A;
            _objBLUsers.PreSave(objDtoUse01);
            objResponse = _objBLUsers.ValidationOnSave();
            if(!objResponse.IsError)
            {
                objResponse = _objBLUsers.Save();
            }

            return Ok(objResponse);
        }

        /// <summary>
        /// Handles user login.
        /// </summary>
        /// <param name="objDtoUse01">User credentials for login.</param>
        /// <returns>Response model</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("api/users/login")]
        public IHttpActionResult LogIn([FromBody] DtoUse01 objDtoUse01)
        {
            _objBLUsers.OperationType = enmOperationTypes.Login;
            _objBLUsers.PreSave(objDtoUse01);
            objResponse = _objBLUsers.LogIn();
            return Ok(objResponse);
        }

        /// <summary>
        /// Retrieves details of the currently logged-in user.
        /// </summary>
        /// <returns>Response model</returns>
        [Authorize(Roles = "A,U")]
        [HttpGet]
        [Route("api/users/details")]
        public IHttpActionResult GetUser()
        {
            int id = GetCurrentUser();
            objResponse = _objBLUsers.GetUser(id);
            return Ok(objResponse);

        }

        /// <summary>
        /// Retrieves details of all users.
        /// </summary>
        /// <returns>Response model</returns>
        [Authorize(Roles = "A")]
        [HttpGet]
        [Route("api/users/")]
        public IHttpActionResult GetAllUser()
        {
            objResponse = _objBLUsers.GetAllUser();
            return Ok(objResponse);
        }

        /// <summary>
        /// Updates details of the currently logged-in user.
        /// </summary>
        /// <param name="objDtoUse01">Updated user details.</param>
        /// <returns>Response model</returns>
        [HttpPut]
        [Route("api/users/")]
        public IHttpActionResult UpdateUser(DtoUse01 objDtoUse01)
        {
            int id = GetCurrentUser();
            _objBLUsers.OperationType = enmOperationTypes.E;
            _objBLUsers.PreSave(objDtoUse01,id);
            objResponse = _objBLUsers.ValidationOnSave();

            if (!objResponse.IsError)
            {
                objResponse = _objBLUsers.Save();
            }
            return Ok(objResponse);
        }

        /// <summary>
        /// Deletes the currently logged-in user.
        /// </summary>
        /// <returns>Response model</returns>
        [HttpDelete]
        [Route("api/users/")]
        public IHttpActionResult DeleteUser()
        {
            int id = GetCurrentUser();

            _objBLUsers.OperationType = enmOperationTypes.D;
            objResponse = _objBLUsers.ValidationOnDelete(id);

            if(!objResponse.IsError)
            {
                objResponse = _objBLUsers.Delete();
            }
            return Ok(objResponse);
        }

        #endregion
    }
}
