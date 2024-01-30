﻿using System;
using System.Collections.Generic;
namespace GenericCollectionList
{
    /// <summary>
    /// custom list for generic types 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomList<T> : List<T>
    {
        #region Constructor
        public CustomList() : base() { }
        #endregion

        #region Public Method
        /// <summary>
        /// Add the item into list
        /// </summary>
        /// <param name="item">item</param>
        public new void Add(T item)
        {
            //Console.WriteLine("Custom message Before call the base function");      
            base.Add(item);
            Console.WriteLine($"{item} is added");
        }

        /// <summary>
        /// Remove the item from list
        /// </summary>
        /// <param name="item">Item</param>
        public new void Remove(T item)
        {
           // Console.WriteLine("Custom message Before call the base function");
            base.Remove(item);
            Console.WriteLine($"{item} is removed");
        }
        #endregion
    }
}
