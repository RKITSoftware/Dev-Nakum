using Data_Serialization.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Data_Serialization.Business_Logic
{
    /// <summary>
    /// Manage the business logic of serialization and deserialization
    /// </summary>
    public class BLStudent
    {
        /// <summary>
        /// serialize the object to json string
        /// </summary>
        /// <returns></returns>
        public static string ObjectToJson()
        {
            Students objStudents = new Students
            {
                Id = 1,
                Name = "Dev",
                Age = 21
            };

            // serialization of object to json string
            JavaScriptSerializer objSerializer = new JavaScriptSerializer();
            return objSerializer.Serialize(objStudents);
        }

        /// <summary>
        /// serialize the json string to object
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static Students JsonToObject(string jsonString)
        {
            JavaScriptSerializer objSerializer = new JavaScriptSerializer();
            Students stu =  objSerializer.Deserialize<Students>(jsonString);
            return stu;
        }


        public static string ObjectToXml(XElement objXml)
        {
            // Serialize the Students object to XML
            string xmlString = SerializeToXml(objXml);
            return xmlString;
        }

        public static string SerializeToXml<T>(T obj)
        {
            // Create an XmlSerializer for the type of the object
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            // Use a StringWriter to write the XML string
            using (StringWriter stringWriter = new StringWriter())
            {
                // Serialize the object to XML and write it to the string writer
                xmlSerializer.Serialize(stringWriter, obj);

                // Return the XML string
                return stringWriter.ToString();
            }
        }

        public static XmlDocument XmlToObject(string xmlStrnig)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlStrnig);
            return xmlDoc;
        }

    }
}