using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;


namespace Collection_Generics
{
    // Generic class 
    class Generic<T>
    {
        #region Public Method 
        public T Value { get; set; }

        /// <summary>
        ///     Display message and value for generic data types
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mes"></param>
        /// <param name="value"></param>
        public void Display<T>(string mes, T value)
        {
            Console.WriteLine($"{mes} : {value}");
        }
        #endregion
    }
    class Program
    {
        /// <summary>
        ///     call generic class and method 
        /// </summary>
        public static void CreateGenericClass()
        {
            Generic<int> genericInt = new Generic<int>();           // generic class with int data type
            genericInt.Value = 1;
            genericInt.Display<int>("Integer", 234243);
            Console.WriteLine($"Generic<int> value is {genericInt.Value}");

            Generic<string> genericString = new Generic<string>();      // generic class with string data type
            genericString.Value = "Hello World!!";
            genericString.Display<string>("String", "Dev Nakum");
            Console.WriteLine($"Generic<string> value is {genericString.Value}");
        }

        /// <summary>
        ///     create list and its operations
        /// </summary>
        public static void CreateList()
        {
            List<int> lstNumber = new List<int>();
            lstNumber.Add(3);
            lstNumber.Add(1);
            lstNumber.Add(5);
            lstNumber.Add(2);
            lstNumber.Add(4);

            // travers entire the list
            lstNumber.ForEach(i => Console.Write(i + " "));
            Console.WriteLine();

            lstNumber.Sort();       // sort the list
            lstNumber.ForEach(i => Console.Write(i + " "));
            Console.WriteLine();

            Console.WriteLine($"counts are : {lstNumber.Count()}");     // count the number of element

            lstNumber.Remove(4);        // remove the first occurence of specified elements
            Console.WriteLine($"counts are : {lstNumber.Count()}");

            lstNumber.RemoveAt(0);          // remove the perticular index elements 
            Console.WriteLine($"counts are : {lstNumber.Count()}");
        }

        /// <summary>
        ///     create collection and its operation
        /// </summary>
        public static void CreateCollection()
        {
            Collection<int> objCollection = new Collection<int>();

            // Mehods 
            objCollection.Add(1);
            objCollection.Add(2);
            objCollection.Add(3);
            objCollection.Add(4);
            objCollection.Add(5);

            // traverse the collection
            foreach (int i in objCollection)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            Console.WriteLine($"Total counts are {objCollection.Count}");
            Console.WriteLine($"Element at index 0 is : {objCollection[0]}");

            Console.WriteLine(objCollection.Contains(1));

        }

        /// <summary>
        ///     create the sorted list and its operation - uses binary search algorithm 
        /// </summary>
        public static void CreateSortedList()
        {
            SortedList objSortedList = new SortedList();
            objSortedList.Add(1.09, "A");
            objSortedList.Add(1.12, "B");
            objSortedList.Add(1.03, "C");
            objSortedList.Add(2.22, "D");

            // traverse the sorted list
            foreach (DictionaryEntry item in objSortedList)
            {
                Console.WriteLine($"{item.Key} : {item.Value}");
            }
            Console.WriteLine();

            objSortedList.Remove(1.12);     // remove the element
            foreach (DictionaryEntry item in objSortedList)
            {
                Console.WriteLine($"{item.Key} : {item.Value}");
            }
            Console.WriteLine();

            objSortedList.RemoveAt(0);          // remove the element at perticular indexes
            foreach (DictionaryEntry item in objSortedList)
            {
                Console.WriteLine($"{item.Key} : {item.Value}");
            }
            Console.WriteLine();
        }

        /// <summary>
        ///     create hash set and its operation
        /// </summary>
        public static void CreateHashSet()
        {
            HashSet<string> objSet = new HashSet<string>();
            objSet.Add("A");
            objSet.Add("B");
            objSet.Add("C");
            objSet.Add("D");
            objSet.Add("D");
            objSet.Add("E");

            // travers the hash set 
            foreach (string item in objSet)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            HashSet<string> objSet2 = new HashSet<string>();
            objSet2.Add("B");
            objSet2.Add("C");
            objSet2.Add("E");
            objSet2.Add("F");
            objSet2.Add("G");
            objSet2.Add("I");

            foreach (string item in objSet2)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            // union method 
            objSet.UnionWith(objSet2);
            foreach (string item in objSet)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            // intersection method 
            //objSet.IntersectWith(objSet2);
            //foreach (string item in objSet)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine();

            // exception method
            //objSet.ExceptWith(objSet2);
            //foreach (string item in objSet)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine();
        }

        /// <summary>
        ///     create the dictionary and its operations 
        /// </summary>
        public static void CreateDictionary()
        {
            Dictionary<int, string> objDictionary = new Dictionary<int, string>();

            objDictionary.Add(234, "A");
            objDictionary.Add(243, "B");
            objDictionary.Add(17470, "C");
            objDictionary.Add(3872, "D");

            // traverse Dictionary
            foreach (KeyValuePair<int, string> ele in objDictionary)
            {
                Console.WriteLine($"{ele.Key} : {ele.Value}");
            }
            Console.WriteLine();


            objDictionary.Remove(234);      // remove based on key

            objDictionary[243] = "Dev";
            foreach (KeyValuePair<int, string> ele in objDictionary)
            {
                Console.WriteLine($"{ele.Key} : {ele.Value}");
            }
            Console.WriteLine();
        }

        /// <summary>
        ///     create sorted dictionary - implement on red-black tree
        /// </summary>
        public static void CreateSortedDictionary()
        {
            SortedDictionary<int, string> objSortedDictionary = new SortedDictionary<int, string>();

            objSortedDictionary.Add(234, "A");
            objSortedDictionary.Add(243, "B");
            objSortedDictionary.Add(17470, "C");
            objSortedDictionary.Add(3872, "D");

            foreach (KeyValuePair<int, string> it in objSortedDictionary)
            {
                Console.WriteLine($"{it.Key} : {it.Value}");
            }

            Console.WriteLine("");

            objSortedDictionary.Remove(234);        // remove element based on key 

            foreach (KeyValuePair<int, string> it in objSortedDictionary)
            {
                Console.WriteLine($"{it.Key} : {it.Value}");
            }

            // check the key is exists or not 
            if (objSortedDictionary.ContainsKey(243) == true)
            {
                Console.WriteLine("Key is contains in the sortedDictionary");
            }
            else
            {
                Console.WriteLine("Key is not contains in the sortedDictionary");
            }

            // retrive data from indexes
            string value = objSortedDictionary[243];
            Console.WriteLine(value);
        }

        /// <summary>
        ///     create hash table and its operations
        /// </summary>
        public static void CreateHashTable()
        {
            Hashtable objHashtable = new Hashtable();
            objHashtable.Add(1, "Dev");
            objHashtable.Add(2, "Kishan");
            objHashtable.Add(3, "Tushar");
            objHashtable.Add(4, "Raj");

            foreach (DictionaryEntry it in objHashtable)
            {
                Console.WriteLine($"{it.Key} : {it.Value}");
            }
            Console.WriteLine("");
            objHashtable.Remove(1);     // remove based on key

            foreach (DictionaryEntry it in objHashtable)
            {
                Console.WriteLine($"{it.Key} : {it.Value}");
            }
            Console.WriteLine("");

            // to check whether key is exists or not
            if (objHashtable.ContainsKey(2))
            {
                Console.WriteLine("Key is exits");
            }

            // to check whether value is exists or not
            if (objHashtable.ContainsValue("Kishan"))
            {
                Console.WriteLine("Value is exits");
            }
        }

        /// <summary>
        ///     create Generic stack and non generic stack and perform its operations
        /// </summary>
        public static void CreateStack()
        {
            // non-generic stack 
            Stack stack = new Stack();

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);

            // to check whether element is exists or 
            if (stack.Contains(1))
            {
                Console.WriteLine("1 is Contain in the stack");
            }

            Console.WriteLine("");
            while (stack.Count!=0)
            {
                Console.WriteLine($"{stack.Peek()}");       // top most element
                stack.Pop();        // remove top most elements
            }

            Console.WriteLine("");

            // generic stack - string dataType
            Stack<string> stackString = new Stack<string>();
            stackString.Push("This");
            stackString.Push("is");
            stackString.Push("a");
            stackString.Push("stackString");

            while (stackString.Count()!=0)
            {
                Console.WriteLine($"{stackString.Peek()}");
                stackString.Pop();
            }
        }

        /// <summary>
        ///     create Generic queue and non generic queue and perform its operations
        /// </summary>
        public static void CreateQueue()
        {
            // non-generic queue
            Queue queue = new Queue();
            queue.Enqueue(1);
            queue.Enqueue("A");
            queue.Enqueue(2.3);
            queue.Enqueue("ASASAMJD");

            foreach(var it in queue)
            {
                Console.WriteLine($"{it}");
            }
            Console.WriteLine("");
            Console.WriteLine($"{queue.Dequeue()}");        // Dequeue gives top most element and also remove it.
            Console.WriteLine($"{queue.Peek()}");           // Peek only gives top most elememt.

            Console.WriteLine("");

            // generic queue - string dataTypes
            Queue<string> queueString = new Queue<string>();
            queueString.Enqueue("This");
            queueString.Enqueue("is");
            queueString.Enqueue("a");
            queueString.Enqueue("queueString");

            foreach(string it in queueString)
            {
                Console.WriteLine($"{it}");
            }
        }
        public static void Main(string[] args)
        {
            // crete the collection of integer
            CreateCollection();

            // create the generic class and objects
            //CreateGenericClass();

            // List
            //CreateList();

            //sortedlist
            //CreateSortedList();

            // HashSet
            //CreateHashSet();

            // Dictionary
            //CreateDictionary();

            //Sorted dictionary
            //CreateSortedDictionary();

            //Hashtable
            //CreateHashTable();

            // stack
            //CreateStack();

            //queue
            //CreateQueue();

            
            Console.ReadLine();

        }
    }
}