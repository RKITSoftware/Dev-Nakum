using System;


namespace DataTypes_Variabes
{
    class Program
    {
        public static void Main(string[] args)
        {
            string characterName = "Dev";       // used to store sequence of the character
            int characterAge = 21;      // used to store the numeric value
            char grade = 'A';       // used to store the character value

            // float, decimal and double stored the fraction value
            float percentage = 98.94f;
            double value = 121.1212;
            decimal decimalValue = 1231.123121312313131m;

            bool isValid = true;        // stored only true or false


            Console.WriteLine("Name of the character is " + characterName);
            Console.WriteLine("Age ${} the charater is " + characterAge);

            Console.WriteLine("float: " + percentage);
            Console.WriteLine("double: " + value);
            Console.WriteLine("decimal: " + decimalValue);


            // Implicit Type Casting
            int intValue = 10;
            double doubleValue = intValue; // Implicit casting from int to double

            Console.WriteLine($"Implicit Casting: int to double - Result: {doubleValue}");

            // Explicit Type Casting
            double anotherDoubleValue = 15.75;
            int anotherIntValue = (int)anotherDoubleValue; // Explicit casting from double to int
            Console.WriteLine($"Explicit Casting: double to int - Result: {anotherIntValue}");

            // Boxing: Converting value type to reference type
            int num1 = 42;
            object boxedValue = num1; // Boxing int to object

            Console.WriteLine($"Boxed Value: {boxedValue}");

            // Unboxing: Converting reference type back to value type
            int unboxedValue = (int)boxedValue; // Unboxing object to int
            Console.WriteLine($"Unboxed Value: {unboxedValue}");
        }
    }
}
