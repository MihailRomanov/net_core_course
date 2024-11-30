namespace MetricsApp
{
    public class MultyWorker(
        ILogger<Worker> logger,
        AppMetrics appMetrics,
        int number) : BackgroundService
    {
        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation($"Worker {number} running at: {{time}}",
                    DateTimeOffset.Now);

                appMetrics.AddIteration(number);
                var pause = Random.Shared.Next(1, 10) * 1000;
                await Task.Delay(pause, stoppingToken);
            }
        }
    }
}
