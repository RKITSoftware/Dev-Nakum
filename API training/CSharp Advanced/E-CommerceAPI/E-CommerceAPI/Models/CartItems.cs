using ServiceStack.DataAnnotations;

namespace E_CommerceAPI.Models
{
    /// <summary>
    /// schema of cart items 
    /// </summary>
    public class Car02
    {
        /// <summary>
        /// CartItems id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int R01F01 { get; set; }

        /// <summary>
        /// Cart id 
        /// </summary>
        [ForeignKey(typeof(Car01))]
        public int R02F02 { get; set; }

        /// <summary>
        /// product id
        /// </summary>
        [ForeignKey(typeof(Pro01))]
        public int R02F03 { get; set; }

        /// <summary>
        /// Product's quantity
        /// </summary>
        public int R02F04 { get; set; }

      
    }
}