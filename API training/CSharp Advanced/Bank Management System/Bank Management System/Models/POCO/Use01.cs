using ServiceStack.DataAnnotations;

namespace Bank_Management_System.Models.POCO
{
    /// <summary>
    /// Schema of the user
    /// </summary>
    public class Use01
    {
        #region Public Properties
        /// <summary>
        /// User's id
        /// </summary>
        [PrimaryKey]
        public int E01F01 { get; set; }

        /// <summary>
        /// User's Name
        /// </summary>
        
        public string E01F02 { get; set; }

        /// <summary>
        /// User's Password
        /// </summary>
        [IgnoreOnUpdate]
        public string E01F03 { get; set; }

        /// <summary>
        /// User's Email
        /// </summary>
        [IgnoreOnUpdate]
        public string E01F04 { get; set; }

        /// <summary>
        /// User's Money
        /// </summary>
        public int E01F05 { get; set; } = 0;

        /// <summary>
        /// User's Role
        /// </summary>
        [IgnoreOnUpdate]
        public char E01F06 { get; set; } = 'U';
        #endregion
    }
}