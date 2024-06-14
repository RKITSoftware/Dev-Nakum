using ServiceStack.DataAnnotations;
using SocialMediaAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaAPI.Model.POCO
{
    /// <summary>
    /// manage the schema of users
    /// </summary>
    public class Use01
    {
        [PrimaryKey]
        [AutoIncrement]
        /// <summary>
        /// User's id
        /// </summary
        public int E01F01 { get; set; }

        /// <summary>
        /// User's username
        /// </summary>
        public string E01F02 { get; set; }

        /// <summary>
        /// User's Email
        /// </summary>
        public string E01F03 { get; set; }

        /// <summary>
        /// User's Password
        /// </summary>
        public string E01F04 { get; set; }

        /// <summary>
        /// User's Profile Picture
        /// </summary>
        public string? E01F05 { get; set; }

        /// <summary>
        /// User's Bio
        /// </summary>
        public string E01F06 { get; set; }

        /// <summary>
        /// User's Role
        /// </summary>
        public string E01F07 { get; set; } = enmRoles.U.ToString();
    }

    
}
