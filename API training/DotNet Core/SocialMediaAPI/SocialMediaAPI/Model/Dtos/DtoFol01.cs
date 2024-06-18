using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaAPI.Model.DTOS
{
    /// <summary>
    /// manage the schema of comments
    /// </summary>
    public class DTOFOL01
    {
        /// <summary>
        /// following's id
        /// </summary>
        [JsonProperty("L01103")]
        [Required(ErrorMessage = "following user's id is required")]
        public int L01F03 { get; set; }

    }
}
