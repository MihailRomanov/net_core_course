using System.CommandLine;

namespace SystemCommandLineSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rootCommand = new RootCommand();
            
            var fileArg = new Argument<FileInfo>("file", "File for print time");
            rootCommand.AddArgument(fileArg);
            
            var countOption = new Option<int>("--count", () => 1, 
                description: "Print count");
            rootCommand.AddOption(countOption);

            rootCommand.SetHandler((file, count) =>
            {
                if (file.Exists)
                {
                    for (int i = 0; i < count; i++)
                    {
                        Console.WriteLine(file.CreationTimeUtc);
                    }
                }
                else
                {
                    Console.WriteLine("File not exist");
                }
            },
            fileArg, countOption);

            rootCommand.Invoke(args);
        }
    }
}
