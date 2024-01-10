using System;
using System.IO;


namespace Exception_Handling
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader streamReader = null;       // for read the data 
            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\data.txt");
                streamReader = new StreamReader(filePath);
                Console.WriteLine(streamReader.ReadToEnd());        // read all the character
                streamReader.Close();
            }
            catch (FileNotFoundException ex)        // file not found 
            {
                Console.WriteLine("File not found");
            }
            catch (Exception ex)        // general exception 
            {
                Console.WriteLine(ex.Message);
            }
            finally     // this block is execute
            {
                if(streamReader != null)
                {
                    streamReader.Close();
                }
                Console.WriteLine("Finally block is executes");
            }


            // inner exception 
            try
            {
                Console.WriteLine("Enter First Number");
                int firstNumber = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter Second Number");
                int secondNumber = Convert.ToInt32(Console.ReadLine());

                int result = firstNumber/secondNumber;
                Console.WriteLine($"Result is {result}");
            }
            catch(Exception ex)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\exceptionName.txt");
                if(File.Exists(filePath))
                {
                    StreamWriter streamWriter = new StreamWriter(filePath);
                    streamWriter.WriteLine(ex.GetType().Name);
                    Console.WriteLine(ex.Message);
                    streamWriter.Close();
                }
                else
                {
                    throw new FileNotFoundException($"{filePath} is not present");
                }
                Console.WriteLine("Something went wrong");
            }

            Console.ReadLine();
        }
    }
}

