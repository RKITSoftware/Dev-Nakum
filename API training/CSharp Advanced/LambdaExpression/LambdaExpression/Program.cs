using System;
using System.Collections.Generic;

namespace LambdaExpression
{
    /// <summary>
    /// class which handle lambda expression
    /// </summary>
    public class Program
    {
        /// <summary>
        /// class which handle lambda expression
        /// </summary>
        static void Main(string[] args)
        {
            // Action method which is doesn't have return statement - without parameter
            Action welcome = () => Console.WriteLine("Welcome !!");
            welcome();

            //Action method with parameter
            Action<string> printName = name => Console.WriteLine($"Hello {name}");
            printName("Dev");

            // function 
            Func<int, int> square = x => x * x;
            Console.WriteLine(square(4));

            // travsers the list
            List<int> lstInt = new List<int>() { 1, 2, 3, 4, 5 };
            lstInt.ForEach(x => Console.Write(x));  

            // predicate - returns true or false
            Predicate<int> isEven = x => x%2 == 0;
            Console.WriteLine(isEven(5));

        }
    }
}
