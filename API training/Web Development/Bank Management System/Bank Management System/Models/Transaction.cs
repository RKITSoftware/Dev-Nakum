using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bank_Management_System.Models
{
    /// <summary>
    /// class which contains the schema of the transactions
    /// </summary>
    public class Transaction
    {
        public int Id { get; set; }

        public int UserId { get; set; } 
        public int Money { get; set; }

        // Deposit or withdraw
        public string Type { get; set; }

        public static List<Transaction> lstTransactions = new List<Transaction>();
    }
}