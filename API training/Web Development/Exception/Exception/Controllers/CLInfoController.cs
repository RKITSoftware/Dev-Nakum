using Exception.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Exception.Controllers
{
    /// <summary>
    /// perform exception on info model
    /// </summary>
    public class CLInfoController : ApiController
    {
        //create the static list of info model
        static List<Info> lstInfo = new List<Info>()
        {
            new Info { Id=1, Name="Dev",City="Surat"},
            new Info { Id=2, Name="Raj",City="Porbandar"},
            new Info { Id=3, Name="Tushar",City="Mumbai"},
            new Info { Id=4, Name="Kishan",City="Surat"},
        };

       
        /// <summary>
        /// HTTPError type exception handling
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/info/{id}")]

        public HttpResponseMessage GetInfo(int id)
        {
            Info user = lstInfo.FirstOrDefault(x => x.Id == id);
            if(user == null)
            {
                string errorMes = $"Data not found for id {id}";
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, errorMes);
            }
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }


        /// <summary>
        ///     HttpResponseException
        ///     if user is null its throw an error 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>status code with error message </returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpGet]
        [Route("api/info/exception/{id}")]
        public IHttpActionResult GetInfoException(int id)
        {
            // find the user based on id
            Info user = lstInfo.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                HttpResponseMessage errorMsg = new HttpResponseMessage(HttpStatusCode.NotFound){
                    Content = new StringContent($"Data not found for id {id}")
                };
          
                throw new HttpResponseException(errorMsg);
            }
            return Ok(user);
        }


        /// <summary>
        ///     HttpResponseException - custom exception filter
        /// </summary>
        /// <param name="id"></param>
        /// <returns>throw an exception</returns>
        [HttpGet]
        [Route("api/info/filter/{id}")]
        public IHttpActionResult FetExceptionFilter(int id)
        {
            var user = lstInfo.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                throw new NullReferenceException();     // throw an from custom exception class 
            }
            return Ok();
        }
    }
}
