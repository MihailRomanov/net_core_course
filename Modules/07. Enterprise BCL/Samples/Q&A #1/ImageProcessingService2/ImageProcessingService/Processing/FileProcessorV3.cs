using ImageProcessingService.Services;

namespace ImageProcessingService.Processing
{
    internal class FileProcessorV3(
        FileProcessingQueue queue,
        FileHelper fileHelper,
        IServiceScopeFactory scopeFactory
        ) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var item = await queue.Get(stoppingToken);

                using (IServiceScope scope = scopeFactory.CreateScope())
                {
                    var imageProcessor =
                        scope.ServiceProvider.GetRequiredService<ImageProcessor>();
                    var success = await imageProcessor.Process(item.FilePath, stoppingToken);

                    if (success) fileHelper.Delete(item.FilePath);
                }
            }
        }
    }
}
