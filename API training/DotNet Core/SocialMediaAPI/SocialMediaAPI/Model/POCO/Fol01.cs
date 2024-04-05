using ServiceStack.DataAnnotations;

namespace SocialMediaAPI.Model.POCO
{
    /// <summary>
    /// manage the schema of comments
    /// </summary>
    public class Fol01
    {
        /// <summary>
        /// id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement] 
        public int L01F01 { get; set; }

        /// <summary>
        /// follower's id
        /// </summary>
        public int L01F02 { get; set; }

        /// <summary>
        /// following's id
        /// </summary>
        public int L01F03 { get; set; }
    }
}
