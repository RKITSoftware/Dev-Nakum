using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Authentication_Authorization.Models
{
    public class Users
    {
        #region Public Properties
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        #endregion
        
        public static List<Users> GetAllUser()
        {
            List<Users> lstUser = new List<Users>()
            {
                new Users() { Id = 1,Username="deven",Password="Abcd@123",Role="admin"},
                new Users() { Id = 2,Username="dev",Password="Abcd@123",Role="user"},
                new Users() { Id = 3,Username="raj",Password="Abcd@123",Role="user"},
                new Users() { Id = 4,Username="tushar",Password="Abcd@123",Role="user"},
                new Users() { Id = 5,Username="pratham",Password="Abcd@123",Role="user"},
            };

            return lstUser;
        }

    }
}