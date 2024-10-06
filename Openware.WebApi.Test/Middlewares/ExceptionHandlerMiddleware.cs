using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Openware.WebApi.Middlewares;

namespace Openware.WebApi.Test.Middlewares;

public class ExceptionHandlerMiddleware
{
    [Fact]
    public async Task Can_Write_Errors_In_Camel_Case()
    {
        using var host = await new HostBuilder()
        .ConfigureWebHost(webBuilder =>
        {
            webBuilder
                .UseTestServer()
                .ConfigureServices(services =>
                {
                    services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(op =>
                    {
                        op.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                        op.SerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                        op.SerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                        op.SerializerOptions.AllowTrailingCommas = true;
                        //op.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    });
                })
                .Configure(app =>
                {
                    app.UseOpenwareExceptionHandlerMiddleware();
                    

                });
        })
        .StartAsync();

        var response = await host.GetTestClient().GetAsync("/");
        var responseText = await response.Content.ReadAsStringAsync();
        Assert.Contains("errors", responseText);
    }
}