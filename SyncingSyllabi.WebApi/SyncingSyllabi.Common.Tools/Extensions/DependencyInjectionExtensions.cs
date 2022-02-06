using Microsoft.Extensions.DependencyInjection;
using System;

namespace SyncingSyllabi.Common.Tools
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddScopedTraced<T, TImplementation>(this IServiceCollection services) where T : class
                                                                                                                    where TImplementation : class, T
        {
            return services.AddScoped<T, TImplementation>();
            // Temp disable Interception:
            //return services.AddScopedWithInterception<T, TImplementation>(s => s.InterceptBy<XRayTracerInterceptor>());
        }

        public static IServiceCollection AddSingletonTraced<T, TImplementation>(this IServiceCollection services) where T : class
                                                                                                                    where TImplementation : class, T
        {
            return services.AddSingleton<T, TImplementation>();
            // Temp disable Interception:
            //return services.AddSingletonWithInterception<T, TImplementation>(s => s.InterceptBy<XRayTracerInterceptor>());
        }

        public static IServiceCollection AddTransientTraced<T, TImplementation>(this IServiceCollection services) where T : class
                                                                                                                    where TImplementation : class, T
        {
            return services.AddTransient<T, TImplementation>();
            // Temp disable Interception:
            //return services.AddTransientWithInterception<T, TImplementation>(s => s.InterceptBy<XRayTracerInterceptor>());
        }
    }
}
