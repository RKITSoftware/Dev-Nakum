using Newtonsoft.Json;

namespace Bank_Management_System.Models.DTO
{
    /// <summary>
    /// Manage the schema of users
    /// </summary>
    public class DtoUse01
    {
        #region Public Properties
        /// <summary>
        /// Employee's Name
        /// </summary>
        [JsonProperty("E01102")]
        public string E01F02 { get; set; }

        /// <summary>
        /// Employee's Password
        /// </summary>
        [JsonProperty("E01103")]
        public string E01F03 { get; set; }

        /// <summary>
        /// Employee's Email
        /// </summary>
        [JsonProperty("E01104")]
        public string E01F04 { get; set; } 
        #endregion
    }
}