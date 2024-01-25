using Bank_Management_System.Models;
using System;
using System.Linq;

namespace Bank_Management_System.BusinessLogic
{
    /// <summary>
    /// Validate user based on username and password
    /// </summary>
    public class BLValidateUser
    {
        /// <summary>
        /// To check user's username and password
        /// </summary>
        /// <param name="username">username of user</param>
        /// <param name="password">password of user</param>
        /// <returns>if user is exist returns object of user</returns>
        public static object ValidateUser(string username, string password,int version = 1)
        {
            if (version == 1)
            {
                return UsersV1.lstUsers.FirstOrDefault(u => u.UserName == username && u.Password == password);

            }
            return UsersV2.lstUsers.FirstOrDefault(u => u.UserName == username && u.Password == password);
        }

      
    }
}