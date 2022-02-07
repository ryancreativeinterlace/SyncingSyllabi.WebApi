using AutoMapper;
using SyncingSyllabi.Contexts.Contexts;
using SyncingSyllabi.Contexts.Interfaces;
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
        IMapper _mapper;

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
