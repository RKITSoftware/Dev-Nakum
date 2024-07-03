using Data_Serialization.Models;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace Data_Serialization.Business_Logic
{
    /// <summary>
    /// Manage the student services for serialization and deserialization
    /// </summary>
    public class BLStudent
    {
        /// <summary>
        /// serialize the json object to json string
        /// </summary>
        /// <param name="objStudents">json object of the student </param>
        /// <returns>json string</returns>
        public string ObjectToJson(Students objStudents)
        {
            return JsonConvert.SerializeObject(objStudents);
        }

        /// <summary>
        /// de-serialize the json string to json object
        /// </summary>
        /// <param name="jsonString">json string</param>
        /// <returns>json object</returns>
        public Students JsonToObject(string jsonString)
        {
            return JsonConvert.DeserializeObject<Students>(jsonString);
        }

        /// <summary>
        /// serialize the xml object to xml string
        /// </summary>
        /// <param name="objXml">xml object</param>
        /// <returns>xml string</returns>
        public string ObjectToXml(XElement objXml)
        {
            return JsonConvert.SerializeXNode(objXml);
        }

        /// <summary>
        /// de-serialize the xml string to xml object
        /// </summary>
        /// <param name="xmlString">xml string</param>
        /// <returns>xml object</returns>
        public XElement XmlToObject(string xmlString)
        {
            return JsonConvert.DeserializeXNode(xmlString).Root;
        }

    }
}