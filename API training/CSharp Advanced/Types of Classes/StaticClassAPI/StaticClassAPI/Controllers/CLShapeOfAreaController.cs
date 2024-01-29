using StaticClassAPI.Business_Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StaticClassAPI.Controllers
{
    /// <summary>
    /// class which can handle all the operation related area
    /// </summary>
    public class CLShapeOfAreaController : ApiController
    {
        /// <summary>
        /// find the area of square
        /// </summary>
        /// <param name="length">length of square</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/area/square/{length}")]
        public IHttpActionResult GetAreaOfSquare(int length)
        {
            int area = BLFindArea.AreaOfSquare(length);
            return Ok(area);
        }

        /// <summary>
        /// find the area of rectangle
        /// </summary>
        /// <param name="length">length of rectangle</param>
        /// <param name="width">width of rectangle</param>
        /// <returns> area of rectangle </returns>
        [HttpGet]
        [Route("api/area/square/{length}/{width}")]
        public IHttpActionResult GetAreaOfRectangle(int length,int width)
        {
            int area = BLFindArea.AreaOfRectangle(length,width);
            return Ok(area);
        }
    }
}
