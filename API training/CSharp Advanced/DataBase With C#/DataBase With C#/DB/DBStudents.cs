using DataBase_With_C_.Business_Logic;
using DataBase_With_C_.Models.POCO;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataBase_With_C_.DB
{
    /// <summary>
    /// class that manage the related of database
    /// </summary>
    public class DBStudents
    {
        #region Public Method
        /// <summary>
        /// Get all students from the database.
        /// </summary>
        public DataTable GetAllStudents()
        {
            using (MySqlConnection objMySqlConnection = new MySqlConnection(BLDbConnection.GetConnectionString()))
            {
                objMySqlConnection.Open();

                string query = @"select
                                        U01F01,
                                        U01F02,
                                        U01F03
                                    from
                                        Stu01";

                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
                MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();

                DataTable dtGetAllStudents = new DataTable();
                dtGetAllStudents.Load(objMySqlDataReader);

                return dtGetAllStudents;
            }
        }

        /// <summary>
        /// Get a specific student by ID from the database.
        /// </summary>
        public DataTable GetStudents(int id)
        {
            using (MySqlConnection objMySqlConnection = new MySqlConnection(BLDbConnection.GetConnectionString()))
            {
                objMySqlConnection.Open();

                ///?
                string query = @"SELECT
                                        U01F01,
                                        U01F02,
                                        U01F03
                                    FROM
                                        Stu01
                                    WHERE 
                                        U01F01 = @U01F01";

                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
                objMySqlCommand.Parameters.AddWithValue("@U01F01", id);

                MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();

                DataTable dtGetStudent = new DataTable();
                dtGetStudent.Load(objMySqlDataReader);

                return dtGetStudent;
            }
        }

        /// <summary>
        /// Add students into database
        /// </summary>
        /// <param name="objStu01">object of the students</param>
        /// <returns>true if student is added or else false</returns>
        public bool AddStudents(Stu01 objStu01)
        {
            using (MySqlConnection objMySqlConnection = new MySqlConnection(BLDbConnection.GetConnectionString()))
            {
                objMySqlConnection.Open();
                string query = @"INSERT INTO 
                                            Stu01 (U01F02,U01F03,U01F04)
                                     VALUES 
                                            (@U01F02,
                                            @U01F03,
                                            @U01F04)";
                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);

                objMySqlCommand.Parameters.AddWithValue("@U01F02", objStu01.U01F02);
                objMySqlCommand.Parameters.AddWithValue("@U01F03", objStu01.U01F03);
                objMySqlCommand.Parameters.AddWithValue("@U01F04", objStu01.U01F04);
                return objMySqlCommand.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// update the students into database
        /// </summary>
        /// <param name="objStu01">object of the student</param>
        /// <returns>true if student is updated or else false</returns>
        public bool UpdateStudent(Stu01 objStu01)
        {
            using (MySqlConnection objMySqlConnection = new MySqlConnection(BLDbConnection.GetConnectionString()))
            {
                objMySqlConnection.Open();
                string query = @"UPDATE
                                            Stu01
                                        SET
                                            U01F02 = @U01F02,
                                            U01F03 = @U01F03,
                                            U01F04 = @U01F04
                                        WHERE
                                            U01F01 = @U01F01";
                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);

                objMySqlCommand.Parameters.AddWithValue("@U01F01", objStu01.U01F01);
                objMySqlCommand.Parameters.AddWithValue("@U01F02", objStu01.U01F02);
                objMySqlCommand.Parameters.AddWithValue("@U01F03", objStu01.U01F03);
                objMySqlCommand.Parameters.AddWithValue("@U01F04", objStu01.U01F04);

                return objMySqlCommand.ExecuteNonQuery() > 0;

            }
        }

        /// <summary>
        /// delete the student into database
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>true if student is deleted or else false</returns>
        public bool DeleteStudent(int id)
        {
            using (MySqlConnection objMySqlConnection = new MySqlConnection(BLDbConnection.GetConnectionString()))
            {
                objMySqlConnection.Open();
                string query = @"DELETE FROM
                                Stu01
                            WHERE
                                U01F01 = @U01F01";
                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);

                objMySqlCommand.Parameters.AddWithValue("@U01F01", id);

                return objMySqlCommand.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// to check email is unique into database
        /// </summary>
        /// <param name="email">student's email</param>
        /// <returns>true if email is unique or else false</returns>
        public bool IsUniqueEmail(string email)
        {
            using (MySqlConnection objMySqlConnection = new MySqlConnection(BLDbConnection.GetConnectionString()))
            {
                objMySqlConnection.Open();

                string query = @"SELECT 
                                    U01F04 
                                FROM 
                                    Stu01 
                                WHERE U01F04 = @U01F04";
                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
                objMySqlCommand.Parameters.AddWithValue("@U01F04", email);

                MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();


                bool isUnique = objMySqlDataReader.HasRows == false;

                return isUnique;
            }
        }

        /// <summary>
        /// to check student is exist or not
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>true if student is exist or else false</returns>
        public bool IsStudentExist(int id)
        {
            using (MySqlConnection objMySqlConnection = new MySqlConnection(BLDbConnection.GetConnectionString()))
            {
                objMySqlConnection.Open();

                //?  string.format
                string query = string.Format(@"select
                                U01F01,
                                U01F02,
                                U01F03
                            from
                                Stu01
                            where 
                                U01F01 = {0}",id);

                MySqlCommand objMySqlCommand = new MySqlCommand(query, objMySqlConnection);
                //objMySqlCommand.Parameters.AddWithValue("@U01F01", id);

                MySqlDataReader objMySqlDataReader = objMySqlCommand.ExecuteReader();

                return objMySqlDataReader.HasRows == true;
            }
        } 
        #endregion
    }
}