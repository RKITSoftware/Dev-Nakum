using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Web_API_Demo.Controllers
{
    public class DemoController : ApiController
    {
        public static List<string> lstUser = new List<string>();

        [HttpGet]
        [Route("api/demo/getdata")]
        public IHttpActionResult DataGet()
        {
            return Ok(lstUser);
        }
        public IHttpActionResult Getdata(int id)
        {
            return Ok(lstUser[id]);
        }

        public IHttpActionResult Post(string name)
        {
            lstUser.Add(name);
            return Ok("user added");
        }
        public IHttpActionResult Put(JObject data)
        {
            string name = data["username"].ToString();
            int id = Convert.ToInt32(data["id"]);

            string temp = lstUser[id];
            if (lstUser[id]!=null)
            {
                lstUser[id] = name;
                return Ok("successfully update name");

            }
          
            return BadRequest("Id is not exist");
           
        }
        public IHttpActionResult Delete(int id)
        {
            if (lstUser[id] != null)
            {
                lstUser.RemoveAt(id);
                return Ok("successfully delete the id");

            }
            return BadRequest("Id is not exist");
        }
    }
}
