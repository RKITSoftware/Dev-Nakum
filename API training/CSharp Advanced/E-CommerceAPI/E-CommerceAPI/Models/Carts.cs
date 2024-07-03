using ServiceStack.DataAnnotations;
using System;

namespace E_CommerceAPI.Models
{
    /// <summary>
    /// schema of Carts
    /// </summary>
    public class Car01
    {
        /// <summary>
        /// Cart Id
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int R01F01 { get; set; }

        /// <summary>
        /// User's Id
        /// </summary>
        [ForeignKey(typeof(Use01))]
        public string R01F02 { get; set; }

        
        /// <summary>
        /// Created At
        /// </summary>
        public DateTime R01F03 { get; set; } = DateTime.Now;
    }
}