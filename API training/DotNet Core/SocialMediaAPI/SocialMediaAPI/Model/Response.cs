namespace SocialMediaAPI.Model
{
    /// <summary>
    /// Manage the schema of response model
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Error is there or not
        /// </summary>
        public bool IsError { get; set; } = false;

        /// <summary>
        /// Response message
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Response data
        /// </summary>
        public dynamic? Data { get; set; }
    }
}
