using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Library.WebApi.SwaggerConfiguration
{
    public class FileUploadOperation : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var fileParams = context.ApiDescription.ParameterDescriptions
                .Where(p => p.ModelMetadata.ModelType == typeof(IFormFile))
                .ToList();

            if (fileParams.Any())
            {
                foreach (var param in fileParams)
                {
                    // Проверка, существует ли параметр
                    var existingParam = operation.Parameters.FirstOrDefault(p => p.Name == param.Name);
                    if (existingParam != null)
                    {
                        operation.Parameters.Remove(existingParam);
                    }

                    // Добавление схемы для multipart/form-data
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
                                        [param.Name] = new OpenApiSchema
                                        {
                                            Type = "string",
                                            Format = "binary"
                                        }
                                    }
                                }
                            }
                        }
                    };
                }
            }
        }
    }
}
