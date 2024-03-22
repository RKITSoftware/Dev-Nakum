using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filter.Filter
{
    public class ResourceFilterAttribute : Attribute, IResourceFilter
    {
        private readonly string _name;
        public ResourceFilterAttribute(string name)
        {
            _name = name;
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine($"after :: resource :: {_name}");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (context.HttpContext.Request.Method == HttpMethods.Get && context.HttpContext.Request.RouteValues.TryGetValue("id", out var idString))
            {
                if (!int.TryParse(idString.ToString(), out _))
                {
                    context.Result = new BadRequestResult();
                    throw new Exception("from resources :: id is not valid");
                }
            }
            Console.WriteLine($"before :: resource :: {_name}");
        }
    }
}
