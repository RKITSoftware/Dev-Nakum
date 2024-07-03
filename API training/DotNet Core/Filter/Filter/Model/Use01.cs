namespace Filter.Model
{
    /// <summary>
    /// schema of user
    /// </summary>
    public class Use01
    {
        /// <summary>
        /// user's id
        /// </summary>
        public int E01F01 { get; set; }

        /// <summary>
        /// user's name
        /// </summary>
        public string E01F02 { get; set; }

        /// <summary>
        /// user's password
        /// </summary>
        public string E01F03 { get; set; }

        /// <summary>
        /// user's role
        /// </summary>
        public string? E01F04 { get; set; } = "U";
    }
}
