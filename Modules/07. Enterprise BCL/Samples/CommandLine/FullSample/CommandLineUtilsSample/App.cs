using CommandLineUtilsSample.Commands;
using McMaster.Extensions.CommandLineUtils;

namespace CommandLineUtilsSample
{
    [Subcommand(typeof(List), typeof(Add), typeof(Commands.Convert))]
    public class App
    {
        public void OnExecute(CommandLineApplication app)
            => app.ShowHelp();
    }
}
