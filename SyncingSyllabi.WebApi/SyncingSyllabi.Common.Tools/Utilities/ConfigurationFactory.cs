using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SyncingSyllabi.Common.Tools.Utilities
{
    public class ConfigurationFactory
    {
        private static IConfigurationRoot _config;

        static ConfigurationFactory()
        {
            Initialize();
        }

        private static void Initialize()
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .AddCommandLine(Environment.GetCommandLineArgs());

            _config = builder.Build();
        }

        public static TConfig GetConfig<TConfig>(string configName) where TConfig : class, new()
        {
            var config = new TConfig();

            var section = _config.GetSection(configName);
            if (section == null || !section.Exists()) throw new InvalidOperationException($"Section {configName} not found");
            section.Bind(config);

            return config;
        }
    }
}
