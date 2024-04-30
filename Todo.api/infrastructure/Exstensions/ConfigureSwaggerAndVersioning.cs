using Asp.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Todo.api.infrastructure.Exstensions;

public static class ConfigureSwaggerAndVersioning
{
    public static void AddConfiguredSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opts =>
        {

            opts.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Description = "My awesome auth"
            });
            opts.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "bearer"
                                }
                            },
                            new string[] {}
                    }
                });

            opts.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "To-Do API",
                Version = "v1",
                Description = "Api for To-Do Project",
            });
        });
    }

    public static void AddCustomVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(opts =>
        {
            opts.AssumeDefaultVersionWhenUnspecified = true;
            opts.DefaultApiVersion = new(1, 0);
            opts.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(opts =>
        {
            opts.GroupNameFormat = "'v'VVV";
            opts.SubstituteApiVersionInUrl = true;
        });
    }
}
