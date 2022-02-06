using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SyncingSyllabi.Common.Tools;
using SyncingSyllabi.Repositories.Interfaces;
using SyncingSyllabi.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SyncingSyllabi.Main.WebApi.Loaders
{
    public partial class Loader
    {
        private static void LoadServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

            services.AddScopedTraced<IUserRepository, UserRepository>();

            //var leadDeliverySettings = ConfigurationFactory.GetConfig<LeadDeliverySettings>("LeadDeliverySettings");
            //services.AddSingleton<LeadDeliverySettings>(leadDeliverySettings);
            //var databaseSettings = ConfigurationFactory.GetConfig<DatabaseSettings>("DatabaseSettings");
            //services.AddSingleton<DatabaseSettings>(databaseSettings);
            //var wipSettings = ConfigurationFactory.GetConfig<WIPSettings>("ServiceBusSettings");
            //services.AddSingleton<WIPSettings>(wipSettings);
            //var s3Settings = ConfigurationFactory.GetConfig<S3Settings>("S3");
            //services.AddSingleton<S3Settings>(s3Settings);
            //var authSettings = ConfigurationFactory.GetConfig<AuthSettings>("AuthSettings");
            //services.AddSingleton<AuthSettings>(authSettings);
            //services.AddTransient<IFileRepository, S3Repository>();
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<ISecuredSession, SecuredSession>();
            //services.AddScoped<IWIPEventsAPIGateway, WIPEventsAPIGateway>();


        }
    }
}
