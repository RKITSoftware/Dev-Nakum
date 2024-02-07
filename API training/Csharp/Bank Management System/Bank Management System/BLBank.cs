using System;
using System.Data;
using System.IO;

namespace Bank_Management_System
{
    /// <summary>
    /// Perform all the operation of bank related
    /// </summary>
    public class BLBank
    {
        #region Private Member
        private static int _id = 1;
        #endregion

        #region Public Properties
        // Public properties for user details
        public int id = 1;
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        #endregion

        #region Public Method

        /// <summary>
        /// Create a new bank account and add it to the DataTable.
        /// </summary>
        /// <param name="dataTable">DataTable to store user data.</param>
        /// <param name="firstName">First name of the account holder.</param>
        /// <param name="lastName">Last name of the account holder.</param>
        /// <param name="email">Email of the account holder.</param>
        /// <param name="phone">Phone number of the account holder.</param>
        public void CreateAccount(DataTable dataTable, string firstName, string lastName, string email, string phone)
        {
            // If DataTable columns don't exist, create them
            if (!ColumnExists(dataTable, "FirstName"))
            {
                // Define data columns
                DataColumn columnUserId = new DataColumn("UserId", typeof(int));
                DataColumn columnFName = new DataColumn("FirstName", typeof(string));
                DataColumn columnLName = new DataColumn("LastName", typeof(string));
                DataColumn columnEmail = new DataColumn("Email", typeof(string));
                DataColumn columnPhone = new DataColumn("Phone", typeof(string));
                DataColumn columnMoney = new DataColumn("Money", typeof(int));

                // Add columns to DataTable
                dataTable.Columns.Add(columnUserId);
                dataTable.Columns.Add(columnFName);
                dataTable.Columns.Add(columnLName);
                dataTable.Columns.Add(columnEmail);
                dataTable.Columns.Add(columnPhone);
                dataTable.Columns.Add(columnMoney);

                // Set UserId as the primary key
                DataColumn[] assignPrimaryKey = { dataTable.Columns["UserId"] };
                dataTable.PrimaryKey = assignPrimaryKey;
            }

            // Create a new DataRow and populate it with user details
            DataRow row = dataTable.NewRow();
            UserId = _id;
            row["UserId"] = _id++;
            row["FirstName"] = firstName;
            row["LastName"] = lastName;
            row["Email"] = email;
            row["Phone"] = phone;
            row["Money"] = 0;       // Initial account balance is set to 0

            // Add the DataRow to the DataTable
            dataTable.Rows.Add(row);

            Console.WriteLine("Your account created successfully");

            // Display the current user data
            DisplayCurrentUserData(dataTable, UserId);
        }

        /// <summary>
        /// Deposit money into the user's account.
        /// </summary>
        /// <param name="dataTable">DataTable to store user data.</param>
        /// <param name="money">Amount of money to deposit.</param>
        public void DepositMoney(DataTable dataTable, int money)
        {
            Console.WriteLine("Money Started Deposit");

            // Find the DataRow based on UserId
            DataRow currentUser = dataTable.Rows.Find(UserId);
            if (currentUser != null)
            {
                // Update the money balance and display the current user data
                money += (int)currentUser["Money"];

                currentUser["Money"] = money;
                DisplayCurrentUserData(dataTable, UserId);
            }
            else
            {
                Console.WriteLine("Account does not exists");
            }
        }

        /// <summary>
        /// Withdraw money from the user's account.
        /// </summary>
        /// <param name="dataTable">DataTable to store user data.</param>
        /// <param name="money">Amount of money to withdraw.</param>
        public void WithdrawMoney(DataTable dataTable, int money)
        {
            Console.WriteLine("Money Started Withdraw");
            // Find the DataRow based on UserId
            DataRow currentUser = dataTable.Rows.Find(UserId);
            if (currentUser != null)
            {
                // Check if sufficient balance exists, update the money balance, and display the current user data
                if ((int)currentUser["Money"] < money)
                {
                    Console.WriteLine("Insufficient Balance");
                }
                else
                {
                    currentUser["Money"] = (int)currentUser["Money"] - money;
                    DisplayCurrentUserData(dataTable, UserId);
                }
            }
            else
            {
                Console.WriteLine("Account does not exists");
            }
        }

        /// <summary>
        /// Close the user's account by deleting the DataRow from the DataTable.
        /// </summary>
        /// <param name="dataTable">DataTable to store user data.</param>
        public void CloseAccount(DataTable dataTable)
        {
            // Find the DataRow based on UserId
            DataRow currentUser = dataTable.Rows.Find(UserId);
            if (currentUser != null)
            {
                // Delete the DataRow and accept changes to the DataTable
                currentUser.Delete();
                dataTable.AcceptChanges();

                Console.WriteLine();
                Console.WriteLine("your account is successfully deleted");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Account does not exists");
            }

        }

        /// <summary>
        /// Check if a column exists in the DataTable.
        /// </summary>
        /// <param name="dataTable">DataTable to check.</param>
        /// <param name="columnName">Name of the column to check.</param>
        /// <returns>True if the column exists, otherwise false.</returns>
        public static bool ColumnExists(DataTable dataTable, string columnName)
        {
            return dataTable.Columns.Contains(columnName);
        }

        /// <summary>
        /// Display the current user's data.
        /// </summary>
        /// <param name="dataTable">DataTable to store user data.</param>
        /// <param name="id">UserId of the current user.</param>
        public static void DisplayCurrentUserData(DataTable dataTable, int id)
        {
            // Find the DataRow based on UserId
            DataRow currentUser = dataTable.Rows.Find(id);
            Console.WriteLine();
            Console.WriteLine("*** Current user data ***");
            Console.WriteLine();
            Console.WriteLine($"UserId : {currentUser["UserId"]}");
            Console.WriteLine($"FirstName : {currentUser["FirstName"]}");
            Console.WriteLine($"LastName : {currentUser["LastName"]}");
            Console.WriteLine($"Email : {currentUser["Email"]}");
            Console.WriteLine($"Phone : {currentUser["Phone"]}");
            Console.WriteLine($"Money : {currentUser["Money"]}");
            Console.WriteLine();
        }

        /// <summary>
        /// Display all user data in the DataTable and write it to a file.
        /// </summary>
        /// <param name="dataTable">DataTable to store user data.</param>
        public static void DisplayAllUserData(DataTable dataTable)
        {
            // Iterating through the DataTable to display user data
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Console.WriteLine();
                Console.WriteLine($"UserId : {dataRow["UserId"]}");
                Console.WriteLine($"FirstName : {dataRow["FirstName"]}");
                Console.WriteLine($"LastName : {dataRow["LastName"]}");
                Console.WriteLine($"Email : {dataRow["Email"]}");
                Console.WriteLine($"Phone : {dataRow["Phone"]}");
                Console.WriteLine($"Money : {dataRow["Money"]}");
                Console.WriteLine();
            }

            // Write user data to a file
            WriteDataIntoFile(dataTable);
        }

        /// <summary>
        /// Write all user data in the DataTable to a file.
        /// </summary>
        /// <param name="dataTable">DataTable to store user data.</param>
        public static void WriteDataIntoFile(DataTable dataTable)
        {
            // Define the file path for writing data
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\display.txt");

            // Write user data to the file
            try
            {
                // streamWrite for write something on file
               using (StreamWriter streamWriter = new StreamWriter(filePath))
               {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        streamWriter.WriteLine();
                        streamWriter.WriteLine($"UserId : {dataRow["UserId"]}");
                        streamWriter.WriteLine($"FirstName : {dataRow["FirstName"]}");
                        streamWriter.WriteLine($"LastName : {dataRow["LastName"]}");
                        streamWriter.WriteLine($"Email : {dataRow["Email"]}");
                        streamWriter.WriteLine($"Phone : {dataRow["Phone"]}");
                        streamWriter.WriteLine($"Money : {dataRow["Money"]}");
                        streamWriter.WriteLine();
                    }
               }
                Console.WriteLine("Successfully wrote into the file");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        #endregion
    }
}
