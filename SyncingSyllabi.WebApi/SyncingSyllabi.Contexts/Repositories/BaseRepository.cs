using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SyncingSyllabi.Contexts.Contexts;
using SyncingSyllabi.Contexts.Interfaces;
using SyncingSyllabi.Data.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SyncingSyllabi.Contexts.Repositories
{
    public class BaseRepository<TDataContext> : IBaseRepository<SyncingSyllabiDataContext> where TDataContext : DbContext
    {

        DatabaseSettings _dbSettings;

        private SyncingSyllabiDataContext NewDataContext()
        {
            var ctx = new SyncingSyllabiDataContext(_dbSettings.ConnectionString,
                            _dbSettings.MaximumRetry,
                            _dbSettings.MaximumRetryIntervalSeconds);

            return ctx;
        }

        protected void UseDataContext(Action<SyncingSyllabiDataContext> useContext)
        {
            using (var ctx = NewDataContext())
            {
                useContext(ctx);
            }
        }

        public async Task UseDataContextAsync(Func<SyncingSyllabiDataContext, Task> useContext)
        {
            using (var ctx = NewDataContext())
            {
                await useContext(ctx);
            }
        }
    }
}
