using Microsoft.Extensions.DependencyInjection;
using System;

namespace SyncingSyllabi.Common.Tools
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddScopedTraced<T, TImplementation>(this IServiceCollection services) where T : class
                                                                                                                    where TImplementation : class, T
        {
            return services.AddScoped<T, TImplementation>();
        }

        public static IServiceCollection AddSingletonTraced<T, TImplementation>(this IServiceCollection services) where T : class
                                                                                                                    where TImplementation : class, T
        {
            return services.AddSingleton<T, TImplementation>();
        }

        public static IServiceCollection AddTransientTraced<T, TImplementation>(this IServiceCollection services) where T : class
                                                                                                                    where TImplementation : class, T
        {
            return services.AddTransient<T, TImplementation>();
        }
    }
}
