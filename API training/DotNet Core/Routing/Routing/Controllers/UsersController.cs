using Microsoft.AspNetCore.Mvc;

namespace Routing.Controllers
{
    [Route("users")]
    public class UsersController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("{id}")]
        public int Count(int id)
        {
            if (id < 0)
            {
                throw new Exception("Id must be greater then zero");
            }
            return id;
        }
    }
}
