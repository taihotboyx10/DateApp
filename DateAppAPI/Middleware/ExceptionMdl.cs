using System.Net;
using System.Text.Json;
using DateAppAPI.Errors;

namespace DateAppAPI.Middleware;

public class ExceptionMdl(RequestDelegate next, ILogger<ExceptionMdl> logger, IHostEnvironment env){
    public async Task InvokeAsync(HttpContext context){
        try
        {
            await next(context);
        }
        catch (Exception exc)
        {
            logger.LogError(exc, exc.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var respone = env.IsDevelopment() ?
                        new ApiExceptions(context.Response.StatusCode, exc.Message, exc.StackTrace)
                        : new ApiExceptions(context.Response.StatusCode, exc.Message, "Internal server error");

            var options = new JsonSerializerOptions{
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var json = JsonSerializer.Serialize(respone, options);

            await context.Response.WriteAsync(json);
        }

    }
}