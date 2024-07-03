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
        /// <summary>
        /// DataTable to store employee data
        /// </summary>
        public DataTable dtEmployee;

        /// <summary>
        /// Constructor initializes the DataTable
        /// </summary
        public LinqWithDataTable()
        {
            dtEmployee = new DataTable();

            dtEmployee.Columns.Add("Id", typeof(int));
            dtEmployee.Columns.Add("Name", typeof(string));
            dtEmployee.Columns.Add("Age", typeof(int));
            dtEmployee.Columns.Add("Department", typeof(string));
            dtEmployee.Columns.Add("Salary", typeof(int));
        }

        /// <summary>
        /// Adds sample data to the DataTable.
        /// </summary>
        public void DataAdd()
        {
            dtEmployee.Rows.Add(1, "Dev", 21, "SDE", 50000);
            dtEmployee.Rows.Add(2, "Kishan", 28, "Manager", 80000);
            dtEmployee.Rows.Add(3, "Raj", 21, "SDE", 51000);
            dtEmployee.Rows.Add(4, "Tushar", 22, "ML-Engineer", 58000);
        }


        /// <summary>
        /// Find rows where the department is SDE and order by salary in descending order
        /// </summary>
        public void Filter()
        {
            // Query 1: Find rows where the department is SDE and order by salary in descending order
            var query1 = from employee in dtEmployee.AsEnumerable()
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
        }

        /// <summary>
        /// Find rows where the department is SDE and order by salary in descending order (using method syntax)
        /// </summary>
        public void FilterMethodSyntax()
        {
            // Query 2: Find rows where the department is SDE and order by salary in descending order (using method syntax)
            var query2 = dtEmployee.AsEnumerable()
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
        }

        /// <summary>
        /// Find the maximum salary
        /// </summary>
        /// <returns>Maximum salary</returns>
        public int MaximumSalary()
        {
            // Query 3: Find the maximum salary
            var query3 = dtEmployee.AsEnumerable()
                .Max(row => row.Field<int>("Salary"));
            return query3;
        }

        /// <summary>
        /// Query 4: Find details of employees with the maximum salary
        /// </summary>
        public void MaximumSalaryWithEmpDetails(int mxSalary)
        {
            //Query 4: Find details of employees with the maximum salary
            var query4 = dtEmployee.AsEnumerable()
                   .Where(row => row.Field<int>("Salary") == mxSalary)
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
        }

        /// <summary>
        /// Find the average salary of employees
        /// </summary>
        public void AverageSalary()
        {
            // Query 5: Find the average salary of employees
            var query5 = dtEmployee.AsEnumerable()
                        .Average(row => row.Field<int>("Salary"));
            Console.WriteLine($"Query 5: Average salary of employees is {query5}");
        }
        
        /// <summary>
        /// Executes various LINQ queries on the DataTable and prints the results.
        /// </summary>
        public void ExecuteQuery()
        {
            // Query 1: Find rows where the department is SDE and order by salary in descending order
            Filter();
            Console.WriteLine();


            // Query 2: Find rows where the department is SDE and order by salary in descending order (using method syntax)
            FilterMethodSyntax();
            Console.WriteLine();


            // Query 3: Find the maximum salary
            int mxSalary = MaximumSalary();
            Console.WriteLine($"Query 3: Maximum salary of employees is {mxSalary}\n");


            // Query 4: Find details of employees with the maximum salary
            MaximumSalaryWithEmpDetails(mxSalary);
            Console.WriteLine();


            // Query 5: Find the average salary of employees
            AverageSalary();
            Console.WriteLine();
        }
    }
}
