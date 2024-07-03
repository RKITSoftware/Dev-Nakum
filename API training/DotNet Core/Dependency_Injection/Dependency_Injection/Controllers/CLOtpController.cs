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

        /// <summary>
        /// create the object of the otp generator interface
        /// </summary>
        private readonly IOtpGenerate _otpGenerate;
        #endregion

        #region Construtor

        /// <summary>
        /// initialize the object of the otp generator interface
        /// </summary>
        /// <param name="otpGenerate"></param>
        public CLOtpController(IOtpGenerate otpGenerate)
        {
            _otpGenerate = otpGenerate;
        }
        #endregion

        #region Public Method
        
        /// <summary>
        /// Generate the otp
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
