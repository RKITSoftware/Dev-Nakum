using System;
using System.Data;
using System.Linq;

namespace LINQ
{
    /// <summary>
    /// Demonstrates LINQ queries with a DataTable representing employee data.
    /// </summary>
    public class LinqWithDataTable
    {
        // DataTable to store employee data
        public Employees objEmployees;

        // Constructor initializes the DataTable
        public LinqWithDataTable()
        {
            objEmployees = new Employees();
        }

        /// <summary>
        /// Adds sample data to the DataTable.
        /// </summary>
        public void DataAdd()
        {
            objEmployees.Rows.Add(1, "Dev", 21, "SDE", 50000);
            objEmployees.Rows.Add(2, "Kishan", 28, "Manager", 80000);
            objEmployees.Rows.Add(3, "Raj", 21, "SDE", 51000);
            objEmployees.Rows.Add(4, "Tushar", 22, "ML-Engineer", 58000);
        }

        /// <summary>
        /// Executes various LINQ queries on the DataTable and prints the results.
        /// </summary>
        public void ExecuteQuery()
        {
            // Query 1: Find rows where the department is SDE and order by salary in descending order
            var query1 = from employee in objEmployees.AsEnumerable()
                         where employee.Field<string>("Department") == "SDE"
                         orderby employee.Field<int>("Salary") descending
                         select new
                         {
                             Id = employee.Field<int>("Id"),
                             Name = employee.Field<string>("Name"),
                             Age = employee.Field<int>("Age"),
                             Department = employee.Field<string>("Department"),
                             Salary = employee.Field<int>("Salary"),
                         };

            // Print the result of Query 1
            Console.WriteLine("Query 1:");
            foreach (var item in query1)
            {
                Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Age: {item.Age}, Department: {item.Department}, Salary: {item.Salary}");
            }
            Console.WriteLine();

            // Query 2: Find rows where the department is SDE and order by salary in descending order (using method syntax)
            var query2 = objEmployees.AsEnumerable()
                        .Where(row => row.Field<string>("Department").Equals("SDE"))
                        .OrderByDescending(row => row.Field<int>("Salary"))
                        .Select(row => new
                        {
                            Id = row.Field<int>("Id"),
                            Name = row.Field<string>("Name"),
                            Age = row.Field<int>("Age"),
                            Department = row.Field<string>("Department"),
                            Salary = row.Field<int>("Salary")
                        }).ToList();

            // Print the result of Query 2
            Console.WriteLine("Query 2:");
            foreach (var item in query2)
            {
                Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Age: {item.Age}, Department: {item.Department}, Salary: {item.Salary}");
            }
            Console.WriteLine();

            // Query 3: Find the maximum salary
            var query3 = objEmployees.AsEnumerable()
                .Max(row => row.Field<int>("Salary"));
            Console.WriteLine($"Query 3: Maximum salary of employees is {query3}");
            Console.WriteLine();

            // Query 4: Find details of employees with the maximum salary
            var query4 = objEmployees.AsEnumerable()
                   .Where(row => row.Field<int>("Salary") == query3)
                   .Select(row => new
                   {
                       Id = row.Field<int>("Id"),
                       Name = row.Field<string>("Name"),
                       Age = row.Field<int>("Age"),
                       Department = row.Field<string>("Department"),
                       Salary = row.Field<int>("Salary")
                   }).ToList();

            // Print the result of Query 4
            Console.WriteLine("Query 4:");
            foreach (var item in query4)
            {
                Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Age: {item.Age}, Department: {item.Department}, Salary: {item.Salary}");
            }
            Console.WriteLine();

            // Query 5: Find the average salary of employees
            var query5 = objEmployees.AsEnumerable()
                        .Average(row => row.Field<int>("Salary"));
            Console.WriteLine($"Query 5: Average salary of employees is {query5}");
            Console.WriteLine();
        }
    }
}
