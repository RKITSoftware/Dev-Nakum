namespace Bank_Management_System.Enums
{
    /// <summary>
    /// manage the operation types (A- Add, E - Edit, D-Delete)
    /// </summary>
    public enum enmOperationTypes
    {
        /// <summary>
        /// Edit
        /// </summary>
        E,

        /// <summary>
        /// Add
        /// </summary>
        A,

        /// <summary>
        /// Delete
        /// </summary>
        D,

        /// <summary>
        /// Login
        /// </summary>
        Login
    };

    /// <summary>
    /// Manage the transaction types (D - Deposit, W - Withdraw)
    /// </summary>
    public enum enmTransactionTypes
    {
        /// <summary>
        /// Deposit
        /// </summary>
        D,

        /// <summary>
        /// Withdraw
        /// </summary>
        W
    }
}