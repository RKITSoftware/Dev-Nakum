using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Method
{
    class Program
    {
        /// <summary>
        ///     Addition of two number
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>int</returns>
        static int Addition(int x, int y)
        {
            return x + y;
        }

        /// <summary>
        ///     multiplication of two number
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>int</returns>
        static int Multiplication(int x, int y)
        {
            return x * y;
        }

        /// <summary>
        ///     division of two number
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>int</returns>
        static int Division(int x, int y)
        {
            return x / y;
        }

        /// <summary>
        ///     subtraction of two number
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>int</returns>
        static int Subtraction(int x, int y)
        {
            return x > y ? x - y : y - x;
        }

        public static void Main(string[] args)
        {
            int one = 10, two = 5;

            Console.WriteLine("Addition is : " + Addition(one, two));
            Console.WriteLine("Multiplication is : " + Multiplication(one, two));
            Console.WriteLine("Subtraction is : " + Subtraction(one, two));
            Console.WriteLine("Division is : " + Division(one, two));

        }

    }
}
