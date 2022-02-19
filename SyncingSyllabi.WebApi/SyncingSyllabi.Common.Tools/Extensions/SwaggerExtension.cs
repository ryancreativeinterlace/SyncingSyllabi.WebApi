using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SyncingSyllabi.Common.Tools.Extensions
{
    public static class SwaggerExtension
    {

        static string GetEntryPointName()
        {
            string entryPointName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            return entryPointName;
        }

        static string GetSpecificationName()
        {
            string entryPointName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            entryPointName = entryPointName.Replace(".", string.Empty);
            string specificationName = $"{entryPointName}Specification";
            specificationName = specificationName.ToLower();
            return specificationName;
        }

        public static void ConfigureSwaggerUI(this IApplicationBuilder app)
        {
            string hostName = GetEntryPointName();
            string specificationName = GetSpecificationName();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint(
                    $"/swagger/{specificationName}/swagger.json",
                    $"{hostName} API");
                setupAction.RoutePrefix = "";
            });
        }

        public static void ConfigureSwaggerGen(this IServiceCollection services)
        {
            string hostName = GetEntryPointName();
            string specificationName = GetSpecificationName();

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                setupAction.SwaggerDoc(
                    specificationName,
                    new OpenApiInfo()
                    {
                        Title = $"{hostName} API",
                        Version = "1",
                        Description = $"{hostName} API"

                    });

                var xmlCommentsFile = $"{hostName}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                setupAction.IncludeXmlComments(xmlCommentsFullPath);
                // if you're using the SecurityRequirementsOperationFilter, you also need to tell Swashbuckle you're using OAuth2
                //setupAction.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                //{
                //    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                //    In = ParameterLocation.Header,
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.OAuth2
                //});

                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setupAction.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });

            });
        }
    }
}
