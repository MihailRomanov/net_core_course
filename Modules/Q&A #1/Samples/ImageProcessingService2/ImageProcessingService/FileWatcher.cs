using System.IO.Abstractions;

namespace ImageProcessingService
{
    internal class FileWatcher : IHostedService
    {
        private readonly IFileSystemWatcher watcher;
        private readonly IHostEnvironment hostEnvironment;
        private readonly FileProcessingQueue queue;

        public FileWatcher(
            IHostEnvironment hostEnvironment,
            IFileSystem fileSystem,
            FileProcessingQueue queue)
        {
            this.hostEnvironment = hostEnvironment;
            this.queue = queue;
            watcher = fileSystem.FileSystemWatcher.New();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var inDirectory = Path.Combine(hostEnvironment.ContentRootPath, "in");

            if (!Directory.Exists(inDirectory))
                Directory.CreateDirectory(inDirectory);

            watcher.Path = inDirectory;
            watcher.Filter = "*.*";

            watcher.Created += FileCreated;
            watcher.EnableRaisingEvents = true;
            return Task.CompletedTask;
        }

        private void FileCreated(object sender, FileSystemEventArgs e) =>
            queue.Add(new FileProcessingItem(e.FullPath));

        public Task StopAsync(CancellationToken cancellationToken)
        {
            watcher.EnableRaisingEvents = false;
            return Task.CompletedTask;
        }
    }
}
