using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeClass
{

    class Program
    {
        static void Main(string[] args)
        {
            DateTime dateTime = new DateTime(2024,01,05,17,51,56);
            Console.WriteLine(dateTime);

            // current date and time
            Console.WriteLine(DateTime.Now);

            // UTC date time
            Console.WriteLine(DateTime.UtcNow);

            //today's date and time
            Console.WriteLine(DateTime.Today);

            // calculate the days in specific month and year
            Console.WriteLine(DateTime.DaysInMonth(1582, 10));

            // add Days into current date
            Console.WriteLine($"after 23 days date is {DateTime.Now.AddDays(18)}");

            DateTime today = DateTime.Now;
            Console.WriteLine($"ToLongDateString : {today.ToLongDateString()}");
            Console.WriteLine($"ToShortDateString : {today.ToShortDateString()}");
            Console.WriteLine($"ToLongTimeString : {today.ToLongTimeString()}");
            Console.WriteLine($"ToShortTimeString : {today.ToShortTimeString()}");

            // print into specific format
            Console.WriteLine(today.ToString("dd/MM/yy"));
        }
    }
}
