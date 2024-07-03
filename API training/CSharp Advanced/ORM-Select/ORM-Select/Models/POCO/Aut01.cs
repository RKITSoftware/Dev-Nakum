using System;

namespace ORM_Select.Models.POCO
{
    /// <summary>
    /// Manage the schema of author
    /// </summary>
    public class Aut01
    {
        /// <summary>
        /// Author's Id
        /// </summary>
        public int T01F01 { get; set; }

        /// <summary>
        /// Author's Name
        /// </summary>
        public string T01F02 { get; set; }

        /// <summary>
        /// Author's Birth day
        /// </summary>
        public DateTime T01F03 { get; set; }

        /// <summary>
        /// Author's City
        /// </summary>
        public string T01F04 { get; set; }

        /// <summary>
        /// Author's Earning
        /// </summary>
        public Decimal T01F05 { get; set; }

        /// <summary>
        /// Author's rating
        /// </summary>
        public int T01F06 { get; set; }
    }
}