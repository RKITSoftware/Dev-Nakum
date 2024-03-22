using Microsoft.AspNetCore.Mvc.Filters;

namespace Filter.Filter
{
    public class ResultFilter : Attribute, IResultFilter
    {
        private readonly string _name;
        public ResultFilter(string name) 
        {
            _name  = name;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine($"after :: result :: {_name}");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine($"before :: result :: {_name}");
        }
    }
}
