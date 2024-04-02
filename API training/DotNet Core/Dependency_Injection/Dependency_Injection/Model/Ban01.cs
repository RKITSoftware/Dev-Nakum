namespace Dependency_Injection.Model
{
    /// <summary>
    /// Schema of user's banking information
    /// </summary>
    public class Ban01
    {
        
        /// <summary>
        /// User's Account Number
        /// </summary>
        public Guid N01F01 { get; set; }

        /// <summary>
        /// Transaction Type - Withdraw or Debit
        /// </summary>
        public string N01F02 { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public int N01F03 { get; set; }
    }
}
