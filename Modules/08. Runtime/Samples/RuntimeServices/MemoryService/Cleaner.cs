namespace MemoryService
{
    internal class Cleaner : BackgroundService
    {
        private readonly ILogger<Cleaner> _logger;
        private readonly Storage _storage;
        private readonly Random _random;

        public Cleaner(ILogger<Cleaner> logger, Storage storage)
        {
            _logger = logger;
            _storage = storage;
            _random = new Random();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _storage.Remove(_random.Next(_storage.Size / 2, _storage.Size));
                _logger.LogInformation("Cleaner: {size} {time}", _storage.Size, DateTime.Now);

                await Task.Delay(_random.Next(1000, 10000), stoppingToken);
            }
        }
    }
}
