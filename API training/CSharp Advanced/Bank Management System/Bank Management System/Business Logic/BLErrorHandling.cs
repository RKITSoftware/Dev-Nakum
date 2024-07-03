using System;
using System.IO;
using System.Web;

namespace Bank_Management_System.Business_Logic
{
    /// <summary>
    /// class for manage the error handling into file
    /// </summary>
    public static class BLErrorHandling
    {
        #region Private Member
        /// <summary>
        /// get the current date and time
        /// </summary>
        private static string _today = DateTime.Today.Date.ToString();

        /// <summary>
        /// file name - only date
        /// </summary>
        private static string _fileName = _today.Substring(0, 10) + ".txt";

        /// <summary>
        /// file path
        /// </summary>
        private static string _filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Logs"),_fileName);
        #endregion


        #region Private Method

        /// <summary>
        /// Create the file if file is not exist
        /// </summary>
        private static void CreateFile()
        {
            try
            {
                //needed if server is running more than one day - if not set date can not be changed
                _today = DateTime.Today.Date.ToString();
                _fileName = _today.Substring(0, 10) + ".txt";
                _filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Logs"), _fileName);

                // to check if file is exist or not 
                if (!File.Exists(_filePath))
                {
                    File.Create(_filePath).Close(); // Create and close
                }
            }
            catch (Exception ex)
            {
                // Handle potential file creation errors (consider logging or throwing a specific exception)
                throw ex;
            }
        }
        #endregion

        #region Public Method
        
        /// <summary>
        /// write the error message into file
        /// </summary>
        /// <param name="msg">error message</param>
        public static void WriteFile(string msg)
        {
            // create the file
            CreateFile();

            // write into file, true - append mode
            using (StreamWriter objStreamWriter = new StreamWriter(_filePath,true))
            {
                objStreamWriter.WriteLine(msg);
            }
        }
        #endregion
    }
}