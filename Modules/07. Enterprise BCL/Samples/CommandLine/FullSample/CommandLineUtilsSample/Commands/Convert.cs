using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using ProcessingServices;
using System.ComponentModel.DataAnnotations;

namespace CommandLineUtilsSample.Commands
{
    [Command("convert", Description = "Convert file")]
    public class Convert(
        IFileConverter fileConverter, 
        ILogger<Convert> logger,
        IConsole console)
    {
        [Argument(0, "inFile", Description = "File for conversion")]
        [Required(ErrorMessage = "Argument {0} is required")]
        public string InFile { get; set; }

        [Argument(1, "outFile", Description = "Result file")]
        [Required(ErrorMessage = "Argument {0} is required")]
        public string OutFile { get; set; }

        public void OnExecute()
        {
            fileConverter.Convert(InFile, OutFile);
            logger.LogDebug("Convertations is done");

            console.WriteLine("Convertation is done");
        }

    }

}
