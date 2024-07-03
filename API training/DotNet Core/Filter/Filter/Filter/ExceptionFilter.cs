using Microsoft.AspNetCore.Mvc.Filters;

namespace Filter.Filter
{
    /// <summary>
    /// exception filter
    /// </summary>
    public class ExceptionFilter : Attribute, IExceptionFilter
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
        public ExceptionFilter(string name)
        {
            _name = name;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// when exception will raised
        /// </summary>
        /// <param name="context">exception context</param>
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine($"on exception :: {_name} :: {context.Exception.Message}");
        }
        #endregion
    }

    /// <summary>
    /// exception filter
    /// </summary>
    public class ExceptionAsyncFilter : Attribute, IAsyncExceptionFilter
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
        public ExceptionAsyncFilter(string name)
        {
            _name = name;
        }
        #endregion

        #region Public Method

        /// <summary>
        /// on exception async
        /// </summary>
        /// <param name="context">exception context</param>
        /// <returns></returns>
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            Console.WriteLine($"on async exception :: {_name} :: {context.Exception.Message}");
        }


        #endregion
    }
}
