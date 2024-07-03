using System;
using System.Data;

namespace DataTables
{
    /// <summary>
    /// Create the data table 
    /// add rows
    /// delete the row
    /// update the row
    /// </summary>
    class Program
    {
        /// <summary>
        /// display all the rows from data table
        /// </summary>
        /// <param name="dataTable"></param>
        public static void DisplayData(DataTable dataTable)
        {
            // Iterating the data Table
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Console.WriteLine($"Id : {dataRow["Id"]}");
                Console.WriteLine($"Name : {dataRow["Name"]}");
                Console.WriteLine($"Age : {dataRow["Age"]}");
                Console.WriteLine($"Gender : {dataRow["Gender"]}");

                Console.WriteLine();
            }
        }
        /// <summary>
        /// Main method  for create the table, add columns, rows and update, delete 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // create the instance of dataTable
            DataTable dataTable = new DataTable();

            // create the instance of dataColumn
            DataColumn columnId = new DataColumn("Id", typeof(int)); 
            DataColumn columnName = new DataColumn("Name", typeof(string)); 
            DataColumn columnAge = new DataColumn("Age", typeof(int)); 
            DataColumn columnGender = new DataColumn("Gender", typeof(string)); 
                
            // Add column instance to Data Table
            dataTable.Columns.Add(columnId);
            dataTable.Columns.Add(columnName);
            dataTable.Columns.Add(columnAge);
            dataTable.Columns.Add(columnGender);

            // add primary key
            DataColumn[] primaryKey = { dataTable.Columns["Id"] };
            dataTable.PrimaryKey = primaryKey;

            // create the instance of dataRow and assign the data into row
            DataRow row = dataTable.NewRow();
            row["Id"] = 1;
            row["Name"] = "Dev";
            row["Age"] = 21;
            row["Gender"] = "Male";
            dataTable.Rows.Add(row);

            row = dataTable.NewRow();
            row["Id"] = 2;
            row["Name"] = "Kishan";
            row["Age"] = 28;
            row["Gender"] = "Male";
            dataTable.Rows.Add(row);

            row = dataTable.NewRow();   
            row["Id"] = 3;
            row["Name"] = "Alice";
            row["Age"] = 20;
            row["Gender"] = "Female";
            dataTable.Rows.Add(row);

            // Display the data 
            DisplayData(dataTable);

            // modify the data
            DataRow modifyRow = dataTable.Rows.Find(3);
            if( modifyRow != null )
            {
                modifyRow["Age"] = 21;
            }

            Console.WriteLine("Update the row data");
            // Display the data 
            DisplayData(dataTable);

            // find the row which Id is equal to 3
            DataRow deleteRow = dataTable.Rows.Find(3);
            if( deleteRow != null )
            {
                deleteRow.Delete();             // delete the row
                dataTable.AcceptChanges();          // commit the changes
            }

            Console.WriteLine("Delete the row 3");
            // Display the data 
            DisplayData(dataTable);
            Console.ReadLine();
        }
    }
}
