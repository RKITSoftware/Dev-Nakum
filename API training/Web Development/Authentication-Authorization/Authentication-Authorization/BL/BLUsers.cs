using Authentication_Authorization.Models;
using System.Collections.Generic;
using System.Linq;

namespace Authentication_Authorization.BL
{
    /// <summary>
    /// class to manage the users data
    /// </summary>
    public class BLUsers
    {
        #region Private Member
        /// <summary>
        /// store the list of user's
        /// </summary>
        private static List<Users> _lstUsers;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize the list with user's data
        /// </summary>
        public BLUsers()
        {
            _lstUsers = new List<Users>()
            {
                new Users() { Id = 1,Username="deven",Password="Abcd@123",Role="admin"},
                new Users() { Id = 2,Username="dev",Password="Abcd@123",Role="user"},
                new Users() { Id = 3,Username="raj",Password="Abcd@123",Role="user"},
                new Users() { Id = 4,Username="tushar",Password="Abcd@123",Role="user"},
                new Users() { Id = 5,Username="pratham",Password="Abcd@123",Role="user"},
            };
        }
        #endregion

        #region Public Method

        /// <summary>
        /// get all the users whi
        /// </summary>
        /// <returns>List of user's</returns>
        public List<Users> GetAllUsers()
        {
            return _lstUsers;
        }

        /// <summary>
        /// Login the user's based on username and password
        /// </summary>
        /// <param name="username">user's username</param>
        /// <param name="password">user's password</param>
        /// <returns>true or false based on user's details</returns>
        public bool Login(string username, string password)
        {
            return _lstUsers.Any(u => u.Username == username && u.Password == password);
        }

        /// <summary>
        /// Get the user's details based on username and password
        /// </summary>
        /// <param name="username">user's username</param>
        /// <param name="password">user's password</param>
        /// <returns>if user exist return user's object or else return null</returns>
        public Users GetUserDetails(string username, string password)
        {
            return _lstUsers.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
        #endregion
    }
}