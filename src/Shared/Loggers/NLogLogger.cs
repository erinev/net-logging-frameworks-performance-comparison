using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Targets.Wrappers;
using Shared.Contracts;

namespace Shared.Loggers
{
    public class NLogLogger
    {
        private static readonly string LogOutputTemplate = "${date:format=yyyy-MM-dd HH\\:mm\\:ss.fff} [${threadid}] ${level:uppercase=true} ${logger} - ${message} ${newline} ${exception:format=tostring}";

        public static void ConfigureSimpleFileLogger(ThreadingType threadingType)
        {
            var logFileName = $"{Parameters.RootLogsDirectory}\\NLog.{threadingType}.SimpleFile.log";

            File.Delete(logFileName);

            var config = new LoggingConfiguration();

            var simpleLogFileTarget = new FileTarget("SimpleLogFileTarget")
            {
                FileName = logFileName,
                Layout = LogOutputTemplate,

                KeepFileOpen = true, // Improves performance drastically (by default is set to false)
            };
            config.AddTarget(simpleLogFileTarget);

            config.AddRuleForOneLevel(LogLevel.Info, simpleLogFileTarget); 

            LogManager.Configuration = config;
        }

        public static void ConfigureRollingSizeFileLogger(ThreadingType threadingType)
        {
            var logFileName = $"{Parameters.RootLogsDirectory}\\NLog.{threadingType}.RollingSizeFile.log";

            File.Delete(logFileName);

            var config = new LoggingConfiguration();

            var rollingSizeLogFileTarget = new FileTarget("RollingSizeLogFileTarget")
            {
                FileName = logFileName,
                Layout = LogOutputTemplate,

                KeepFileOpen = true, // Improves performance drastically (by default is set to false)

                ArchiveAboveSize = Parameters.ArchiveAboveBytes,
                ArchiveNumbering = ArchiveNumberingMode.Rolling,
                MaxArchiveFiles = Parameters.MaxArchiveFiles
            };
            config.AddTarget(rollingSizeLogFileTarget);

            config.AddRule(LogLevel.Info, LogLevel.Fatal, rollingSizeLogFileTarget); 

            LogManager.Configuration = config;
        }

        public static void ConfigureOptimizedSimpleFileLogger(ThreadingType threadingType)
        {
            var logFileName = $"{Parameters.RootLogsDirectory}\\NLog.{threadingType}.Optimized.SimpleFile.log";

            File.Delete(logFileName);

            var config = new LoggingConfiguration();

            var simpleLogFileTarget = new FileTarget("SimpleLogFileTarget")
            {
                FileName = logFileName,
                Layout = LogOutputTemplate,

                KeepFileOpen = true, // Improves performance drastically (by default is set to false)
            };

            var asyncTargetWrapper = new AsyncTargetWrapper()
            {
                Name = "AsyncTargetWrapper",
                BatchSize = 50,
                FullBatchSizeWriteLimit = 1,
                OptimizeBufferReuse = true,
                OverflowAction = AsyncTargetWrapperOverflowAction.Grow,
                QueueLimit = 10000,
                TimeToSleepBetweenBatches = 0,
                WrappedTarget = simpleLogFileTarget
            };

            config.AddTarget(asyncTargetWrapper);

            config.AddRule(LogLevel.Info, LogLevel.Fatal, asyncTargetWrapper); 

            LogManager.Configuration = config;
        }
    }
}
