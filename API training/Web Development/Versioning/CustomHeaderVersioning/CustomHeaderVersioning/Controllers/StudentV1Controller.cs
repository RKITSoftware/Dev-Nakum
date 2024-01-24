using CustomHeaderVersioning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CustomHeaderVersioning.Controllers
{
    public class StudentV1Controller : ApiController
    {
        #region Public Method
        /// <summary>
        /// Display all the list of student version 1
        /// </summary>
        /// <returns>list of student</returns>
        [HttpGet]
        //[Route("api/v1/student")]

        public IHttpActionResult GetAllStudent()
        {
            List<StudentV1> lstStudentV1 = StudentV1.StudentList();
            return Ok(lstStudentV1);
        }

        /// <summary>
        /// get student details based on student id
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>details of student</returns>
        [HttpGet]
        //[Route("api/v1/student/{id}")]

        public IHttpActionResult GetStudent(int id)
        {
            StudentV1 student = StudentV1.StudentList().FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return BadRequest($"Student id {id} is not found");
            }
            return Ok(student);
        }

        #endregion

    }

}
