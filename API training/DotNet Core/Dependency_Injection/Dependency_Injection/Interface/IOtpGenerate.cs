namespace Dependency_Injection.Interface
{
    /// <summary>
    /// Interface of otp generate
    /// </summary>
    public interface IOtpGenerate
    {
        /// <summary>
        /// generate the otp
        /// </summary>
        /// <returns>generated otp</returns>
        int GetOTP();
    }
}
