using System.IO;
using NUnit.Framework;
using Serilog.Core;
using Shared;

namespace Serilog.Tests
{
    [TestFixture]
    public class SerilogPerformanceTests
    {
        private static readonly string LogOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [ThreadId: {ThreadId}] {Level:u3} {LoggerName} - {Message}{NewLine}";

        private static readonly string RootLogsDirectory =
            "C:\\Development\\Logging Frameworks POC\\net-logging-frameworks-performance-test\\logs";

        [Test]
        public void Serilog_SingleThread_SimpleFileLog_Test()
        {
            var logFileName = $"{RootLogsDirectory}\\Serilog.SimpleFile.log";

            File.Delete(logFileName);

            var logger = new LoggerConfiguration()
                .WriteTo.File(
                    logFileName, 
                    outputTemplate: LogOutputTemplate)
                .Enrich.WithThreadId()
                .Enrich.WithProperty("LoggerName", "PerformanceTests")
                .CreateLogger();

            SingleThreadTestsCaseRunner.Run(
                LoggingLib.Serilog,  
                LogFileType.SimpleFile,
                (runNr, logNrInRun) => logger.Information($"Run #{runNr} - Log #{logNrInRun}"));
        }

        [Explicit("OtherNeedsToBeRunned")]
        [Test]
        public void Serilog_SingleThread_RollingSizeFileLog_Test()
        {
            var logFileName = $"{RootLogsDirectory}\\Serilog.RollingSizeFile.log";

            File.Delete(logFileName);

            var logger = new LoggerConfiguration()
                .WriteTo.File(
                    logFileName, 
                    outputTemplate: LogOutputTemplate,
                    fileSizeLimitBytes: 52428800,
                    rollOnFileSizeLimit: true)
                .Enrich.WithThreadId()
                .Enrich.WithProperty("LoggerName", "PerformanceTests")
                .CreateLogger();

            SingleThreadTestsCaseRunner.Run(
                LoggingLib.Serilog,  
                LogFileType.RollingSizeFile,
                (runNr, logNrInRun) => logger.Information($"Run #{runNr} - Log #{logNrInRun}"));
        }
    }
}
