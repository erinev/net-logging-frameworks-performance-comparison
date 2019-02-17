using NUnit.Framework;

namespace Shared
{
    [TestFixture]
    public class ForeachPerformanceTests
    {
        [Test]
        public void NoLogging_SingleThread_Foreach_Test()
        {
            SingleThreadTestsCaseRunner.Run(
                LoggingLib.None,
                Shared.LogFileType.Foreach,
                (runNr, logNrInRun) => { });
        }
    }
}
