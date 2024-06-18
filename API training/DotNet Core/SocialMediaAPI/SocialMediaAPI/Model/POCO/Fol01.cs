using ServiceStack.DataAnnotations;

namespace SocialMediaAPI.Model.POCO
{
    /// <summary>
    /// manage the schema of comments
    /// </summary>
    public class FOL01
    {
        /// <summary>
        /// id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement] 
        public int L01F01 { get; set; }

        /// <summary>
        /// user's id
        /// </summary>
        public int L01F02 { get; set; }

        /// <summary>
        /// following user's id
        /// </summary>
        public int L01F03 { get; set; }
    }
}
