using System;

namespace Method
{
    class Employee
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
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

        static void IncrementNumber(ref int num,int num2)
        {
            num++;
            num2++;
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


            dynamic dynamicVariable = 10;
            Console.WriteLine("Dynamic Variable: " + dynamicVariable);

            dynamicVariable = "Hello, dynamic!";
            Console.WriteLine("Dynamic Variable: " + dynamicVariable);

            dynamicVariable = new { Name = "John", Age = 30 };
            Console.WriteLine($"Name: {dynamicVariable.Name}, Age: {dynamicVariable.Age}");
            dynamic objDynEmployee = new Employee();
            objDynEmployee.Name = "Dev";
            objDynEmployee.Age= 21;
            Console.WriteLine($"Dynamic - Employee name : {objDynEmployee.Name}");
            Console.WriteLine($"Dynamic - Employee Age : {objDynEmployee.Age}");

            object objectVariable = 10;
            Console.WriteLine("Object Variable: " + objectVariable);

            objectVariable = "Hello, object!";
            Console.WriteLine("Object Variable: " + objectVariable);

            // Explicit casting is needed
            string stringValue = (string)objectVariable;
            Console.WriteLine("Casted String Value: " + stringValue);

            object objEmployee = new Employee();
            ((Employee)objEmployee).Name = "Dev";
            Console.WriteLine($"Object - Employee name : {((Employee)objEmployee).Name}");


            int num3 = 1;
            int num4 = 1;
            Console.WriteLine($"original number : {num3}");
            IncrementNumber(ref num3,num4);
            Console.WriteLine($"incremented number with reference: {num3}");
            Console.WriteLine($"incremented number without reference: {num4}");
        }


    }
}
