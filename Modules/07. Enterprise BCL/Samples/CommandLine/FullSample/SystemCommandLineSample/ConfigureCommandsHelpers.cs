using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProcessingServices;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;

namespace SystemCommandLineSample
{
    internal static class ConfigureCommandsHelpers
    {
        public static RootCommand ConfigureRootCommand()
        {
            var rootCommand = new RootCommand();

            rootCommand.AddCommand(CreateListCommand());
            rootCommand.AddCommand(CreateAddCommand());
            rootCommand.AddCommand(CreateConvertCommand());

            return rootCommand;
        }

        private static Command CreateConvertCommand()
        {
            var convertCommand = new Command("convert", "Convert file");
            var inFileArgument = new Argument<FileInfo>("inFile", "File for conversion")
            {
                Arity = ArgumentArity.ExactlyOne
            };
            var outFileArgument = new Argument<FileInfo>("outFile", "Result file")
            {
                Arity = ArgumentArity.ExactlyOne
            };
            convertCommand.AddArgument(inFileArgument);
            convertCommand.AddArgument(outFileArgument);
            convertCommand.Handler =
                CommandHandler.Create(
                    (ConvertCommandOptions options, IHost host)
                    =>
                    {
                        var fileConverter = host.Services
                            .GetService<IFileConverter>();

                        fileConverter?
                            .Convert(
                                options.InFile.ToString(),
                                options.OutFile.ToString());

                        Console.WriteLine("Convertation is done");
                    });
            return convertCommand;
        }

        private static Command CreateAddCommand()
        {
            var addCommand = new Command("add", "Add new item");
            var addFilesArgument = new Argument<FileInfo[]>("files", "Files for add")
            {
                Arity = ArgumentArity.OneOrMore
            };
            addCommand.AddArgument(addFilesArgument);
            addCommand.SetHandler(
                (files) => Console.WriteLine($"Add command {files.Count()}"),
                addFilesArgument);
            return addCommand;
        }

        private static Command CreateListCommand()
        {
            var listCommand = new Command("list", "Show list");
            var printFullPathOption = new Option<bool>("--fullPath", "Print full path")
            {
                Arity = ArgumentArity.Zero
            };
            printFullPathOption.AddAlias("-f");

            var searchTemplateOption = new Option<string>(
                "--template", description: "Search template",
                getDefaultValue: () => "*.*")
            {
                Arity = ArgumentArity.ExactlyOne,
            };
            searchTemplateOption.AddAlias("-t");

            listCommand.AddOption(printFullPathOption);
            listCommand.AddOption(searchTemplateOption);

            listCommand.SetHandler((printFull, template)
                => Console.WriteLine($"List command {printFull} `{template}`"),
                printFullPathOption, searchTemplateOption);
            return listCommand;
        }
    }
}