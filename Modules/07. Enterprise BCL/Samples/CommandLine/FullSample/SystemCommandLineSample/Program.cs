using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProcessingServices;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;

namespace SystemCommandLineSample
{

    internal class Program
    {
        static void Main(string[] args)
        {
            var rootCommand = ConfigureCommandsHelpers.ConfigureRootCommand();
            var commandLineBuilder = new CommandLineBuilder(rootCommand);

            commandLineBuilder
                .UseDefaults()
                .UseHost(
                    _ => Host.CreateDefaultBuilder(),
                    hostBuilder =>
                        hostBuilder.ConfigureServices(serviceCollection =>
                         {
                             serviceCollection.AddTransient<IFileConverter, FileConverter>();
                             serviceCollection.AddOptions<ConvertCommandOptions>()
                                .BindCommandLine();
                         })
                        .ConfigureLogging(loggingBuilder =>
                        {
                            loggingBuilder.AddConsole();
                            loggingBuilder.SetMinimumLevel(LogLevel.Debug);
                        })
                )
                .Build()
                .Invoke(args);
        }
    }
}
