using ORM.Models;
using ORM.Models.DTO;
using ORM.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ORM.Business_Logic
{
    /// <summary>
    /// Main logic of students api
    /// </summary>
    public class BLStudent
    {
        #region Private Member
        /// <summary>
        /// Create the object of the connection Factory
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Create the object student model
        /// </summary>
        private Stu01 _objStu01 = new Stu01();
        #endregion

        #region Public Member

        /// <summary>
        /// Create the object of the Response model
        /// </summary>
        public Response objResponse;
        #endregion

        #region Public Property
        /// <summary>
        /// Operation types A - Add, U - Update, D - Delete
        /// </summary>
        public enmOperationTypes OperationTypes { get; set; }
        #endregion

        #region Constructor

        /// <summary>
        ///  initialize the dbFactory
        /// </summary>
        public BLStudent()
        {
            _dbFactory = BLDbConnection.Instance;
        }
        #endregion

        #region Private Method
        private bool IsUniqueEmail(string email)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                //exist
                Stu01 user = db.Single<Stu01>(s => s.U01F04 == email);
                return user == null;
            }
        }

        /// <summary>
        /// To check the student is exist or not based on student id
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>true if student is exists or else false</returns>
        private bool IsStudentExist(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                Stu01 user = db.SingleById<Stu01>(id);
                if (OperationTypes == enmOperationTypes.D)
                {
                    _objStu01.U01F01 = id;      // for delete the student
                }
                return user != null;
            }
        }
        #endregion

        #region Public Method

        /// <summary>
        /// PreSave the details which is required for insert or update the student details
        /// </summary>
        /// <param name="objDtoStu01">object of the student model</param>
        /// <param name="id">student id, optional - required for update the student</param>
        public void PreSave(DtoStu01 objDtoStu01, int id = 0)   
        {
            _objStu01 = BLConvertModel.MapDtoToPoco<DtoStu01, Stu01>(objDtoStu01);
            if (OperationTypes == enmOperationTypes.U)
            {
                _objStu01.U01F01 = id;
            }
        }

        /// <summary>
        /// to check the student object details before changing the databased record
        /// </summary>
        /// <returns>response model</returns>
        public Response ValidationOnSave()
        {
            objResponse = new Response();
            if (OperationTypes == enmOperationTypes.U)
            {
                bool isExist = IsStudentExist(_objStu01.U01F01);
                if (!isExist)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "User is not exist";
                }
            }
            else if (OperationTypes == enmOperationTypes.A)
            {
                bool isUnique = IsUniqueEmail(_objStu01.U01F04);
                if (!isUnique)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Email is already an exist";
                }
            }
            return objResponse;
        }

        /// <summary>
        /// to check the student object details before changing the databased record
        /// </summary>
        /// <returns>response model</returns>
        public Response ValidationOnDelete(int id)
        {
            objResponse = new Response();
            if (OperationTypes == enmOperationTypes.D)
            {
                bool isExist = IsStudentExist(id);
                if (!isExist)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "User is not exist";
                }
            }
            return objResponse;
        }

        /// <summary>
        /// Save the details on data base
        /// </summary>
        /// <returns>response model</returns>
        public Response Save()
        {
            objResponse = new Response();
            if (OperationTypes == enmOperationTypes.A)
            {
                try
                {
                    using (IDbConnection db = _dbFactory.OpenDbConnection())
                    {
                        db.Insert(_objStu01);
                        objResponse.Message = "Student added successfully";
                    }
                }
                catch (Exception ex)
                {
                    objResponse.IsError = true;
                    objResponse.Message = $"something went wrong {ex.Message}";
                }
            }
            else if (OperationTypes == enmOperationTypes.U)
            {
                try
                {
                    using (IDbConnection db = _dbFactory.OpenDbConnection())
                    {
                        db.Update(_objStu01);
                        objResponse.Message = "Student details is successfully updated";
                    }
                }
                catch (Exception ex)
                {
                    objResponse.IsError = true;
                    objResponse.Message = $"something went wrong {ex.Message}";
                }
            }
            return objResponse;
        }

        /// <summary>
        /// Get all the students from database
        /// </summary>
        /// <returns>response model</returns>
        public Response GetAllStudents()
        {
            objResponse = new Response();
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    List<Stu01> lstStu01 = db.Select<Stu01>();

                    if (lstStu01.Count > 0)
                    {
                        objResponse.Data = lstStu01;
                    }
                    else
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "No student found";
                    }
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                objResponse.IsError = true;
                objResponse.Message = $"something went wrong {ex.Message}";
                return objResponse;
            }
        }

        /// <summary>
        /// Get the student info from student id
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>response model</returns>
        public Response GetStudentById(int id)
        {
            objResponse = new Response();
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    Stu01 objStu = db.Single<Stu01>(x => x.U01F01 == id);

                    if (objStu == null)
                    {
                        objResponse.IsError = true;
                        objResponse.Message = "Student is not found";
                    }
                    else
                    {
                        objResponse.Data = objStu;
                    }
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                objResponse.IsError = true;
                objResponse.Message = $"something went wrong {ex.Message}";
                return objResponse;
            }
        }

        /// <summary>
        /// Delete the student based on student id
        /// </summary>
        /// <returns>Response model</returns>
        public Response Delete()
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    bool isDeleted = db.Delete<Stu01>(s => s.U01F01 == _objStu01.U01F01) > 0;
                    objResponse.Message = "Student is deleted successfully";

                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                objResponse.IsError = true; 
                objResponse.Message = $"something went wrong {ex.Message}"; 
                return objResponse;
            }
        }
        #endregion
    }
}