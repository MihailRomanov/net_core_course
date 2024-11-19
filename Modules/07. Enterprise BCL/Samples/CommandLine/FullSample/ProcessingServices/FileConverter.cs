using Microsoft.Extensions.Logging;

namespace ProcessingServices
{
    public interface IFileConverter
    {
        bool Convert(string inFile, string outFile);
    }

    public class FileConverter(ILogger<FileConverter> logger) : IFileConverter
    {
        public bool Convert(string inFile, string outFile)
        {
            logger.LogDebug($"Convert {inFile} to {outFile}");
            return true;
        }
    }
}
