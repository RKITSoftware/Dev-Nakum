using System.Web.Http;
using System.Web.Http.Cors;

namespace CORS.Controllers
{
    // enabled cors 
    [EnableCors(origins:"*", headers:"*", methods:"*")]

    /// <summary>
    ///    to check cors is enabled or not 
    ///    if enabled - request is executes
    /// </summary>
    public class CorsController : ApiController
    {
        /// <summary>
        ///     display the message when cors is enabled
        /// </summary>
        /// <returns></returns>
        
        public IHttpActionResult Get()
        {
            return Ok("Hello from cors demo");
        }

        /// <summary>
        ///     display the message when cors is enabled
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/cors/{id}")]
        public IHttpActionResult GetById(int id)
        {
            return Ok($"your id is {id}");
        }

        /// <summary>
        ///     display the message when cors is enabled
        /// </summary>
        /// <returns></returns>
        [DisableCors]
        [HttpGet]
        [Route("api/cors2/{name}")]
        public IHttpActionResult GetByName(string name)
        {
            return Ok($"your name is {name}");
        }
    }
}
