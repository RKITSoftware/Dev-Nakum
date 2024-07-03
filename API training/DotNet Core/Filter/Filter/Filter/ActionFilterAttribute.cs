using Microsoft.AspNetCore.Mvc.Filters;

namespace Filter.Filter
{
    /// <summary>
    /// manage the action filter
    /// </summary>
    public class ActionFilterAttribute : Attribute, IActionFilter
    {
        #region Private Member
        /// <summary>
        /// action or controller name
        /// </summary>
        private readonly string _name;
        #endregion

        #region Constructor
        /// <summary>
        /// assign the name
        /// </summary>
        /// <param name="name"></param>
        public ActionFilterAttribute(string name)
        {
            _name = name;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// after action is executed
        /// </summary>
        /// <param name="context">action context</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"after :: action :: {_name}");
        }

        /// <summary>
        /// before action was executing
        /// </summary>
        /// <param name="context">action context</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"before :: action :: {_name}");
        }
        #endregion
    }

    /// <summary>
    /// action filter async
    /// </summary>
    public class ActionAsyncFilterAttribute : Attribute, IAsyncActionFilter, IOrderedFilter
    {
        #region Private Member
        /// <summary>
        /// action or controller name
        /// </summary>
        private readonly string _name;
        #endregion

        #region Public propeorties
        /// <summary>
        /// manage the order of execution
        /// </summary>
        public int Order { get; set; } 
        #endregion

        #region Constructor
        /// <summary>
        /// assign the name
        /// </summary>
        /// <param name="name"></param>
        public ActionAsyncFilterAttribute(string name, int order =0)
        {
            _name = name;
            Order = order;
        }

        #endregion

        #region Public Method

        /// <summary>
        /// while exception filter occurred
        /// </summary>
        /// <param name="context">exception context</param>
        /// <param name="next">delegate</param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine($"before :: action async :: {_name}");
            await next();
            Console.WriteLine($"after :: action async :: {_name}");
        }
        #endregion
    }
}
