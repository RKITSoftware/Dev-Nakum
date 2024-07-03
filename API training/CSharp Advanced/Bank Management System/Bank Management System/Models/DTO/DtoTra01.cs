using Newtonsoft.Json;
using ServiceStack.DataAnnotations;

namespace Bank_Management_System.Models.DTO
{
    /// <summary>
    /// manage the schema of transactions
    /// </summary>
    public class DtoTra01
    {
        #region Public Property
        /// <summary>
        /// Transactions Money
        /// </summary>
        [JsonProperty("A01103")]
        [Required]
        [Range(0, int.MaxValue)]
        public int A01F03 { get; set; } 
        #endregion
    }
}