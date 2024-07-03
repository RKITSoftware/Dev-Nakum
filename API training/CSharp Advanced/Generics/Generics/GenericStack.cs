using System;
using System.Collections.Generic;

namespace Generics
{
    /// <summary>
    /// stack related operation 
    /// </summary>
    public class GenericStack
    {
        /// <summary>
        /// create stack with generic method
        /// </summary>
        public void CreateStack()
        {
            // stack with int
            Stack<int> objStackInt = new Stack<int>();
            objStackInt.Push(1);
            objStackInt.Push(2);
            objStackInt.Push(3);
            objStackInt.Push(4);
            objStackInt.Push(5);

            // traverse the stack 
            while(objStackInt.Count != 0)
            {
                Console.Write($"{objStackInt.Peek()} ");
                objStackInt.Pop();
            }
            Console.WriteLine();

            // stack with string
            Stack<string> objStackString = new Stack<string>();
            objStackString.Push("a");
            objStackString.Push("b");
            objStackString.Push("c");
            objStackString.Push("d");
            objStackString.Push("e");

            while(objStackString.Count != 0)
            {
                Console.Write($"{objStackString.Peek()} ");
                objStackString.Pop();
            }
        }
    }
}
