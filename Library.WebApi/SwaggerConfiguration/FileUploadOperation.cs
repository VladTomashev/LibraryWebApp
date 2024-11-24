using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Library.WebApi.SwaggerConfiguration
{
    public class FileUploadOperation : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Проверяем, что operation и context не равны null
            if (operation == null || context == null)
                return;

            // Проверяем, что ParameterDescriptions существует
            var fileParams = context.ApiDescription?.ParameterDescriptions?
                .Where(p => p.ModelMetadata?.ModelType == typeof(IFormFile))
                .ToList();

            if (fileParams != null && fileParams.Any())
            {
                foreach (var param in fileParams)
                {
                    // Проверяем, что Parameters инициализированы
                    if (operation.Parameters != null)
                    {
                        var existingParam = operation.Parameters.FirstOrDefault(p => p.Name == param.Name);
                        if (existingParam != null)
                        {
                            operation.Parameters.Remove(existingParam);
                        }
                    }

                    // Проверяем, что Content инициализирован
                    operation.RequestBody ??= new OpenApiRequestBody
                    {
                        Content = new Dictionary<string, OpenApiMediaType>()
                    };

                    if (!operation.RequestBody.Content.ContainsKey("multipart/form-data"))
                    {
                        operation.RequestBody.Content["multipart/form-data"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties = new Dictionary<string, OpenApiSchema>()
                            }
                        };
                    }

                    operation.RequestBody.Content["multipart/form-data"].Schema.Properties[param.Name] = new OpenApiSchema
                    {
                        Type = "string",
                        Format = "binary"
                    };
                }
            }
        }
    }
}
