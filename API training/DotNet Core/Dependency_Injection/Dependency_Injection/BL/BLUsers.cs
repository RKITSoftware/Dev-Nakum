using Dependency_Injection.Interface;
using Dependency_Injection.Model;

namespace Dependency_Injection.BL
{
    /// <summary>
    /// class of users related operation
    /// </summary>
    public class BLUsers : IUsers
    {
        #region Private Member
        private List<Use01> _lstUsers = new List<Use01>();
        private int id = 1;
        #endregion

        #region Public Method
        /// <summary>
        /// Add users into list
        /// </summary>
        /// <param name="obhUse01">object of users</param>
        /// <returns>true or false for successfully adding into list</returns>
        public bool AddUsers(Use01 objUse01)
        {
            try
            {
                objUse01.E01F01 = id++;
                Guid guid = Guid.NewGuid();
                objUse01.E01F03 = guid;

                _lstUsers.Add(objUse01);   
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// List of all the users
        /// </summary>
        /// <returns>List of all the users</returns>
        public List<Use01> GetAllUsers()
        {
            return _lstUsers;
        }


        /// <summary>
        /// get user's details based on account number
        /// </summary>
        /// <param name="id">account number</param>
        /// <returns>user's details</returns>
        public Use01? GetUserById(Guid id)
        {
            return _lstUsers.FirstOrDefault(u => u.E01F03 == id);
        }


        /// <summary>
        /// Update the user's money while perform the transaction
        /// </summary>
        /// <param name="accountNo">account number</param>
        /// <param name="amount">transaction amount</param>
        /// <param name="type">transaction type</param>
        /// <returns>true or false as per successfully updated user</returns>
        public bool UpdateUser(Guid accountNo, int amount,string type)
        {
            Use01 user = GetUserById(accountNo);
            if (user != null)
            {
                if (type == "Deposit")
                {
                    user.E01F04 += amount;
                }
                else
                {
                    user.E01F04 -= amount;
                }

                return true;
            }
            return false ;
        }


        #endregion

    }
}
