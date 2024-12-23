using ImageProcessingService.ImageDb;
using ImageProcessingService.Services;
using Microsoft.EntityFrameworkCore;

namespace ImageProcessingService.Processing
{
    internal interface IBarcodeReaderFactory
    {
        BarcodeReader CreateReader();
    }

    internal class BarcodeReaderFactory(IServiceProvider serviceProvider) 
        : IBarcodeReaderFactory
    {
        public BarcodeReader CreateReader()
        {
            return serviceProvider.GetRequiredService<BarcodeReader>();
        }
    }

    internal class FileProcessorV2(
        FileProcessingQueue queue,
        FileHelper fileHelper,
        IDbContextFactory<ImageDbContext> dbContextFactory,
        IBarcodeReaderFactory barcodeReaderFactory
        ) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var item = await queue.Get(stoppingToken);
                using var barcodeReader = barcodeReaderFactory.CreateReader();
                var text = barcodeReader.Read(item.FilePath);

                using var imageDbContext =
                    await dbContextFactory.CreateDbContextAsync();

                imageDbContext.Images.Add(
                    new Image
                    {
                        FileName = Path.GetFileName(item.FilePath),
                        AddedAt = DateTimeOffset.Now,
                        Content = text,
                    });

                await imageDbContext.SaveChangesAsync(stoppingToken);
                fileHelper.Delete(item.FilePath);
            }
        }
    }
}
