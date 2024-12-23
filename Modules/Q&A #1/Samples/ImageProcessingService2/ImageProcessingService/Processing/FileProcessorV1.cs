using ImageProcessingService.ImageDb;
using ImageProcessingService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ImageProcessingService.Processing
{
    internal class FileProcessorV1Options
    {
        public string? ImageDbConnectionString { get; set; }
    }

    internal class FileProcessorV1(
        FileProcessingQueue queue,
        FileHelper fileHelper,
        IOptions<FileProcessorV1Options> options,
        BarcodeReader barcodeReader,
        ILoggerFactory loggerFactory
        ) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var item = await queue.Get(stoppingToken);
                var text = barcodeReader.Read(item.FilePath);

                var dbConnectionOptions = new DbContextOptionsBuilder<ImageDbContext>()
                    .UseSqlServer(options.Value.ImageDbConnectionString)
                    .UseLoggerFactory(loggerFactory)
                    .Options;

                using var imageDbContext = new ImageDbContext(dbConnectionOptions);
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
