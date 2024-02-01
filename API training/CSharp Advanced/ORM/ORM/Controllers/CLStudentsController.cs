using ORM.Business_Logic;
using ORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ORM.Controllers
{
    /// <summary>
    /// manage all the API related students
    /// </summary>
    public class CLStudentsController : ApiController
    {
        #region Private Member
        private BLStudent objBLStudent;
        #endregion

        #region Constructor
        public CLStudentsController()
        {
            objBLStudent = new BLStudent();
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Get all the students
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/students")]
        public IHttpActionResult GetAllStudents()
        {
            return Ok(objBLStudent.GetAllStudents());
        }

        /// <summary>
        /// Get student based on student id
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/students/{id}")]
        public IHttpActionResult GetStudentById(int id)
        {
            Stu01 objStudents = objBLStudent.GetStudentById(id);
            if(objStudents == null)
            {
                return BadRequest($"student id {id} is not found");
            }
            return Ok(objStudents);
        }

        /// <summary>
        /// Add student details into data base
        /// </summary>
        /// <param name="objStudent"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/students")]
        public IHttpActionResult AddStudent(Stu01 objStudent)
        {
            if(objBLStudent.AddStudent(objStudent))
            {
                return Ok("User added successfully");
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }

        /// <summary>
        /// Update the student based on student id
        /// </summary>
        /// <param name="id">student id</param>
        /// <param name="objStudent"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/students/{id}")]
        public IHttpActionResult UpdateStudent(int id,Stu01 objStudent)
        {
            if (objBLStudent.UpdateStudent(id, objStudent))
            {
                return Ok("User Updated successfully");
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }

        /// <summary>
        /// Delete the student based on student id
        /// </summary>
        /// <param name="id">student id </param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/students/{id}")]
        public IHttpActionResult DeleteStudent(int id)
        {
            if (objBLStudent.DeleteStudent(id))
            {
                return Ok("User Deleted successfully");
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }

        #endregion
    }
}
