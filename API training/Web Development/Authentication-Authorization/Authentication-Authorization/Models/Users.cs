namespace Authentication_Authorization.Models
{
    /// <summary>
    /// schema of users
    /// </summary>
    public class Users
    {
        #region Public Properties

        /// <summary>
        /// User's Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User's username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User's role
        /// </summary>
        public string Role { get; set; }
        #endregion
    }
}