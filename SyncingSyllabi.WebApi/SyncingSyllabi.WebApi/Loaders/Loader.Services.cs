using Microsoft.AspNetCore.Http;
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

            //Settings
            var databaseSettings = ConfigurationUtility.GetConfig<DatabaseSettings>("DatabaseSettings");
            services.AddSingleton<DatabaseSettings>(databaseSettings);

            var authSettings = ConfigurationUtility.GetConfig<AuthSettings>("AuthSettings");
            services.AddSingleton<AuthSettings>(authSettings);

            //Repositories
            services.AddScopedTraced<IUserBaseRepository, UserBaseRepository>();
            services.AddScopedTraced<IAuthTokenBaseRepository, AuthTokenBaseRepository>();
            services.AddScopedTraced<IGoalBaseRepository, GoalBaseRepository>();

            //Services
            services.AddScopedTraced<IUserService, UserService>();
            services.AddScopedTraced<IAuthService, AuthService>();
            services.AddScopedTraced<IGoalService, GoalService>();
        }
    }
}
