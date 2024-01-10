using System;

namespace enums
{
    /// <summary>
    ///     enum month retuns integer value
    /// </summary>
    enum enmMonth
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }
    class Customer
    {
        #region Public Method
        public string Name { get; set; }
        public string Gender { get; set; }

        public int Month { get; set; }
        #endregion
    }
    class Program
    {
       
        static void Main(string[] args)
        {
            // custoners array 
            Customer[] objCustomers = new Customer[3];
            objCustomers[0] = new Customer
            {
                Name = "Dev",
                Gender = "Male",
                Month = (int)enmMonth.April,
            };
            objCustomers[1] = new Customer
            {
                Name = "Alice",
                Gender = "Female",
                Month = (int)enmMonth.March,
            };
            objCustomers[2] = new Customer
            {
                Name = "Bob",
                Gender = "Male",
                Month = (int)enmMonth.August,
            };

            // get the month names from enum and store it 
            string[] MonthsName = Enum.GetNames(typeof(enmMonth));
           
            // traverse for display the data
            foreach (Customer customer in objCustomers)
            {
                Console.WriteLine($"Name : {customer.Name}");
                Console.WriteLine($"Gender : {customer.Gender}");
                Console.WriteLine($"Month : {MonthsName[(customer.Month)]}");
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
