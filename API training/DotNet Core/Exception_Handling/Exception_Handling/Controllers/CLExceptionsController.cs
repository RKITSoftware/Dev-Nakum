using Microsoft.AspNetCore.Mvc;

namespace Exception_Handling.Controllers
{
    /// <summary>
    /// class which manage the exceptions related api
    /// </summary>
    [ApiController]
    public class CLExceptionsController : ControllerBase
    {
        #region Public Method
        /// <summary>
        /// division of two number
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        [HttpGet("api/exception/{num1}/{num2}")]
        public IActionResult GetValues(int num1, int num2)
        {
            try
            {
                return Ok(num1 / num2);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// print error message 
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/error")]
        public IActionResult Error()
        {
            return BadRequest("There is an error due to unhandled exception");
        } 
        #endregion
    }
}
