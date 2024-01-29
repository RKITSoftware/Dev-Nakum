using System;

namespace Generics
{
    /// <summary>
    /// create object of the generic class and call it
    /// </summary>
    public class Program
    {
        /// <summary>
        /// create object of the generic class and call it
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // generic class with data type int
            GenericClass<int> objGenericClassInt = new GenericClass<int>();
            objGenericClassInt.GenericField1 = 234;
            objGenericClassInt.GenericField2 = 243;
            objGenericClassInt.Display();
            Console.WriteLine($"Addition is : {objGenericClassInt.Addition()}");
            Console.WriteLine($"Multiplication is : {objGenericClassInt.Multiplication()}");
            Console.WriteLine();

            // generic class with data type string
            GenericClass<string> objGenericClassString = new GenericClass<string>();
            objGenericClassString.GenericField1 = "D";
            objGenericClassString.GenericField2 = "N";
            objGenericClassString.Display();
            Console.WriteLine($"Addition is : {objGenericClassString.Addition()}");
            Console.WriteLine($"Multiplication is : {objGenericClassString.Multiplication()}");
            Console.WriteLine();

            //generic stack 
            GenericStack objGenericStack = new GenericStack(); 
            objGenericStack.CreateStack();
            Console.WriteLine();

            //generic queue
            GenericQueue objGenericQueue = new GenericQueue();
            objGenericQueue.CreateQueue();
            Console.WriteLine();

        }
    }
}
