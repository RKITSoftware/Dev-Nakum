namespace JWT_Custom.Models
{
    /// <summary>
    /// Represents a student in the system.
    /// </summary>
    public class Students
    {
        /// <summary>
        /// Unique identifier for the student.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the student.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Password of the student
        /// </summary>
        public string Password { get; set; }
    }
}
