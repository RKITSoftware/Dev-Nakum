using ServiceStack.DataAnnotations;

namespace E_CommerceAPI.Models
{
    /// <summary>
    /// schema of orders
    /// </summary>
    public class Ord01
    {
        /// <summary>
        /// Order's Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int D01F01 { get; set; }

        /// <summary>
        /// Cart Id
        /// </summary>
        [ForeignKey(typeof(Car01))]
        public int D01F02 { get; set; }

        /// <summary>
        /// Total Price
        /// </summary>
        public int D01F03 { get; set; }

        /// <summary>
        /// User's Contact No
        /// </summary>
        public string D01F04 { get; set; }

        /// <summary>
        /// User's Address
        /// </summary>
        public string D01F05 { get; set; }

        /// <summary>
        /// PinCode
        /// </summary>
        public string D01F06 { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public string D01F07{ get; set; }

        /// <summary>
        /// Country
        /// </summary>
        public string D01F08 { get; set; }
    }
}