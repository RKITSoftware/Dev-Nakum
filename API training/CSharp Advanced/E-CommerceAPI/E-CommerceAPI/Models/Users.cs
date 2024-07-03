using ServiceStack.DataAnnotations;
using System;

namespace E_CommerceAPI.Models
{
    /// <summary>
    /// manage the schema of User's
    /// </summary>
    public class Use01
    {
        /// <summary>
        /// User's Id
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public string E01F01 { get; set; }

        /// <summary>
        /// User's username
        /// </summary>
        public string E01F02 { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        public string E01F03 { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        public string E01F04 { get; set; }

        /// <summary>
        /// User's Role
        /// </summary>
        public string E01F05 { get; set; } = "User";
    }
}