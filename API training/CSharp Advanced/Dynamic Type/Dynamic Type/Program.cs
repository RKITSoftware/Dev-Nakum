using System;

namespace Dynamic_Type
{
    /// <summary>
    /// A demonstration of the dynamic type in C# and its usage in various scenarios.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Performs addition of two dynamic items.
        /// </summary>
        /// <param name="item1">The first dynamic item.</param>
        /// <param name="item2">The second dynamic item.</param>
        /// <returns>The result of the addition.</returns>
        public static dynamic Addition(dynamic item1, dynamic item2)
        {
            return item1 + item2;
        }

        /// <summary>
        /// Main entry point of the program.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args)
        {
            // Creating a dynamic variable
            dynamic dynamicVariable;

            // Assigning different types to the dynamic variable
            dynamicVariable = 10;  // Assigning an integer
            Console.WriteLine($"Dynamic Variable Type: {dynamicVariable.GetType()}, Value: {dynamicVariable}");

            dynamicVariable = "Hello, Dynamic!";  // Assigning a string
            Console.WriteLine($"Dynamic Variable Type: {dynamicVariable.GetType()}, Value: {dynamicVariable}");

            dynamicVariable = 3.14;  // Assigning a double
            Console.WriteLine($"Dynamic Variable Type: {dynamicVariable.GetType()}, Value: {dynamicVariable}");

            // Performing operations without compile-time checking
            dynamic result = dynamicVariable + 5;
            Console.WriteLine($"Result Type: {result.GetType()}, Value: {result}");

            // Accessing properties dynamically
            dynamic person = new { Name = "John", Age = 30 };
            Console.WriteLine($"Person Type: {person.GetType()}, Name: {person.Name}, Age: {person.Age}");

            // Demonstrating dynamic addition method
            Console.WriteLine($"Addition of 3 and 5.1 is : {Addition(3, 5.1)}");
            Console.WriteLine($"Addition of 3 and 5 is : {Addition(3, 5)}");
            Console.WriteLine($"Addition of 'Hello' and 'World' is : {Addition("Hello", "World")}");
            Console.WriteLine($"Addition of 'Hello' and 213 is : {Addition("Hello", 213)}");
        }
    }
}
