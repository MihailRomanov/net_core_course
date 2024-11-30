namespace MetricsApp
{
    public class Worker(
        ILogger<Worker> logger,
        AppMetrics appMetrics) : BackgroundService
    {
        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Worker running at: {time}",
                    DateTimeOffset.Now);

                appMetrics.AddIteration();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
