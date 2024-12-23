using SixLabors.ImageSharp.PixelFormats;
using ZXing.ImageSharp;

namespace ImageProcessingService.Services
{
    public class BarcodeReader : IDisposable
    {
        private readonly FileHelper fileHelper;
        private readonly ILogger<BarcodeReader> logger;

        public BarcodeReader(
            FileHelper fileHelper,
            ILogger<BarcodeReader> logger)
        {
            this.fileHelper = fileHelper;
            this.logger = logger;

            logger.LogInformation("Create");
        }
        public void Dispose()
        {
            logger.LogInformation("Dispose");
        }

        public string Read(string path)
        {
            using var stream = fileHelper.Open(path);
            using var image = SixLabors.ImageSharp.Image.Load<Rgba32>(stream);
            var reader = new BarcodeReader<Rgba32>();
            var result = reader.Decode(image);

            return result.Text;
        }
    }
}
