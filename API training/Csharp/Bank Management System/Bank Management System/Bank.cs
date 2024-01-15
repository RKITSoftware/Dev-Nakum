using System;
using System.Data;
using System.IO;

namespace Bank_Management_System
{
    /// <summary>
    ///     Perform all the operation 
    ///     create account,
    ///     deposit money,
    ///     withdraw money,
    ///     close account
    /// </summary>
    public class Bank
    {
        #region Public Member
        private int id = 1;
        #endregion

        #region Public Properties
        public int UserId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
        #endregion

        #region Public Method
        /// <summary>
        ///     Create the data column if the data table is not exist and add the rows
        ///     call display current user data function
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        public void CreateAccount(DataTable dataTable, string firstName, string lastName, string email, string phone)
        {
            if (!ColumnExists(dataTable, "FirstName"))
            {
                DataColumn columnUserId = new DataColumn("UserId", typeof(int));
                DataColumn columnFName = new DataColumn("FirstName", typeof(string));
                DataColumn columnLName = new DataColumn("LastName", typeof(string));
                DataColumn columnEmail = new DataColumn("Email", typeof(string));
                DataColumn columnPhone = new DataColumn("Phone", typeof(string));
                DataColumn columnMoney = new DataColumn("Money", typeof(int));

                dataTable.Columns.Add(columnUserId);
                dataTable.Columns.Add(columnFName);
                dataTable.Columns.Add(columnLName);
                dataTable.Columns.Add(columnEmail);
                dataTable.Columns.Add(columnPhone);
                dataTable.Columns.Add(columnMoney);

                DataColumn[] assignPrimaryKey = { dataTable.Columns["UserId"] };
                dataTable.PrimaryKey = assignPrimaryKey;
            }


            DataRow row = dataTable.NewRow();
            UserId = id;
            row["UserId"] = id++;
            row["FirstName"] = firstName;
            row["LastName"] = lastName;
            row["Email"] = email;
            row["Phone"] = phone;
            row["Money"] = 0;

            dataTable.Rows.Add(row);

            Console.WriteLine("Your account created sucessfully");

            DisplayCurrentUserData(dataTable, UserId);
        }

        /// <summary>
        /// find the datarow based on userid 
        /// update the money - add money
        /// display current user data
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="money"></param>
        public void DepositMoney(DataTable dataTable, int money)
        {
            try
            {
                DataRow currentUser = dataTable.Rows.Find(UserId);
                if (currentUser != null)
                {
                    Console.WriteLine("Money Started Deposit");
                    money += (int)currentUser["Money"];

                    currentUser["Money"] = money;
                    DisplayCurrentUserData(dataTable, UserId);
                }
                else
                {
                    Console.WriteLine("Account does not exists");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong");
            }
        }

        /// <summary>
        /// find the datarow based on userid 
        /// update the money - decrease money
        /// display current user data
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="money"></param>
        public void WithdrawMoney(DataTable dataTable, int money)
        {

            DataRow currentUser = dataTable.Rows.Find(UserId);
            if (currentUser != null)
            {
                if ((int)currentUser["Money"] < money)
                {
                    Console.WriteLine("Insufficient Balance");
                }
                else
                {
                    currentUser["Money"] = (int)currentUser["Money"] - money;
                    Console.WriteLine("Money Started Withdraw");
                    DisplayCurrentUserData(dataTable, UserId);
                }
            }
            else
            {
                Console.WriteLine("Account does not exists");
            }
        }

        /// <summary>
        ///     find the datarow based on usedid
        ///     delete the row
        /// </summary>
        /// <param name="dataTable"></param>
        public void CloseAccount(DataTable dataTable)
        {
            DataRow currentUser = dataTable.Rows.Find(UserId);
            if (currentUser != null)
            {
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
        /// check whether data-base is exist or not
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="columnName"></param>
        /// <returns>bool</returns>
        public static bool ColumnExists(DataTable dataTable, string columnName)
        {
            return dataTable.Columns.Contains(columnName);
        }

        /// <summary>
        /// Display current user data
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="id"></param>
        public static void DisplayCurrentUserData(DataTable dataTable, int id)
        {
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
        /// Display all user data
        /// </summary>
        /// <param name="dataTable"></param>
        public static void DisplayAllUserData(DataTable dataTable)
        {
            // Iterating the data Table
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

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\display.txt");
            try
            {
                using (StreamWriter objSreamWriter = new StreamWriter(filePath))
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        objSreamWriter.WriteLine();
                        objSreamWriter.WriteLine($"UserId : {dataRow["UserId"]}");
                        objSreamWriter.WriteLine($"FirstName : {dataRow["FirstName"]}");
                        objSreamWriter.WriteLine($"LastName : {dataRow["LastName"]}");
                        objSreamWriter.WriteLine($"Email : {dataRow["Email"]}");
                        objSreamWriter.WriteLine($"Phone : {dataRow["Phone"]}");
                        objSreamWriter.WriteLine($"Money : {dataRow["Money"]}");
                        objSreamWriter.WriteLine();
                    }

                    Console.WriteLine($"You can view receipt at {filePath}");
                }

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("File not found !!!");
            }
        }
        #endregion
    }


}
