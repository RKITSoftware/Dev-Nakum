using Dependency_Injection.Model;

namespace Dependency_Injection.Interface
{
    /// <summary>
    /// Interface of bank related transaction
    /// </summary>
    public interface IBank
    {
        /// <summary>
        /// Add transaction into list
        /// </summary>
        /// <param name="objBan01">object of bank</param>
        /// <returns>true or false for successfully add into list</returns>
        bool AddTransaction(Ban01 objBan01);
    }
}
