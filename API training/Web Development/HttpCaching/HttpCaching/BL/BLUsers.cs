namespace HttpCaching.BL
{
    /// <summary>
    /// class can manage the users services
    /// </summary>
    public class BLUsers
    {
        #region Public Method

        /// <summary>
        /// Display users and added into cache storage
        /// </summary>
        /// <returns>users data</returns>
        public string[][] DisplayAndAddCache()
        {
            string[][] result = new string[5][]
            {
                new string[]{"Dev","Nakum"},
                new string[]{"Kishan","Nakum"},
                new string[]{"Raj","Mandaviya"},
                new string[]{"Pratham","Modi"},
                new string[]{"Tushar","Gohil"},
            };


            for (int i = 0; i < result.Length; i++)
            {
                BLCache.Add((i + 1).ToString(), result[i]);
            }

            return result;
        }

        /// <summary>
        /// Get user by id and remove the user form cache based on id
        /// </summary>
        /// <param name="id">user's id</param>
        /// <returns>user's details based on user id</returns>
        public object GetUserById(string id)
        {
            object data = BLCache.Get(id);
            if (data == null)
            {
                return null;
            }
            BLCache.Remove(id);
            return data;
        }
        #endregion
    }
}