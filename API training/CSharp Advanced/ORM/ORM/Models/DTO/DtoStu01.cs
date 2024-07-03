using Newtonsoft.Json;

namespace ORM.Models.DTO
{
    /// <summary>
    /// Schema of students
    /// </summary>
    public class DtoStu01
    {
        /// <summary>
        /// Student Name 
        /// </summary>
        [JsonProperty("U01102")]
        public string U01F02 { get; set; }

        /// <summary>
        /// Student Age
        /// </summary>
        [JsonProperty("U01103")]
        public int U01F03 { get; set; }

        /// <summary>
        /// Student Email
        /// </summary>
        [JsonProperty("U01104")]
        public string U01F04 { get; set; }
    }
}