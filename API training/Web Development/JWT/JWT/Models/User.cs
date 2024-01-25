using System.Collections.Generic;


namespace JWT.Models
{
    /// <summary>
    /// class which can contains schema of users
    /// </summary>
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }


        #region Public Methods

        /// <summary>
        /// Static method to get a list of all users
        /// </summary>
        /// <returns>List of Users</returns>
        public static List<User> GetUsers()
        {
            // Return a list of User objects with sample data
            return new List<User>
            {
                new User { UserId = 1, Username ="Dev", Password = "123456", Role = "Admin,SuperAdmin", Email = "dev@gmail.com"},
                new User { UserId = 2, Username ="Raj", Password = "123456", Role = "Admin", Email = "raj@gmail.com"},
                new User { UserId = 3, Username ="Kishan", Password = "123456", Role = "User", Email = "kishan@gmail.com"},
                new User { UserId = 4, Username ="Tushar", Password = "123456", Role = "User", Email = "tushar@gmail.com"}
            };
        }

        #endregion
    }
}