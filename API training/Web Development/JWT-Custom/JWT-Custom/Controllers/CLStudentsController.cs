using JWT_Custom.Business_Logic;
using JWT_Custom.Models;
using System.Web.Http;

namespace JWT_Custom.Controllers
{
    /// <summary>
    /// Controller for handling student authentication and token management.
    /// </summary>
    public class CLStudentsController : ApiController
    {
        #region Private Member
        private BLStudents _objBLStudents;
        #endregion

        #region Constructor
        public CLStudentsController()
        {
            _objBLStudents = new BLStudents();
        }
        #endregion


        #region Public Method

        /// <summary>
        /// Generates a JWT token for a student based on submitted credentials.
        /// </summary>
        /// <param name="objStudents">The student object containing name and password.</param>
        /// <returns>An IHttpActionResult object containing the generated token on success, or BadRequest with an error message otherwise.</returns>
        [HttpPost]
        [Route("api/students/token")]
        public IHttpActionResult GenerateToken(Students objStudents)
        {
            string token = _objBLStudents.GenerateToken(objStudents);
            if(token == null)
            {
                return BadRequest("Invalid credentials.");
            }

            return Ok(token);
        }

        /// <summary>
        /// Verifies a JWT token and returns the associated student name if valid.
        /// </summary>
        /// <param name="token">The JWT token to be verified.</param>
        /// <returns>An IHttpActionResult object containing the student name on success, or BadRequest with an error message otherwise.</returns>
        [HttpPost]
        [Route("api/students/verify")]
        public IHttpActionResult VerifyToken([FromBody] string token)
        {
            string name = _objBLStudents.VerifyToken(token);
            if(name == null)
            {
                return BadRequest("Invalid or expired token.");
            }

            return Ok(name);
        }
        #endregion
    }
}
