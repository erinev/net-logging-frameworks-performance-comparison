using System.IO;
using NLog.Config;
using NLog.Targets;
using Shared;

namespace NLog.Tests
{
    public class NLogLogger
    {
        private static readonly string LogOutputTemplate = "${date:format=yyyy-MM-dd HH\\:mm\\:ss.fff} [${threadname}] ${level:uppercase=true} ${logger} - ${message}";

        public static void ConfigureSimpleFileLogger()
        {
            var logFileName = $"{Constants.RootLogsDirectory}\\NLog.SimpleFile.log";

            File.Delete(logFileName);

            var config = new LoggingConfiguration();

            var simpleLogFileTarget = new FileTarget("SimpleLogFileTarget")
            {
                FileName = logFileName,
                Layout = LogOutputTemplate,

                KeepFileOpen = true, // Improves performance drastically
            };
            config.AddTarget(simpleLogFileTarget);

            config.AddRuleForOneLevel(LogLevel.Info, simpleLogFileTarget); 

            LogManager.Configuration = config;
        }

        public static void ConfigureRollingSizeFileLogger()
        {
            var logFileName = $"{Constants.RootLogsDirectory}\\NLog.RollingSizeFile.log";

            File.Delete(logFileName);

            var config = new LoggingConfiguration();

            var rollingSizeLogFileTarget = new FileTarget("RollingSizeLogFileTarget")
            {
                FileName = logFileName,
                Layout = LogOutputTemplate,

                KeepFileOpen = true, // Improves performance drastically

                ArchiveAboveSize = Constants.ArchiveAboveBytes,
                ArchiveNumbering = ArchiveNumberingMode.Rolling,
                MaxArchiveFiles = Constants.MaxArchiveFiles
            };
            config.AddTarget(rollingSizeLogFileTarget);

            config.AddRuleForOneLevel(LogLevel.Info, rollingSizeLogFileTarget); 

            LogManager.Configuration = config;
        }
    }
}
