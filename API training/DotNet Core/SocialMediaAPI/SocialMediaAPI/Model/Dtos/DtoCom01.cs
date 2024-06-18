using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaAPI.Model.Dtos
{
    /// <summary>
    /// manage the DTO schema of comments
    /// </summary>
    public class DTOCOM01
    {
        /// <summary>
        /// post id
        /// </summary>
        [JsonProperty("M01102")]
        [Range(0, int.MaxValue,ErrorMessage = "Post id must be in range")]
        public int M01F02 { get; set; }

        /// <summary>
        /// Comment's content
        /// </summary>
        [JsonProperty("M01104")]
        [Required(ErrorMessage = "Comment content is required")]
        public string M01F04 { get; set; }
    }
}
