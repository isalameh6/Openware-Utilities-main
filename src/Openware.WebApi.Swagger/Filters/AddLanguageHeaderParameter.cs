using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Openware.Core.Helpers;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Openware.WebApi.Swagger;
public class AddLanguageHeaderParameter : IOperationFilter
{
    public string HeaderName { get; private set; } = "user-language";
    public string DefaultLanguage { get;private set; } = "en";

    public AddLanguageHeaderParameter()
    {

    }

    public AddLanguageHeaderParameter(string headerName,string defaultLanguage)
    {
        ExceptionHelper.ThrowIfNullOrEmpty(headerName);
        ExceptionHelper.ThrowIfNullOrEmpty(defaultLanguage);

        HeaderName = headerName;
        DefaultLanguage = defaultLanguage;
        
    }
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
        {
            operation.Parameters = new List<OpenApiParameter>();

        }

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = HeaderName,
            Description = "User Language",
            In = ParameterLocation.Header,
            Schema = new OpenApiSchema()
            {
                Type = "string",
                Example = new OpenApiString(DefaultLanguage)
            }
        });
    }
}
