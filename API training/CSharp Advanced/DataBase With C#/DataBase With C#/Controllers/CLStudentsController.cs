using DataBase_With_C_.Business_Logic;
using DataBase_With_C_.Models;
using DataBase_With_C_.Models.DTO;
using System.Web.Http;

namespace DataBase_With_C_.Controllers
{
    /// <summary>
    /// Controller for handling CRUD operations on student data.
    /// </summary>
    public class CLStudentsController : ApiController
    {
        #region Private Member
        /// <summary>
        /// create the object of the student services
        /// </summary>
        private readonly BLStudents _objBLStudents; 
        #endregion

        #region Public Member
        /// <summary>
        /// create the object of the response model
        /// </summary>
        public Response objResponse;
        #endregion

        #region Constructor

        /// <summary>
        /// initialize the object
        /// </summary>
        public CLStudentsController()
        {
            _objBLStudents = new BLStudents();
            objResponse = new Response();
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Get all students.
        /// </summary>
        [HttpGet]
        [Route("api/students")]
        public Response GetAllStudents()
        {
            objResponse = _objBLStudents.GetAllStudents();
            return objResponse;
        }

        /// <summary>
        /// Get a student by ID.
        /// </summary>
        [HttpGet]
        [Route("api/students/{id}")]
        public Response GetStudentById(int id)
        {
            objResponse = _objBLStudents.GetStudent(id);
            return objResponse;
        }

        /// <summary>
        /// Add a new student.
        /// </summary>
        [HttpPost]
        [Route("api/students")]
        public Response AddStudent(DtoStu01 objDtoStu01)
        {
            _objBLStudents.OperationTypes = EnmOperationTypes.A;
            _objBLStudents.PreSave(objDtoStu01);
            objResponse = _objBLStudents.ValidationOnSave();

            if (!objResponse.IsError)
            {
                objResponse = _objBLStudents.Save();
            }
            return objResponse;
        }

        /// <summary>
        /// Update an existing student by ID.
        /// </summary>
        [HttpPut]
        [Route("api/students/{id}")]
        public Response UpdateStudent(int id, DtoStu01 objDtoStu01)
        {
            _objBLStudents.OperationTypes = EnmOperationTypes.E;
            _objBLStudents.PreSave(objDtoStu01,id);
            objResponse = _objBLStudents.ValidationOnSave();

            if (!objResponse.IsError)
            {
                objResponse = _objBLStudents.Save();
            }
            return objResponse;
        }

        /// <summary>
        /// Delete a student by ID.
        /// </summary>
        [HttpDelete]
        [Route("api/students/{id}")]
        public Response DeleteStudent(int id)
        {
            _objBLStudents.OperationTypes = EnmOperationTypes.D;
            objResponse = _objBLStudents.ValidationOnDelete(id);

            if (!objResponse.IsError)
            {
                objResponse = _objBLStudents.Delete();
            }
            return objResponse;
        }
        #endregion
    }
}
