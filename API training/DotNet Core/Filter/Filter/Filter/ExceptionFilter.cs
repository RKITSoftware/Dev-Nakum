using Microsoft.AspNetCore.Mvc.Filters;

namespace Filter.Filter
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        private readonly string _name;
        public ExceptionFilter(string name)
        {
            _name = name;
        }

        public void OnException(ExceptionContext context)
        {
            Console.WriteLine($"on exception :: {_name} :: {context.Exception}");
        }
    }
}
