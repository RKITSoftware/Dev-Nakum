using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    // Polymorphism and override
    class Animal
    {
        #region Public Method 

        /// <summary>
        ///     make virtual method
        /// </summary>
        public virtual void AnimalSound()
        {
            Console.WriteLine("The animal makes sound");
        }
        #endregion
    }

    class Pig : Animal
    {
        #region Public Method

        /// <summary>
        ///     make override method 
        /// </summary>
        public override void AnimalSound()
        {
            Console.WriteLine("The pig says: wee wee");
        }
        #endregion
    }

    class Dog : Animal
    {
        #region Public Method

        /// <summary>
        ///     make override method 
        /// </summary>
        public override void AnimalSound()
        {
            Console.WriteLine("The dog says: bow bow");
        }

        public void OverloadMethod(int num)
        {
            Console.WriteLine($"overload method {num}");
        }

        public void OverloadMethod(int num1,int num2)
        {
            Console.WriteLine($"overload method {num1} and {num2}");
        }

        public int OverloadMethodWithOptional(int num1=5,int num2=10)
        {
            Console.WriteLine($"Optional Parameter is {num1} and {num2}");
            return num1 + num2;
        }

        public string OverloadMethodWithOptional(string word1 = "Hello",  string word2 = "World")
        {
            Console.WriteLine($"Optional Parameter is {word1} and {word2}");
            return word1 + word2;   
        }
        #endregion
    }
}
