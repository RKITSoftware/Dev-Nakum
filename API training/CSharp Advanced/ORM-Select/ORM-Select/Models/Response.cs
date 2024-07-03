namespace ORM_Select.Models
{
    /// <summary>
    /// Manage the response model
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Error is available or not
        /// </summary>
        public bool IsError { get; set; } = false;

        /// <summary>
        /// response message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// response data
        /// </summary>
        public dynamic Data { get; set; }
    }
}