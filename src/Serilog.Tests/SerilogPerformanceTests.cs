using NUnit.Framework;
using Serilog.Core;
using Shared;

namespace Serilog.Tests
{
    [TestFixture]
    public class SerilogPerformanceTests
    {
        [Explicit]
        [Test]
        public void Serilog_SingleThread_SimpleFileLog_Test()
        {
            Logger logger = SerilogLogger.ConfigureSimpleFileLogger();

            SingleThreadTestsCaseRunner.Run(
                LoggingLib.Serilog,  
                LogFileType.SimpleFile,
                (runNr, logNrInRun) => logger.Information($"Run #{runNr} - Log #{logNrInRun}"));
        }

        [Explicit]
        [Test]
        public void Serilog_SingleThread_RollingSizeFileLog_Test()
        {
            Logger logger = SerilogLogger.ConfigureRollingSizeFileLogger();

            SingleThreadTestsCaseRunner.Run(
                LoggingLib.Serilog,  
                LogFileType.RollingSizeFile,
                (runNr, logNrInRun) => logger.Information($"Run #{runNr} - Log #{logNrInRun}"));
        }
    }
}
