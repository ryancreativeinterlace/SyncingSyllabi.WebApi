using SyncingSyllabi.Contexts.Contexts;
using SyncingSyllabi.Contexts.Interfaces;
using SyncingSyllabi.Data.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SyncingSyllabi.Contexts.Repositories
{
    public partial class ContextRepository : IContextRepository, IBaseRepository<SyncingSyllabiDataContext>
    {
        DatabaseSettings _databaseSettings;

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
