using QueryStringParameterVersioning.BL;
using QueryStringParameterVersioning.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace QueryStringParameterVersioning.Controllers
{
    /// <summary>
    /// manage the API of student version 2
    /// </summary>
    public class StudentV2Controller : ApiController
    {
        #region Private Member

        /// <summary>
        /// Initialize the object of student services of version 2
        /// </summary>
        private BLStudentV2 _objBLStuV2;
        #endregion

        #region Constructor

        /// <summary>
        /// create the object of student services of version 2
        /// </summary>
        public StudentV2Controller()
        {
            _objBLStuV2 = new BLStudentV2();
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Display all the list of student version 2
        /// </summary>
        /// <returns>list of student</returns>
        [HttpGet]
        //[Route("api/v2/student")]
        public IHttpActionResult GetAllStudent()
        {
            List<StudentV2> lstStuV2 = _objBLStuV2.GetAllStudents();
            return Ok(lstStuV2);
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
            StudentV2 objStuV2 = _objBLStuV2.GetStudentbyId(id);
            if (objStuV2 == null)
            {
                return BadRequest($"Student id {id} is not found");
            }
            return Ok(objStuV2);
        }

        #endregion

    }
}
