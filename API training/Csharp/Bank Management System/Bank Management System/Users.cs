namespace Bank_Management_System
{
    /// <summary>
    /// Schema of user's details 
    /// </summary>
    public class Users
    {
        #region Public Properties

        /// <summary>
        /// User's UserId
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User's phone number
        /// </summary>
        public double Phone { get; set; }
        #endregion

    }
}
