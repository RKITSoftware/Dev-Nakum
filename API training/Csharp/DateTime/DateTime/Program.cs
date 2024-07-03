using System;

namespace DateTimeClass
{
    /// <summary>
    /// class which can manage all the operation related on DateTime
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method to start the program
        /// Manage all the operation related on DataTime
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            DateTime objDateTime = new DateTime(2024,01,05,17,51,56);
            Console.WriteLine(objDateTime);

            // current date and time
            Console.WriteLine(DateTime.Now);            



            // UTC date time
            Console.WriteLine(DateTime.UtcNow);

            //today's date and time
            Console.WriteLine(DateTime.Today);

            // calculate the days in specific month and year
            Console.WriteLine(DateTime.DaysInMonth(1582, 10));

            // add Days into current date
            Console.WriteLine($"after 23 days date is {DateTime.Now.AddDays(23)}");

            //difference between two date
            Console.WriteLine($"The difference between two dates are {DateTime.Now.AddDays(21).AddHours(2) - DateTime.Now}");

            DateTime today = DateTime.Now;
            Console.WriteLine($"ToLongDateString : {today.ToLongDateString()}");
            Console.WriteLine($"ToShortDateString : {today.ToShortDateString()}");
            Console.WriteLine($"ToLongTimeString : {today.ToLongTimeString()}");
            Console.WriteLine($"ToShortTimeString : {today.ToShortTimeString()}");

            // print into specific format
            Console.WriteLine(today.ToString("dd/MM/yy"));      // 01 - 02 - 24
            Console.WriteLine(today.ToString("dd-MMM-yy"));     // 01 - Feb - 24
            Console.WriteLine(today.ToString("dd-MMM-yyyy"));       // 01 - Feb - 2024
        }
    }
}
