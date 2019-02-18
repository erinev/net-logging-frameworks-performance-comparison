using System;
using NUnit.Framework;
using Shared.Contracts;
using Shared.TestCaseRunners;

namespace Performance.Tests.SingleThreadedTests
{
    [TestFixture]
    public class NoLoggingLibsPerformanceTests
    {
        [Explicit]
        [Test]
        public void NoLoggingLib_SingleThread_InMemoryForeach_Test()
        {
            Console.WriteLine($"'No logs -> single thread' test STARTED: {DateTime.Now}");


            #region No Logging

            SingleThreadTestsCaseRunner.Run(
                LoggingLib.NoLoggingLib,
                LogFileType.InMemoryForeach,
                (runNr, logNr) => { });

            #endregion

            
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine($"'No logs -> single thread' test FINISHED: {DateTime.Now}");
        }
    }
}
