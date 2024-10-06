using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using Openware.Core;

namespace Openware.WebApi.Middlewares;

internal sealed class UnAthenticatedApiHandlerMiddleware
{
    private readonly RequestDelegate _next;
    public UnAthenticatedApiHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        await _next(httpContext);
        if (httpContext.Response.StatusCode == (int)StatusCodes.Status401Unauthorized)
        {
            throw new NotAuthorizedAccessException();
        }
        if (httpContext.Response.StatusCode == (int)StatusCodes.Status403Forbidden)
        {
            throw new ForbiddenAccessException();
        }
    }
}
public static class UnAthenticatedApiHandlerMiddlewareExtension
{
    /// <summary>
    /// Registers a middleware that throws NotAuthorizedAccessException or ForbiddenAccessException based on the .Net authentication/authorization middlewars
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseCustomAuthenticationResponse(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UnAthenticatedApiHandlerMiddleware>();
    }
}
