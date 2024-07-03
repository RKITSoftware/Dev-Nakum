using GenericCollectionList;
using System;

namespace Base_Library
{
    /// <summary>
    /// Handle the objects and call their methods
    /// </summary>
    public class Program
    {
        /// <summary>
        /// create the object of the customList and call the custom method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // create the object
            CustomList<int> lstInt = new CustomList<int>();

            // add items into list
            lstInt.Add(1);
            lstInt.Add(2);
            lstInt.Add(3);
            lstInt.Add(4);

            //remove the item from list
            lstInt.Remove(2);

            // traverse the list
            lstInt.ForEach(x=>Console.Write($"{x} "));
        }
    }
}
