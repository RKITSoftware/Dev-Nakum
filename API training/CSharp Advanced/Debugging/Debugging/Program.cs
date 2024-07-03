using System;

namespace Debugging
{
    /// <summary>
    ///  manage the type of debugging
    /// </summary>
    public class Program
    {

        /// <summary>
        /// Add two number
        /// </summary>
        /// <param name="a">number 1</param>
        /// <param name="b">number 2</param>
        /// <returns>addition of two number</returns>
        static int AddNumbers(int a, int b)
        {
            // Set a breakpoint here to debug
            if (a == 7)
            {
                a++;        // hit: when b == 5
            }

            return a + b;
        }

        /// <summary>
        /// multiplication between two number
        /// </summary>
        /// <param name="a">number 1</param>
        /// <param name="b">number 2</param>
        /// <returns>multiplication of two number</returns>
        static int Multiplication(int a, int b)
        {
            // condition expression : if b==5
            return a * b;
        }

        /// <summary>
        /// generate the random number and check number is above 50 or not
        /// </summary>
        /// <returns>true if number is greater than 50 or else false</returns>
        public static bool Check()
        {
            Random objRandom = new Random();

            //generate the random value between 1 to 100
            int num = objRandom.Next(1, 101);
            bool ans = num > 50;

            if(ans)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// traverse the loop and check the condition
        /// </summary>
        /// <returns>true if number is greater than 50 or else false</returns>
        public static bool Check2()
        {
            for(int i = 0; i < 10; i++)
            {
                if (Check())
                {
                    continue;       // trace point : get the custom message in the output window
                }

                if(i==2)        // temporary check point
                {
                    Console.WriteLine("temporary checkpoint executes");
                }
                Console.WriteLine($"The value of {i}");
            }
            return Check();
        }

        /// <summary>
        /// Start the main program 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            // conditional compilations
            #if DEBUG
                Console.WriteLine("Debug mode is enabled");
            #else 
                Console.WriteLine("Debug mode is disabled");
            #endif


            Console.WriteLine("Enter the first number:");
            int num1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the second number:");
            int num2 = int.Parse(Console.ReadLine());

            int sum = AddNumbers(num1, num2);
            int mul = Multiplication(num1, num2);

            Console.WriteLine($"Addition of two numbers: {sum}");
            Console.WriteLine($"multiplication of two numbers: {mul}");

            if (Check2())
            {
                Console.WriteLine("Hello World");       // dependent on breakpoint line no 97
            }

            Console.ReadLine();
        }
    }

}
