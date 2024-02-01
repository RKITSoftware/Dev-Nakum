using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    /// <summary>
    /// Demonstrates various LINQ operations on a list of integers.
    /// </summary>
    public class LinqWithList
    {
        #region Public Member
        // List to store integers
        public List<int> lstNums;
        #endregion

        #region Private Method
        // Helper method to print the elements of a list
        private void PrintList(List<int> lst)
        {
            lst.ForEach(x => Console.Write($"{x} "));
            Console.WriteLine();
            Console.WriteLine();
        }
        #endregion

        #region Constructor
        // Constructor initializes the list with some integers
        public LinqWithList()
        {
            lstNums = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Performs various LINQ operations on the list and prints the results.
        /// </summary>
        public void ListOperation()
        {
            // Filtering: Find even numbers in the list
            List<int> lstEvenNumbers = lstNums.Where(num => num % 2 == 0).ToList();
            PrintList(lstEvenNumbers);

            // Sorting: Order the list in descending order based on whether the number is even
            List<int> lstOrderBy = lstNums.OrderByDescending(num => num % 2 == 0).ToList();
            PrintList(lstOrderBy);

            // Grouping: Group the numbers by their remainder when divided by 3
            var groupBy = lstNums.GroupBy(num => num % 3);
            foreach (var item in groupBy)
            {
                Console.Write($"Remainder {item.Key} -> ");
                foreach (var num in item)
                {
                    Console.Write($"{num} ");
                }
                Console.WriteLine();
            }

            // Prepend: Add 0 at the beginning of the list
            var lstNew = lstNums.Prepend(0);
            foreach (var item in lstNew)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
            Console.WriteLine();

            // Check if 1 exists in the list
            Console.WriteLine($"to check 1 is exist or not : {lstNums.Contains(1)}\n");

            // Aggregate: Sum of all elements in the list
            Console.WriteLine($"Addition of list is : {lstNums.Sum()}");

            // Partitioning Operators: Take the first 6 elements of the list
            foreach (var number in lstNums.Take(6))
            {
                Console.Write(number + " ");
            }
            Console.WriteLine();

            // First, Last, ElementAt: Access specific elements in the list
            Console.WriteLine($"First element of the list is : {lstNums.First()}");
            Console.WriteLine($"Last element of the list is : {lstNums.Last()}");
            Console.WriteLine($"Element at index 4 in the list is : {lstNums.ElementAt(4)}");
        }
        #endregion
    }
}
