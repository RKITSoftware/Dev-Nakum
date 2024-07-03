using ServiceStack.DataAnnotations;

namespace E_CommerceAPI.Models
{
    /// <summary>
    /// Manage the schema of comments
    /// </summary>
    public class Com01
    {
        /// <summary>
        /// Comment's Id
        /// </summary>
        public int M01F01 { get; set; }

        /// <summary>
        /// Product's Id
        /// </summary>
        [ForeignKey(typeof(Pro01))]
        public int M01F02 { get; set; }

        /// <summary>
        /// User's Id
        /// </summary>
        [ForeignKey(typeof(Use01))]
        public string M01F03 { get; set; }

        /// <summary>
        /// Comment's Description
        /// </summary>
        public string M01F04 { get; set; }
    }
}