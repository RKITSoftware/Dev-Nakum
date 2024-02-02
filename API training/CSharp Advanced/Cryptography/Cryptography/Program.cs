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

            Console.WriteLine();
            Console.WriteLine("RSA Algorithm");
           
            // object of the RSA
            RSA objRSA = new RSA();

            // encrypted string 
            string encryptedString = objRSA.Encrypt(plainText);
            Console.WriteLine($"encrypted string is : {encryptedString}");

            // Decrypted string = plain text
            string decryptedString = objRSA.Decrypt(encryptedString);
            Console.WriteLine($"Decrypted string is : {decryptedString}");
            Console.WriteLine();

            Console.WriteLine("AES Algorithm");
            
            //object of the AES
            AES objAES = new AES();

            // encrypted string
            string encryptedStringAES = objAES.Encrypt(plainText);
            Console.WriteLine($"encrypted string is : {encryptedStringAES}");

            // decrypted string
            string decryptedStringAES = objAES.Decrypt(encryptedStringAES);
            Console.WriteLine($"Decrypted string is : {decryptedStringAES}");
            Console.WriteLine();


            Console.WriteLine("Rijndeal Algorithm");

            //object of the AES
            Rijndael objRijndael = new Rijndael();

            // encrypted string
            string encryptedStringRijndael = objRijndael.Encrypt(plainText);
            Console.WriteLine($"encrypted string is : {encryptedStringRijndael}");

            // decrypted string
            string decryptedStringRijndael = objRijndael.Decrypt(encryptedStringRijndael);
            Console.WriteLine($"Decrypted string is : {decryptedStringRijndael}");
        }
    }
}
