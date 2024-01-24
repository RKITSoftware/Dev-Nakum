
using CustomHeaderVersioning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CustomHeaderVersioning.Controllers
{
    public class StudentV2Controller : ApiController
    {
        #region Public Method
        /// <summary>
        /// Display all the list of student version 2
        /// </summary>
        /// <returns>list of student</returns>
        [HttpGet]
        //[Route("api/v2/student")]

        public IHttpActionResult GetAllStudent()
        {
            List<StudentV2> lstStudentV2 = StudentV2.StudentList();
            return Ok(lstStudentV2);
        }

        /// <summary>
        /// get student details based on student id
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>details of student</returns>
        [HttpGet]
        //[Route("api/v2/student/{id}")]

        public IHttpActionResult GetStudent(int id)
        {
            StudentV2 student = StudentV2.StudentList().FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return BadRequest($"Student id {id} is not found");
            }
            return Ok(student);
        }

        #endregion

    }
}
