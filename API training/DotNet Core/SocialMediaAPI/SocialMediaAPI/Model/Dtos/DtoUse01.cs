
using static ServiceStack.LicenseUtils;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaAPI.Model.Dtos
{
    public class DtoUse01
    {
        /// <summary>
        /// User's username
        /// </summary>
        public string E01101 { get; set; }

        /// <summary>
        /// User's Email
        /// </summary>

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string E01102 { get; set; }

        /// <summary>
        /// User's Password
        /// </summary>
        public string E01103 { get; set; }

        /// <summary>
        ///  Property to receive the uploaded image file
        /// </summary>
        public IFormFile? E01104 { get; set; }

        /// <summary>
        /// User's Bio
        /// </summary>
        public string E01105 { get; set; }
    }
}
