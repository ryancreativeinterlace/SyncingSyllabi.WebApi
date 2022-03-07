﻿using AutoMapper;
using SyncingSyllabi.Contexts.Contexts;
using SyncingSyllabi.Contexts.Interfaces;
using SyncingSyllabi.Contexts.Loaders;
using SyncingSyllabi.Data.Settings;
using SyncingSyllabi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SyncingSyllabi.Repositories.Repositories
{
    public partial class UserBaseRepository : IUserBaseRepository, IBaseRepository<SyncingSyllabiDataContext>
    {
        DatabaseSettings _databaseSettings;
        SyncingSyllabiSettings _syncingSyllabiSettings;
        IMapper _mapper;

        public UserBaseRepository(DatabaseSettings databaseSettings, SyncingSyllabiSettings syncingSyllabiSettings)
        {
            _databaseSettings = databaseSettings;
            _syncingSyllabiSettings = syncingSyllabiSettings;

            _mapper = AutoMapperLoader.Load();
        }

        private SyncingSyllabiContext NewDataContext()
        {
           
            var ctx = new SyncingSyllabiContext(_databaseSettings.ConnectionString, 
                _databaseSettings.MaximumRetry,
                _databaseSettings.MaximumRetryIntervalSeconds);

            return ctx;
        }

        protected void UseDataContext(Action<SyncingSyllabiContext> useContext)
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
