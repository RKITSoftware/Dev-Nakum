namespace SealedClassAPI.Models
{
    /// <summary>
    /// schema of Child 
    /// </summary>
    public class Child
    {
        #region Public Properties

        /// <summary>
        /// Child's Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Child's Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Child's Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Parents object
        /// </summary>
        public Parents Parent { get; set; }
        #endregion
    }
}