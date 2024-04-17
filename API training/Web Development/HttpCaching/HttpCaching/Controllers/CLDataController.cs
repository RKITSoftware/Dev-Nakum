using HttpCaching.BL;
using System.Web.Http;

namespace HttpCaching.Controllers
{
    /// <summary>
    /// Operation related on cache
    /// </summary>
    public class CLDataController : ApiController
    {
        /// <summary>
        ///  Get the user data and store into cache
        /// </summary>
        /// <returns>users data</returns>
        [HttpGet]
        [Route("api/data/")]
        public IHttpActionResult GetResult()
        {
            string[][] result = new string[5][]
            {
                new string[]{"Dev","Nakum"},
                new string[]{"Kishan","Nakum"},
                new string[]{"Raj","Mandaviya"},
                new string[]{"Pratham","Modi"},
                new string[]{"Tushar","Gohil"},
            };


            for (int i = 0; i < result.Length; i++)
            {
                BLCache.Add((i+1).ToString(), result[i]);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get the result by id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>user's data based on user id</returns>
        [HttpGet]
        [Route("api/data/{id}")]
        public IHttpActionResult GetResultById(string id)
        {
            object data = BLCache.Get(id);
            if(data == null)
            {
                return BadRequest("Not Found");
            }
            BLCache.Remove(id);
            return Ok(data);
        }

    }
}
