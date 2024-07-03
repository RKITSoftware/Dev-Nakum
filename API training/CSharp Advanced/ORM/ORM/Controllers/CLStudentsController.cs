using ORM.Business_Logic;
using ORM.Models;
using ORM.Models.DTO;
using System.Web.Http;

namespace ORM.Controllers
{
    /// <summary>
    /// manage all the API related students
    /// </summary>
    public class CLStudentsController : ApiController
    {
        #region Private Member
        /// <summary>
        /// Create the object of the student service
        /// </summary>
        private readonly BLStudent _objBLStudent;
        #endregion

        #region Public Member
        /// <summary>
        /// Create the object of the response model
        /// </summary>
        public Response objResponse;
        #endregion

        #region Constructor
        /// <summary>
        /// initialize the student service and response model
        /// </summary>
        public CLStudentsController()
        {
            _objBLStudent = new BLStudent();
            objResponse = new Response();
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Get all the students
        /// </summary>
        /// <returns>response model</returns>
        [HttpGet]
        [Route("api/students")]
        public Response GetAllStudents()
        {
            objResponse = _objBLStudent.GetAllStudents();
            return objResponse;
        }

        /// <summary>
        /// Get student based on student id
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>response model</returns>
        [HttpGet]
        [Route("api/students/{id}")]
        public Response GetStudentById(int id)
        {
            objResponse = _objBLStudent.GetStudentById(id);
            return objResponse;
        }

        /// <summary>
        /// Add student details into data base
        /// </summary>
        /// <param name="objDtoStu01">object of the student</param>
        /// <returns>response model</returns>
        [HttpPost]
        [Route("api/students")]
        public Response AddStudent(DtoStu01 objDtoStu01)
        {
            _objBLStudent.OperationTypes = enmOperationTypes.A;
            _objBLStudent.PreSave(objDtoStu01);
            objResponse = _objBLStudent.ValidationOnSave();

            if(!objResponse.IsError)
            {
                objResponse = _objBLStudent.Save();
            }
            return objResponse;
        }

        /// <summary>
        /// Update the student based on student id
        /// </summary>
        /// <param name="id">student id</param>
        /// <param name="objDtoStu01">object of the student</param>
        /// <returns>response model</returns>
        [HttpPut]
        [Route("api/students/{id}")]
        public Response UpdateStudent(int id, DtoStu01 objDtoStu01)
        {
            _objBLStudent.OperationTypes = enmOperationTypes.U;
            _objBLStudent.PreSave(objDtoStu01,id);
            objResponse = _objBLStudent.ValidationOnSave();

            if (!objResponse.IsError)
            {
                objResponse = _objBLStudent.Save();
            }
            return objResponse;
        }

        /// <summary>
        /// Delete the student based on student id
        /// </summary>
        /// <param name="id">student id </param>
        /// <returns>response model</returns>
        [HttpDelete]
        [Route("api/students/{id}")]
        public Response DeleteStudent(int id)
        {
            _objBLStudent.OperationTypes = enmOperationTypes.D;
            objResponse = _objBLStudent.ValidationOnDelete(id);

            if (!objResponse.IsError)
            {
                objResponse = _objBLStudent.Delete();
            }
            return objResponse;
        }

        #endregion
    }
}
