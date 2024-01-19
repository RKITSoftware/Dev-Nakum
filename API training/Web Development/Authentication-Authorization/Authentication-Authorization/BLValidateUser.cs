using Authentication_Authorization.Models;
using System.Linq;

namespace Authentication_Authorization
{
    /// <summary>
    ///  class which is validate the user and return all the user data
    /// </summary>
    public class BLValidateUser
    {
        #region Public Method 

        /// <summary>
        ///   to check user's username and password
        /// </summary>
        /// <param name="username">user's username</param>
        /// <param name="password">user's password</param>
        /// <returns>true or false </returns>
        public static bool Login(string username, string password)
        {
            return Users.GetAllUser().Any(u => u.Username == username && u.Password == password);
        }

        /// <summary>
        ///  get userdata based on username and password
        /// </summary>
        /// <param name="username">user's username</param>
        /// <param name="password">user's password</param>
        /// <returns>object of user</returns>
        public static Users GetUserDetails(string username, string password)
        {
            return Users.GetAllUser().FirstOrDefault(u => u.Username==username && u.Password==password);
        }
        #endregion
    }
}