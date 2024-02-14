using JWT_Custom.Models;
using System.Collections.Generic;
using System.Linq;

namespace JWT_Custom.Business_Logic
{
    /// <summary>
    /// Class containing methods for student authentication and token generation/verification.
    /// </summary>
    public class BLStudents
    {
        #region Private Member
        private static List<Students> _lstStudent = new List<Students>
        {
            new Students{ Id=1, Name="Dev", Password="Abcd@123"},
            new Students{ Id=2, Name="Kishan", Password="Abcd@123"},
            new Students{ Id=3, Name="Raj", Password="Abcd@123"},
        };
        #endregion

        #region Private Method
        /// <summary>
        /// Authenticates a student based on name and password.
        /// </summary>
        /// <param name="objStudent">The student object containing name and password for authentication.</param>
        /// <returns>True if the student is found and credentials match, False otherwise.</returns>
        private bool GetStudent(Students objStudent)
        {
            return _lstStudent.Any(s => s.Name == objStudent.Name && s.Password == objStudent.Password);
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Generates a JWT token for a valid student.
        /// </summary>
        /// <param name="objStudent">The student object for whom the token is generated.</param>
        /// <returns>The generated JWT token or null if authentication fails.</returns>
        public string GenerateToken(Students objStudent)
        {
            bool isAuthenticated = GetStudent(objStudent);

            if(!isAuthenticated)
            {
                return null;
            }

            BLJwt objBLJwt = new BLJwt();
            string token = objBLJwt.GenerateTWT(objStudent.Name);
            return token;
        }

        /// <summary>
        /// Verifies a JWT token and returns the student name if valid, otherwise null.
        /// </summary>
        /// <param name="token">The JWT token to verify.</param>
        /// <returns>The name of the student extracted from the token if valid, otherwise null.</returns>
        public string VerifyToken(string token)
        {
            BLJwt objBLJwt = new BLJwt();
            string name = objBLJwt.VerifyToken(token);

            if(name == null)
            {
                return null;
            }
            return name;
        }
        #endregion
    }
}