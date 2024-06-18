using Newtonsoft.Json;

namespace SocialMediaAPI.Model.Dtos
{
    public class DTOPOS01
    {
        /// <summary>
        /// Post img
        /// </summary>
        [JsonProperty("S01103")]
        public IFormFile? S01F03 { get; set; }

        /// <summary>
        /// Post content
        /// </summary>
        [JsonProperty("S01104")]
        public string? S01F04 { get; set; }
    }
}
