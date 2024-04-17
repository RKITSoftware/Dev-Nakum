using System.Collections.Generic;
using System.Linq;
using UriVersioning.Models;

namespace UriVersioning.BL
{
    /// <summary>
    /// Manage the student services of version 2
    /// </summary>
    public class BLStudentV1
    {
        #region Private Member

        /// <summary>
        /// static list of students
        /// </summary>
        private static List<StudentV1> _lstStuV1 = new List<StudentV1>()
        {
            new StudentV1() { Id = 1, Name = "Dev",Email="dev@gmail.com" },
            new StudentV1() { Id = 2, Name = "Kishan",Email="kishan@gmail.com" },
            new StudentV1() { Id = 3, Name = "Raj",Email="raj@gmail.com" },
            new StudentV1() { Id = 4, Name = "Tushar",Email="tushar@gmail.com" },
            new StudentV1() { Id = 5, Name = "Pratham",Email="pratham@gmail.com" }
        };
        #endregion

        #region Public Method

        /// <summary>
        /// Get all the list of students
        /// </summary>
        /// <returns>list of students</returns>
        public List<StudentV1> GetAllStudents()
        {
            List<StudentV1> lstStu = _lstStuV1;
            return lstStu;
        }

        /// <summary>
        /// student details based on id
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>student details</returns>
        public StudentV1 GetStudentById(int id)
        {
            StudentV1 objStuV1 = _lstStuV1.FirstOrDefault(s => s.Id == id);

            return objStuV1;
        }
        #endregion
    }
}