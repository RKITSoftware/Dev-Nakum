using Newtonsoft.Json;
using ServiceStack.DataAnnotations;

namespace SocialMediaAPI.Model.Dtos
{
    public class DtoUse01
    {
        /// <summary>
        /// User's username
        /// </summary>

        [JsonProperty("E01102")]
        [Unique]
        public string E01F02 { get; set; }

        /// <summary>
        /// User's Email
        /// </summary>

        //[EmailAddress(ErrorMessage = "Invalid Email Address")]
        [JsonProperty("E01103")]
        public string E01F03 { get; set; }

        /// <summary>
        /// User's Password
        /// </summary>
        [JsonProperty("E01104")]
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
