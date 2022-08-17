using System;
using log4net;
using log4net.Config;
using System.IO;
using System.Text;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SyncingSyllabi.Common.Tools.Hosting
{
    public class BaseLambdaEntryPoint<THandler> where THandler : IHandler
    {
        private DependencyResolver resolver;
        //static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //private int _instanceId;

        static BaseLambdaEntryPoint()
        {
        }

        protected BaseLambdaEntryPoint()
        {
            InitilizeRuntime();
        }

        private void InitilizeRuntime()
        {
            RegisterLogging();
            //_instanceId = _log.GenerateInstanceId();
            resolver = new DependencyResolver(RegisterServicesCore);
        }

        private static void RegisterLogging()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        protected virtual void RegisterServicesCore(IServiceCollection services)
        {
            // ensure we add this here instead of derived
            // allow override for derived registration
            RegisterServices(services);
        }

        protected virtual void RegisterServices(IServiceCollection services)
        {
        }

        protected void Execute(Action<THandler> onExecute)
        {
            var serviceProvider = resolver.ServiceProvider;
            string source = "Execute<TResult>";

            try
            {
                var serviceScopeFactory = serviceProvider.GetService<IServiceScopeFactory>();
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var handler = scope.ServiceProvider.GetRequiredService<THandler>();

                    onExecute(handler);

                    if (handler is IDisposable)
                    {
                        (handler as IDisposable).Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                //_log.LogError(_instanceId, source, ex);
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        protected TResult Execute<TResult>(Func<THandler, TResult> onExecute) where TResult : class
        {
            var serviceProvider = resolver.ServiceProvider;
            string source = "Execute<TResult>";

            TResult result = null;

            try
            {
                var serviceScopeFactory = serviceProvider.GetService<IServiceScopeFactory>();
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var handler = scope.ServiceProvider.GetRequiredService<THandler>();

                    result = onExecute(handler);

                    if (handler is IDisposable)
                    {
                        (handler as IDisposable).Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                //_log.LogError(_instanceId, source, ex);
                Console.WriteLine(ex.ToString());
                throw;
            }

            return result;
        }
    }
}
