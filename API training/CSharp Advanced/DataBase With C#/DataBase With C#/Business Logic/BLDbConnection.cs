using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Web;

namespace DataBase_With_C_.Business_Logic
{
    /// <summary>
    /// Db connection configuration
    /// </summary>
    public class BLDbConnection
    {
        #region Public method 
        /// <summary>
        /// Get the connection string from db.json file
        /// </summary>
        /// <returns>connection string</returns>
        public static string GetConnectionString()
        {
            // Get path to App_Data folder
            string appDataPath = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data");
            string jsonFilePath = Path.Combine(appDataPath, "db.json");

            //Get Json object from json file
            string jsonString = File.ReadAllText(jsonFilePath);
            JObject jsonObject = JsonConvert.DeserializeObject<JObject>(jsonString);

            //Get connection string from JsonObject
            string connectionString = jsonObject["ConnectionString"].ToString();
            return connectionString;
        }
        #endregion
    }
}