using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomHeaderVersioning.Models
{
    public class StudentV2
    {
        #region Public Properties
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        #endregion


        #region Public Method

        /// <summary>
        /// list of student - version 2
        /// </summary>
        /// <returns>list of all student</returns>
        public static List<StudentV2> StudentList()
        {
            return new List<StudentV2>()
            {
                new StudentV2() { Id = 1, FirstName = "Dev",LastName="Nakum", Email="dev@gmail.com" },
                new StudentV2() { Id = 2, FirstName = "Kishan",LastName="Nakum", Email="kishan@gmail.com" },
                new StudentV2() { Id = 3, FirstName = "Raj",LastName="Mandaviya", Email="raj@gmail.com" },
                new StudentV2() { Id = 4, FirstName = "Tushar",LastName="Gohil", Email="tushar@gmail.com" },
                new StudentV2() { Id = 5, FirstName = "Pratham",LastName="Modi", Email="pratham@gmail.com" }
            };
        }
        #endregion
    }
}