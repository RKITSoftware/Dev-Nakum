namespace BMS.Models
{
    /// <summary>
    /// class which contains the schema of the transactions
    /// </summary>
    public class Transactions
    {
        /// <summary>
        /// Transaction's Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User's Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// amount
        /// </summary>
        public int Money { get; set; }

        /// <summary>
        /// Transaction's type - saving or deposit
        /// </summary>
        public string Type { get; set; }

    }
}