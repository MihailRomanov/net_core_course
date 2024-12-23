using System.Threading.Channels;

namespace ImageProcessingService
{
    public record FileProcessingItem(string FilePath);

    internal class FileProcessingQueue
    {
        private readonly Channel<FileProcessingItem> channel =
            Channel.CreateUnbounded<FileProcessingItem>();

        public async Task Add(FileProcessingItem item)
            => await channel.Writer.WriteAsync(item);

        public async Task<FileProcessingItem> Get(CancellationToken cancellationToken)
            => await channel.Reader.ReadAsync(cancellationToken);
    }
}
