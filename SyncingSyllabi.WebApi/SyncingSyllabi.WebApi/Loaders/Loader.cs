using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncingSyllabi.Main.WebApi.Loaders
{
    public partial class Loader
    {
        public static void Load(IServiceCollection services)
        {
            LoadPackages(services);
            LoadServices(services);
        }
    }
}
