using System;
using System.Collections.Generic;


namespace ExtensionMethods
{
    /// <summary>
    /// Main program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method, the entry point of the program.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Create a list of strings
            List<string> lstString = new List<string>() { "A", "B", "C", "D" };

            // Use the PrintList extension method to print the elements of the list
            lstString.PrintList();
            Console.WriteLine();

            // Use the ConvertCommaSeparatedString extension method to convert the list to a string
            Console.WriteLine(lstString.ConvertCommaSeparatedString());

            // Create a list of integers
            List<int> lstInt = new List<int> { 1, 2, 3, 4 };

            // Use the PrintList extension method to print the elements of the list
            lstInt.PrintList();
            Console.WriteLine();

            // Use the ConvertCommaSeparatedString extension method to convert the list to a string
            Console.WriteLine(lstInt.ConvertCommaSeparatedString());
        }
    }
}
