using NUnit.Framework;
using Shared.Contracts;
using Shared.TestCaseRunners;

namespace Performance.Tests
{
    [TestFixture]
    public class NoLoggingLibsPerformanceTests
    {
        [Test]
        public void NoLoggingLib_SingleThread_InMemoryForeach_Test()
        {
            SingleThreadTestsCaseRunner.ReportStartedTest(ThreadingType.SingleThreaded, LogFileType.InMemoryForeach);

            #region No Logging

            SingleThreadTestsCaseRunner.Run(
                LoggingLib.NoLoggingLib,
                LogFileType.InMemoryForeach,
                (runNr, logNr) => {  });

            #endregion

            
            SingleThreadTestsCaseRunner.ReportTestResults(ThreadingType.SingleThreaded, LogFileType.InMemoryForeach);
        }

        [Test]
        public void NoLoggingLib_MultiThread_InMemoryForeach_Test()
        {
            SingleThreadTestsCaseRunner.ReportStartedTest(ThreadingType.MultiThreaded, LogFileType.InMemoryForeach);


            #region No Logging

            MultiThreadTestsCaseRunner.Run(
                LoggingLib.NoLoggingLib,
                LogFileType.InMemoryForeach,
                (runNr, logNr) => {  });

            #endregion

            
            SingleThreadTestsCaseRunner.ReportTestResults(ThreadingType.MultiThreaded, LogFileType.InMemoryForeach);
        }
    }
}
