using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Web_API_Demo.Models;

namespace Web_API_Demo.Controllers
{
    /// <summary>
    /// Manage the user's api
    /// </summary>
    public class DemoController : ApiController
    {
        #region Private Member
        private static int _id = 1;
        private static List<Users> _lstUser = new List<Users>();
        #endregion

        #region Public Method

        /// <summary>
        /// Get all the users
        /// </summary>
        /// <returns>list of the users</returns>
        [HttpGet]
        [Route("api/users/all")]
        public IHttpActionResult AllUsers()
        {
            return Ok(_lstUser);
        }

        /// <summary>
        /// Get user details by user's id
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <returns>User's Details</returns>
        [HttpGet]
        [Route("api/users/{id}")]
        public IHttpActionResult Getdata(int id)
        {
            return Ok(_lstUser.FirstOrDefault(u => u.Id == id));
        }

        /// <summary>
        /// Add user into list
        /// </summary>
        /// <param name="objUsers">object of the user</param>
        /// <returns>response message</returns>
        public IHttpActionResult Post([FromBody] Users objUsers)
        {
            objUsers.Id = _id++;
            _lstUser.Add(objUsers);
            return Ok("user added");
        }

        /// <summary>
        ///  Update the user based on user's Id
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <param name="objUsers">object of the user</param>
        /// <returns>response message</returns>
        [HttpPut]
        [Route("api/users/{id}")]
        public IHttpActionResult Put(int id, [FromBody] Users objUsers)
        {
            //get the user object based on user's id
            Users user = _lstUser.FirstOrDefault(u => u.Id == id);
            
            // check the user
            if (user == null)
            {
                return NotFound();
            }
            user.Name = objUsers.Name;

            return Ok("User updated successfully");
        }

        /// <summary>
        /// Delete the user based on user's id
        /// </summary>
        /// <param name="id">user's id</param>
        /// <returns>response message</returns>
        [HttpDelete]
        [Route("api/users/{id}")]
        public IHttpActionResult Delete(int id)
        {
            //get the user object based on user's id
            Users user = _lstUser.FirstOrDefault(u => u.Id == id);

            // check the user
            if (user == null)
            {
                return NotFound();
            }
            _lstUser.Remove(user);
            
            return Ok("successfully delete the id");

        }
        #endregion
    }
}
