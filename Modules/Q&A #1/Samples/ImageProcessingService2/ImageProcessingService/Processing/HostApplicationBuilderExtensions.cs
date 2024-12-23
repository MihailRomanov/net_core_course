using ImageProcessingService.ImageDb;
using ImageProcessingService.Services;
using Microsoft.EntityFrameworkCore;

namespace ImageProcessingService.Processing
{
    internal static class HostApplicationBuilderExtensions
    {
        public static void AddProcessingV0(
            this HostApplicationBuilder builder)
        {
            builder.Services.AddHostedService<FileProcessorV0>();
            builder.Services.AddSingleton<BarcodeReader>();
        }


        public static void AddProcessingV1(
            this HostApplicationBuilder builder)
        {
            builder.Services.AddHostedService<FileProcessorV1>();
            builder.Services.AddSingleton<BarcodeReader>();
            builder.Services.Configure<FileProcessorV1Options>(
                o =>
                o.ImageDbConnectionString = builder.GetImageDbConnectionString());
        }

        public static void AddProcessingV2(
            this HostApplicationBuilder builder)
        {
            builder.Services.AddHostedService<FileProcessorV2>();
            builder.Services.AddTransient<BarcodeReader>();
            builder.Services.AddSingleton<IBarcodeReaderFactory, BarcodeReaderFactory>();

            builder.Services.AddDbContextFactory<ImageDbContext>(
                optionsBuilder => optionsBuilder.UseSqlServer(
                    builder.GetImageDbConnectionString()));
        }

        public static void AddProcessingV3(
            this HostApplicationBuilder builder)
        {
            builder.Services.AddHostedService<FileProcessorV3>();
            builder.Services.AddScoped<BarcodeReader>();

            builder.Services.AddDbContext<ImageDbContext>(
                optionsBuilder => optionsBuilder.UseSqlServer(
                    builder.GetImageDbConnectionString()));

            builder.Services.AddScoped<ImageProcessor>();
        }

        public static string? GetImageDbConnectionString(
            this HostApplicationBuilder builder)
        {
            return builder.Configuration.GetConnectionString("ImageDb");
        }

        public static DbContextOptions<ImageDbContext> GetDbContextOptions(
            this HostApplicationBuilder builder)
        {
            return new DbContextOptionsBuilder<ImageDbContext>()
                .UseSqlServer(builder.GetImageDbConnectionString())
                .Options;
        }
    }
}
