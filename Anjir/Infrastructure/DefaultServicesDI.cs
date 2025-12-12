using Application.MiddleWare;
using Domain.Setting;
using Infrastructure.MyData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.RateLimiting;
using System.Threading.Tasks;
using Domain.Extensions;
using Newtonsoft.Json;

namespace Infrastructure;

public static class DefaultServicesDI
{
    public static void AddMyDefaultServicesDI(this IServiceCollection services, WebApplicationBuilder builder)
    {
        string settingsFilePath = Path.Combine(AppContext.BaseDirectory, "App.Settings.json");

        builder.Configuration.AddJsonFile(settingsFilePath);

        Settings.SmsConfig.Originator = builder.Configuration["SmsConfig:Originator"]!;
        Settings.SmsConfig.Login = builder.Configuration["SmsConfig:Login"]!;
        Settings.SmsConfig.Password = builder.Configuration["SmsConfig:Password"]!;
        Settings.SmsConfig.SendUrl = builder.Configuration["SmsConfig:SendUrl"]!;

        Settings.IpBlockTimeMinutes = int.Parse(builder.Configuration["RateLimit:IpBlockTimeMinutes"]!) * -1;
        Settings.TruistIpSet = [.. builder.Configuration["RateLimit:TruistIps"]!.Split(',')];

        Settings.SearchEngineBots = builder.Configuration.GetSection("SearchEngineBots")
                                                         .GetChildren()
                                                         .SelectMany(child => child.Value?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? [])
                                                         .ToHashSet();

        builder.Services.AddRateLimiter(options =>
        {
            options.AddPolicy("slidingPolicy", context =>
            {
                string ipAddress = context.GetIPAddress();
                var ips = ipAddress.Split(',')
                                   .Where(ip => !Settings.TruistIpSet.Contains(ip))
                                   .ToHashSet();

                if (ips.Count == 0)
                    return RateLimitPartition.GetNoLimiter("");

                //if (ips.Any(CIDRMatcher.IsSearchEngineBot))
                //    return RateLimitPartition.GetNoLimiter("");

                return RateLimitPartition.GetSlidingWindowLimiter(ipAddress, _ => new SlidingWindowRateLimiterOptions
                {
                    PermitLimit = 4,
                    SegmentsPerWindow = 1,
                    Window = TimeSpan.FromSeconds(12),
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = 4,
                });
            });
        });

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.ForwardLimit = null;
            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();
        });

        //services.AddMemoryCache();
        services.AddControllers()
                .AddJsonOptions(options =>
                {
                   // options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

        services.AddDbContext<MyDbContext>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        #region Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            string jwtSecret = builder.Configuration["JWT:Secret"]!;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                LifetimeValidator = (before, expires, token, parameters) => expires > DateTime.UtcNow,
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateActor = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
            };
        });
        #endregion

        #region Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.CustomSchemaIds(type => $"{type.Name}_{Guid.NewGuid()}".Replace("`1", "").Replace("+", "."));

            c.AddSecurityDefinition("Bearer", new()
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                {
                    new OpenApiSecurityScheme() {
                        Reference = new OpenApiReference() {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            c.EnableAnnotations();
        });
        #endregion

        #region AddCors
        services.AddCors(options =>
        {
            options.AddPolicy("ApiCorsPolicy", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });
        #endregion

        #region Logging
        builder.Logging.ClearProviders();
        Log.Logger = new LoggerConfiguration().ReadFrom
                                              .Configuration(builder.Configuration)
                                              .Enrich.FromLogContext()
                                              .CreateLogger();

        builder.Logging.AddSerilog(Log.Logger);
        builder.Services.AddSingleton(Log.Logger);
        builder.Host.UseSerilog(Log.Logger);
        #endregion

        services.AddConnections();
    }

    public static void UseMyDefaultServicesDI(this WebApplication app, string swaggerPrefix)
    {
        var url = app.Configuration["Kestrel:EndPoints:Http:Url"]?.Replace("[::]", "localhost");
        Console.WriteLine($"Swagger {url}/api/{swaggerPrefix}/swagger");

        app.UseForwardedHeaders();

        #region Swagger
        app.UseSwaggerAuthorize();
        app.UseSwagger(c =>
        {
            c.RouteTemplate = $"api/{swaggerPrefix}/{"{documentName}"}/swagger.json";
        });
        //app.UseSwaggerUI(c =>
        //{
        //    c.DocumentTitle = $"Osonapteka {swaggerPrefix.ToUpper()} - swagger";
        //    c.RoutePrefix = $"api/{swaggerPrefix}/swagger";
        //    c.SwaggerEndpoint($"/api/{swaggerPrefix}/v1/swagger.json", $"API");

        //    c.EnablePersistAuthorization();
        //    c.DefaultModelsExpandDepth(-1);
        //    c.DisplayRequestDuration();
        //    c.EnableDeepLinking();
        //    c.EnableValidator();
        //});
        #endregion

        app.UseMyExceptionHandler();

        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseRouting();
        app.UseRateLimiter();

        #region Authentication
        app.UseAuthentication();
        app.UseAuthorization();
        #endregion

        app.UseCors("ApiCorsPolicy");

        //app.UseDefaultFiles();
        //app.UseStaticFiles();
        //app.MapFallbackToFile("index.html");

        #region MigrateDatabase
        using (IServiceScope serviceScope = app.Services.CreateScope())
        {
            serviceScope.ServiceProvider.GetRequiredService<MyDbContext>().Database.Migrate();
        }
        #endregion

        app.MapControllers();

        app.Run();
    }
}
