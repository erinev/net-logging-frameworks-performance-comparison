using log4net;
using NUnit.Framework;
using Shared;

namespace Log4net.Tests
{
    [TestFixture]
    public class Log4NetPerformanceTests
    {
        private static readonly ILog Logger = LogManager.GetLogger("PerformanceTests");
        
        [Explicit]
        [Test]
        public void Log4Net_SingleThread_SimpleFileLog_Test()
        {
            Log4NetLogger.ConfigureSimpleFileLogger();

            SingleThreadTestsCaseRunner.Run(
                LoggingLib.Log4Net,
                Shared.LogFileType.SimpleFile,
                (runNr, logNrInRun) => Logger.Info($"Run #{runNr} - Log #{logNrInRun}"));
        }

        [Explicit]
        [Test]
        public void Log4Net_SingleThread_RollingSizeFileLog_Test()
        {
            Log4NetLogger.ConfigureRollingSizeFileLogger();

            SingleThreadTestsCaseRunner.Run(
                LoggingLib.Log4Net,
                Shared.LogFileType.RollingSizeFile,
                (runNr, logNrInRun) => Logger.Info($"Run #{runNr} - Log #{logNrInRun}"));
        }
    }
}
