using Dependency_Injection.Interface;

namespace Dependency_Injection.BL
{
    /// <summary>
    /// class of otp generate
    /// </summary>
    public class BLOTP : IOtpGenerate
    {
        #region Public Method
        /// <summary>
        /// generate the otp
        /// </summary>
        /// <returns>generated otp</returns>
        public int GetOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        } 
        #endregion
    }
}
