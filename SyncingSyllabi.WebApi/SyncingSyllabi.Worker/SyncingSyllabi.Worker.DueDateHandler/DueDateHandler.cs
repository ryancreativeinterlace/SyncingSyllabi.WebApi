using SyncingSyllabi.Common.Tools.Hosting;
using SyncingSyllabi.Data.Settings;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SyncingSyllabi.Worker.DueDateHandler
{
    public interface IDueDateHandler : IHandler
    {
        Task Handle();
    }
    public class DueDateHandler : IDueDateHandler
    {
        private readonly DueDateSettings _dueDateSettings;
        public DueDateHandler(DueDateSettings dueDateSettings)
        {
            this._dueDateSettings = dueDateSettings;
        }

        public async Task Handle()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync($"{_dueDateSettings.ApiUrl}");

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Success");
                    }
                    else
                    {
                        Console.WriteLine("Failure");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
