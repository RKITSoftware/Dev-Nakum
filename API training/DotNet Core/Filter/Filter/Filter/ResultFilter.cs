using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filter.Filter
{
    /// <summary>
    /// result filter
    /// </summary>
    public class ResultFilter : Attribute, IResultFilter
    {
        #region Private Member
        /// <summary>
        /// controller or action name
        /// </summary>
        private readonly string _name;
        #endregion

        #region Constructor
        /// <summary>
        /// initialize the name
        /// </summary>
        /// <param name="name"></param>
        public ResultFilter(string name) 
        {
            _name  = name;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// when result will executed
        /// </summary>
        /// <param name="context">result context</param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                //objectResult.Value = new
                //{
                //    Data = objectResult.Value,
                //    Metadata = new
                //    {
                //        Timestamp = DateTime.UtcNow,
                //        RequestId = Guid.NewGuid()
                //    }
                //};
            Console.WriteLine($"after :: result :: {_name} :: {context.Result}");
            }
        }

        /// <summary>
        /// before result will executing
        /// </summary>
        /// <param name="context">result context</param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine($"before :: result :: {_name}");
        }
        #endregion
    }


    /// <summary>
    /// result filter async
    /// </summary>
    public class ResultAsyncFilter : Attribute, IAsyncResultFilter
    {
        #region Private Member
        /// <summary>
        /// controller or action name
        /// </summary>
        private readonly string _name;
        #endregion

        #region Constructor
        /// <summary>
        /// initialize the name
        /// </summary>
        /// <param name="name"></param>
        public ResultAsyncFilter(string name)
        {
            _name = name;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// on result execution
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            Console.WriteLine($"before :: result-async :: {_name}");
            await next();
            Console.WriteLine($"after :: result-async :: {_name}");
        }
        #endregion
    }
}
