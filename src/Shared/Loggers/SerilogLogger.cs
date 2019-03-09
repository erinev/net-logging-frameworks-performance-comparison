using System.IO;
using Serilog;
using Serilog.Core;
using Shared.Contracts;

namespace Shared.Loggers
{
    public class SerilogLogger
    {
        private static readonly string LogOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{ThreadId}] {Level:u4} {LoggerName} - {Message}{NewLine}{Exception}";

        public static Logger ConfigureSimpleFileLogger(ThreadingType threadingType)
        {
            var logFileName = $"{Parameters.RootLogsDirectory}\\Serilog.{threadingType}.SimpleFile.log";

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

        public static Logger ConfigureRollingSizeFileLogger(ThreadingType threadingType)
        {
            var logFileName = $"{Parameters.RootLogsDirectory}\\Serilog.{threadingType}.RollingSizeFile.log";

            File.Delete(logFileName);

            var logger = new LoggerConfiguration()
                .WriteTo.File(
                    logFileName, 
                    outputTemplate: LogOutputTemplate,

                    rollOnFileSizeLimit: true,
                    fileSizeLimitBytes: Parameters.ArchiveAboveBytes,
                    retainedFileCountLimit: Parameters.MaxArchiveFiles)
                .Enrich.WithThreadId()
                .Enrich.WithProperty("LoggerName", "PerformanceTests")
                .MinimumLevel.Information()
                .CreateLogger();

            return logger;
        }
    }
}
