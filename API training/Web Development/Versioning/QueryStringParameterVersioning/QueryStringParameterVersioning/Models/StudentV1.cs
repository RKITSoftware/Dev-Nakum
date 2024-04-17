namespace QueryStringParameterVersioning.Models
{
    /// <summary>
    /// manage the schema of student - version1
    /// </summary>
    public class StudentV1
    {
        #region Public Properties

        /// <summary>
        /// Student's Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Student's Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Student's Email
        /// </summary>
        public string Email { get; set; }

        #endregion
    }

}