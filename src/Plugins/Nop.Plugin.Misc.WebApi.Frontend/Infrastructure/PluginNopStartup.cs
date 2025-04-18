using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.WebApi.Framework.Middleware;
using Nop.Plugin.Misc.WebApi.Frontend.Services;

namespace Nop.Plugin.Misc.WebApi.Frontend.Infrastructure;

/// <summary>
/// Represents object for the configuring services on application startup
/// </summary>
public partial class PluginNopStartup : INopStartup
{
    /// <summary>
    /// Add and configure any of the middleware
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    /// <param name="configuration">Configuration of the application</param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMvc()
            .AddNewtonsoftJson(opts =>
            {
                opts.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

        services.AddCors(options =>
        {
            options.AddPolicy(name: WebApiFrontendDefaults.CORS_POLICY_NAME,
                policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc($"{WebApiFrontendDefaults.VERSION}", new OpenApiInfo
            {
                Title = "Web API",
                Version = WebApiFrontendDefaults.VERSION,
                Description = "Web API"
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Scheme = "bearer",
                BearerFormat = "JWT",
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Name = WebApiFrontendDefaults.SecurityHeaderName,
                Description = $"JWT {WebApiFrontendDefaults.SecurityHeaderName} header using the Bearer scheme."
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IAuthorizationUserService, AuthorizationUserService>();
    }

    /// <summary>
    /// Configure the using of added middleware
    /// </summary>
    /// <param name="application">Builder for configuring an application's request pipeline</param>
    public void Configure(IApplicationBuilder application)
    {
        application.UseSwagger(options =>
        {
            options.RouteTemplate = "api/{documentName}/swagger.json";
        });
        application.UseSwaggerUI(c =>
        {
            //Uses single entry point for Swagger UI
            c.RoutePrefix = WebApiFrontendDefaults.SwaggerUIRoutePrefix;

            c.SwaggerEndpoint($"{WebApiFrontendDefaults.VERSION}/swagger.json",
                $"{WebApiFrontendDefaults.VERSION}");
        });

        application.UseRouting();

        // cors policy
        application.UseCors();

        // custom jwt auth middleware
        application.UseMiddleware<JwtMiddleware>();
    }

    /// <summary>
    /// Gets order of this startup configuration implementation
    /// </summary>
    public int Order => 100;
}