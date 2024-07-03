using Microsoft.AspNetCore.Mvc;
using Routing_webapi.Model;

namespace Routing_webapi.Business_Logic
{
    /// <summary>
    /// business logic of users
    /// </summary>
    public class BLUsers
    {
        #region Private Member

        /// <summary>
        /// User's id
        /// </summary>
        private static int _id = 1;

        /// <summary>
        /// manage the list of the all users
        /// </summary>
        private static List<Use01> _lstUse01 = new List<Use01>();
        #endregion


        #region Public Method
        
        /// <summary>
        /// Add users into list
        /// </summary>
        /// <param name="objUse01">object of the user</param>
        /// <returns></returns>
        public EmptyResult AddUser(Use01 objUse01)
        {
            objUse01.E01F01 = _id++;
            _lstUse01.Add(objUse01);
            return new EmptyResult();
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
        public JsonResult GetUser(int id)
        {
            Use01 objUse01 = _lstUse01.FirstOrDefault(u => u.E01F01 == id);
            return new JsonResult(objUse01);
        }
        #endregion 
    }
}
