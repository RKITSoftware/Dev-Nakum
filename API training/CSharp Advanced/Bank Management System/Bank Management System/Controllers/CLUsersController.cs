using Bank_Management_System.Attributes;
using Bank_Management_System.Business_Logic;
using Bank_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace Bank_Management_System.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    public class CLUsersController : ApiController
    {
        #region Private Member
        private readonly BLUsers _objBLUsers;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the CLUsersController class.
        /// </summary>
        public CLUsersController()
        {
            _objBLUsers = new BLUsers();
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
        /// <param name="objUse01">User details for sign-up.</param>
        /// <returns>HTTP response indicating success or failure.</returns>
        [HttpPost]
        [Route("api/students/signup")]
        public IHttpActionResult SignUp(Use01 objUse01)
        {
            Use01 user = _objBLUsers.SignUp(objUse01);

            if (user == null)
            {
                return BadRequest("Something went wrong");
            }

            return Ok("User added successfully");
        }

        /// <summary>
        /// Handles user login.
        /// </summary>
        /// <param name="objUse01">User credentials for login.</param>
        /// <returns>HTTP response indicating success or failure.</returns>
        [HttpPost]
        [Route("api/students/login")]
        public HttpResponseMessage LogIn(Use01 objUse01)
        {
            string user = _objBLUsers.LogIn(objUse01);                   
            if (user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Email or Password is incorrect");
            }
            return Request.CreateResponse(HttpStatusCode.OK,user);
        }

        /// <summary>
        /// Retrieves details of the currently logged-in user.
        /// </summary>
        /// <returns>HTTP response containing user details.</returns>
        [JwtAuthorization]
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        [Route("api/students/me")]
        public IHttpActionResult GetUser()
        {
            int id = GetCurrentUser();

            Use01 objUse01 = _objBLUsers.GetUser(id);
            try
            {
                return Ok(objUse01);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        /// <summary>
        /// Retrieves details of all users.
        /// </summary>
        /// <returns>HTTP response containing a list of user details.</returns>
        [JwtAuthorization]
        //[Authorization(Roles = "Admin")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/students")]
        public IHttpActionResult GetAllUser()
        {      
            List<Use01> lstUse01 = _objBLUsers.GetAllUser();
            try
            {
                return Ok(lstUse01);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        /// <summary>
        /// Updates details of the currently logged-in user.
        /// </summary>
        /// <param name="objUse01">Updated user details.</param>
        /// <returns>HTTP response indicating success or failure.</returns>
        [JwtAuthorization]
        [HttpPut]
        [Route("api/students")]
        public IHttpActionResult UpdateUser(Use01 objUse01)
        {
            int id = GetCurrentUser();
            try
            {
                if (_objBLUsers.UpdateUser(id, objUse01))
                {
                    return Ok("Successfully Update the user's details");
                }
                else
                {
                    return BadRequest("Something went wrong");
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Deletes the currently logged-in user.
        /// </summary>
        /// <returns>HTTP response indicating success or failure.</returns>
        [JwtAuthorization]
        [HttpDelete]
        [Route("api/students")]
        public IHttpActionResult DeleteUser()
        {
            int id = GetCurrentUser();
            try
            {
                if (_objBLUsers.DeleteUser(id))
                {
                    return Ok("Successfully Deleted the user");
                }
                else
                {
                    return BadRequest("Something went wrong");
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        #endregion
    }
}
