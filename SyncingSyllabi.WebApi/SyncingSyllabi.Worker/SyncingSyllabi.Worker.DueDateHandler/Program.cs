using System;

namespace SyncingSyllabi.Worker.DueDateHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "DEV");
            Environment.SetEnvironmentVariable("DueDateSettings__ApiUrl", "dev");

            try
            {
                var func = new Function();
                func.FunctionHandler(null, null);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
