using Microsoft.AspNetCore.Mvc.Filters;

namespace Filter.Filter
{
    public class ActionFilterAttribute : Attribute, IActionFilter
    {
        private readonly string _name;
        public ActionFilterAttribute(string name)
        {
            _name = name;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"after :: action :: {_name}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"before :: action :: {_name}");
        }
    }
}
