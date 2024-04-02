namespace Dependency_Injection.Model
{
    /// <summary>
    /// schema of user's details
    /// </summary>
    public class Use01
    {
        /// <summary>
        /// User's Id
        /// </summary>
        public int E01F01 { get; set; }

        /// <summary>
        /// User's Name
        /// </summary>
        public string? E02F02 { get; set; }

        /// <summary>
        /// User's Account Number
        /// </summary>
        public Guid E01F03 { get; set; }

        /// <summary>
        /// User's Money
        /// </summary>
        public int E01F04 { get; set; } = 0;
    }
}
