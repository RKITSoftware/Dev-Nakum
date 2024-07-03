using BMS.BL;
using System.ComponentModel.DataAnnotations;

namespace BMS.Models
{
    /// <summary>
    /// Schema of user version - 1
    /// </summary>
    public class UsersV1
    {
        #region Public Properties

        /// <summary>
        /// User's Id (assumed to be unique)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User's first name (required, maximum length of 50 characters)
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// User's last name (required, maximum length of 50 characters)
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// User's username (required, unique, maximum length of 20 characters)
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// User's password (required, minimum length of 8 characters)
        /// </summary>
        [Required]
        [MinLength(8, ErrorMessage = "Password length need to be more then or equal to 8")]
        public string Password { get; set; }

        /// <summary>
        /// User's phone number 
        /// </summary>
        [StringLength(10, ErrorMessage = "Invalid phone number")] 
        public string Number { get; set; }

        /// <summary>
        /// User's email address (required, valid email format)
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// User's money (defaults to 0, non-negative)
        /// </summary>
        [Range(0, int.MaxValue)] // Assuming money cannot be negative
        public int Money { get; set; } = 0;

        /// <summary>
        /// User's role (defaults to "U")
        /// </summary>
        public string Role { get; set; } = enmRoles.U.ToString();
        #endregion
    }
}
