using FileHandling.Business_Logic;
using FileHandling.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace FileHandling.Controllers
{
    /// <summary>
    /// controller which manage the file and student's related api
    /// </summary>
    public class CLStudentsController : ApiController
    {
        #region Private Member
        /// <summary>
        /// file path for download the file
        /// </summary>
        private static string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileUpload", "studentData.txt");

        /// <summary>
        /// create the object of the student services
        /// </summary>
        private readonly BLStudent _objBLStudent;
        #endregion

        #region Constoller

        /// <summary>
        /// initialize the object of the student services
        /// </summary>
        public CLStudentsController()
        {
            _objBLStudent = new BLStudent();
        }
        
        #endregion


        #region Public Method

        /// <summary>
        /// Get all the student which is listed on list
        /// </summary>
        /// <returns>list of the </returns>
        [HttpGet]
        [Route("api/students")]
        public IHttpActionResult GetAllStudents()
        {
            List<Students> lstStudents = _objBLStudent.GetAllStudents();
            return Ok(lstStudents);
        }

        /// <summary>
        /// get the student based on student id
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>student object if student id found or else response message</returns>
        [HttpGet]
        [Route("api/students/{id}")]
        public IHttpActionResult GetStudentById(int id)
        {
            Students objStudents = _objBLStudent.GetStudentById(id);
            if (objStudents == null)
            {
                return Ok($"Student id {id} is not found");
            }
            return Ok(objStudents);
        }

        /// <summary>
        /// Create the student
        /// </summary>
        /// <param name="objStudents">object of the student</param>
        /// <returns>response message</returns>
        [HttpPost]
        [Route("api/students")]
        public IHttpActionResult CreateStudent(Students objStudents)
        {
            _objBLStudent.CreateStudent(objStudents);
            return Ok("Student is successfully added");
        }

        /// <summary>
        /// delete the student based on student id 
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>response message</returns>
        [HttpDelete]
        [Route("api/students/{id}")]
        public IHttpActionResult DeleteStudent(int id)
        {
            bool isDeleted = _objBLStudent.DeleteStudentById(id);
            if (isDeleted)
            {
                return Ok("Successfully deleted the students");
            }
            return Ok("student is not found");
        }

        /// <summary>
        /// create the file and write into it
        /// </summary>
        /// <returns>response messages</returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [Route("api/students/write")]
        public IHttpActionResult WriteStudentDataIntoFile()
        {
            try
            {
                string mes = _objBLStudent.FileWrite();
                return Ok(mes);
            }
            catch (Exception ex)
            {
                // Handle the exception or rethrow with additional information
                throw new Exception("Error writing student data into file.", ex);
            }
        }

        /// <summary>
        /// download the file
        /// </summary>
        /// <returns>response</returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [Route("api/students/download")]
        public HttpResponseMessage DownloadFile()
        {
            try
            {
                // check file exists
                if (!File.Exists(_filePath))
                {
                    throw new Exception("File Not Found");
                }

                // To Download file with response

                // Read the file content
                byte[] fileBytes = File.ReadAllBytes(_filePath);

                // Create a response with the file content
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new ByteArrayContent(fileBytes);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "studentData.txt"
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// upload the file which is stored in destination folder
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/students/upload")]
        public IHttpActionResult UploadFile( )
        {
            try
            {
                if (_objBLStudent.UploadFile())
                {
                    return Ok("File uploaded successfully");
                }
                else
                {
                    return Ok("No files attached with request or file already exists");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        /// <summary>
        /// Read the file and display the text
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/students/read")]
        public IHttpActionResult ReadData()
        {
            string str = _objBLStudent.ReadData();
            return Ok(str);
        }

        /// <summary>
        /// delete the file
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/students/file")]
        public IHttpActionResult DeleteFile()
        {
            if (_objBLStudent.DeleteFile())
            {
                return Ok("File deleted successfully");
            }
            return Ok("File is not exist");
        }

        /// <summary>
        /// fileinto related operation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/students/file")]
        public IHttpActionResult FileInfo()
        {
            try
            {
                return Ok(_objBLStudent.FileInfo());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion
    }
}