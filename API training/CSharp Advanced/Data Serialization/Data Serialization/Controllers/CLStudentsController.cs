using Data_Serialization.Business_Logic;
using Data_Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Xml;
using System.Xml.Linq;

namespace Data_Serialization.Controllers
{
    /// <summary>
    /// Manage the api of serialization and deserialization
    /// </summary>
    public class CLStudentsController : ApiController
    {
        /// <summary>
        /// convert object to json string
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/students/object/json")]
        public IHttpActionResult ObjectToJson() => Ok(BLStudent.ObjectToJson());

        [HttpPost]
        [Route("api/students/json/object")]
        public IHttpActionResult JsonToObject([FromBody]string jsonString) => Ok(BLStudent.JsonToObject(jsonString));

        [HttpPost]
        [Route("api/students/object/xml")]
        public IHttpActionResult ObjectToXml([FromBody]XElement objXml) => Ok(BLStudent.ObjectToXml(objXml));

        [HttpPost]
        [Route("api/students/xml/object")]
        public IHttpActionResult XmlToObject([FromBody] string xmlString) => Ok(BLStudent.XmlToObject(xmlString));

    }
}
