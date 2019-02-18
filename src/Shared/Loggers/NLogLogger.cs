using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;
using Shared.Contracts;

namespace Shared.Loggers
{
    public class NLogLogger
    {
        private static readonly string LogOutputTemplate = "${date:format=yyyy-MM-dd HH\\:mm\\:ss.fff} [${threadid}] ${level:uppercase=true} ${logger} - ${message}";

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

            config.AddRuleForOneLevel(LogLevel.Info, rollingSizeLogFileTarget); 

            LogManager.Configuration = config;
        }
    }
}
