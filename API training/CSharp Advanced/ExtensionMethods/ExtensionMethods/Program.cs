using System;
using System.Collections.Generic;


namespace ExtensionMethods
{
    /// <summary>
    /// static class containing extension method for List<T>
    /// </summary>
    public static class ListExtension
    {
        /// <summary>
        /// Extension method to print the element of a list
        /// </summary>
        /// <typeparam name="T">Type of element into list </typeparam>
        /// <param name="lstDemo">the list to be printed</param>
        public static void PrintList<T>(this List<T> lstDemo)
        {
            // use ForEach to print the element separated by space
            lstDemo.ForEach(x => Console.Write($"{x} "));
        }

        /// <summary>
        /// Extension method to convert a list of element to comma separated string
        /// </summary>
        /// <typeparam name="T">Type of elements in the list.</typeparam>
        /// <param name="lstDemo">The list to be converted to a string.</param>
        /// <returns>A string containing the elements separated by commas.</returns>
        public static string ListIntToString<T>(this List<T> lstDemo)
        {
            // Use string.Join to concatenate the elements with commas
            return string.Join(",",lstDemo);
        }
    }

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

            // Use the ListIntToString extension method to convert the list to a string
            Console.WriteLine(lstString.ListIntToString());

            // Create a list of integers
            List<int> lstInt = new List<int> { 1, 2, 3, 4 };

            // Use the PrintList extension method to print the elements of the list
            lstInt.PrintList();
            Console.WriteLine();  

            // Use the ListIntToString extension method to convert the list to a string
            Console.WriteLine(lstInt.ListIntToString());
        }
    }
}
