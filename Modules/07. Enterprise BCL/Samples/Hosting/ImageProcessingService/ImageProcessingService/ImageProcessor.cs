namespace ImageProcessingService
{
    internal class ImageProcessor(
        IHostEnvironment hostEnvironment,
        FileProcessingQueue queue) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var outDirectory = Path.Combine(hostEnvironment.ContentRootPath, "out");
            var outFile = Path.Combine(outDirectory, "result.txt");

            if (!Directory.Exists(outDirectory))
                Directory.CreateDirectory(outDirectory);

            while (!stoppingToken.IsCancellationRequested)
            {
                var item = await queue.Get(stoppingToken);

                File.AppendAllText(outFile, item.FilePath + Environment.NewLine);
            }
        }
    }
}
