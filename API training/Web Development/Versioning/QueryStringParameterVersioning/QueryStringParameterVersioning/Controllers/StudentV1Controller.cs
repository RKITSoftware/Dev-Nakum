using QueryStringParameterVersioning.BL;
using QueryStringParameterVersioning.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace QueryStringParameterVersioning.Controllers
{
    //// <summary>
    /// manage the API of student version 1
    /// </summary>
    public class StudentV1Controller : ApiController
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
        public StudentV1Controller()
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
        //[Route("api/v1/student")]
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
        //[Route("api/v1/student/{id}")]
        public IHttpActionResult GetStudent(int id)
        {
            StudentV1 objStuV1 = _objBLStuV1.GetStudentbyId(id);
            if (objStuV1 == null)
            {
                return BadRequest($"Student id {id} is not found");
            }
            return Ok(objStuV1);
        }

        #endregion

    }
}
