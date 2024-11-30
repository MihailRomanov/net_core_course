using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using NLog.Extensions.Logging;
using Serilog;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace ConsoleApp
{
    internal class MinService(
        ILogger<MinService> minServiceLogger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                minServiceLogger.WorkerStep(DateTimeOffset.Now);

                minServiceLogger.StartPause();
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                minServiceLogger.StopPause();

            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var hostBuilder = Host.CreateApplicationBuilder();

            hostBuilder.Logging
                .ClearProviders()
                .AddEventLog(new EventLogSettings { SourceName = "MyApp" })
                //.AddFilter<EventLogLoggerProvider>(null, LogLevel.Information)
                .AddConsole()
                .AddEventSourceLogger()
                .AddDebug()
                .AddNLog(LogProvidersHelper.GetNLogConfiguration())
                .AddSerilog(LogProvidersHelper.GetSerilogConfiguration()
                    .CreateLogger());

            hostBuilder.Services.AddHostedService<MinService>();

            var host = hostBuilder.Build();

            var mainLogger = host.Services.GetService<ILogger<Program>>()!;

            mainLogger.StartApp(
                Environment.CurrentDirectory, Environment.CommandLine);

            host.Run();
        }
    }
}
