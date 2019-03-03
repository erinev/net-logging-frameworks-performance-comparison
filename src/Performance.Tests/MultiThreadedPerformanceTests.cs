using System.Threading;
using log4net;
using NLog;
using NUnit.Framework;
using Shared.Contracts;
using Shared.Loggers;
using Shared.TestCaseRunners;

namespace Performance.Tests
{
    [TestFixture]
    public class MultiThreadedPerformanceTests
    {
        private static readonly ILog Log4NetLog = log4net.LogManager.GetLogger("PerformanceTests");
        private static readonly Logger NLogLog = NLog.LogManager.GetLogger("PerformanceTests");

        private static void SleepBeforeNextTestCaseRun()
        {
            Thread.Sleep(Parameters.SleepForMsBeforeNextParallelRun);
        }

        [Test]
        public void MultiThread_SimpleLogFile_Test()
        {
            MultiThreadTestsCaseRunner.ReportStartedTest(ThreadingType.MultiThreaded, LogFileType.SimpleFile);

            #region Log4net

            Log4NetLogger.ConfigureSimpleFileLogger(ThreadingType.MultiThreaded);
            MultiThreadTestsCaseRunner.Run(
                LoggingLib.Log4Net,
                LogFileType.SimpleFile,
                (runNr, logNr) => Log4NetLog.Info($"Run #{runNr} - Log #{logNr}"));
            Log4NetLogger.DisableLogger();

            #endregion

            SleepBeforeNextTestCaseRun();

            #region NLog

            NLogLogger.ConfigureSimpleFileLogger(ThreadingType.MultiThreaded);
            MultiThreadTestsCaseRunner.Run(
                LoggingLib.NLog,  
                LogFileType.SimpleFile,
                (runNr, logNr) => NLogLog.Info($"Run #{runNr} - Log #{logNr}"));

            #endregion

            SleepBeforeNextTestCaseRun();

            #region Serilog

            Serilog.Core.Logger serilogLogger = SerilogLogger.ConfigureSimpleFileLogger(ThreadingType.MultiThreaded);
            MultiThreadTestsCaseRunner.Run(
                LoggingLib.Serilog,  
                LogFileType.SimpleFile,
                (runNr, logNr) => serilogLogger.Information($"Run #{runNr} - Log #{logNr}"));

            #endregion

            MultiThreadTestsCaseRunner.ReportTestResults(ThreadingType.MultiThreaded, LogFileType.SimpleFile);
        }

        [Test]
        public void MultiThread_RollingSizeLogFile_Test()
        {
            MultiThreadTestsCaseRunner.ReportStartedTest(ThreadingType.MultiThreaded, LogFileType.RollingSizeFile);

            #region Log4net

            Log4NetLogger.ConfigureRollingSizeFileLogger(ThreadingType.MultiThreaded);
            MultiThreadTestsCaseRunner.Run(
                LoggingLib.Log4Net,
                LogFileType.RollingSizeFile,
                (runNr, logNr) => Log4NetLog.Info($"Run #{runNr} - Log #{logNr}"));
            Log4NetLogger.DisableLogger();

            #endregion

            SleepBeforeNextTestCaseRun();

            #region NLog

            NLogLogger.ConfigureRollingSizeFileLogger(ThreadingType.MultiThreaded);
            MultiThreadTestsCaseRunner.Run(
                LoggingLib.NLog,  
                LogFileType.RollingSizeFile,
                (runNr, logNr) => NLogLog.Info($"Run #{runNr} - Log #{logNr}"));

            #endregion

            SleepBeforeNextTestCaseRun();

            #region Serilog

            Serilog.Core.Logger serilogLogger = SerilogLogger.ConfigureRollingSizeFileLogger(ThreadingType.MultiThreaded);
            MultiThreadTestsCaseRunner.Run(
                LoggingLib.Serilog,  
                LogFileType.RollingSizeFile,
                (runNr, logNr) => serilogLogger.Information($"Run #{runNr} - Log #{logNr}"));

            #endregion

            MultiThreadTestsCaseRunner.ReportTestResults(ThreadingType.MultiThreaded, LogFileType.RollingSizeFile);
        }
    }
}
