using McMaster.Extensions.CommandLineUtils;

namespace CommandLineUtilsSample.Commands
{
    [Command("list", Description = "Show list")]
    public class List
    {
        [Option("-f|--fullPath", CommandOptionType.NoValue,
            Description = "Print full path")]
        public bool PrintFullPath { get; set; }

        [Option("-t|--template", Description = "Search template")]
        public string SearchTemplate { get; set; } = "*.*";

        public void OnExecute()
            => Console.WriteLine($"List command {PrintFullPath} `{SearchTemplate}`");
    }

}
