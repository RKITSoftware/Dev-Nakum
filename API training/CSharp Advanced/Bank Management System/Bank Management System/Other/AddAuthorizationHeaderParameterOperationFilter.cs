using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

/// <summary>
/// add the authorization header parameters
/// </summary>
public class AddAuthorizationHeaderParameterOperationFilter : IOperationFilter
{
    public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
    {
        var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline();
        var isAuthorized = filterPipeline
            .Select(filterInfo => filterInfo.Instance)
            .Any(filter => filter is AuthorizeAttribute);

        var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();

        if (isAuthorized && !allowAnonymous)
        {
            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }

            operation.parameters.Add(new Parameter
            {
                name = "Authorization",
                @in = "header",
                type = "string",
                required = true,
                description = "Bearer token",
                @default = "Bearer "
            });
        }
    }
}
