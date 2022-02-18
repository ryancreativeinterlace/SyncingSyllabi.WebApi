using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SyncingSyllabi.Common.Tools.Utilities;
using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Contexts.Mappings;
using System;

namespace SyncingSyllabi.Contexts.Contexts
{
    public class SyncingSyllabiDataContext : DbContext
    {
        private readonly string _connectionString;
        private readonly int _databaseMaxRetryCount;
        private readonly int _databaseMaxRetryDelayInSeconds;

        public SyncingSyllabiDataContext(string connectionString,
           int databaseMaxRetryCount,
           int databaseMaxRetryDelayInSeconds)
           : base()
        {
            _connectionString = connectionString;
            _databaseMaxRetryCount = databaseMaxRetryCount;
            _databaseMaxRetryDelayInSeconds = databaseMaxRetryDelayInSeconds;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(EncryptionUtility.DecryptString(_connectionString),
                options =>
                {
                    options
                        .EnableRetryOnFailure(
                            maxRetryCount: _databaseMaxRetryCount,
                            maxRetryDelay: TimeSpan.FromSeconds(_databaseMaxRetryDelayInSeconds),
                            errorNumbersToAdd: null);
                });

#if DEBUG
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddFile(@"D:\Logs\ss-ef.log");
            });
            optionsBuilder.UseLoggerFactory(loggerFactory);
#endif
        }
        public DbSet<IntegrationStatusCode> IntegrationStatusCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new IntegrationStatusCodeMap());
        }
    }
}
