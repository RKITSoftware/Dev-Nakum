using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueryStringParameterVersioning.Models
{
    public class StudentV1
    {
        #region Public Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        #endregion


        #region Public Method

        /// <summary>
        /// list of student - version 1
        /// </summary>
        /// <returns>list of all student</returns>
        public static List<StudentV1> StudentList()
        {
            return new List<StudentV1>()
            {
                new StudentV1() { Id = 1, Name = "Dev",Email="dev@gmail.com" },
                new StudentV1() { Id = 2, Name = "Kishan",Email="kishan@gmail.com" },
                new StudentV1() { Id = 3, Name = "Raj",Email="raj@gmail.com" },
                new StudentV1() { Id = 4, Name = "Tushar",Email="tushar@gmail.com" },
                new StudentV1() { Id = 5, Name = "Pratham",Email="pratham@gmail.com" }
            };
        }
        #endregion
    }

}