using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaAPI.Model.Dtos
{
    public class DTOUSE02
    {
        /// <summary>
        /// User's username
        /// </summary>
        [JsonProperty("E02102")]
        [Required(ErrorMessage = "Username is required")]
        public string E02F02 { get; set; }

        /// <summary>
        /// User's Password
        /// </summary>
        [JsonProperty("E02104")]
        [Required(ErrorMessage = "Password is required")]
        public string E02F04 { get; set; }
    }
}
