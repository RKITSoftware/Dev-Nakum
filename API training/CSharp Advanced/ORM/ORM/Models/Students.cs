using Newtonsoft.Json;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ORM.Models
{
    /// <summary>
    /// Schema of students
    /// </summary>
    [Alias("stu01")]
    public class Stu01
    {
        /// <summary>
        /// Student ID
        /// </summary>
        public int U01F01 { get; set; }
        /// <summary>
        /// Student Name 
        /// </summary>
        public string U01F02 { get; set; }

        /// <summary>
        /// Student Age
        /// </summary>
        public int U01F03 { get; set; }
    }
}