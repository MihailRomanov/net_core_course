using System.Runtime;

namespace MemoryService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            var workerNumber =
                builder.Configuration.GetValue("WorkerNumber", 1);
            if (workerNumber == 0)
                builder.Services.AddHostedService<Worker>();
            else
            {
                for (int i = 0; i < workerNumber; i++)
                {
                    var current = i + 1;
                    builder.Services.AddSingleton<IHostedService, Worker>
                        (provider =>
                            new Worker(
                                provider.GetRequiredService<ILogger<Worker>>(),
                                provider.GetRequiredService<Storage>()));
                }
            }

            builder.Services.AddHostedService<Cleaner>();
            builder.Services.AddSingleton<Storage>();

            var host = builder.Build();
            host.Run();
        }
    }
}