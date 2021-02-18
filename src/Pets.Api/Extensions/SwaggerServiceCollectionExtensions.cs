using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Pets.Api.Extensions
{
    internal static class SwaggerServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Pets Api"
                });

    
                var securityScheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    Description = "<p>You need to set Bearer Token to get access to API</p>" +
                        "<p><b>Example:</b> <br />" +
                        "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c</p>",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                };


                options.AddSecurityDefinition("Bearer", securityScheme);  
                

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<String>()
                    }
                });

                options.CustomSchemaIds(type => type.FullName);

                options.MapType<Guid>(() => new OpenApiSchema
                {
                    Type = "string",
                    Format = "uuid",
                    Default = new OpenApiString(Guid.NewGuid().ToString())
                });
                options.DescribeAllEnumsAsStrings();

                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                options.IncludeXmlComments(Path.Combine(basePath, "Pets.Api.xml"));
                options.IncludeXmlComments(Path.Combine(basePath, "Pets.Queries.xml"));
                options.IncludeXmlComments(Path.Combine(basePath, "Pets.Types.xml"));
            });
        }
    }
}

