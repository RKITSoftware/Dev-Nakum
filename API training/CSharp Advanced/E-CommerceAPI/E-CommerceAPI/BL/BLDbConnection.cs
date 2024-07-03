using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack.Data;
using System.IO;
using System.Web;

namespace E_CommerceAPI.BL
{
    /// <summary>
    /// Db connection configuration
    /// </summary>
    public class BLDbConnection
    {
        #region Public Properties
        public static IDbConnectionFactory Instance { get; set; }
        #endregion

        #region Public Method

        /// <summary>
        /// Get the connection string from db.json file
        /// </summary>
        /// <returns>connection string</returns>
        public static string GetConnectionString()
        {
            // get path to App_Data folder
            string appDataPath = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data");
            string jsonFilePath = Path.Combine(appDataPath, "db.json");

            // get json object from json file
            string jsonString = File.ReadAllText(jsonFilePath);
            JObject jsonObject = JsonConvert.DeserializeObject<JObject>(jsonString);

            string connectionString = jsonObject["ConnectionString"].ToString();
            return connectionString;
        }
        #endregion
    }
}