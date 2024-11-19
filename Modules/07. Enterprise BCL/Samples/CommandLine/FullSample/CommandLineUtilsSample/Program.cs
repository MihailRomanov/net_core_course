using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProcessingServices;

namespace CommandLineUtilsSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .UseCommandLineApplication<App>(args)
                .ConfigureServices(serviceCollection =>
                {
                    serviceCollection.AddTransient<IFileConverter, FileConverter>();
                })
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.AddConsole();
                    loggingBuilder.SetMinimumLevel(LogLevel.Debug);
                })
                .Build()
                .RunCommandLineApplicationAsync();
        }
    }
}
