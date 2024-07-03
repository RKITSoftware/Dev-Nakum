using FileHandling.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FileHandling.Business_Logic
{
    /// <summary>
    /// Manage the all the logic related to file and students
    /// </summary>
    public class BLStudent
    {
        #region Private Member
        /// <summary>
        /// list of the all user
        /// </summary>
        private static List<Students> _lstStudents = new List<Students>();

        /// <summary>
        /// Manage the student id
        /// </summary>
        private static int _id = 1;

        /// <summary>
        /// file path for write the file
        /// </summary>
        private static string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileUpload", "studentData.txt");

        /// <summary>
        /// file path for create the copy of the file
        /// </summary>
        private static string _filePathCopy = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileUploadCopy", "studentData.txt");

        /// <summary>
        /// file path for upload the file
        /// </summary>
        private static string _filePathDestination = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileUploadByUser");
        #endregion

        #region Public Methods
        /// <summary>
        /// get all the students which is listed
        /// </summary>
        /// <returns>list of the student</returns>
        public List<Students> GetAllStudents()
        {
            return _lstStudents;
        }

        /// <summary>
        /// get the student info based on student id
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>student object</returns>
        public Students GetStudentById(int id)
        {
            return _lstStudents.FirstOrDefault(s => s.Id == id);
        }

        /// <summary>
        /// create the students
        /// </summary>
        /// <param name="students">student object</param>
        /// <returns></returns>
        public void CreateStudent(Students objStudents)
        {
            objStudents.Id = _id++;
            _lstStudents.Add(objStudents);
        }

        /// <summary>
        /// Delete the student based on student Id
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>true if student is successfully deleted or else false</returns>
        public bool DeleteStudentById(int id)
        {
            // get the student object based on student id
            Students objStudents = GetStudentById(id);

            // if student is not found
            if (objStudents == null)
            {
                return false;
            }

            ///  remove the student from list
            _lstStudents.Remove(objStudents);
            return true;
        }

        /// <summary>
        /// write into file
        /// </summary>
        /// <returns>response messages</returns>
        /// <exception cref="Exception"></exception>
        public string FileWrite()
        {
            try
            {
                // to check file is exist or not
                if (!File.Exists(_filePath))
                {
                    // create the file
                    File.Create(_filePath);
                }

                //  higher level abstraction, automatic encoding and safer and easier - not needed to closed
                // write into file
                using (StreamWriter objStreamWriter = new StreamWriter(_filePath))
                {
                    foreach (Students student in _lstStudents)
                    {
                        objStreamWriter.WriteLine($"Id: {student.Id}\n Name: {student.Name}\n Age: {student.Age}\n\n");
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
        public bool UploadFile()
        {
            // Get file from the request.
            var request = HttpContext.Current.Request;

            // If request body contains the file
            if (request.Files.Count > 0)
            {
                foreach (string fileName in request.Files)
                {
                    var file = request.Files[fileName];

                    // file Path = path + fileName
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
        /// <returns>response messages</returns>
        /// <exception cref="Exception"></exception>
        public string ReadData()
        {
            string str = "";
            string readFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileUpload", "studentData.txt");
            if (File.Exists(readFilePath))
            {
                //  higher level abstraction, automatic encoding and safer and easier - no needed to closed
                using (StreamReader sr = new StreamReader(readFilePath))
                {
                    while (!sr.EndOfStream)
                    {
                        str += sr.ReadLine();
                    }
                }
                return str;
            }

            return "File is not found";
        }

        /// <summary>
        /// delete the file 
        /// </summary>
        /// <returns>true if file is deleted successfully or else false</returns>
        /// <exception cref="Exception"></exception>
        public bool DeleteFile()
        {
            // to check file is exist or not
            if (!File.Exists(_filePath))
            {
                return false;
            }
            File.Delete(_filePath);
            return true;
        }

        /// <summary>
        /// fileInto related operation
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        public object FileInfo()
        {
            try
            {
                FileInfo fileInfo = new FileInfo(_filePath);
                if (fileInfo.Exists)
                {
                    // to check file is exists or not
                    if (File.Exists(_filePathCopy))
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
            }
            catch (IOException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}