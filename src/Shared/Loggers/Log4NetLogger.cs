using System.IO;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace Shared.Loggers
{
    public class Log4NetLogger
    {
        private static readonly Hierarchy Hierarchy = (Hierarchy)LogManager.GetRepository();

        private static string LogOutputTemplate = "%date{yyyy'-'MM'-'dd HH':'mm':'ss'.'fff} [%thread] %level %logger - %message%newline";

        public static void ConfigureSimpleFileLogger()
        {
            var logFileName = $"{Constants.RootLogsDirectory}\\Log4Net.SimpleFile.log";

            File.Delete(logFileName);

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
            Hierarchy.Root.AddAppender(simpleFileAppender);

            Hierarchy.Root.Level = Level.Info;

            Hierarchy.Configured = true;
        }

        public static void ConfigureRollingSizeFileLogger()
        {
            var logFileName = $"{Constants.RootLogsDirectory}\\Log4Net.RollingSizeFile.log";

            File.Delete(logFileName);

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
                MaximumFileSize = Constants.Log4NetArchiveAboveString,
                RollingStyle = RollingFileAppender.RollingMode.Size,
                StaticLogFileName = true
            };
            rollingFileAppender.ActivateOptions();
            Hierarchy.Root.AddAppender(rollingFileAppender);

            Hierarchy.Root.Level = Level.Info;

            Hierarchy.Configured = true;
        }

        public static void DisableLogger()
        {
            Hierarchy.Root.RemoveAllAppenders();

            Hierarchy.Configured = true;
        }
    }
}
