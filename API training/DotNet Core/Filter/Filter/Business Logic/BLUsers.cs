using Filter.Model;

namespace Filter.Business_Logic
{
    /// <summary>
    /// Manage the User services
    /// </summary>
    public class BLUsers
    {
        #region Private Member

        /// <summary>
        /// User's id
        /// </summary>
        private static int _id = 1;

        /// <summary>
        /// manage the list to store the all users
        /// </summary>
        private static List<Use01> _lstUse01 = new List<Use01>();
        #endregion

        #region Public Method

        /// <summary>
        /// register the user into list
        /// </summary>
        /// <param name="objUse01">object of the users</param>
        public void SignUp (Use01 objUse01)
        {
            objUse01.E01F01 = _id++;
            _lstUse01.Add(objUse01);
        }
        /// <summary>
        /// login the user
        /// </summary>
        /// <param name="objUse01">object of the user</param>
        /// <returns>token and role</returns>
        public object? Login(Use01 objUse01)
        {
            Use01 user = _lstUse01.FirstOrDefault(u => u.E01F02 == objUse01.E01F02 && u.E01F03 == objUse01.E01F03);
            if (user == null)
            {
                return null;
            }
            BLAuth objBLAuth = new BLAuth();
            string token = objBLAuth.GenerateJWT(user.E01F01,user.E01F02,user.E01F04);
            
            return new
            {
                token,
                user.E01F04,
            };
        }

        /// <summary>
        /// get the user details based on user id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>user details</returns>
        public Use01 UserDetails(int id)
        {
            return _lstUse01.FirstOrDefault(u => u.E01F01 == id);
        }

        /// <summary>
        /// get all user details
        /// </summary>
        /// <returns>list of all users</returns>
        public List<Use01> AllUser()
        {
            return _lstUse01;
        }
        #endregion
    }
}
