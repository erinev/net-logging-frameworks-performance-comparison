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
        private static readonly string LibName = "Log4net";
        private static readonly string LogFileType = "RollingLogFile"; // LogFile, RollingLogFile

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            XmlConfigurator.Configure();
        }

        [Test]
        public void Log4NetSingleThread()
        {
            SingleThreadTestsCaseRunner.Run(
                LibName,
                LogFileType,
                (runNr, logNrInRun) => Logger.Info($"Run #{runNr} - Log #{logNrInRun}"));
        }
    }
}
