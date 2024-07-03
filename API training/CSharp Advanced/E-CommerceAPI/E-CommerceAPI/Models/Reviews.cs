using ServiceStack.DataAnnotations;

namespace E_CommerceAPI.Models
{
    /// <summary>
    /// Manage the schema of reviews
    /// </summary>
    public class Rev01
    {
        /// <summary>
        /// Review's Id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int V01F01 { get; set; }

        /// <summary>
        /// Product's Id
        /// </summary>
        [ForeignKey(typeof(Pro01))]
        public int V01F02 { get; set; }

        /// <summary>
        /// User's Id
        /// </summary>
        [ForeignKey(typeof(Use01))]
        public string V01F03 { get; set; }

        /// <summary>
        /// Review's Count
        /// </summary>
        public int V01F04 { get; set; }

    }
}