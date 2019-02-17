using NUnit.Framework;
using Shared;

namespace NLog.Tests
{
    [TestFixture]
    public class NLogPerformanceTests
    {
        private static readonly Logger Logger = LogManager.GetLogger("PerformanceTests");

        [Test]
        public void NLog_SingleThread_SimpleFileLog_Test()
        {
            SingleThreadTestsCaseRunner.Run(
                LoggingLib.NLog,  
                LogFileType.SimpleFile,
                (runNr, logNrInRun) => Logger.Info($"Run #{runNr} - Log #{logNrInRun}"));
        }

        [Ignore("SimpleFileNow")]
        [Test]
        public void NLog_SingleThread_RollingFileLog_Test()
        {
            SingleThreadTestsCaseRunner.Run(
                LoggingLib.NLog,  
                LogFileType.RollingFile,
                (runNr, logNrInRun) => Logger.Info($"Run #{runNr} - Log #{logNrInRun}"));
        }
    }
}
