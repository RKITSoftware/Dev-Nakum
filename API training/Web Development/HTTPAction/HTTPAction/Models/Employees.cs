namespace HTTPAction.Models
{
    /// <summary>
    /// Manage the schema of employees
    /// </summary>
    public class Employees
    {
        #region Public Properties

        /// <summary>
        /// Employee's id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Employee's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Employee's age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Employee's gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Employee's designation
        /// </summary>
        public string Designation { get; set; }
        #endregion
    }
}