namespace Generators
{
    public class ExportersTestsBase
    {
        protected static string PrepareTestDir(string dir)
        {
            var exportDir = Path.Combine(Directory.GetCurrentDirectory(), dir);

            if (Directory.Exists(exportDir))
                Directory.Delete(exportDir, true);

            Directory.CreateDirectory(exportDir);
            return exportDir;
        }
    }
}