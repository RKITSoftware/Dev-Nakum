using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using UriVersioning.BL;
using UriVersioning.Models;

namespace UriVersioning.Controllers
{
    //// <summary>
    /// manage the API of student version 1
    /// </summary>
    public class CLStudentV1Controller : ApiController
    {
        #region Private Member
        /// <summary>
        /// Initialize the object of student services of version 1
        /// </summary>
        private readonly BLStudentV1 _objBLStuV1;
        #endregion

        #region Constructor

        /// <summary>
        /// create the object of student services of version 1
        /// </summary>
        public CLStudentV1Controller()
        {
            _objBLStuV1 = new BLStudentV1();
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Display all the list of student version 1
        /// </summary>
        /// <returns>list of student</returns>
        [HttpGet]
        [Route("api/v1/students")]
        public IHttpActionResult GetAllStudent()
        {
            List<StudentV1> lstStuV1 = _objBLStuV1.GetAllStudents();
            return Ok(lstStuV1);
        }

        /// <summary>
        /// get student details based on student id
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>details of student</returns>
        [HttpGet]
        [Route("api/v1/students/{id}")]
        public IHttpActionResult GetStudent(int id)
        {
            StudentV1 objStuV1 = _objBLStuV1.GetStudentById(id);
            if (objStuV1 == null)
            {
                return BadRequest($"Student id {id} is not found");
            }
            return Ok(objStuV1);
        }

        #endregion

    }
}
