using MySql.Data.MySqlClient;
using ORM.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ORM.Business_Logic
{
    /// <summary>
    /// Main logic of students api
    /// </summary>
    public class BLStudent
    {
        #region Private Member
        private readonly IDbConnectionFactory _dbFactory;
        private static int _id = 1;
        #endregion

        #region Constructor
        public BLStudent()
        {
            this._dbFactory = BLDbConnection.Instance;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// Get all the students from database
        /// </summary>
        /// <returns>List of the students</returns>
        public List<Stu01> GetAllStudents()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Select<Stu01>();
            }
        }

        /// <summary>
        /// Get the student info from student id
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>student info</returns>
        public Stu01 GetStudentById(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Single<Stu01>(x => x.U01F01 == id);
            }
        }

        /// <summary>
        /// Add student data into Database
        /// </summary>
        /// <param name="studentData"></param>
        /// <returns>True if rows affected > 0 and false if 0</returns>
        public bool AddStudent(Stu01 studentData)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                try
                {
                    Stu01 objStudent = new Stu01();
                    objStudent.U01F01 = _id++;
                    objStudent.U01F02 = studentData.U01F02;
                    objStudent.U01F03 = studentData.U01F03;

                    db.Insert(objStudent);
                    return true;
                }
                catch (MySqlException)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// update student based on student id
        /// </summary>
        /// <param name="id">student id</param>
        /// <param name="studentData"></param>
        /// <returns></returns>
        public bool UpdateStudent(int id, Stu01 studentData)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                int updatedRows = db.Update(new Stu01
                {
                    U01F01 = id,
                    U01F02 = studentData.U01F02,
                    U01F03 = studentData.U01F03,
                }, s => s.U01F01 == id);


                return updatedRows > 0;
            }
        }

        /// <summary>
        /// Delete the student based on student id
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns></returns>
        public bool DeleteStudent(int id)
        {
            using(IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Delete<Stu01>(s=>s.U01F01==id) > 0;
            }
        }
        #endregion
    }
}