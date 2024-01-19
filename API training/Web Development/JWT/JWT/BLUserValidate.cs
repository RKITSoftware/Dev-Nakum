using JWT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JWT
{
    /// <summary>
    /// validate the user
    /// manage the user's operation
    /// </summary>
    public class BLUserValidate : IDisposable
    {
        #region Static Properties
        public static List<User> lstUsers = User.GetUsers();

       
        #endregion

        #region
        /// <summary>
        /// to validate the username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>user's object</returns>
        public User ValidateUser(string username,string password)
        {
            return lstUsers.FirstOrDefault(u=>u.Username == username && u.Password == password);    
        }

        /// <summary>
        /// dispose method to clear the list
        /// </summary>
        public void Dispose()
        {
            lstUsers.Clear();
        }
        #endregion
    }
}