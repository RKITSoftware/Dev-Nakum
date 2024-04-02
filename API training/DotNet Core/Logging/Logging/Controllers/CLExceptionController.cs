using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Logging.Controllers
{
    /// <summary>
    /// Class which can manage the exception api
    /// </summary>
    [ApiController]
    public class CLExceptionsController : ControllerBase
    {
        #region Private Member
        private Logger _logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Public Method

        /// <summary>
        /// Get divided value
        /// </summary>
        /// <param name="num1">number 1</param>
        /// <param name="num2">number 2</param>
        /// <returns>divided value</returns>
        [HttpGet("api/exception/{num1}/{num2}")]
        public IActionResult GetValues(int num1, int num2)
        {
            try
            {
                int ans = num1 / num2;
                // get the log-info into file
                _logger.Info($"ans is {ans}");
                return Ok(ans);
            }
            catch (Exception ex)
            {
                // get the log-error into file
                _logger.Error(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// display an error message
        /// </summary>
        /// <returns>response message</returns>
        [HttpGet("api/error")]
        public IActionResult Error()
        {
            return BadRequest("There is an error due to unhandled exception");
        }
        #endregion
    }
}
