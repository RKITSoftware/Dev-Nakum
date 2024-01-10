using MVC.Models;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            // fetch the details from models
            Employee objEmployee = new Employee()
            {
                Name = "Dev",
                Age = 21,
                Designation = "Software Engineer"
            };

            ViewData["Employee"] = objEmployee;
            return View();
        }
    }
}