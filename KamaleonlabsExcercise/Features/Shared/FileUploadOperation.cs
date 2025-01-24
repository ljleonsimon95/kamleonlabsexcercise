using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace KamaleonlabsExercise.Features.Shared;

/// <summary>
/// FileUploadSchemaFilter modifies the OpenApiOperation to include file upload capability.
/// </summary>
public class FileUploadSchemaFilter : IOperationFilter
{
    /// <summary>
    /// Applies the filter to the specified OpenApiOperation, adding support for IFormFile parameters.
    /// </summary>
    /// <param name="operation">The operation to modify.</param>
    /// <param name="context">The filter context.</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var parametersWithIFormFile = context.MethodInfo.GetParameters()
            .SelectMany(p => p.ParameterType.GetProperties())
            .Where(p => p.PropertyType == typeof(IFormFile));

        if (parametersWithIFormFile.Any())
        {
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = new Dictionary<string, OpenApiSchema>
                            {
                                ["File"] = new OpenApiSchema { Type = "string", Format = "binary" }
                            },
                            Required = new HashSet<string> { }
                        }
                    }
                }
            };
        }
    }
}