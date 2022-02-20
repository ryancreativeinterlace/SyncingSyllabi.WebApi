using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SyncingSyllabi.Data.Transformation.AutoMapperProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncingSyllabi.WebApi.Loaders
{
    public partial class Loader
    {
        private static void LoadPackages(IServiceCollection services)
        {
            // AutoMapper Provider

            Func<IServiceProvider, IMapper> autoMapperProviderFactory =
                serviceFactory =>
                {
                    using (serviceFactory.CreateScope())
                    {
                        var config = new MapperConfigurationExpression();

                        config.AddProfile<SyncingSyllabiMappingProfiles>();

                        var mapperConfig = new MapperConfiguration(config);
                        mapperConfig.AssertConfigurationIsValid();

                        return new Mapper(mapperConfig);
                    }
                };

            services.AddSingleton(autoMapperProviderFactory);
        }
    }
}
