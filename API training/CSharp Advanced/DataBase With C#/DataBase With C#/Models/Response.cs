namespace DataBase_With_C_.Models
{
    /// <summary>
    /// manage the schema of response model
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Is any error available or not
        /// </summary>
        public bool IsError { get; set; } = false;

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Response data
        /// </summary>
        public dynamic Data { get; set; }
    }
}