using System.IO;
using Serilog;
using Serilog.Core;

namespace Shared.Loggers
{
    public class SerilogLogger
    {
        private static readonly string LogOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [ThreadId: {ThreadId}] {Level:u3} {LoggerName} - {Message}{NewLine}";

        public static Logger ConfigureSimpleFileLogger()
        {
            var logFileName = $"{Constants.RootLogsDirectory}\\Serilog.SimpleFile.log";

            File.Delete(logFileName);

            var logger = new LoggerConfiguration()
                .WriteTo.File(
                    logFileName, 
                    outputTemplate: LogOutputTemplate)
                .Enrich.WithThreadId()
                .Enrich.WithProperty("LoggerName", "PerformanceTests")
                .MinimumLevel.Information()
                .CreateLogger();

            return logger;
        }

        public static Logger ConfigureRollingSizeFileLogger()
        {
            var logFileName = $"{Constants.RootLogsDirectory}\\Serilog.RollingSizeFile.log";

            File.Delete(logFileName);

            var logger = new LoggerConfiguration()
                .WriteTo.File(
                    logFileName, 
                    outputTemplate: LogOutputTemplate,

                    rollOnFileSizeLimit: true,
                    fileSizeLimitBytes: Constants.ArchiveAboveBytes,
                    retainedFileCountLimit: Constants.MaxArchiveFiles)
                .Enrich.WithThreadId()
                .Enrich.WithProperty("LoggerName", "PerformanceTests")
                .MinimumLevel.Information()
                .CreateLogger();

            return logger;
        }
    }
}
