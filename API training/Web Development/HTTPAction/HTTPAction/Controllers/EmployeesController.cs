using HTTPAction.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace HTTPAction.Controllers
{
    /// <summary>
    ///     It is a class which can manage all operation over employeeList
    /// </summary>
    public class EmployeesController : ApiController
    {
        #region Public Member
        public static List<Employee> lstEmployees;
        #endregion

        #region Constrctor
        /// <summary>
        ///     Create static list
        /// </summary>
        static EmployeesController()
        {
            lstEmployees = new List<Employee>
            {
                new Employee{ Id=1, Name="Dev", Age=21,Gender="Male", Designation="SDE"},
                new Employee{ Id=2, Name="Kishan", Age=26,Gender="Male", Designation="Manager"},
                new Employee{ Id=3, Name="Raj", Age=21,Gender="Male", Designation="SDE"},
                new Employee{ Id=4, Name="Alice", Age=20,Gender="Female", Designation="BDA"},
            };
        }
        #endregion

        #region GET: Employee

        /// <summary>
        ///     display all the data into list
        /// </summary>
        /// <returns>employee list</returns>
        [HttpGet]
        [Route("api/employee")]
        public IHttpActionResult Get()
        {
            return Ok(lstEmployees);
            //return Request.CreateResponse(HttpStatusCode.OK, lstEmployees);
        }

        /// <summary>
        ///     Display employee's data based on specific id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>if valid id return employee data </returns>
        [HttpGet]
        [Route("api/employee/{id}")]
        public HttpResponseMessage GetEmployeeData(int id)
        {
            //to check id is exist or not in lstEmployees
            Employee result = lstEmployees.FirstOrDefault(e => e.Id == id);
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Employee is {id} is not found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #endregion

        #region POST - Employee

        /// <summary>
        ///  Create the employee based on request body
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>status code with appropriate message</returns>
        [HttpPost]
        [Route("api/employee")]
        public HttpResponseMessage CreateEmployee(Employee employee)
        {
            lstEmployees.Add(employee);
            return Request.CreateResponse(HttpStatusCode.Created, employee);
        }
        #endregion

        #region PUT - Employee

        /// <summary>
        ///     update the employee based on id 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns>status code with appropriate message</returns>
        [HttpPut]
        [Route("api/employee/{id}")]
        public HttpResponseMessage UpdateEmployee(int id, Employee employee)
        {
            //to check id is exist or not in lstEmployees
            Employee emp = lstEmployees.FirstOrDefault(e => e.Id == id);
            if (emp != null)
            {
                emp.Name = employee.Name;
                emp.Age = employee.Age;
                emp.Designation = employee.Designation;
                emp.Gender = employee.Gender;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Employee is {id} is not found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, employee);
        }
        #endregion

        #region DELETE - Employee

        /// <summary>
        ///     Delete the employee based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>status code with appropriate message</returns>
        [HttpDelete]
        [Route("api/employee/{id}")]
        public HttpResponseMessage DeleteEmployee(int id)
        {
            //to check id is exist or not in lstEmployees
            Employee emp = lstEmployees.FirstOrDefault(e=>e.Id == id);
            if (emp != null)
            {
                lstEmployees.Remove(emp);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Employee is {id} is not found");
            }
            return Request.CreateResponse(HttpStatusCode.OK,"Successfully deleted employee");
        }

        #endregion
    }
}
