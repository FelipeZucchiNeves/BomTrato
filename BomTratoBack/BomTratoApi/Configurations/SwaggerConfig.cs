using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace BomTratoApi.Configurations
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Bom Trato Project",
                    Description = "Bom Trato Project API Swager",
                    Contact = new OpenApiContact { Name = "Felipe Neves", Email = "felipe_neves@bt.com.br" },
                    License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://github.com/FelipeZucchiNeves") }
                });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "input the Jwt like: Bearer {your token}",
                    Name = "Autohrization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] { }
                    }
                });
            });

        }
        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            app.UseSwagger();
            app.UseSwaggerUI(x =>
           {
               x.SwaggerEndpoint("v1/swagger.json", "Web API v1");
           });
        }
    }
}
