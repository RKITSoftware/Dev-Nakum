using Dependency_Injection.Model;

namespace Dependency_Injection.Interface
{
    /// <summary>
    /// Interface of users related operation
    /// </summary>
    public interface IUsers
    {
        /// <summary>
        /// Add users into list
        /// </summary>
        /// <param name="obhUse01">object of users</param>
        /// <returns>true or false for successfully adding into list</returns>
        bool AddUsers(Use01 obhUse01);

        /// <summary>
        /// List of all the users
        /// </summary>
        /// <returns>List of all the users</returns>
        List<Use01> GetAllUsers();

        /// <summary>
        /// Update the user's money while perform the transaction
        /// </summary>
        /// <param name="accountNo">account number</param>
        /// <param name="amount">transaction amount</param>
        /// <param name="type">transaction type</param>
        /// <returns>true or false as per successfully updated user</returns>
        bool UpdateUser(Guid accountNo, int amount,string type);

        /// <summary>
        /// get user's details based on account number
        /// </summary>
        /// <param name="id">account number</param>
        /// <returns>user's details</returns>
        Use01 GetUserById(Guid id);
    }
}
