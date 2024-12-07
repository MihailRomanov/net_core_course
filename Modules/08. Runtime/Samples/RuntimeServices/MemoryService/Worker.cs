namespace MemoryService
{
    internal class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Storage _storage;
        private readonly Random _random;

        public Worker(ILogger<Worker> logger, Storage storage)
        {
            _logger = logger;
            _storage = storage;
            _random = new Random();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _storage.Add(_random.Next(100));
                _logger.LogInformation("Worker {size} {time}", _storage.Size, DateTime.Now);

                await Task.Delay(_random.Next(1000, 10000), stoppingToken);
            }
        }
    }
}
