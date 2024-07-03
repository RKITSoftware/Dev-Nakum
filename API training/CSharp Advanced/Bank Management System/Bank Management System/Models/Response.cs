namespace Bank_Management_System.Models
{
    /// <summary>
    /// schema of Response 
    /// </summary>
    public class Response
    {
        #region Public Properties
        /// <summary>
        /// Error is exist or not
        /// </summary>
        public bool IsError { get; set; } = false;

        /// <summary>
        /// Messages
        /// </summary>
        public string Message { get; set; } = "";

        /// <summary>
        /// Data to Display
        /// </summary>
        public dynamic Data { get; set; }  
        #endregion
    }
}