using System.Collections.Generic;

namespace CustomHeaderVersioning.Models
{
    /// <summary>
    /// manage the schema of student - version2
    /// </summary>
    public class StudentV2
    {
        #region Public Properties
        /// <summary>
        /// Student's Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Student's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Student's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Student's email
        /// </summary>
        public string Email { get; set; }

        #endregion
    }
}