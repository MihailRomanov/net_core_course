namespace MetricsApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            var workerNumber =
                builder.Configuration.GetValue<int>("WorkerNumber", 0);
            if (workerNumber == 0)
                builder.Services.AddHostedService<Worker>();
            else
            {
                for (int i = 0; i < workerNumber; i++)
                {
                    var current = i + 1;
                    builder.Services.AddSingleton<IHostedService, MultyWorker>
                        (provider =>
                            new MultyWorker(
                                provider.GetRequiredService<ILogger<Worker>>(),
                                provider.GetRequiredService<AppMetrics>(),
                                current));
                }
            }


            builder.Services.AddMetrics();
            builder.Services.AddSingleton<AppMetrics>();

            var host = builder.Build();

            using var meterProvider =
                MetricPublisihingHelpers.GetMeterProvider(
                AppMetrics.MeterName, "http://localhost:9184/");

            using var meterListener =
                MetricPublisihingHelpers
                .GetMeterListener(AppMetrics.MeterName);

            host.Run();
        }
    }
}