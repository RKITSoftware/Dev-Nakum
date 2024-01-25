using Bank_Management_System.Basic_Auth;
using Bank_Management_System.BusinessLogic;
using Bank_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bank_Management_System.Controllers
{
    public class UsersV2Controller : ApiController
    {
        #region Private Member
        private static int _id = 3;
        #endregion

        /// <summary>
        /// signup the user
        /// </summary>
        /// <param name="user">Request body</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/v2/users/signup")]

        public HttpResponseMessage SignUp(UsersV2 user)
        {
            UsersV2 objUser = new UsersV2();
            objUser.Id = _id++;
            objUser.FullName = user.FullName;
            objUser.UserName = user.UserName;
            objUser.Password = user.Password;
            objUser.Number = user.Number;
            objUser.Email = user.Email;
            objUser.Money = 0;
            objUser.Role = "User";
            UsersV2.lstUsers.Add(objUser);

            return Request.CreateResponse(HttpStatusCode.OK, objUser);
        }

        /// <summary>
        /// Login the user
        /// </summary>
        /// <param name="user">request body</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/v2/users/login")]
        public HttpResponseMessage Login(UsersV2 user)
        {
            var objUser = BLValidateUser.ValidateUser(user.UserName, user.Password,2);
            if (objUser == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Enter valid Username or Password");
            }

            return Request.CreateResponse(HttpStatusCode.OK, objUser);
        }

        /// <summary>
        /// get user by user id
        /// required authentication
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>object of the user</returns>
        [Authentication(typeof(UsersV2))]
        [HttpGet]
        [Route("api/v2/users/{id}")]
        public HttpResponseMessage GetUserById(int id)
        {
            UsersV2 user = UsersV2.lstUsers.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"User id {id} is not found !!");
            }
            return Request.CreateResponse(HttpStatusCode.OK, user);

        }

        /// <summary>
        /// get all the user
        /// required authentication
        /// only admin can see the all user's details 
        /// </summary>
        /// <returns>all the user</returns>
        [Authentication(typeof(UsersV2))]
        [Authorization(Roles = "Admin")]
        [HttpGet]
        [Route("api/v2/users")]
        public HttpResponseMessage GetAllUser()
        {
            return Request.CreateResponse(HttpStatusCode.OK, UsersV2.lstUsers);
        }


        /// <summary>
        /// update the user
        /// required authentication
        /// only admin can update the user
        /// </summary>
        /// <param name="id">user id</param>
        /// <param name="user">request body</param>
        /// <returns>updated user</returns>
        [Authentication(typeof(UsersV2))]
        [Authorization(Roles = "User")]
        [HttpPut]
        [Route("api/v2/users/{id}")]
        public HttpResponseMessage UpdateUser(int id, UsersV2 user)
        {
            UsersV2 objUser = UsersV2.lstUsers.FirstOrDefault(u => u.Id == id);

            if (objUser == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"User id {id} is not found !!");
            }

            // if username is already exist - user can not take this username
            if (user.UserName != null)
            {
                if (!(UsersV2.lstUsers.Any(u => u.UserName == user.UserName)))
                {
                    objUser.UserName = user.UserName;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotModified, $"Username {user.UserName} is already exists please try another !!");
                }
            }
            objUser.Email = user.Email;
            objUser.Number = user.Number;
            objUser.FullName = user.FullName;

            return Request.CreateResponse(HttpStatusCode.OK, objUser);
        }

        /// <summary>
        /// Delete the user
        /// required authentication
        /// only admin can delete the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authentication(typeof(UsersV2))]
        [Authorization(Roles = "Admin")]
        [HttpDelete]
        [Route("api/v2/users/{id}")]
        public HttpResponseMessage DeleteUser(int id)
        {
            UsersV2 objUser = UsersV2.lstUsers.FirstOrDefault(u => u.Id == id);

            if (objUser == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"User id {id} is not found !!");
            }

            UsersV2.lstUsers.Remove(objUser);
            return Request.CreateResponse(HttpStatusCode.OK, "User is successfully deleted");
        }

    }
}
