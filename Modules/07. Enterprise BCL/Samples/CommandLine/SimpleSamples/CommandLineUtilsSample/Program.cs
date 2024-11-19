using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace CommandLineUtilsSample
{
    internal class Program
    {
        public class App
        {
            [Argument(0, "file", "File for print time")]
            [Required(ErrorMessage = "file argument is required")]
            public string FilePath { get; set; }

            [Option(CommandOptionType.SingleValue)]
            public int Count { get; set; } = 1;

            public void OnExecute(CommandLineApplication app)
            {
                var file = new FileInfo(FilePath);

                if (file.Exists)
                {
                    for (int i = 0; i < Count; i++)
                    {
                        Console.WriteLine(file.CreationTimeUtc);
                    }
                }
                else
                {
                    Console.WriteLine("File not exist");
                }
            }
        }

        static void Main(string[] args)
        {
            CommandLineApplication.Execute<App>(args);
        }
    }
}
