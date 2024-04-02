using Dependency_Injection.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Dependency_Injection.Controllers
{
    /// <summary>
    /// controller can manage the otp related api
    /// </summary>
    [Route("api/generate")]
    [ApiController]
    public class CLOtpController : ControllerBase
    {
        #region Private Member
        private readonly IOtpGenerate _otpGenerate;
        #endregion

        #region Construtor
        public CLOtpController(IOtpGenerate otpGenerate)
        {
            _otpGenerate = otpGenerate;
        }
        #endregion

        #region Public Method
        
        /// <summary>
        /// Generated the otp
        /// </summary>
        /// <returns>generated otp</returns>
        [HttpGet]
        public IActionResult GenerateOTP()
        {
            return Ok(_otpGenerate.GetOTP());
        }
        #endregion
    }
}
