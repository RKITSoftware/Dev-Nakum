using Routing_webapi.Model;

namespace Routing_webapi.Business_Logic
{
    /// <summary>
    /// business logic of users
    /// </summary>
    public class BLUsers
    {
        #region Private Member
        private static int _id = 1;
        private static List<Use01> _lstUse01 = new List<Use01>();
        #endregion


        #region Public Method
        public bool AddUser(Use01 objUse01)
        {
            objUse01.E01F01 = _id++;
            _lstUse01.Add(objUse01);
            return true;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>List of all the users</returns>
        public List<Use01> GetUsers() { return _lstUse01; }

        /// <summary>
        /// get specific user
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>object of the user</returns>
        public Use01 GetUser(int id)
        {
            return _lstUse01.FirstOrDefault(u => u.E01F01 == id);
        }
        #endregion 
    }
}
