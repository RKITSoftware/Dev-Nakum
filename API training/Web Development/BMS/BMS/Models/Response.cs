﻿using System.Data;

namespace BMS.Models
{
    /// <summary>
    /// Manage the schema of response
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Error is exists or not
        /// </summary>
        public bool IsError { get; set; } = false;

        /// <summary>
        /// Message 
        /// </summary>
        public string Message { get; set; } = "";


        /// <summary>
        /// Data which you want to display
        /// </summary>
        public DataTable Data{ get; set; }
    }
}