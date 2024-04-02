using Microsoft.AspNetCore.Mvc;

namespace Exception_Handling.Controllers
{
    [ApiController]
    public class CLExceptionsController : ControllerBase
    {
        [HttpGet("api/exception/{num1}/{num2}")]
        public IActionResult GetValues(int num1,int num2)
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

        [HttpGet("api/error")]
        public IActionResult Error()
        {
            return BadRequest("There is an error due to unhandled exception");
        }
    }
}
