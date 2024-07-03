namespace PartialClassAPI.Models
{
    /// <summary>
    /// Schema of customer 
    /// </summary>
    public class Customer
    {
        #region Public Properties

        /// <summary>
        /// Customer's Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Customer's Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Customer's Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Customer's City
        /// </summary>
        public string City { get; set; }
        #endregion
    }
}