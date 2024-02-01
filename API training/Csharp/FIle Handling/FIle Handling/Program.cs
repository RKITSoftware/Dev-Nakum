using System;
using System.IO;

namespace FIle_Handling
{
    /// <summary>
    /// Main class for file handling
    /// </summary>
    public class Program
    {
        /// <summary>
        /// entry point of program handle the file operation
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),@"..\..\data.txt");
            string destinationFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\data-copy.txt");


            // write into file
            try
            {
                // streamWrite for write something on file
                // second parameter is true or false - default is false , true for append the data into file
                using (StreamWriter objStreamWriter = new StreamWriter(filePath))      
                {
                    objStreamWriter.WriteLine("Hello from data file !!!");
                    objStreamWriter.WriteLine("This is a testing file for file handling.");
                }
                Console.WriteLine("Successfully wrote into the file");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            // read the file
            try
            {
                // streamReader for read something from file
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    // read and display each line from file
                    while(!streamReader.EndOfStream)
                    {
                        string line = streamReader.ReadLine();
                        Console.WriteLine(line);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"An error is occurred {ex.Message}");
            }

            Console.WriteLine();
            
            // fileInfo
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);

                // Display file operation
                Console.WriteLine($"File name : {fileInfo.Name}");
                Console.WriteLine($"File Directory Name : {fileInfo.Directory}");
                Console.WriteLine($"File Size: {fileInfo.Length} bytes");
                Console.WriteLine($"Creation Time: {fileInfo.CreationTime} bytes");
                Console.WriteLine($"Last Access Time: {fileInfo.LastAccessTime} bytes");
                Console.WriteLine($"Last Write Time: {fileInfo.LastWriteTime} bytes");

                // to check file is exists or not
                if(fileInfo.Exists)
                {
                    Console.WriteLine("File is exists");

                    File.Copy(filePath, destinationFilePath);       // copy the file
                    Console.WriteLine("Copied file successfully") ;
                    File.Delete(destinationFilePath);      // delete the file 
                    Console.WriteLine("Delete the file successfully") ;
                }
                else
                {
                    Console.WriteLine("File does not exists");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
