using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Filter.Filter
{
    /// <summary>
    /// Resource filter
    /// </summary>
    public class ResourceFilterAttribute : Attribute, IResourceFilter
    {
        #region Private Member
        /// <summary>
        /// action or controller name
        /// </summary>
        private readonly string _name;

        #endregion

        #region Constructor

        /// <summary>
        /// initialize the name 
        /// </summary>
        /// <param name="name"></param>
        public ResourceFilterAttribute(string name)
        {
            _name = name;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// when resource will executed
        /// </summary>
        /// <param name="context">resource context</param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine($"after :: resource :: {_name}");
        }

        /// <summary>
        /// before resource will executing
        /// </summary>
        /// <param name="context">resource context</param>
        /// <exception cref="Exception"></exception>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine($"before :: resource :: {_name}");
        }

        #endregion
    }


    /// <summary>
    /// Resource filter async
    /// </summary>
    public class ResourceAsyncFilterAttribute : Attribute, IAsyncResourceFilter
    {
        #region Private Member
        /// <summary>
        /// action or controller name
        /// </summary>
        private readonly string _name;
        private readonly IMemoryCache _cache;

        #endregion

        #region Constructor

        /// <summary>
        /// initialize the name 
        /// </summary>
        /// <param name="name"></param>
        public ResourceAsyncFilterAttribute(string name, IMemoryCache cache)
        {
            _name = name;
            _cache = cache;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// on resource execution - async
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            Console.WriteLine($"before :: resource-async :: {_name}");

            //cache
            var routeData = context.RouteData;
            var idValue = routeData.Values["id"];

            var cacheKey = idValue;
            //var cachedData = _cache.Get(cacheKey);
            if (_cache.TryGetValue(cacheKey, out string cachedData))
            {
                context.Result = new ContentResult
                {
                    Content = cachedData,
                    StatusCode = StatusCodes.Status200OK,
                    ContentType = "application/json"
                };
                Console.WriteLine($"Using cached data for :: resource-async :: {_name} with cacheKey: {cacheKey}");
                return; // Short-circuit the pipeline, since we have cached data
            }
            else
            {
                var executedContext = await next();
                var result1= executedContext.Result;
                // After execution, cache the result if appropriate
                if (executedContext.Result is ObjectResult result && result.StatusCode == StatusCodes.Status200OK)
                {
                   var resultData = JsonSerializer.Serialize(result.Value);
                    _cache.Set(cacheKey, resultData, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3000)
                    });
                    Console.WriteLine($"Cached data for :: resource-async :: {_name}");
                }
            }

            Console.WriteLine($"After :: resource-async :: {_name}");
        }
        #endregion
    }
}
