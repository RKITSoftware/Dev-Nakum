using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack.Data;
using System.IO;
using System.Web;

namespace Bank_Management_System.Business_Logic
{
    /// <summary>
    /// Db Connection configuration
    /// </summary>
    public class BLDbConnection
    {
        #region Public Member

        /// <summary>
        /// create the static instance of connnection factory
        /// </summary>
        public static IDbConnectionFactory Instance { get; set; }
        #endregion

        #region Public Method
        /// <summary>
        /// Get the connection string from db.json file
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            // get the path of "App_Data"
            string appDataPath = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data");
            string jsonFilePath = Path.Combine(appDataPath, "db.json");

            // get the json object 
            string jsonString = File.ReadAllText(jsonFilePath);
            JObject jsonObject = JsonConvert.DeserializeObject<JObject>(jsonString);
                
            // get the connection string
            string connectionString = jsonObject["ConnectionString"].ToString();
            return connectionString;
        }
        #endregion
    }
}