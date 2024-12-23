using ImageProcessingService.ImageDb;
using ImageProcessingService.Services;

namespace ImageProcessingService.Processing
{
    internal class ImageProcessor : IDisposable
    {
        private readonly BarcodeReader barcodeReader;
        private readonly ImageDbContext imageDbContext;
        private readonly ILogger<ImageProcessor> logger;

        public ImageProcessor(
            BarcodeReader barcodeReader,
            ImageDbContext imageDbContext,
            ILogger<ImageProcessor> logger)
        {
            this.barcodeReader = barcodeReader;
            this.imageDbContext = imageDbContext;
            this.logger = logger;

            logger.LogInformation("Create");
        }

        public void Dispose()
        {
            logger.LogInformation("Dispose");
        }

        public async Task<bool> Process(string path, CancellationToken stoppingToken)
        {
            var text = barcodeReader.Read(path);

            imageDbContext.Images.Add(
                new Image
                {
                    FileName = Path.GetFileName(path),
                    AddedAt = DateTimeOffset.Now,
                    Content = text,
                });

            await imageDbContext.SaveChangesAsync(stoppingToken);

            return true;
        }
    }
}
