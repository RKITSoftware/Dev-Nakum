using System;
using System.Text;

namespace StringClass
{
    /// <summary>
    /// Main class for handling the string operations
    /// </summary>
    public class Program
    {
        /// <summary>
        /// main method to perform the various string operation 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string name = "Dev Nakum";

            // lenght of string 
            Console.WriteLine($"The length of name is {name.Length}");

            // convert string into uppercase
            Console.WriteLine($"Uppercase : {name.ToUpper()}");

            // convert string into lowercase
            Console.WriteLine($"Lowercase : {name.ToLower()}");

            // remove the space in between word
            Console.WriteLine($"Remove the space : {name.Trim()}");
            
            // find the index of first character
            Console.WriteLine($"Index of N is: {name.IndexOf('N')}");

            // find the index of first character
            Console.WriteLine($"Last Index of N is : {name.LastIndexOf("N")}");

            // substring from index 2 to length of 4 
            Console.WriteLine($"Substring : {name.Substring(2,4)}");

            // replace the word to another word
            Console.WriteLine($"Replace Dev to Kishan: {name.Replace("Dev","Kishan")}");

            // insert new word at specific index
            Console.WriteLine($"Insert : {name.Insert(0,"Welcome ")}");

            string stringNumber = "12312";
            // convert string into int
            int intNumber = int.Parse(stringNumber);    
            Console.WriteLine($"Convert string number into integer {intNumber}");

            // convert int into string
            Console.WriteLine($"Convert integer into string {intNumber.ToString()}");

            // concatenation
            Console.WriteLine("Welcome "+name);

            // compare the string -- return 0 for same , -1 for ascending, 1 from descending
            Console.WriteLine($"Compare string {String.Compare("Dev" , "Kishan")}");


            string[] stringArray = new string[]
            {
                "Hello",
                "from",
                "another",
                "side",
            };
            // join with specific delimiter
            string arrayToString = String.Join(" ", stringArray);
            Console.WriteLine(arrayToString);

            // compare the string -- return true or false 
            if (String.Equals("Dev", "Dev"))
            {
                Console.WriteLine("Dev and Dev both are same");
            }
            else
            {
                Console.WriteLine("Both are not same");
            }

            Console.ReadLine();

        }
    }
}
