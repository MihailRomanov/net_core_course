using Microsoft.Extensions.Hosting.Systemd;
using Microsoft.Extensions.Hosting.WindowsServices;

namespace ImageProcessingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<FileWatcher>();
            builder.Services.AddHostedService<ImageProcessor>();
            builder.Services.AddSingleton<FileProcessingQueue>();

            if (WindowsServiceHelpers.IsWindowsService())
                builder.Services.AddWindowsService();
            else if (SystemdHelpers.IsSystemdService())
                builder.Services.AddSystemd();

            var host = builder.Build();
            host.Run();
        }
    }
}