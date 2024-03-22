using Middleware___webapi.Model;

namespace Middleware___webapi.Business_Logic
{
    /// <summary>
    /// Business logic of users
    /// </summary>
    public class BLUsers
    {
        #region Private Member
        private static int _id = 1;
        private static List<Use01> _lstUse01 = new List<Use01>();
        #endregion
        

        #region Public Method
        /// <summary>
        /// Add users into List
        /// </summary>
        /// <param name="objUse01"></param>
        /// <returns></returns>
        public bool AddUsers(Use01 objUse01)
        {
            objUse01.E01F01 = _id++;
            _lstUse01.Add(objUse01);
            return true;
        }

        /// <summary>
        /// list of all users
        /// </summary>
        /// <returns></returns>
        public List<Use01> GetAllUsers()
        {
            return _lstUse01;
        }
        
        /// <summary>
        /// Validate the username and password
        /// </summary>
        /// <param name="username">user's username</param>
        /// <param name="password">user's password</param>
        /// <returns>true or false based on user's exist or not</returns>
        public bool ValidateCredentials(string username, string password)
        {
            return _lstUse01.Any(u => u.E01F02 == username && u.E01F04 == password);
        }
        
        #endregion
    }
}
