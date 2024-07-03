using ServiceStack.DataAnnotations;

namespace E_CommerceAPI.Models
{
    /// <summary>
    /// Manage the schema of Categories
    /// </summary>
    public class Cat01
    {

        /// <summary>
        /// Category's Id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement] 
        public int T01F01 { get; set; }

        /// <summary>
        /// Category's Name
        /// </summary>
        public string T01F02 { get; set; }

        /// <summary>
        /// Product's count
        /// </summary>
        public int T01F03 { get; set; } = 0;

    }
}