namespace SystemCommandLineSample
{
    public class ConvertCommandOptions(
        FileInfo inFile, FileInfo outFile)
    {
        public FileInfo InFile => inFile;
        public FileInfo OutFile => outFile;
    }
}
