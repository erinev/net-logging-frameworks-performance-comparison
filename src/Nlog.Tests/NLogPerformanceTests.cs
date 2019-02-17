using NUnit.Framework;
using Shared;

namespace NLog.Tests
{
    [TestFixture]
    public class NLogPerformanceTests
    {
        private static readonly Logger Logger = LogManager.GetLogger("PerformanceTests");
        private static readonly string LibName = "NLog";
        private static readonly string LogFileType = "RollingLogFile"; // LogFile, RollingLogFile

        [Test]
        public void NLogSingleThread()
        {
            SingleThreadTestsCaseRunner.Run(
                LibName,  
                LogFileType,
                (runNr, logNrInRun) => Logger.Info($"Run #{runNr} - Log #{logNrInRun}"));
        }
    }
}
