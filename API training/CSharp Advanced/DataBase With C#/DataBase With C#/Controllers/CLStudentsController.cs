using DataBase_With_C_.Business_Logic;
using DataBase_With_C_.Models;
using System;
using System.Web.Http;

namespace DataBase_With_C_.Controllers
{
    /// <summary>
    /// Controller for handling CRUD operations on student data.
    /// </summary>
    public class CLStudentsController : ApiController
    {
        #region Private Member
        private BLStudents objBLStudents = new BLStudents(); // Business Logic layer instance
        #endregion

        #region Public Methods

        /// <summary>
        /// Get all students.
        /// </summary>
        [HttpGet]
        [Route("api/students")]
        public IHttpActionResult GetAllStudents()
        {
            try
            {
                return Ok(objBLStudents.GetAllStudents()); // Return all students if successful
            }
            catch (Exception)
            {
                return InternalServerError(); // Return 500 Internal Server Error for any exception
            }
        }

        /// <summary>
        /// Get a student by ID.
        /// </summary>
        [HttpGet]
        [Route("api/students/{id}")]
        public IHttpActionResult GetStudentById(int id)
        {
            try
            {
                Stu01 objStu01 = objBLStudents.GetStudent(id); // Get student by ID
                if (objStu01 == null)
                {
                    return BadRequest($"Student id {id} is not found"); // Return 400 Bad Request if student not found
                }
                return Ok(objStu01); // Return student if successful
            }
            catch (Exception)
            {
                return InternalServerError(); // Return 500 Internal Server Error for any exception
            }
        }

        /// <summary>
        /// Add a new student.
        /// </summary>
        [HttpPost]
        [Route("api/students")]
        public IHttpActionResult AddStudent(Stu01 objStu01)
        {
            if (objBLStudents.AddStudent(objStu01))
            {
                return Ok("User added Successfully"); // Return success message if addition is successful
            }
            return BadRequest("Something went wrong"); // Return 400 Bad Request if something goes wrong
        }

        /// <summary>
        /// Update an existing student by ID.
        /// </summary>
        [HttpPut]
        [Route("api/students/{id}")]
        public IHttpActionResult UpdateStudent(int id, Stu01 objStu01)
        {
            if (objBLStudents.UpdateStudent(id, objStu01))
            {
                return Ok("User updated Successfully"); // Return success message if update is successful
            }
            return BadRequest("Something went wrong"); // Return 400 Bad Request if something goes wrong
        }

        /// <summary>
        /// Delete a student by ID.
        /// </summary>
        [HttpDelete]
        [Route("api/students/{id}")]
        public IHttpActionResult DeleteStudent(int id)
        {
            if (objBLStudents.DeleteStudent(id))
            {
                return Ok("User deleted Successfully"); // Return success message if deletion is successful
            }
            return BadRequest("Something went wrong"); // Return 400 Bad Request if something goes wrong
        }
        #endregion
    }
}
