using FileHandling.Business_Logic;
using FileHandling.Models;
using System;
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
        private static string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileUpload", "studentData.txt");
        //private static string _filePath2 = HttpContext.Current.Server.MapPath("~/FileUpload");
        #endregion

        #region Public Method


        /// <summary>
        /// Get all the student which is listed on list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/students")]
        public IHttpActionResult GetAllStudents() => Ok(BLStudent.GetAllStudents());

        /// <summary>
        /// get the student based on student id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/students/{id}")]
        public IHttpActionResult GetStudentById(int id)
        {
            Students objStudents = BLStudent.GetStudentById(id);
            if(objStudents == null)
            {
                return BadRequest($"Student id {id} is not found");
            }
            return Ok(objStudents);
        }

        /// <summary>
        /// Create the student
        /// </summary>
        /// <param name="objStudents"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/students")]
        public IHttpActionResult CreateStudent(Students objStudents) => Ok(BLStudent.CreateStudent(objStudents));

        /// <summary>
        /// delete the student based on student id 
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/students/{id}")]
        public IHttpActionResult DeleteStudent(int id)
        {
            Students objStudent = BLStudent.GetStudentById(id);
            if(objStudent == null)
            {
                return BadRequest($"Student id {id} is not found");
            }
            BLStudent.DeleteStudentById(objStudent);
            return Ok("Successfully deleted the students");
        }

        /// <summary>
        /// create the file and write into it
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [Route("api/students/write")]
        public IHttpActionResult WriteStudentDataIntoFile()
        {
            try
            {
                return Ok(BLStudent.FileWrite());
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
        /// <returns></returns>
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
        public IHttpActionResult UploadFile()
        {
            try
            {
                if (BLStudent.UploadFile())
                {
                    return Ok("File(s) uploaded successfully");
                }
                else
                {
                    return BadRequest("No files attached with request or file already exists");
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
        [HttpPost]
        [Route("api/students/read")]
        public IHttpActionResult ReadData()
        {
            try
            {
                string str = BLStudent.ReadData();
                return Ok(str);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// delete the file
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/students/file")]
        public IHttpActionResult DeleteFile()
        {
            try
            {
                return Ok(BLStudent.DeleteFile());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion
    }
}
