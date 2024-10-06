using System.Globalization;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Configuration;

namespace Openware.WebApi.Middlewares;

internal sealed class RequestLanguageMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly string[] _supporedCultures = new[] { "ar", "en" };

    public RequestLanguageMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var currentLanguage = httpContext.Request.Headers["user-language"].FirstOrDefault()
            ?? _configuration.GetValue<string>("DefaultLanguage") ?? "ar";

        if (_supporedCultures.Any(x => x.Equals(currentLanguage, StringComparison.InvariantCultureIgnoreCase)))
        {

            var culture = new CultureInfo(currentLanguage);

            //We are only changing UI Culture to handle resources.
            CultureInfo.CurrentUICulture = culture;
        }

        await _next(httpContext);
    }
}
public static class RequestLanguageMiddlewareExtensions
{
    /// <summary>
    /// Registers a middleware that reads the user language from "user-language" header, supported languages
    /// are arabic and english
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseRequestLanguage(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestLanguageMiddleware>();
    }
}