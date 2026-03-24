using TestTaskPushPostRequest.Data;

namespace TestTaskPushPostRequest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.Configure<SettingSendMessage>(builder.Configuration.GetSection("SettingSendMessage"));
            builder.Services.AddHostedService<WorkerSendMessage>();
            builder.Services.AddHttpClient();
            var host = builder.Build();
            host.Run();
        }
    }
}
