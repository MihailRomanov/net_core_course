using NLog.Config;
using NLog;
using Serilog;

namespace ConsoleApp
{
    internal static class LogProvidersHelper
    {
        public static LoggingConfiguration GetNLogConfiguration()
        {
            return new LogFactory().Setup()
                .LoadConfiguration(builder =>
                    builder.ForLogger().WriteToFile("nlog.txt"))
                .LogFactory
                .Configuration;
        }

        public static LoggerConfiguration GetSerilogConfiguration()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File("slog.txt");
        }

    }
}
