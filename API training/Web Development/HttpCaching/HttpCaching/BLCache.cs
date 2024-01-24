using System.Web;
using System.Web.Caching;

namespace HttpCaching.Controllers
{
    /// <summary>
    /// class provides a simple caching mechanism for storing and retrieving data using ASP.NET Cache.
    /// This class encapsulates the caching logic, allowing easy integration of caching features in ASP.NET applications.
    /// </summary>
    public class BLCache
    {
        #region Private Member
        private static Cache _cache = null;
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the cache instance.
        /// If the cache is not set, it initializes it based on the current HTTP context or HttpRuntime.
        /// </summary
        public static Cache CacheInfo
        {
            get
            {
                if(_cache == null)
                {
                    _cache = HttpContext.Current == null ? HttpRuntime.Cache : HttpContext.Current.Cache;
                }
                return _cache;
            }
            set { _cache = value; }
        }
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

        /// <summary>
        /// Adds an object to the cache using the specified key.
        /// </summary>
        /// <param name="key">The key used to identify the cached object.</param>
        /// <param name="value">The object to be cached.</param>
        public static void Add(string key, object value)
        {

            CacheInfo.Insert(key, value);
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