using System;
using System.Data;

namespace Bank_Management_System
{
    /// <summary>
    /// Main entry point for the Bank Management System.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Takes user input to create a new bank account and calls the createAccount method.
        /// </summary>
        /// <param name="dataTable">DataTable to store user data.</param>
        /// <param name="objBank">Business logic object for banking operations.</param>
        public static void CreateAccount(DataTable dataTable, BLBank objBank)
        {
            Console.WriteLine("Account creating processing is start...");

            Console.WriteLine("Enter your first name");
            objBank.FirstName = Console.ReadLine();

            Console.WriteLine("Enter your last name");
            objBank.LastName = Console.ReadLine();

            Console.WriteLine("Enter your email");
            objBank.Email = Console.ReadLine();

            Console.WriteLine("Enter your phone number");
            objBank.Phone = Console.ReadLine();

            Console.WriteLine((int.TryParse(objBank.Phone, out int r)));
            if ((int.TryParse(objBank.FirstName, out int res)) || (int.TryParse(objBank.LastName, out int res2)))
            {
                Console.WriteLine("Enter the valid First Name or Last Name");
                return;
            }
            if (!(double.TryParse(objBank.Phone, out double result)) || (objBank.Phone).Length != 10)
            {
                Console.WriteLine("Phone number is not valid, Please try again !!!");
                return;
            }
            objBank.CreateAccount(dataTable, objBank.FirstName, objBank.LastName, objBank.Email, objBank.Phone);
        }

        /// <summary>
        /// Takes user input for the deposit amount and calls the depositMoney method.
        /// </summary>
        /// <param name="dataTable">DataTable to store user data.</param>
        /// <param name="objBank">Business logic object for banking operations.</param>
        public static void DepositMoney(DataTable dataTable, BLBank objBank)
        {
            Console.WriteLine("Enter the amount");
            string moneyString = Console.ReadLine();
            if (!(int.TryParse(moneyString, out int result)))
            {
                Console.WriteLine("Amount is not valid, Please try again !!!");
                return;
            }
            else
            {
                int money = Convert.ToInt32(moneyString);
                objBank.DepositMoney(dataTable, money);
            }
        }

        /// <summary>
        /// Takes user input for the withdrawal amount and calls the withdrawMoney method.
        /// </summary>
        /// <param name="dataTable">DataTable to store user data.</param>
        /// <param name="objBank">Business logic object for banking operations.</param>
        public static void WithdrawMoney(DataTable dataTable, BLBank objBank)
        {
            Console.WriteLine("Enter the amount");
            string moneyString = Console.ReadLine();
            if (!(int.TryParse(moneyString, out int result)))
            {
                Console.WriteLine("Amount is not valid, Please try again !!!");
                return;
            }
            else
            {
                int money = Convert.ToInt32(moneyString);
                objBank.WithdrawMoney(dataTable, money);
            }
        }

        /// <summary>
        /// Calls the closeAccount method to close a user account.
        /// </summary>
        /// <param name="dataTable">DataTable to store user data.</param>
        /// <param name="objBank">Business logic object for banking operations.</param>
        public static void CloseAccount(DataTable dataTable, BLBank objBank)
        {
            objBank.CloseAccount(dataTable);
        }

        /// <summary>
        /// Based on user choice, calls the appropriate function for banking operations.
        /// </summary>
        /// <param name="choice">User choice for banking operation.</param>
        /// <param name="dataTable">DataTable to store user data.</param>
        /// <param name="objBank">Business logic object for banking operations.</param>
        public static void selectChoice(string choice, DataTable dataTable, BLBank objBank)
        {

            switch (choice)
            {
                case "1":
                    CreateAccount(dataTable, objBank);
                    break;
                case "2":
                    WithdrawMoney(dataTable, objBank);
                    break;
                case "3":
                    DepositMoney(dataTable, objBank);
                    break;
                case "4":
                    CloseAccount(dataTable, objBank);
                    break;
                default:
                    Console.WriteLine("Enter the valid choice");
                    break;
            }
        }

        /// <summary>
        /// Main entry point for the Bank Management System.
        /// </summary>
        public static void Main(string[] args)
        {
            // DataTable to store user data
            DataTable dataTable = new DataTable("UserData");

            // Business logic for banking operations
            BLBank objBank = new BLBank();

            // Main program loop
            while (true)
            {
                Console.WriteLine("Bank Management System");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Withdraw Money");
                Console.WriteLine("3. Deposit Money");
                Console.WriteLine("4. Close Account");
                Console.WriteLine("5. Exit Bank");

                // Get user choice
                string choice = Console.ReadLine();

                // if user enter the 5 break the while loop and display allUserDataTable from dataTable 
                if (choice == "5")
                {
                    BLBank.DisplayAllUserData(dataTable);
                    break;
                }

                // Call the appropriate method based on user choice
                selectChoice(choice, dataTable, objBank);
            }

        }
    }
}
