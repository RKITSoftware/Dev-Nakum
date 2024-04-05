using ServiceStack.DataAnnotations;

namespace SocialMediaAPI.Model.POCO
{
    /// <summary>
    /// manage the schema of posts
    /// </summary>
    public class Pos01
    {
        /// <summary>
        /// Post's id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int S01F01 { get; set; }

        /// <summary>
        /// User's id
        /// </summary>
        public int S01F02 { get; set; }

        /// <summary>
        /// Post img
        /// </summary>
        public string S01F03 { get; set; }

        /// <summary>
        /// Post content
        /// </summary>
        public string S01F04 { get; set; }

        /// <summary>
        /// Post created at
        /// </summary>
        public DateTime S01F05 { get; set; } = DateTime.Now;

        /// <summary>
        /// Post's updated at
        /// </summary>
        public DateTime S01F06 { get; set; } = DateTime.Now;
    }
}
