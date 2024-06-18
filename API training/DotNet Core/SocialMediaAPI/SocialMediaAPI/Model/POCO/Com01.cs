using ServiceStack.DataAnnotations;

namespace SocialMediaAPI.Model.POCO
{
    /// <summary>
    /// manage the schema of comments
    /// </summary>
    public class COM01
    {
        /// <summary>
        /// Comment's id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int M01F01 { get; set; }

        /// <summary>
        /// post id
        /// </summary>
        public int M01F02 { get; set; }

        /// <summary>
        /// user id
        /// </summary>
        public int M01F03 { get; set; }

        /// <summary>
        /// Comment's content
        /// </summary>
        public string M01F04 { get; set; } = string.Empty;

        /// <summary>
        /// created at
        /// </summary>
        public DateTime M01F05 { get; set; } = DateTime.Now;
    }
}
