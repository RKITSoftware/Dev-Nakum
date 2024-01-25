using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bank_Management_System.Models
{
    /// <summary>
    /// class which contains the schema of the user
    /// </summary>
    public class UsersV1
    {
        #region Public Properties
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public int Money { get; set; }
        public string Role { get; set; }
        #endregion

        #region Public Member
        public static List<UsersV1> lstUsers = new List<UsersV1>()
        {
            new UsersV1(){Id=1, FirstName="Dev",LastName="Nakum",UserName="deven",Password="Abcd@123", Number="9856895874",Email="dev@gmail.com",Money=0,Role="Admin"},
            new UsersV1(){Id=2, FirstName="Kishan",LastName="Nakum",UserName="kishan",Password="Abcd@123", Number="7458962351",Email="kishan@gmail.com",Money=0,Role="User"},
        };

        #endregion
    }
}