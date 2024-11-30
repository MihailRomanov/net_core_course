using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    public static partial class LoggingMessages
    {
        [LoggerMessage(
            50,
            LogLevel.Information,
            "App run from `{Dir}` with `{CmdLine}`")]
        public static partial void StartApp(
            this ILogger logger,
            string dir,
            string cmdLine);


        [LoggerMessage(
            51,
            LogLevel.Information,
            "Worker running at: {RunnigDate}")]
        public static partial void WorkerStep(
            this ILogger logger,
            DateTimeOffset runnigDate);

        [LoggerMessage(
            151,
            LogLevel.Debug,
            "Start pause")]
        public static partial void StartPause(this ILogger logger);

        [LoggerMessage(
            152,
            LogLevel.Debug,
            "Stop pause")]
        public static partial void StopPause(this ILogger logger);
    }

}
