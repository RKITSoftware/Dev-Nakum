using System.ComponentModel.DataAnnotations;

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
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// amount
        /// </summary>
        [Range(0, int.MaxValue)] // Assuming money cannot be negative
        public int Money { get; set; }

        /// <summary>
        /// Transaction's type - withdraw or deposit
        /// </summary>
        public string Type { get; set; }

    }
}