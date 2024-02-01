using System;

namespace Cryptography
{
    /// <summary>
    /// main class for executing the program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method take input from user and call the function
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Take input from the user
            Console.WriteLine("Enter a plain text");
            string plainText = Console.ReadLine();

            // object of the RSA
            RSA objRSA = new RSA();

            // encrypted string 
            string encryptedString = objRSA.Encrypt(plainText);
            Console.WriteLine($"encrypted string is : {encryptedString}");
            Console.WriteLine();

            // Decrypted string = plain text
            string decryptedString = objRSA.Decrypt(encryptedString);
            Console.WriteLine($"Decrypted string is : {decryptedString}");
        } 
    }
}
