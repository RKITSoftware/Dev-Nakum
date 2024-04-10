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
        #region Private Member
        private static List<Employees> _lstEmployees;
        #endregion

        #region Constrctor
        /// <summary>
        ///     Create static list
        /// </summary>
        static EmployeesController()
        {
            _lstEmployees = new List<Employees>
            {
                new Employees{ Id=1, Name="Dev", Age=21,Gender="Male", Designation="SDE"},
                new Employees{ Id=2, Name="Kishan", Age=26,Gender="Male", Designation="Manager"},
                new Employees{ Id=3, Name="Raj", Age=21,Gender="Male", Designation="SDE"},
                new Employees{ Id=4, Name="Alice", Age=20,Gender="Female", Designation="BDA"},
            };
        }
        #endregion

        #region GET: Employee

        /// <summary>
        ///     display all the data into list
        /// </summary>
        /// <returns>employee list</returns>
        [HttpGet]
        [Route("api/employees")]
        public IHttpActionResult Get()
        {
            return Ok(_lstEmployees);
            //return Request.CreateResponse(HttpStatusCode.OK, lstEmployees);
        }

        /// <summary>
        ///     Display employee's data based on specific id
        /// </summary>
        /// <param name="id">employee id</param>
        /// <returns>if valid id return employee data </returns>
        [HttpGet]
        [Route("api/employees/{id}")]
        public HttpResponseMessage GetEmployeeData(int id)
        {
            //to check id is exist or not in lstEmployees
            Employees result = _lstEmployees.FirstOrDefault(e => e.Id == id);
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
        /// <param name="objEmployee">object of employee's</param>
        /// <returns>status code with appropriate message</returns>
        [HttpPost]
        [Route("api/employees")]
        public HttpResponseMessage CreateEmployee(Employees objEmployee)
        {
            _lstEmployees.Add(objEmployee);
            return Request.CreateResponse(HttpStatusCode.Created, objEmployee);
        }
        #endregion

        #region PUT - Employee

        /// <summary>
        ///     update the employee based on id 
        /// </summary>
        /// <param name="id">employee's id</param>
        /// <param name="objEmployee">object of the employee</param>
        /// <returns>status code with appropriate message</returns>
        [HttpPut]
        [Route("api/employees/{id}")]
        public HttpResponseMessage UpdateEmployee(int id, Employees objEmployee)
        {
            //to check id is exist or not in lstEmployees
            Employees emp = _lstEmployees.FirstOrDefault(e => e.Id == id);
            if (emp != null)
            {
                emp.Name = objEmployee.Name;
                emp.Age = objEmployee.Age;
                emp.Designation = objEmployee.Designation;
                emp.Gender = objEmployee.Gender;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Employee is {id} is not found");
            }

            return Request.CreateResponse(HttpStatusCode.OK, objEmployee);
        }
        #endregion

        #region DELETE - Employee

        /// <summary>
        ///     Delete the employee based on id
        /// </summary>
        /// <param name="id">employee's id</param>
        /// <returns>status code with appropriate message</returns>
        [HttpDelete]
        [Route("api/employees/{id}")]
        public HttpResponseMessage DeleteEmployee(int id)
        {
            //to check id is exist or not in lstEmployees
            Employees emp = _lstEmployees.FirstOrDefault(e=>e.Id == id);
            if (emp != null)
            {
                _lstEmployees.Remove(emp);
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
