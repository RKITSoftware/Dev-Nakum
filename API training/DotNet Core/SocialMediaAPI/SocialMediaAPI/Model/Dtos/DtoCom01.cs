using Newtonsoft.Json;

namespace SocialMediaAPI.Model.Dtos
{
    /// <summary>
    /// manage the DTO schema of comments
    /// </summary>
    public class DtoCom01
    {
        /// <summary>
        /// post id
        /// </summary>
        [JsonProperty("M01102")]
        public int M01F02 { get; set; }

        /// <summary>
        /// Comment's content
        /// </summary>
        [JsonProperty("M01104")]
        public string M01F04 { get; set; }
    }
}
