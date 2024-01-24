using JWT.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JWT.Controllers
{
    /// <summary>
    /// Manage all the employee related operations
    /// </summary>
    public class EmployeesController : ApiController
    {
        #region Private Member
        private BLEmployeeService objBLEmployeeService;
        #endregion

        #region Counstuctor
        public EmployeesController()
        {
            objBLEmployeeService = BLEmployeeService.Instance;
        }
        #endregion

        /// <summary>
        /// find the employee from lstEmployee using employee id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>response message </returns>
        [Authorize(Roles = "User,Admin,SuperAdmin")]
        [HttpGet]
        [Route("api/employees/{id}")]
        public HttpResponseMessage GetEmployeeById(int id)
        {
            Employee employee = objBLEmployeeService.LstEmployees.FirstOrDefault(x => x.Id == id);
            if(employee == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee id {id} is not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }


        /// <summary>
        /// create the employee
        /// </summary>
        /// <param name="employee">request body</param>
        /// <returns>response message</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/employees/")]
        public HttpResponseMessage CreateEmployee(Employee employee)
        {
            objBLEmployeeService.LstEmployees.Add(employee);
            return Request.CreateResponse(HttpStatusCode.Created, employee);
        }

        /// <summary>
        /// update the employee based on employee id 
        /// </summary>
        /// <param name="id">employee id</param>
        /// <param name="employee">request body</param>
        /// <returns>response message based on employee exists</returns>
        [Authorize(Roles = "SuperAdmin")]
        [HttpPut]
        [Route("api/employees/{id}")]

        public HttpResponseMessage UpdateEmployee(int id, Employee employee)
        {
            Employee emp = objBLEmployeeService.LstEmployees.FirstOrDefault(x => x.Id == id);
            if (emp == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee id {id} is not found");
            }
            emp.FirstName = employee.FirstName;
            emp.LastName = employee.LastName;
            emp.Email = employee.Email;
            emp.Gender = employee.Gender;

            return Request.CreateResponse(HttpStatusCode.OK, "Successfully update the Employee");
        }

        /// <summary>
        /// delete the employee based on employee id 
        /// </summary>
        /// <param name="id">employee id</param>
        /// <returns>response message based on employee exists</returns>
        [Authorize (Roles ="SuperAdmin")]
        [HttpDelete]
        [Route("api/employees/{id}")]

        public HttpResponseMessage DeleteEmployee(int id)
        {
            Employee emp = objBLEmployeeService.LstEmployees.FirstOrDefault(x => x.Id == id);
            if (emp == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee id {id} is not found");
            }
            objBLEmployeeService.LstEmployees.Remove(emp);
            return Request.CreateResponse(HttpStatusCode.OK, "Successfully delete the Employee");

        }
    }
}
