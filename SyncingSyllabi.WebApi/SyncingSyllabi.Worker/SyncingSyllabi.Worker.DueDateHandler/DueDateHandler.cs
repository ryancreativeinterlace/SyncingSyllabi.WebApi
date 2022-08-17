using SyncingSyllabi.Common.Tools.Hosting;
using SyncingSyllabi.Common.Tools.Utilities;
using SyncingSyllabi.Data.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Worker.DueDateHandler
{
    public interface IDueDateHandler : IHandler
    {
        void Handle();
    }
    public class DueDateHandler : IDueDateHandler
    {
        private readonly DueDateSettings _dueDateSettings;
        public DueDateHandler(DueDateSettings dueDateSettings)
        {
            this._dueDateSettings = dueDateSettings;
        }

        public void Handle()
        {
            try
            {
                Console.WriteLine($"Trigger Due Date {_dueDateSettings.ApiUrl}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
