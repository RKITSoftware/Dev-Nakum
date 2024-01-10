using System;
using System.Data;

namespace Bank_Management_System
{
    public class Bank
    {
        public int id = 1;
        public int UserId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

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
            Console.WriteLine("Money Started Deposit");

            DataRow currentUser = dataTable.Rows.Find(UserId);
            if (currentUser != null)
            {
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
        /// find the datarow based on userid 
        /// update the money - decrease money
        /// display current user data
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="money"></param>
        public void WithdrawMoney(DataTable dataTable, int money)
        {
            Console.WriteLine("Money Started Withdraw");
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
        }
    }


    internal class Program
    {
        /// <summary>
        ///     Take input from user
        ///     call the createAccount method
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="objBank"></param>
        public static void CreateAccount(DataTable dataTable, Bank objBank)
        {
            Console.WriteLine("Account creating processing is strat...");

            Console.WriteLine("Enter your first name");
            objBank.FirstName = Console.ReadLine();

            Console.WriteLine("Enter your last name");
            objBank.LastName = Console.ReadLine();

            Console.WriteLine("Enter your email");
            objBank.Email = Console.ReadLine();

            Console.WriteLine("Enter your phone number");
            objBank.Phone = Console.ReadLine();

            objBank.CreateAccount(dataTable, objBank.FirstName, objBank.LastName, objBank.Email, objBank.Phone);
        }

        /// <summary>
        ///     Take input money from user
        ///     call the depositMoney method
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="objBank"></param>
        public static void DepositMoney(DataTable dataTable, Bank objBank)
        {
            Console.WriteLine("Enter the amount");
            int money = Convert.ToInt32(Console.ReadLine());

            objBank.DepositMoney(dataTable, money);
        }

        /// <summary>
        ///     Take input money from user
        ///     call the withdrawMoney method
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="objBank"></param>
        public static void WithdrawMoney(DataTable dataTable, Bank objBank)
        {
            Console.WriteLine("Enter the amount");
            int money = Convert.ToInt32(Console.ReadLine());

            objBank.WithdrawMoney(dataTable, money);
        }

        /// <summary>
        ///  Call the closeAccount Method
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="objBank"></param>
        public static void CloseAccount(DataTable dataTable, Bank objBank)
        {
            objBank.CloseAccount(dataTable);
        }
       
        /// <summary>
        ///     based on user choice call the appropriate function
        /// </summary>
        /// <param name="choice"></param>
        /// <param name="dataTable"></param>
        /// <param name="objBank"></param>
        public static void selectChoice(int choice, DataTable dataTable, Bank objBank)
        {

            switch (choice)
            {
                case 1:
                    CreateAccount(dataTable, objBank);
                    break;
                case 2:
                    WithdrawMoney(dataTable, objBank);
                    break;
                case 3:
                    DepositMoney(dataTable, objBank);
                    break;
                case 4:
                    CloseAccount(dataTable, objBank);
                    break;
            }
        }
     
        public static void Main(string[] args)
        {
            DataTable dataTable = new DataTable("UserData");
            Bank objBank = new Bank();
            while (true)
            {
                Console.WriteLine("Bank Management System");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Withdraw Money");
                Console.WriteLine("3. Deposit Money");
                Console.WriteLine("4. Close Account");
                Console.WriteLine("5. Exit Bank");

                int choice = Convert.ToInt32(Console.ReadLine());

                // if user enter the 5 break the while loop 
                // display allUserDataTable from dataTable 
                if (choice == 5)
                {
                    Bank.DisplayAllUserData(dataTable);
                    break;
                }
                selectChoice(choice, dataTable, objBank);
            }

        }
    }
}
