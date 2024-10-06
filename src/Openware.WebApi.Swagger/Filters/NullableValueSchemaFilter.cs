using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Openware.WebApi.Swagger;

public class NullableValueSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsGenericType && context.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            if (schema.Example == null)
            {
                schema.Example = new OpenApiString(null);

            }
        }
    }
}