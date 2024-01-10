using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statements
{
     class Program
    {
        public static void Main(string[] args)
        {
            // if-else statement
            int age = 19;
            if (age > 18)
            {
                Console.WriteLine("You can vote!!");
            }
            else
            {
                Console.WriteLine("You can not vote, you have to wait {0} years.", 18 - age);
            }


            // if-else-if ladder statements
            if (age > 18)
            {
                Console.WriteLine("You can get driving license.");
            }
            else if (age >= 16)
            {
                Console.WriteLine("You can get learning driving license.");
            }
            else
            {
                Console.WriteLine("You can not get license yet");
            }


            // switch statement
            int number = 10;
            switch (number)
            {
                case 10:
                    Console.WriteLine("Number is 10");
                    break;
                case 20:
                    Console.WriteLine("Number is 20");
                    break;
                default:
                    break;
            }

            // while loop
            int i = 1;
            while (i <= 4)
            {
                Console.WriteLine("The value of i is " + i++);
            }

            // for loop
            for (int j = 0; j <= 5; j++)
            {
                if (j == 3)
                    continue;
                Console.WriteLine("The value of j is " + j);
            }

            // do while
            i = 1;
            do
            {
                Console.WriteLine("The value of i is " + i++);
            } while (i <= 4);
        }
    }
}
