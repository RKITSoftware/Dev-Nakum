using System.Web.Caching;

namespace BMS.BL
{
    public class BLCache
    {
        #region Private Member
        /// <summary>
        /// Create the object of cache
        /// </summary>
        private static Cache _cache = new Cache();
        #endregion


        #region Public Method
        /// <summary>
        /// Retrieves the cached object associated with the specified key.
        /// </summary>
        /// <param name="key">The key used to identify the cached object.</param>
        /// <returns>The cached object or null if the key is not found.</returns>

        public static object Get(string key)
        {
            return _cache.Get(key);
        }

        // <summary>
        /// Adds an object to the cache using the specified key.
        /// </summary>
        /// <param name="key">The key used to identify the cached object.</param>
        /// <param name="value">The value to be cached.</param>
        public static void Add(string key, object value)
        {
            _cache.Insert(key, value);
        }

        /// <summary>
        /// Removes the object with the specified key from the cache.
        /// </summary>
        /// <param name="key">The key used to identify the cached object to be removed.</param>
        public static void Remove(string key)
        {
            _cache.Remove(key);
        }
        #endregion
    }
}