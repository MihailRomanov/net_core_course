namespace ImageProcessingService
{
    internal class FileWatcher(
        IHostEnvironment hostEnvironment,
        FileProcessingQueue queue)
        : IHostedService
    {
        private readonly FileSystemWatcher watcher = new FileSystemWatcher();
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
