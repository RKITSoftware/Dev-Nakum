using System;

namespace Generics
{
    /// <summary>
    /// class which contains generic field and properties
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericClass<T>
    {

        public T GenericField1 { get; set; }
        public T GenericField2 { get; set; }


        /// <summary>
        /// Display the message for generic type
        /// </summary>
        public void Display()
        {
            Console.WriteLine($"The value of generic field 1 is {GenericField1}");
            Console.WriteLine($"The value of generic field 2 is {GenericField2}");
        }

        /// <summary>
        /// Addition of two generic type fields
        /// </summary>
        /// <returns>addition of two numbers</returns>
        public T Addition()
        {
            dynamic result = (dynamic)GenericField1 + (dynamic)GenericField2;
            return result;
        }

        /// <summary>
        /// Multiplication of two generic type fields
        /// </summary>
        /// <returns>multiplication of two numbers</returns>
        public T Multiplication()
        {
            if (typeof(T) == typeof(string))
            {
                T res = (dynamic)"For string type multiplication is not allowed";
                return res;
            }

            dynamic result = (dynamic)GenericField1 * (dynamic)GenericField2;
            return result;
        }
    }
}
