using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace CommandLineUtilsSample.Commands
{
    [Command("add", Description = "Add new item")]
    public class Add
    {
        [Argument(0, "files", Description = "Files for add")]
        [Required(ErrorMessage = "Argument {0} is required")]
        public string[] Files { get; set; }

        public void OnExecute() 
            => Console.WriteLine($"Add command {Files?.Length}");

    }

}
