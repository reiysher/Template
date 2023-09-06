using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Host.OpenApi;

internal static class Configure
{
    internal static IServiceCollection AddSwaggerPreConfigured(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer",
                Name = "Authorization",
                Description = "Authorization token"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
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
        }});

            options.SwaggerDoc("default", new OpenApiInfo { Title = "Template", Version = "default" });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerPreConfigured(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = string.Empty;
            options.SwaggerEndpoint("swagger/default/swagger.json", "Template - Default");
        });

        return app;
    }
}
