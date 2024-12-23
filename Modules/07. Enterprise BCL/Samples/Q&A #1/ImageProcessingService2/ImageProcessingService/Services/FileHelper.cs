using Polly;
using Polly.Retry;
using System.IO.Abstractions;

namespace ImageProcessingService.Services
{
    public class FileHelper
    {
        private readonly IFileSystem fileSystem;
        private readonly ResiliencePipeline pipeline;

        public FileHelper(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
            pipeline = new ResiliencePipelineBuilder()
                .AddRetry(new RetryStrategyOptions())
                .Build();
        }

        public Stream Open(string filePath)
        {
            return pipeline.Execute(() => fileSystem.File.OpenRead(filePath));
        }

        public void Delete(string filePath)
        {
            pipeline.Execute(() => fileSystem.File.Delete(filePath));
        }

    }
}
