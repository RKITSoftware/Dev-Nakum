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
        private Users _objUsers;
        #endregion

        public BLBank()
        {
            _objUsers = new Users();
        }

        #region Public Method

        /// <summary>
        /// Takes user input to create a new bank account and calls the createAccount method.
        /// </summary>
        /// <param name="dataTable">DataTable to store user data.</param>
        public void GetInputCreateAccount(DataTable dataTable)
        {
            Console.WriteLine("Account creating processing is start...");
            
            Console.WriteLine("Enter your first name");
            _objUsers.FirstName = Console.ReadLine();

            Console.WriteLine("Enter your last name");
            _objUsers.LastName = Console.ReadLine();

            Console.WriteLine("Enter your email");
            _objUsers.Email = Console.ReadLine();

            Console.WriteLine("Enter your phone number");
            _objUsers.Phone = Convert.ToDouble(Console.ReadLine());

            if ((int.TryParse(_objUsers.FirstName, out int res)) || (int.TryParse(_objUsers.LastName, out int res2)))
            {
                Console.WriteLine("Enter the valid First Name or Last Name");
                return;
            }
            if ((_objUsers.Phone).ToString().Length != 10)
            {
                Console.WriteLine("Phone number is not valid, Please try again !!!");
                return;
            }

            CreateAccount(dataTable);
        }

        /// <summary>
        /// Takes user input for the deposit amount and calls the `TransactionAmount` method.
        /// </summary>
        /// <param name="dataTable">DataTable to store user data.</param>
        public void GetInputDepositMoney(DataTable dataTable)
        {
            Console.WriteLine("Enter the amount");
            int money = Convert.ToInt32(Console.ReadLine());

            TransactionAmount(dataTable, money, "Deposit");
        }

        /// <summary>
        /// Takes user input for the withdrawal amount and calls the `TransactionAmount` method.
        /// </summary>
        /// <param name="dataTable">DataTable to store user data.</param>
        public void GetInputWithdrawMoney(DataTable dataTable)
        {
            Console.WriteLine("Enter the amount");
            int money = Convert.ToInt32(Console.ReadLine());
            TransactionAmount(dataTable, money,"Withdraw");  
        }
       
        /// <summary>
        /// Based on user choice, calls the appropriate function for banking operations.
        /// </summary>
        /// <param name="choice">User choice for banking operation.</param>
        /// <param name="dataTable">DataTable to store user data.</param>
        public void selectChoice(string choice, DataTable dataTable)
        {

            switch (choice)
            {
                case "1":
                    GetInputCreateAccount(dataTable);
                    break;
                case "2":
                    GetInputWithdrawMoney(dataTable);
                    break;
                case "3":
                    GetInputDepositMoney(dataTable);
                    break;
                case "4":
                    CloseAccount(dataTable);
                    break;
                default:
                    Console.WriteLine("Enter the valid choice");
                    break;
            }
        }

        /// <summary>
        /// Create a new bank account and add it to the DataTable.
        /// </summary>
        /// <param name="dataTable">DataTable to store user data.</param>
        public void CreateAccount(DataTable dataTable)
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
            _objUsers.UserId = _id;
            row["UserId"] = _id++;
            row["FirstName"] = _objUsers.FirstName;
            row["LastName"] = _objUsers.LastName;
            row["Email"] = _objUsers.Email;
            row["Phone"] = _objUsers.Phone;
            row["Money"] = 0;       // Initial account balance is set to 0

            // Add the DataRow to the DataTable
            dataTable.Rows.Add(row);

            Console.WriteLine("Your account created successfully");

            // Display the current user data
            DisplayCurrentUserData(dataTable, _objUsers.UserId);
        }

        /// <summary>
        /// Perform transaction based on type
        /// </summary>
        /// <param name="dataTable">DataTable to store user data.</param>
        /// <param name="money">transaction amount</param>
        /// <param name="type">Deposit or Withdraw</param>
        public void TransactionAmount(DataTable dataTable, int money,string type)
        {
            if (!ColumnExists(dataTable, "FirstName"))
            {
                Console.WriteLine("Account does not exists");
                return;
            }

            // Find the DataRow based on UserId
            DataRow currentUser = dataTable.Rows.Find(_objUsers.UserId);
            if (currentUser != null)
            {
                // perform deposit - add money 
                if (type == "Deposit")
                {
                    Console.WriteLine("Money Started Deposit");
                    // Update the money balance and display the current user data
                    money += (int)currentUser["Money"];
                    currentUser["Money"] = money;  
                }
                else   // perform withdraw - remove money
                {
                    // if original money is not more then withdraw money
                    if ((int)currentUser["Money"] < money)
                    {
                        Console.WriteLine("Insufficient Balance");
                    }
                    else
                    {
                        Console.WriteLine("Money Started Withdrawing");
                        currentUser["Money"] = (int)currentUser["Money"] - money;
                    }
                }

                // display current user's data
                DisplayCurrentUserData(dataTable, _objUsers.UserId);
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
            if (!ColumnExists(dataTable, "FirstName"))
            {
                Console.WriteLine("Account does not exists");
                return;
            }
            // Find the DataRow based on UserId
            DataRow currentUser = dataTable.Rows.Find(_objUsers.UserId);
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
        public  bool ColumnExists(DataTable dataTable, string columnName)
        {
            return dataTable.Columns.Contains(columnName);
        }

        /// <summary>
        /// Display the current user's data.
        /// </summary>
        /// <param name="dataTable">DataTable to store user data.</param>
        /// <param name="id">UserId of the current user.</param>
        public void DisplayCurrentUserData(DataTable dataTable, int id)
        {
            // Find the DataRow based on UserId
            DataRow currentUser = dataTable.Rows.Find(id);
            Console.WriteLine();
            Console.WriteLine("*** Current user data ***");
            DisplayUserById(currentUser);
        }

        /// <summary>
        /// Display user's data based on data row
        /// </summary>
        /// <param name="currentUser">data row of current user's data</param>
        public void DisplayUserById(DataRow currentUser)
        {
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
        public void DisplayAllUserData(DataTable dataTable)
        {
            // Iterating through the DataTable to display user data
            foreach (DataRow dataRow in dataTable.Rows)
            {
                DisplayUserById(dataRow);
            }

            // Write user data to a file
            WriteDataIntoFile(dataTable);
        }

        /// <summary>
        /// Write all user data in the DataTable to a file.
        /// </summary>
        /// <param name="dataTable">DataTable to store user data.</param>
        public void WriteDataIntoFile(DataTable dataTable)
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
                Console.WriteLine($"Successfully wrote into the file at {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        #endregion
    }
}
