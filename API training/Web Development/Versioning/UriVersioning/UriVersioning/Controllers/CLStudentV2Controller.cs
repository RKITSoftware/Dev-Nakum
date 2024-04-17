using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using UriVersioning.BL;
using UriVersioning.Models;

namespace UriVersioning.Controllers
{
    /// <summary>
    /// manage the API of student version 2
    /// </summary>
    public class CLStudentV2Controller : ApiController
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
        public CLStudentV2Controller()
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
        [Route("api/v2/students")]
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
        [Route("api/v2/students/{id}")]
        public IHttpActionResult GetStudent(int id)
        {
            StudentV2 objStuV2 = _objBLStuV2.GetStudentById(id);
            if (objStuV2 == null)
            {
                return BadRequest($"Student id {id} is not found");
            }
            return Ok(objStuV2);
        }

        #endregion

    }
}
