using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HttpCaching.Controllers
{
    public class CLDataController : ApiController
    {
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
                var temp = result[i];
                BLCache.Add((i+1).ToString(), result[i]);
            }
            return Ok(result);
        }

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
