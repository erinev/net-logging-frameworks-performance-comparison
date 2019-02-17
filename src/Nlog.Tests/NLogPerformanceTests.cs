﻿using NUnit.Framework;
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

        [Ignore("OtherNeedsToBeRunned")]
        [Test]
        public void NLog_SingleThread_RollingSizeFileLog_Test()
        {
            SingleThreadTestsCaseRunner.Run(
                LoggingLib.NLog,  
                LogFileType.RollingSizeFile,
                (runNr, logNrInRun) => Logger.Info($"Run #{runNr} - Log #{logNrInRun}"));
        }
    }
}
