using ORM_Select.BL;
using ORM_Select.Models;
using System.Web.Http;

namespace ORM_Select.Controllers
{
    [RoutePrefix("api/author")]
    public class CLAuthorController : ApiController
    {
        #region Private Member
        private readonly BLAuthor _objBLAuthor; 
        private readonly BLPerson _objBLPerson; 
        #endregion

        #region Public Member
        public Response objResponse;
        #endregion

        #region Constructor
        public CLAuthorController()
        {
            objResponse = new Response();
            _objBLAuthor = new BLAuthor();
            _objBLPerson = new BLPerson();
        }
        #endregion    

        [HttpGet]
        [Route("GetAuthorFilterBirth")]
        public IHttpActionResult GetAuthorFilterBirth()
        {
            objResponse = _objBLAuthor.GetAuthorFilterBirth();    
            return Ok(objResponse);
        }

        [HttpGet]
        [Route("SqlIn")]
        public IHttpActionResult SqlIn()
        {
            objResponse = _objBLAuthor.SqlIn();
            return Ok(objResponse);
        }
        
        [HttpGet]
        [Route("GetALLPerson")]
        public IHttpActionResult GetALLPerson()
        {
            objResponse = _objBLPerson.GetALLPerson();
            return Ok(objResponse);
        }
        
        [HttpGet]
        [Route("Has42YearOlds")]
        public IHttpActionResult Has42YearOlds()
        {
            objResponse = _objBLPerson.Has42YearOlds();
            return Ok(objResponse);
        }

        [HttpGet]
        [Route("FilterOnAge")]
        public IHttpActionResult FilterOnAge()
        {
            objResponse = _objBLPerson.FilterOnAge();
            return Ok(objResponse);
        }

        [HttpGet]
        [Route("Count")]
        public IHttpActionResult Count()
        {
            objResponse = _objBLPerson.Count();
            return Ok(objResponse);
        }
    }
}
