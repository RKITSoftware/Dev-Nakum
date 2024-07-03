namespace FileHandling.Models
{
    /// <summary>
    /// Manage the schema of student 
    /// </summary>
    public class Students
    {
        #region Public Properies

        /// <summary>
        /// Student's Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Student's Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Student's Age
        /// </summary>
        public int Age { get; set; }

        #endregion
    }
}