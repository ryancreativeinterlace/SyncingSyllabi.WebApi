using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Common.Tools.Hosting
{
    public class DependencyResolver
    {
        public IServiceProvider ServiceProvider { get; }
        public Action<IServiceCollection> RegisterServices { get; }

        public DependencyResolver(Action<IServiceCollection> registerServices = null)
        {
            // Set up Dependency Injection
            var serviceCollection = new ServiceCollection();
            RegisterServices = registerServices;
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            //ProviderFactory.Factory = ServiceProvider;
        }

        private void ConfigureServices(IServiceCollection services)
        {
            RegisterServices?.Invoke(services);
        }
    }
}
