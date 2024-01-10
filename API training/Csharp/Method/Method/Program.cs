using System;

namespace Method
{
    class Program
    {
        /// <summary>
        ///     Addition of num2 number
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>int</returns>
        static int Addition(int x, int y)
        {
            return x + y;
        }

        /// <summary>
        ///     multiplication of num2 number
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>int</returns>
        static int Multiplication(int x, int y)
        {
            return x * y;
        }

        /// <summary>
        ///     division of num2 number
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>int</returns>
        static int Division(int x, int y)
        {
            return x / y;
        }

        /// <summary>
        ///     subtraction of num2 number
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>int</returns>
        static int Subtraction(int x, int y)
        {
            return x > y ? x - y : y - x;
        }

        /// <summary>
        ///     print the message of passed    
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>return the defalut parameter</returns>
        static int DefaultValueMethod(int x, int y=15)
        {
            Console.WriteLine($"The value of x is {x}");
            return y;
        }

        /// <summary>
        ///     print the message of default parameter
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        static void DefaultValueAnotherMethod(int x=32, int y=143)
        {
            Console.WriteLine($"The value of x is {x} in the default another method");
            Console.WriteLine($"The value of y is {y} in the default another method");
        }

        public static void Main(string[] args)
        {
            int num1 = 10, num2 = 5;

            Console.WriteLine("Addition is " + Addition(num1, num2));
            Console.WriteLine("Multiplication is " + Multiplication(num1, num2));
            Console.WriteLine("Subtraction is " + Subtraction(num1, num2));
            Console.WriteLine("Division is " + Division(num1, num2));
            Console.WriteLine("The Value of y is " + DefaultValueMethod(num1));
            DefaultValueAnotherMethod();
        }

    }
}
