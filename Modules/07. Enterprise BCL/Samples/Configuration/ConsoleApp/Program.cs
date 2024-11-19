using Microsoft.Extensions.Configuration;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddInMemoryCollection(
                new Dictionary<string, string?>
                {
                    ["WorkDir"] = Environment.CurrentDirectory
                });


            configurationBuilder.AddJsonFile("AppConfig.json", true);
            configurationBuilder.AddEnvironmentVariables("App_");
            configurationBuilder.AddCommandLine(args);

            var configuration = configurationBuilder.Build();

            var workdir =
                configuration.GetSection("WorkDir").Value;

            Console.WriteLine($"WorkDir = {workdir}");
        }
    }
}
