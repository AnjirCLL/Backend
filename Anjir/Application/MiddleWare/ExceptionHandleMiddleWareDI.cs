using Domain.BaseAnswer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.MiddleWare;

public static class ExceptionHandleMiddleWareDI
{
    public static IApplicationBuilder UseMyExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandleMiddleWare>();
    }
}

public class ExceptionHandleMiddleWare(RequestDelegate next, ILogger<ExceptionHandleMiddleWare> logger)
{
    private readonly RequestDelegate next = next;
    private readonly ILogger<ExceptionHandleMiddleWare> logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            LogException(context, ex);
            await HandleExceptionAsync(context, ex);
        }
    }

    private void LogException(HttpContext context, Exception exception)
    {
        var request = context.Request;
        var logDetails = new
        {
            request.Path,
            QueryString = request.QueryString.ToString(),
            Headers = request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
            Exception = exception.ToString(),
            exception.Source,
            exception.StackTrace,
            InnerException = exception.InnerException?.ToString()
        };

        logger.LogError($"ExceptionHandlingMiddleware: {JsonSerializer.Serialize(logDetails)}");
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.OK;

        JsonSerializerOptions options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        string result = JsonSerializer.Serialize(new BaseAnswer<string> { Messages = [exception.Message] }, options);
        return context.Response.WriteAsync(result);
    }
}
