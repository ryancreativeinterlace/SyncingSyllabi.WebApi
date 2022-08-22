using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Worker.DueDateHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "DEV");
            Environment.SetEnvironmentVariable("DueDateSettings__ApiUrl", "http://dev-api-syncingsyllabi.us-west-1.elasticbeanstalk.com/api/notification/DueNotification");

            try
            {
                var func = new Function();
                func.FunctionHandler(null, null).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
