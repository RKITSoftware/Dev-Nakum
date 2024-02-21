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
                    objBank.DisplayAllUserData(dataTable);
                    break;
                }

                // Call the appropriate method based on user choice
                objBank.selectChoice(choice, dataTable);
            }

        }
    }
}
