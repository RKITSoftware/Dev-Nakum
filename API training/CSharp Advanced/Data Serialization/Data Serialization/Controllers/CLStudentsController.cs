using Data_Serialization.Business_Logic;
using Data_Serialization.Models;
using System.Web.Http;
using System.Xml.Linq;

namespace Data_Serialization.Controllers
{
    /// <summary>
    /// Manage the api of serialization and deserialization
    /// </summary>
    public class CLStudentsController : ApiController
    {
        #region Private Member
        /// <summary>
        /// create the object of the student service
        /// </summary>
        private readonly BLStudent _objBLStudent;
        #endregion

        #region Constructor

        /// <summary>
        /// initialize the object of the student service
        /// </summary>
        public CLStudentsController()
        {
            _objBLStudent = new BLStudent();
        }
        #endregion

        #region Public Method

        /// <summary>
        /// convert json object to the json string
        /// </summary>
        /// <returns>json string</returns>
        [HttpPost]
        [Route("api/students/object/json")]
        public IHttpActionResult ObjectToJson([FromBody] Students objStudents)
        {
            return Ok(_objBLStudent.ObjectToJson(objStudents));
        }

        /// <summary>
        /// convert json string to the json object
        /// </summary>
        /// <param name="jsonString">json string</param>
        /// <returns>json object</returns>
        [HttpPost]
        [Route("api/students/json/object")]
        public IHttpActionResult JsonToObject([FromBody] string jsonString)
        {
            return Ok(_objBLStudent.JsonToObject(jsonString));
        }

        /// <summary>
        /// convert xml object to the xml string
        /// </summary>
        /// <param name="objXml">xml object</param>
        /// <returns>xml string</returns>
        [HttpPost]
        [Route("api/students/object/xml")]
        public IHttpActionResult ObjectToXml([FromBody] XElement objXml)
        {
            return Ok(_objBLStudent.ObjectToXml(objXml));
        }

        /// <summary>
        /// convert xml string to the xml object
        /// </summary>
        /// <param name="xmlString">xml string</param>
        /// <returns>xml object</returns>
        [HttpPost]
        [Route("api/students/xml/object")]
        public IHttpActionResult XmlToObject([FromBody] string xmlString)
        {
            return Ok(_objBLStudent.XmlToObject(xmlString));
        }
        #endregion
    }
}
