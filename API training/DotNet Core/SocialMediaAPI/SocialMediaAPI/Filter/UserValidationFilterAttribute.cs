using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialMediaAPI.Model.Dtos;

namespace SocialMediaAPI.Filter
{
    /// <summary>
    /// This class implements a user validation filter attribute. 
    /// </summary>
    public class UserValidationFilterAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// This method is not used in the current implementation 
        /// </summary>
        /// <param name="context">The action execution context.</param>
        public void OnActionExecuted(ActionExecutedContext context) { }

        /// <summary>
        /// This method is called before the action method is executed.
        /// It retrieves the user data model from the action arguments and performs basic validation.
        /// </summary>
        /// <param name="context">The action execution context.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Retrieve the user model from action arguments with expected name "objDtoUse01" 
            var model = context.ActionArguments["objDtoUse01"] as DtoUse01;

            // Check if the user model is null
            if (model == null)
            {
                // If the model is null, return a bad request response with a specific message
                context.Result = new BadRequestObjectResult("User data is missing.");
                return;
            }

            // Perform basic validation on required user properties
            if (string.IsNullOrEmpty(model.E01101))
            {
                context.Result = new BadRequestObjectResult("Username is required.");
                return;
            }

            if (string.IsNullOrEmpty(model.E01102))
            {
                context.Result = new BadRequestObjectResult("User's Email is required.");
                return;
            }

            if (string.IsNullOrEmpty(model.E01103))
            {
                context.Result = new BadRequestObjectResult("User's Password is required.");
                return;
            }

            if (string.IsNullOrEmpty(model.E01105))
            {
                context.Result = new BadRequestObjectResult("User's Bio is required.");
                return;
            }
        }
    }
}

