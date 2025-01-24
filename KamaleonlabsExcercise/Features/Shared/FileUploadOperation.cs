using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace KamaleonlabsExcercise.Features.Shared;

public class FileUploadSchemaFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Verifica si el modelo tiene propiedades del tipo IFormFile
        var parametersWithIFormFile = context.MethodInfo.GetParameters()
            .SelectMany(p => p.ParameterType.GetProperties())
            .Where(p => p.PropertyType == typeof(IFormFile));

        if (parametersWithIFormFile.Any())
        {
            // Limpia parámetros preexistentes si los hay
            operation.Parameters.Clear();

            // Configura el body como "multipart/form-data"
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
                                // Define las propiedades explícitamente
                                ["Title"] = new OpenApiSchema { Type = "string" },
                                ["Body"] = new OpenApiSchema { Type = "string" },
                                ["File"] = new OpenApiSchema { Type = "string", Format = "binary" }
                            },
                            Required = new HashSet<string> { "Title"} // Propiedades requeridas
                        }
                    }
                }
            };
        }
    }
}