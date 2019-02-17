using log4net;
using log4net.Config;
using NUnit.Framework;
using Shared;

namespace Log4net.Tests
{
    [TestFixture]
    public class Log4NetPerformanceTests
    {
        private static readonly ILog Logger = LogManager.GetLogger("PerformanceTests");

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            XmlConfigurator.Configure();
        }

        [Test]
        public void Log4Net_SingleThread_SimpleFileLog_Test()
        {
            SingleThreadTestsCaseRunner.Run(
                LoggingLib.Log4Net,
                Shared.LogFileType.SimpleFile,
                (runNr, logNrInRun) => Logger.Info($"Run #{runNr} - Log #{logNrInRun}"));
        }

        [Ignore("SimpleFileNow")]
        [Test]
        public void Log4Net_SingleThread_RollingFileLog_Test()
        {
            SingleThreadTestsCaseRunner.Run(
                LoggingLib.Log4Net,
                Shared.LogFileType.RollingFile,
                (runNr, logNrInRun) => Logger.Info($"Run #{runNr} - Log #{logNrInRun}"));
        }
    }
}
