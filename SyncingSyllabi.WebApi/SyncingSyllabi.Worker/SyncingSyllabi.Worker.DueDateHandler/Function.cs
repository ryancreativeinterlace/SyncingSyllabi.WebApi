using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using SyncingSyllabi.Common.Tools.Utilities;
using SyncingSyllabi.Common.Tools.Hosting;
using SyncingSyllabi.Data.Settings;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace SyncingSyllabi.Worker.DueDateHandler
{
    public class Function : BaseLambdaEntryPoint<IDueDateHandler>
    {
        protected override void RegisterServices(IServiceCollection services)
        {
            var dueDateSettings = ConfigurationUtility.GetConfig<DueDateSettings>("DueDateSettings");

            services.AddSingleton<DueDateSettings>(dueDateSettings);
            services.AddScoped<IDueDateHandler, DueDateHandler>();
        }

        public async Task FunctionHandler(object evnt, ILambdaContext context)
        {
            await Execute<Task>(async (handler) =>
            {
                await handler.Handle();
            });
        }
    }
}
