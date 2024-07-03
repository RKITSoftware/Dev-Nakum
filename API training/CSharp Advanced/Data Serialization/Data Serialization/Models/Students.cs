namespace Data_Serialization.Models
{
    /// <summary>
    /// manage the schema of students
    /// </summary>
    public class Students
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
        /// Student's Age
        /// </summary>
        public int Age { get; set; }
        #endregion
    }
}