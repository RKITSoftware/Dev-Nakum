using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Debugging
{
    public class Program
    {
        static int AddNumbers(int a, int b)
        {
            // Set a breakpoint here to debug
            if (a == 7)
            {
                a++;
            }

            return a + b;
        }
        
        static int Multiplication(int a, int b)
        {
            // Set a breakpoint here to debug
            return a * b;
        }

        public static bool check()
        {
            Random objRandom = new Random();

            int num = objRandom.Next(1, 101);
            bool ans = num > 50;

            if(ans==true)
            {
                return true;
            }
            return false;
        }
        
        public static bool check2()
        {
            for(int i = 0; i < 10; i++)
            {
                if (check())
                    continue;

                if(i==2)
                {
                    Console.WriteLine("dependent checkpoint executes");
                }
                Console.WriteLine($"The value of {i}");
            }
            return check();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the first number:");
            int num1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the second number:");
            int num2 = int.Parse(Console.ReadLine());

            int sum = AddNumbers(num1, num2);
            int mul = Multiplication(num1, num2);

            Console.WriteLine($"Addition of two numbers: {sum}");
            Console.WriteLine($"multiplication of two numbers: {sum}");

            if (check2())
            {
                Console.WriteLine("Hello World");
            }
        }
    }
}
