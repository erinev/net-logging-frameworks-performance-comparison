using System.IO;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using Shared;

namespace Log4net.Tests
{
    public class Log4NetLogger
    {
        private static string LogOutputTemplate = "%date{yyyy'-'MM'-'dd HH':'mm':'ss'.'fff} [%thread] %level %logger - %message%newline";

        public static void ConfigureSimpleFileLogger()
        {
            var logFileName = $"{Constants.RootLogsDirectory}\\Log4Net.SimpleFile.log";

            File.Delete(logFileName);

            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout
            {
                ConversionPattern = LogOutputTemplate
            };
            patternLayout.ActivateOptions();

            var simpleFileAppender = new FileAppender
            {
                File = logFileName,
                Layout = patternLayout
            };
            simpleFileAppender.ActivateOptions();
            hierarchy.Root.AddAppender(simpleFileAppender);

            hierarchy.Root.Level = Level.Info;

            hierarchy.Configured = true;
        }

        public static void ConfigureRollingSizeFileLogger()
        {
            var logFileName = $"{Constants.RootLogsDirectory}\\Log4Net.RollingSizeFile.log";

            File.Delete(logFileName);

            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout
            {
                ConversionPattern = LogOutputTemplate
            };
            patternLayout.ActivateOptions();

            var rollingFileAppender = new RollingFileAppender
            {
                File = logFileName,
                Layout = patternLayout,

                MaxSizeRollBackups = Constants.MaxArchiveFiles,
                MaximumFileSize = "50MB",
                RollingStyle = RollingFileAppender.RollingMode.Size,
                StaticLogFileName = true
            };
            rollingFileAppender.ActivateOptions();
            hierarchy.Root.AddAppender(rollingFileAppender);

            hierarchy.Root.Level = Level.Info;

            hierarchy.Configured = true;
        }
    }
}
