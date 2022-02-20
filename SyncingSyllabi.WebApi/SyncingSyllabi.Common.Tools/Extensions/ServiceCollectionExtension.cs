using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using SyncingSyllabi.Common.Tools.Utilities;
using SyncingSyllabi.Data.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace SyncingSyllabi.Common.Tools.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddJWTAuthentication(this IServiceCollection services)
        {
            var authSettings = ConfigurationUtility.GetConfig<AuthSettings>("AuthSettings");

            // JWT Authentication
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = authSettings.Issuer,
                    ValidAudience = authSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Key)),
                    RequireExpirationTime = authSettings.RequireExpirationTime,
                    ValidateLifetime = authSettings.ValidateLifetime
                };

                //cfg.Events = new JwtBearerEvents()
                //{
                //    OnTokenValidated = c =>
                //    {
                //        try
                //        {
                //            var userIdClaim = c.Principal.FindFirst(ClaimTypes.NameIdentifier);
                //            if (userIdClaim != null)
                //            {
                //                XrayHelper.AddXRayAnnotation(DataConstant.XrayUserId, userIdClaim.Value);
                //            }

                //            var userNameClaim = c.Principal.FindFirst(ClaimTypes.Name);
                //            if (userNameClaim != null)
                //            {
                //                XrayHelper.AddXRayAnnotation(DataConstant.XrayUserFullName, userNameClaim.Value);
                //            }

                //            var userEmailClaim = c.Principal.FindFirst(ClaimTypes.Email);
                //            if (userEmailClaim != null)
                //            {
                //                XrayHelper.AddXRayAnnotation(DataConstant.XrayUserEmail, userEmailClaim.Value);
                //            }
                //        }
                //        catch
                //        {
                //            // Catch exception
                //        }

                //        return Task.CompletedTask;
                //    }
                //};
            });
        }
    }
}
