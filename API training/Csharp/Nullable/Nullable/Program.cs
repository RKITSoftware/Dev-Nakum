using System;

namespace Nullable
{
    public class Program
    {
        static void Main(string[] args)
        {
            // defining Nullable type
            Nullable<int> num = null;
            
            // using the method
            // output will be 0 as default 
            // value of null is 0

            Console.WriteLine(num.GetValueOrDefault());
            Console.WriteLine(num.HasValue);        // return true - null value assigned

            int? num2 = null;
            Console.WriteLine(num2.GetValueOrDefault());

            // using Nullable type syntax 
            // to define non-nullable
            int? num3 = 234;
            Console.WriteLine(num3);

            // using Nullable type syntax 
            // to define non-nullable
            Nullable<int> num4 = 243;
            Console.WriteLine(num4.HasValue);       // return false - value is assigned
        }
    }
}
