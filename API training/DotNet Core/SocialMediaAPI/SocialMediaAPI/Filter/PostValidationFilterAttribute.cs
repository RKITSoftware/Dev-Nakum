using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Filter
{
    /// <summary>
    /// This class implements a post validation filter attribute. 
    /// </summary>
    public class PostValidationFilterAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// This method is not used in the current implementation 
        /// </summary>
        /// <param name="context">The action execution context.</param>
        public void OnActionExecuted(ActionExecutedContext context) { }

        /// <summary>
        /// This method is called before the action method is executed.
        /// It retrieves the post data model from the action arguments and performs basic validation.
        /// </summary>
        /// <param name="context">The action execution context.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Retrieve the post model from action arguments with expected name "objDtoPos01" 
            var model = context.ActionArguments["objDtoPos01"] as DtoPos01;

            // Check if the model is null
            if (model == null)
            {
                // If the model is null, return a bad request response with a specific message
                context.Result = new BadRequestObjectResult("Post data is missing.");
                return;
            }

            // Perform basic validation on the content property (S01102) of the model
            if (string.IsNullOrEmpty(model.S01F04))
            {
                // If the content is missing or empty, return a bad request response with a specific message
                context.Result = new BadRequestObjectResult("Post content is required.");
                return;
            }
        }
    }
}