using ServiceStack.DataAnnotations;

namespace E_CommerceAPI.Models
{
    /// <summary>
    /// manage the schema of Products
    /// </summary>
    public class Pro01
    {
        /// <summary>
        /// Product's Id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int O01F01 { get; set; }

        /// <summary>
        /// Product's Name
        /// </summary>
        public string O01F02 { get; set; }

        /// <summary>
        /// Product's Description
        /// </summary>
        public string O01F03 { get; set; }

        /// <summary>
        /// Product's Price
        /// </summary>
        public int O01F04 { get; set; }

        /// <summary>
        /// Product's Stocks
        /// </summary>
        public int O01F05 { get; set; }

        /// <summary>
        /// Product's Category
        /// </summary>
        [ForeignKey(typeof(Com01))]
        public int O01F06 { get; set; }
    }
}