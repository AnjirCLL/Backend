using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.MiddleWare;

public static class SwaggerAuthorizeMiddleWareDI
{
    public static IApplicationBuilder UseSwaggerAuthorize(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SwaggerAuthorizeMiddleWare>();
    }
}

public class SwaggerAuthorizeMiddleWare(RequestDelegate next)
{
    private readonly RequestDelegate next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.ToString().Contains("/swagger/"))
        {
            context.Request.Headers.TryGetValue("Authorization", out var authHeaderValues);
            var authHeader = authHeaderValues.FirstOrDefault();
            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                var header = AuthenticationHeaderValue.Parse(authHeader);
                var inBytes = Convert.FromBase64String(header?.Parameter ?? string.Empty);
                var credentials = Encoding.UTF8.GetString(inBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];

                if (username.Equals("root_swag") && password.Equals("A$n@I%"))
                {
                    await next.Invoke(context).ConfigureAwait(false);
                    return;
                }
            }

            context.Response.Headers.WWWAuthenticate = "Basic";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
        else if (context != null)
        {
            await next.Invoke(context).ConfigureAwait(false);
        }
    }
}
