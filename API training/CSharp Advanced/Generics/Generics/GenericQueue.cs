using System;
using System.Collections.Generic;

namespace Generics
{
    /// <summary>
    /// Queue related operation
    /// </summary>
    public class GenericQueue
    {
        public void CreateQueue()
        {
            //create the object with data type int type
            Queue<int> objQueue = new Queue<int>();

            // push the value into queue
            objQueue.Enqueue(1);
            objQueue.Enqueue(2);
            objQueue.Enqueue(3);
            objQueue.Enqueue(4);
            objQueue.Enqueue(5);

            // traverse the queue
            foreach (int it in objQueue)
            {
                Console.WriteLine($"{it}");
            }
            Console.WriteLine("");
            Console.WriteLine($"{objQueue.Dequeue()}");        // Dequeue gives top most element and also remove it.
            Console.WriteLine($"{objQueue.Peek()}");           // Peek only gives top most element.

            //create the object with data type int type
            Queue<string> objQueueString = new Queue<string>();
            objQueueString.Enqueue("This");
            objQueueString.Enqueue("is");
            objQueueString.Enqueue("a");
            objQueueString.Enqueue("queueString");

            foreach (string it in objQueueString)
            {
                Console.WriteLine($"{it}");
            }
        }
    }
}
