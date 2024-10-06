using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

using Openware.Core.Helpers;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Openware.WebApi.Swagger;
public class AuthorizationRequirementsOperationFilter : IOperationFilter
{
    public string AuthenticationSchema { get; private set; } = "Bearer";

    public AuthorizationRequirementsOperationFilter()
    {

    }

    public AuthorizationRequirementsOperationFilter(string authenticationSchema)
    {
        ExceptionHelper.ThrowIfNullOrEmpty(authenticationSchema);

        AuthenticationSchema = authenticationSchema;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        //if ((context.MethodInfo
        // .GetCustomAttributes(true)
        // .OfType<AllowAnonymousAttribute>().Any()))
        //{
        //    return;
        //}
        var requiredScopes = new List<string>();
        if (context.MethodInfo != null)
        {
            requiredScopes.AddRange(
                context.MethodInfo
                 .GetCustomAttributes(true)
                 .OfType<AuthorizeAttribute>()
                 .Select(attr => attr.Policy)
                 .Distinct()
                 .ToList()
                 );
        }
        if (context.ApiDescription.ActionDescriptor.EndpointMetadata.Any(
           x => x is AuthorizeAttribute
           ))
        {
            requiredScopes.AddRange(
                context.ApiDescription.ActionDescriptor.EndpointMetadata.OfType<AuthorizeAttribute>()
                .Select(attr => attr.Policy)
                .Distinct()
                .ToList()
                );
        }


        if (requiredScopes.Any())
        {
            if (!operation.Responses.Any(x => x.Key == "401"))
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            }
            else
            {
                operation.Responses["401"].Description = "Unauthorized";
            }

            if (!operation.Responses.Any(x => x.Key == "403"))
            {
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });
            }
            else
            {
                operation.Responses["401"].Description = "Forbidden";

            }

            var oAuthScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = AuthenticationSchema,
                    Type = ReferenceType.SecurityScheme
                }
            };

            operation.Security = new List<OpenApiSecurityRequirement>
        {
            new OpenApiSecurityRequirement
            {
                [ oAuthScheme ] = requiredScopes.ToList()
            }
        };
        }
    }
}
