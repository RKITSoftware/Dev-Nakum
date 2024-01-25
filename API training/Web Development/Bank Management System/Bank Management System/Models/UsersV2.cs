using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bank_Management_System.Models
{
    public class UsersV2
    {
        #region Public Properties
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public int Money { get; set; }
        public string Role { get; set; }
        #endregion

        #region Public Member
        public static List<UsersV2> lstUsers = new List<UsersV2>()
        {
            new UsersV2(){Id=1, FullName="Dev Nakum",UserName="deven",Password="Abcd@123", Number="9856895874",Email="dev@gmail.com",Money=0,Role="Admin"},
            new UsersV2(){Id=2, FullName="Kishan Nakum",UserName="kishan",Password="Abcd@123", Number="7458962351",Email="kishan@gmail.com",Money=0,Role="User"},
        };

        #endregion
    }
}