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
        public static string ConvertCommaSeparatedString<T>(this List<T> lstDemo)
        {
            // Use string.Join to concatenate the elements with commas
            return string.Join(",", lstDemo);
        }
    }
}
