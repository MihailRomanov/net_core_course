using ImageProcessingService.Services;

namespace ImageProcessingService.Processing
{
    internal class FileProcessorV0(
        FileProcessingQueue queue,
        FileHelper fileHelper,
        BarcodeReader barcodeReader
        ) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var item = await queue.Get(stoppingToken);
                var text = barcodeReader.Read(item.FilePath);

                Console.WriteLine(text);

                fileHelper.Delete(item.FilePath);
            }
        }
    }
}
