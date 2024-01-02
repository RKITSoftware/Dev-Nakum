using System.Collections;
using System.Collections.ObjectModel;

class Generic<T>
{
    public T Value { get; set; }   
    public void Display<T>(string mes, T value)
    {
        Console.WriteLine($"{mes} : {value}");
    }
}
class Program
{
    public static void CreateGenericClass()
    {
        Generic<int> genericInt = new Generic<int>();
        genericInt.Value = 1;
        genericInt.Display<int>("Integer", 234243);
        Console.WriteLine($"Generic<int> value is {genericInt.Value}");

        Generic<string> genericString = new Generic<string>();
        genericString.Value = "Hello World!!";
        genericString.Display<string>("String", "Dev Nakum");
        Console.WriteLine($"Generic<string> value is {genericString.Value}");
    }

    public static void CreateList()
    {
        List<int> list = new List<int>();
        list.Add(3);
        list.Add(1);
        list.Add(5);
        list.Add(2);
        list.Add(4);

        list.ForEach(i => Console.Write(i + " "));
        Console.WriteLine();

        list.Sort();
        list.ForEach(i => Console.Write(i + " "));
        Console.WriteLine();

        Console.WriteLine($"counts are : {list.Count()}");

        list.Remove(4);
        Console.WriteLine($"counts are : {list.Count()}");

        list.RemoveAt(0);
        Console.WriteLine($"counts are : {list.Count()}");
    }

    public static void CreateCollection()
    {
        Collection<int> collection = new Collection<int>();

        // Mehods 
        collection.Add(1);
        collection.Add(2);
        collection.Add(3);
        collection.Add(4);
        collection.Add(5);

        foreach (int i in collection)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();
        Console.WriteLine($"Total counts are {collection.Count}");
        Console.WriteLine($"Element at index 2 is : {collection[0]}");

        Console.WriteLine(collection.Contains(1));

    }

    public static void CreateSortedList()
    {
        SortedList sortedList = new SortedList();
        sortedList.Add(1.09, "A");
        sortedList.Add(1.12, "B");
        sortedList.Add(1.03, "C");
        sortedList.Add(2.22, "D");

        foreach (DictionaryEntry item in sortedList)
        {
            Console.WriteLine($"{item.Key} : {item.Value}");
        }
        Console.WriteLine();

        sortedList.Remove(1.12);
        foreach (DictionaryEntry item in sortedList)
        {
            Console.WriteLine($"{item.Key} : {item.Value}");
        }
        Console.WriteLine();

        sortedList.RemoveAt(0);
        foreach (DictionaryEntry item in sortedList)
        {
            Console.WriteLine($"{item.Key} : {item.Value}");
        }
        Console.WriteLine();
    }
    
    public static void CreateHashSet()
    {
        HashSet<string> set = new HashSet<string>();
        set.Add("A");
        set.Add("B");
        set.Add("C");
        set.Add("D");
        set.Add("D");
        set.Add("E");

        foreach (string item in set)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        HashSet<string> set2 = new HashSet<string>();
        set2.Add("B");
        set2.Add("C");
        set2.Add("E");
        set2.Add("F");
        set2.Add("G");
        set2.Add("I");

        foreach (string item in set2)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        set.UnionWith(set2);
        foreach (string item in set)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine();

        //set.IntersectWith(set2);
        //foreach (string item in set)
        //{
        //    Console.WriteLine(item);
        //}
        //Console.WriteLine();

        //set.ExceptWith(set2);
        //foreach (string item in set)
        //{
        //    Console.WriteLine(item);
        //}
        //Console.WriteLine();
    }
    
    public static void CreateDictionary()
    {
        Dictionary<int, string> dictionary = new Dictionary<int, string>();

    
        dictionary.Add(234, "A");
        dictionary.Add(243, "B");
        dictionary.Add(17470, "C");
        dictionary.Add(3872, "D");

       
        foreach (KeyValuePair<int, string> ele in dictionary)
        {
            Console.WriteLine($"{ele.Key} : {ele.Value}");
        }
        Console.WriteLine();

        
        dictionary.Remove(234);

        
        foreach (KeyValuePair<int, string> ele in dictionary)
        {
            Console.WriteLine($"{ele.Key} : {ele.Value}");
        }
        Console.WriteLine();
    }
    public static void Main(string[] args)
    {
        // crete the collection of integer
        //CreateCollection();

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
    }
}