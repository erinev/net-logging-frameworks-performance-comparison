using System.IO;
using Easy.Logger;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using Shared.Contracts;

namespace Shared.Loggers
{
    public class Log4NetLogger
    {
        private static readonly Hierarchy Hierarchy = (Hierarchy)LogManager.GetRepository();

        private static string LogOutputTemplate = "%date{yyyy'-'MM'-'dd HH':'mm':'ss'.'fff} [%thread] %level %logger - %message%newline";

        public static void ConfigureSimpleFileLogger(ThreadingType threadingType)
        {
            var logFileName = $"{Parameters.RootLogsDirectory}\\Log4Net.{threadingType}.SimpleFile.log";

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

        public static void ConfigureRollingSizeFileLogger(ThreadingType threadingType)
        {
            var logFileName = $"{Parameters.RootLogsDirectory}\\Log4Net.{threadingType}.RollingSizeFile.log";

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

                MaxSizeRollBackups = Parameters.MaxArchiveFiles,
                MaximumFileSize = Parameters.Log4NetArchiveAboveString,
                RollingStyle = RollingFileAppender.RollingMode.Size,
                StaticLogFileName = true
            };
            rollingFileAppender.ActivateOptions();
            Hierarchy.Root.AddAppender(rollingFileAppender);

            Hierarchy.Root.Level = Level.Info;

            Hierarchy.Configured = true;
        }

        public static void ConfigureOptimizedSimpleFileLogger(ThreadingType threadingType)
        {
            var logFileName = $"{Parameters.RootLogsDirectory}\\Log4Net.{threadingType}.Optimized.SimpleFile.log";

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

            var asyncBufferingForwarderAppender = new AsyncBufferingForwardingAppender()
            {
                Lossy = false,
                BufferSize = 512,
                Fix = FixFlags.ThreadName,
                IdleTime = 500
            };
            asyncBufferingForwarderAppender.AddAppender(simpleFileAppender);
            asyncBufferingForwarderAppender.ActivateOptions();
            Hierarchy.Root.AddAppender(asyncBufferingForwarderAppender);

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
