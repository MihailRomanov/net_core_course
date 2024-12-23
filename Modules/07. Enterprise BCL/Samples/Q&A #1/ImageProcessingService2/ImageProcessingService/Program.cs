using ImageProcessingService.ImageDb;
using ImageProcessingService.Processing;
using ImageProcessingService.Services;
using Microsoft.EntityFrameworkCore;
using System.IO.Abstractions;

namespace ImageProcessingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            EnsureDb(builder.GetImageDbConnectionString());

            builder.Services.AddHostedService<FileWatcher>();
            builder.Services.AddSingleton<FileProcessingQueue>();
            builder.Services.AddSingleton<FileHelper>();
            builder.Services.AddSingleton<IFileSystem, FileSystem>();
            builder.AddProcessingV0();

            var host = builder.Build();
            host.Run();
        }

        private static void EnsureDb(string? connectionSring)
        {
            var dbContextOptions = new DbContextOptionsBuilder<ImageDbContext>()
                .UseSqlServer(connectionSring)
                .Options;

            using var imageDbContext = new ImageDbContext(dbContextOptions);
            imageDbContext.Database.EnsureCreated();
        }
    }
}