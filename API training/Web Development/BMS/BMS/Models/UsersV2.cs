using System.ComponentModel.DataAnnotations;

namespace BMS.Models
{
    /// <summary>
    /// Schema of user version - 2
    /// </summary>
    public class UsersV2
    {
        #region Public Properties

        /// <summary>
        /// User's Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User's FullName
        /// </summary>
        [Required]
        public string FullName { get; set; }

        /// <summary>
        /// User's UserName
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// User's Password
        /// </summary>
        [Required]
        [MinLength(8, ErrorMessage = "Password length need to be more then or equal to 8")]
        public string Password { get; set; }

        /// <summary>
        /// User's Number
        /// </summary>
        [StringLength(10, ErrorMessage = "Invalid phone number")]
        public string Number { get; set; }

        /// <summary>
        /// User's Email
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// User's Money
        /// </summary>
        [Range(0, int.MaxValue)] // Assuming money cannot be negative
        public int Money { get; set; } = 0;

        /// <summary>
        /// User's Role
        /// </summary>
        public string Role { get; set; } = "U";
        #endregion
    }
}