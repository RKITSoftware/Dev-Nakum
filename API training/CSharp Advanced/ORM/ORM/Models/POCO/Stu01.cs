using ServiceStack.DataAnnotations;

namespace ORM.Models.POCO
{
    /// <summary>
    /// Schema of students
    /// </summary>
    [Alias("stu01")]
    public class Stu01
    {
        /// <summary>
        /// Student ID
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int U01F01 { get; set; }
        /// <summary>
        /// Student Name 
        /// </summary>
        public string U01F02 { get; set; }

        /// <summary>
        /// Student Age
        /// </summary>
        public int U01F03 { get; set; }
        
        /// <summary>
        /// Student Email
        /// </summary>
        public string U01F04 { get; set; }
    }
}