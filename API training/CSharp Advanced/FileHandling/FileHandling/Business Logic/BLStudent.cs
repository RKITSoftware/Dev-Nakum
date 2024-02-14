using FileHandling.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Net.Http.Headers;
using System.Web.Http;

namespace FileHandling.Business_Logic
{
    /// <summary>
    /// Manage the all the logic related to file and students
    /// </summary>
    public class BLStudent
    {
        #region Private Member
        private static List<Students> _lstStudents = new List<Students>();
        private static int _id = 1;
        private static string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileUpload", "studentData.txt");
        private static string _filePathCopy = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileUploadCopy", "studentData.txt");
        private static string _filePathDestination = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileUploadByUser");
        #endregion


        #region Public Methods
        /// <summary>
        /// get all the students which is listed
        /// </summary>
        /// <returns></returns>
        public static List<Students> GetAllStudents() => _lstStudents;

        /// <summary>
        /// get the student info based on student id
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns></returns>
        public static Students GetStudentById(int id) => _lstStudents.FirstOrDefault(s => s.Id == id);

        /// <summary>
        /// create the students
        /// </summary>
        /// <param name="students">request body</param>
        /// <returns></returns>
        public static Students CreateStudent(Students students)
        {
            Students objStudents = new Students();
            objStudents.Id = _id++;
            objStudents.Name = students.Name;
            objStudents.Age = students.Age;
            
            _lstStudents.Add(objStudents);
            return objStudents;
        }

        /// <summary>
        /// Delete the student based on student id
        /// </summary>
        /// <param name="objStudents">student id</param>
        /// <returns></returns>
        public static bool DeleteStudentById(Students objStudents) => _lstStudents.Remove(objStudents);

        /// <summary>
        /// write into file
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string FileWrite()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    File.Create(_filePath);
                }

                using (StreamWriter objStreamWriter = new StreamWriter(_filePath))
                {
                    foreach (Students student in _lstStudents)
                    {
                        objStreamWriter.WriteLine($"{student.Id}, {student.Name}, {student.Age}");
                        objStreamWriter.WriteLine();
                    }
                }

                return "file written Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception("Error writing file.", ex);
            }
            
        }

        /// <summary>
        /// upload the file and store it into destination folder
        /// </summary>
        /// <returns></returns>
        public static bool UploadFile()
        {
            // Get file from the request.

            var request = HttpContext.Current.Request;

            // If request body consists file
            if (request.Files.Count > 0)
            {
                foreach (string fileName in request.Files)
                {
                    var file = request.Files[fileName];

                    // filepath = path + fileName
                    string filePath = $"{_filePathDestination}\\{file.FileName}";

                    // Check if filePath exists already

                    if (File.Exists(filePath))
                    {
                        return false;
                    }

                    file.SaveAs(filePath);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// read the file line by line 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string ReadData()
        {
            string str = "";
            string readFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileUpload", "studentData.txt");
            if (File.Exists(readFilePath))
            {
                using (StreamReader sr = new StreamReader(readFilePath))
                {
                    while (!sr.EndOfStream)
                    {
                        str = str + sr.ReadLine();
                    }
                }

                return str;
            }

            throw new Exception("Not found");
        }

        /// <summary>
        /// delete the file 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string DeleteFile()
        {
            if (!File.Exists(_filePath))
            {
                throw new Exception("File not found");
            }
            File.Delete(_filePath);
            return "Successfully delete the file";
        }

        /// <summary>
        /// fileInto related operation
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        public static object FileInfo()
        {
            try
            {
                FileInfo fileInfo = new FileInfo(_filePath);
                if (fileInfo.Exists)
                {
                    if(File.Exists(_filePathCopy))
                    {
                        File.Delete(_filePathCopy);
                    }
                    File.Copy(_filePath, _filePathCopy);
                }
                else
                {
                    throw new FileNotFoundException();
                }

                var fileInfoObject = new
                {
                    FileName = fileInfo.Name,
                    FileDirectory = fileInfo.Directory.FullName,
                    FileSize = fileInfo.Length,
                    CreationTime = fileInfo.CreationTime,
                    LastAccessTime = fileInfo.LastAccessTime,
                    LastWriteTime = fileInfo.LastWriteTime
                };
                return fileInfoObject;


                // to check file is exists or not

            }
            catch (IOException ex)
            {
                throw new Exception(ex.Message);
            }

        }

        #endregion
    }
}