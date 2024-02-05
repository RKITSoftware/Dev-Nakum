using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using DataBase_With_C_.Models;
using MySql.Data.MySqlClient;

namespace DataBase_With_C_.Business_Logic
{
    /// <summary>
    /// Business Logic class for handling operations related to student data.
    /// </summary>
    public class BLStudents
    {
        #region Private Methods

        /// <summary>
        /// Get the database connection string from a JSON file.
        /// </summary>
        private string GetConnectionString()
        {
            // Get path to the App_Data folder
            string appDataPath = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data");
            string jsonFilePath = Path.Combine(appDataPath, "db.json");

            // Get JSON Object from the JSON file
            string jsonString = File.ReadAllText(jsonFilePath);
            JObject jsonObject = JsonConvert.DeserializeObject<JObject>(jsonString);

            // Get Connection String from JsonObject
            string connectionString = jsonObject["ConnectionString"].ToString();
            return connectionString;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get all students from the database.
        /// </summary>
        public List<Stu01> GetAllStudents()
        {
            MySqlConnection objMySqlConnection = new MySqlConnection(GetConnectionString());
            objMySqlConnection.Open();

            string query = @"select
                                U01F01,
                                U01F02,
                                U01F03
                            from
                                Stu01";

            MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
            MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();

            List<Stu01> lstStu01 = new List<Stu01>();
            while (objMySqlDataReader.Read())
            {
                lstStu01.Add(new Stu01()
                {
                    U01F01 = Convert.ToInt32(objMySqlDataReader["U01F01"]),
                    U01F02 = objMySqlDataReader["U01F02"].ToString(),
                    U01F03 = Convert.ToInt32(objMySqlDataReader["U01F03"]),
                });
            }

            objMySqlConnection.Close();
            return lstStu01;
        }

        /// <summary>
        /// Get a specific student by ID from the database.
        /// </summary>
        public Stu01 GetStudent(int id)
        {
            MySqlConnection objMySqlConnection = new MySqlConnection(GetConnectionString());
            objMySqlConnection.Open();

            string query = @"select
                                U01F01,
                                U01F02,
                                U01F03
                            from
                                Stu01
                            where 
                                U01F01 = @U01F01";

            MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
            objMySqlCommand.Parameters.AddWithValue("@U01F01", id);

            MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();

            while (objMySqlDataReader.Read())
            {
                Stu01 objStu01 = new Stu01()
                {
                    U01F01 = Convert.ToInt32(objMySqlDataReader["U01F01"]),
                    U01F02 = objMySqlDataReader["U01F02"].ToString(),
                    U01F03 = Convert.ToInt32(objMySqlDataReader["U01F03"]),
                };

                objMySqlConnection.Close();
                return objStu01;
            }

            objMySqlConnection.Close();
            return null;
        }

        /// <summary>
        /// Add a new student to the database.
        /// </summary>
        public bool AddStudent(Stu01 objStu01)
        {
            MySqlConnection objMySqlConnection = new MySqlConnection(GetConnectionString());
            objMySqlConnection.Open();

            string query = @"insert into 
                                Stu01 (U01F02,
                                        U01F03) 
                                values (@U01F02,
                                        @U01F03)";

            MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
            objMySqlCommand.Parameters.AddWithValue("@U01F02", objStu01.U01F02);
            objMySqlCommand.Parameters.AddWithValue("@U01F03", objStu01.U01F03);

            return objMySqlCommand.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Update an existing student in the database by ID.
        /// </summary>
        public bool UpdateStudent(int id, Stu01 objStu01)
        {
            MySqlConnection objMySqlConnection = new MySqlConnection(GetConnectionString());
            objMySqlConnection.Open();

            string query = @"update
                                Stu01
                            set
                                U01F02 = @U01F02,
                                U01F03 = @U01F03
                            where
                                U01F01 = @U01F01";

            MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
            objMySqlCommand.Parameters.AddWithValue("@U01F01", id);
            objMySqlCommand.Parameters.AddWithValue("@U01F02", objStu01.U01F02);
            objMySqlCommand.Parameters.AddWithValue("@U01F03", objStu01.U01F03);

            return objMySqlCommand.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Delete a student from the database by ID.
        /// </summary>
        public bool DeleteStudent(int id)
        {
            MySqlConnection objMySqlConnection = new MySqlConnection(GetConnectionString());
            objMySqlConnection.Open();

            string query = @"delete from
                                Stu01
                            where
                                U01F01 = @U01F01";

            MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
            objMySqlCommand.Parameters.AddWithValue("@U01F01", id);

            return objMySqlCommand.ExecuteNonQuery() > 0;
        }

        #endregion
    }
}