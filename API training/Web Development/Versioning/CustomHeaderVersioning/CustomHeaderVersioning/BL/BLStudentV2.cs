using CustomHeaderVersioning.Models;
using System.Collections.Generic;
using System.Linq;

namespace CustomHeaderVersioning.BL
{
    /// <summary>
    /// Manage the student services of version 2
    /// </summary>
    public class BLStudentV2
    {
        #region Private Member
        /// <summary>
        /// static list of students
        /// </summary>
        private static List<StudentV2> _lstStuV2 = new List<StudentV2>()
        {
            new StudentV2() { Id = 1, FirstName = "Dev",LastName="Nakum", Email="dev@gmail.com" },
                new StudentV2() { Id = 2, FirstName = "Kishan",LastName="Nakum", Email="kishan@gmail.com" },
                new StudentV2() { Id = 3, FirstName = "Raj",LastName="Mandaviya", Email="raj@gmail.com" },
                new StudentV2() { Id = 4, FirstName = "Tushar",LastName="Gohil", Email="tushar@gmail.com" },
                new StudentV2() { Id = 5, FirstName = "Pratham",LastName="Modi", Email="pratham@gmail.com" }
        };
        #endregion

        #region Public Method
        
        /// <summary>
        /// Get all the list of students
        /// </summary>
        /// <returns>list of students</returns>
        public List<StudentV2> GetAllStudents()
        {
            List<StudentV2> lstStu = _lstStuV2;
            return lstStu;
        }

        /// <summary>
        /// student details based on id
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>student details</returns>
        public StudentV2 GetStudentbyId(int id)
        {
            StudentV2 objStuV2 = _lstStuV2.FirstOrDefault(s => s.Id == id);

            return objStuV2;
        }
        #endregion
    }
}