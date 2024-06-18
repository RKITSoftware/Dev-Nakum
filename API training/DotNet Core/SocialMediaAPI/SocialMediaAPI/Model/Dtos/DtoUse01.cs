using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaAPI.Model.Dtos
{
    public class DTOUSE01
    {
        /// <summary>
        /// User's username
        /// </summary>

        [JsonProperty("E01102")]
        [Required(ErrorMessage = "Username is required")]
        public string E01F02 { get; set; }

        /// <summary>
        /// User's Email
        /// </summary>

        [JsonProperty("E01103")]
        [EmailAddress ( ErrorMessage = "Invalid Email address")]
        [Required ( ErrorMessage = "Email is required")]
        public string E01F03 { get; set; }

        /// <summary>
        /// User's Password
        /// </summary>
        [JsonProperty("E01104")]
        [Required ( ErrorMessage = "Password is required")]
        public string E01F04 { get; set; }

        /// <summary>
        ///  Property to receive the uploaded image file
        /// </summary>
        [JsonProperty("E01105")]
        public IFormFile? E01F05 { get; set; }

        /// <summary>
        /// User's Bio
        /// </summary>
        [JsonProperty("E01106")]
        public string E01F06 { get; set; }
    }
}
