using DataBase_With_C_.DB;
using DataBase_With_C_.Models;
using DataBase_With_C_.Models.DTO;
using DataBase_With_C_.Models.POCO;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DataBase_With_C_.Business_Logic
{
    /// <summary>
    /// Business Logic class for handling operations related to student data.
    /// </summary>
    public class BLStudents
    {
        #region Private Member
        /// <summary>
        /// Create the object of the student model
        /// </summary>
        private Stu01 _objStu01 = new Stu01();

        /// <summary>
        /// create the object of the DB class
        /// </summary>
        private readonly DBStudents _objDBStudents;
        #endregion

        #region Public Member

        /// <summary>
        /// Create the object of the response model
        /// </summary>
        public Response objResponse;
        #endregion

        #region Public Property

        /// <summary>
        /// Operation types A - Add, U - Update, D - Delete
        /// </summary>
        public EnmOperationTypes OperationTypes { get; set; }
        #endregion

        #region Constructor

        /// <summary>
        /// initialize the db class
        /// </summary>
        public BLStudents()
        {
            _objDBStudents = new DBStudents();
        }
        #endregion

        #region Private Method
        /// <summary>
        /// to check into database whether email is unique or not
        /// </summary>
        /// <param name="email">Response model</param>
        /// <returns>true if email is unique or else false</returns>
        private bool IsUniqueEmail(string email)
        {
            try
            {
                return _objDBStudents.IsUniqueEmail(email);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// to check student is exist or not based on student id
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>true if student is exist or else false</returns>
        private bool IsStudentExist(int id)
        {
            try
            {
                return _objDBStudents.IsStudentExist(id);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get all students from the database.
        /// </summary>
        public Response GetAllStudents()
        {
            objResponse = new Response();
            try
            {
                DataTable dtGetAllStudents = _objDBStudents.GetAllStudents();
                if (dtGetAllStudents.Rows.Count == 0)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Students is not found";
                }
                else
                {
                    objResponse.Data = dtGetAllStudents;
                }

                return objResponse;
            }
            catch (Exception ex)
            {
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }
        }

        /// <summary>
        /// Get a specific student by ID from the database.
        /// </summary>
        public Response GetStudent(int id)
        {
            objResponse = new Response();
            try
            {
                DataTable dtGetStudents = _objDBStudents.GetStudents(id);
                if (dtGetStudents.Rows.Count == 0)
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Student is not found";
                }
                else
                {
                    objResponse.Data = dtGetStudents;
                }

                return objResponse;
            }
            catch (Exception ex)
            {
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }
        }

        /// <summary>
        /// PreSave the record for insert or update the record
        /// </summary>
        /// <param name="objDtoStu01">Object of the student</param>
        /// <param name="id">student id</param>
        public void PreSave(DtoStu01 objDtoStu01, int id = 0)
        {
            _objStu01 = BLConvertModel.MapDtoToPoco<DtoStu01, Stu01>(objDtoStu01);
            if (OperationTypes == EnmOperationTypes.E)
            {
                _objStu01.U01F01 = id;
            }
        }

        /// <summary>
        ///  validate the record before insert or update the record
        /// </summary>
        /// <returns>response model</returns>
        public Response ValidationOnSave()
        {
            objResponse = new Response();
            if (!IsUniqueEmail(_objStu01.U01F04))
            {
                objResponse.IsError = true;
                objResponse.Message = "Email is already exists";
            }
            if (OperationTypes == EnmOperationTypes.E)
            {
                if (!IsStudentExist(_objStu01.U01F01))
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Student is not exist";
                }
            }
            return objResponse;
        }

        /// <summary>
        /// save the record into database
        /// </summary>
        /// <returns>response model</returns>
        public Response Save()
        {
            objResponse = new Response();
            try
            {
                if (OperationTypes == EnmOperationTypes.A)
                {
                    bool isAdded = _objDBStudents.AddStudents(_objStu01);
                    if (isAdded)
                    {
                        objResponse.Message = "Student added successfully";
                    }
                }
                else if (OperationTypes == EnmOperationTypes.E)
                {
                    bool isUpdated = _objDBStudents.UpdateStudent(_objStu01);
                    if (isUpdated)
                    {
                        objResponse.Message = "Student updated successfully";
                    }
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }
        }

        /// <summary>
        /// Validation before delete the student
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>response model</returns>
        public Response ValidationOnDelete(int id)
        {
            objResponse = new Response();
            if (OperationTypes == EnmOperationTypes.D)
            {
                if (!IsStudentExist(id))
                {
                    objResponse.IsError = true;
                    objResponse.Message = "Student is not exist";
                }
                else
                {
                    _objStu01.U01F01 = id;
                }
            }
            return objResponse;
        }

        /// <summary>
        /// Delete the student from database based on student id
        /// </summary>
        /// <returns>response model</returns>
        public Response Delete()
        {
            try
            {
                if (OperationTypes == EnmOperationTypes.D)
                {
                    bool isDeleted = _objDBStudents.DeleteStudent(_objStu01.U01F01);
                    if (isDeleted)
                    {
                        objResponse.Message = "Student deleted successfully";
                    }
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                objResponse.IsError = true;
                objResponse.Message = ex.Message;
                return objResponse;
            }
        }

        #endregion
    }
}