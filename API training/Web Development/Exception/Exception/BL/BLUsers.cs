using Exception.Models;
using System.Collections.Generic;
using System.Linq;

namespace Exception.BL
{
    /// <summary>
    /// Manage the user's services
    /// </summary>
    public class BLUsers
    {
        #region Private Member

        /// <summary>
        /// static list that contains the user details 
        /// </summary>
        private static List<Info> _lstInfo = new List<Info>()
        {
            new Info { Id=1, Name="Dev",City="Surat"},
            new Info { Id=2, Name="Raj",City="Porbandar"},
            new Info { Id=3, Name="Tushar",City="Mumbai"},
            new Info { Id=4, Name="Kishan",City="Surat"},
        };
        #endregion

        #region Public Method

        /// <summary>
        /// Get the user details based on user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Info GetUser(int id)
        {
            Info user = _lstInfo.FirstOrDefault(x => x.Id == id);
            return user;
        }

        #endregion
    }
}