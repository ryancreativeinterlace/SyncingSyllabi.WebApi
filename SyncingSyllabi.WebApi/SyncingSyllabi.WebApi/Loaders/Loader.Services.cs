﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SyncingSyllabi.Common.Tools;
using SyncingSyllabi.Common.Tools.Utilities;
using SyncingSyllabi.Data.Settings;
using SyncingSyllabi.Repositories.Interfaces;
using SyncingSyllabi.Repositories.Repositories;
using SyncingSyllabi.Services.Interfaces;
using SyncingSyllabi.Services.Services;
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

            //Repositories
            services.AddScopedTraced<IUserBaseRepository, UserBaseRepository>();

            //Services
            services.AddScopedTraced<IUserService, UserService>();

            var databaseSettings = ConfigurationFactory.GetConfig<DatabaseSettings>("DatabaseSettings");
            services.AddSingleton<DatabaseSettings>(databaseSettings);
        }
    }
}
