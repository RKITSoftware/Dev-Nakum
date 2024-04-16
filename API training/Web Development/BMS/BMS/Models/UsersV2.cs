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
        public string FullName { get; set; }

        /// <summary>
        /// User's UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User's Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User's Number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// User's Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User's Money
        /// </summary>
        public int Money { get; set; } = 0;

        /// <summary>
        /// User's Role
        /// </summary>
        public string Role { get; set; } = "U";
        #endregion
    }
}