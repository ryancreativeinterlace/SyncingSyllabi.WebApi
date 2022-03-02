﻿using Microsoft.EntityFrameworkCore;
using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Contexts.Mappings;


namespace SyncingSyllabi.Contexts.Contexts
{
    public class SyncingSyllabiContext : SyncingSyllabiDataContext
    {
        public SyncingSyllabiContext(string connectionString,
           int databaseMaxRetryCount,
           int databaseMaxRetryDelayInSeconds)
           : base(connectionString,
           databaseMaxRetryCount,
           databaseMaxRetryDelayInSeconds)
        {

        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<AuthTokenEntity> AuthTokens { get; set; }
        public DbSet<GoalEntity> Goals { get; set; }
        public DbSet<UserCodeEntity> UserCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new AuthTokenMap());
            modelBuilder.ApplyConfiguration(new GoalMap());
            modelBuilder.ApplyConfiguration(new UserCodeMap());
        }
    }
}
