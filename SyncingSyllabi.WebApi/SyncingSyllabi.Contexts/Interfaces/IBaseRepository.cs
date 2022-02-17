using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SyncingSyllabi.Contexts.Interfaces
{
    public interface IBaseRepository<TDataContext>
    {
        Task UseDataContextAsync(Func<TDataContext, Task> useContext);
    }
}
