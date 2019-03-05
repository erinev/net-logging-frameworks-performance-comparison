using System.Threading;
using log4net;
using NLog;
using NUnit.Framework;
using Shared;
using Shared.Contracts;
using Shared.Loggers;
using Shared.TestCaseRunners;

namespace Performance.Tests
{
    [TestFixture]
    public class SingleThreadedPerformanceTests
    {
        private static readonly ILog Log4NetLog = log4net.LogManager.GetLogger("PerformanceTests");
        private static readonly Logger NLogLog = NLog.LogManager.GetLogger("PerformanceTests");

         private static void SleepBeforeNextTestCaseRun()
        {
            Thread.Sleep(Parameters.SleepForMsBeforeNextSyncRun);
        }

        [Test]
        public void SingleThread_SimpleLogFile_Test()
        {
            SingleThreadTestsCaseRunner.ReportStartedTest(ThreadingType.SingleThreaded, LogFileType.SimpleFile);

            #region Log4net

            Log4NetLogger.ConfigureSimpleFileLogger(ThreadingType.SingleThreaded);
            SingleThreadTestsCaseRunner.Run(
                LoggingLib.Log4Net,
                LogFileType.SimpleFile,
                (runNr, logNr) => Log4NetLog.Info($"Run #{runNr} - Log #{logNr}"));
            Log4NetLogger.DisableLogger();

            #endregion

            SleepBeforeNextTestCaseRun();

            #region NLog

            NLogLogger.ConfigureSimpleFileLogger(ThreadingType.SingleThreaded);
            SingleThreadTestsCaseRunner.Run(
                LoggingLib.NLog,  
                LogFileType.SimpleFile,
                (runNr, logNr) => NLogLog.Info($"Run #{runNr} - Log #{logNr}"));

            #endregion

            SleepBeforeNextTestCaseRun();

            #region Serilog

            Serilog.Core.Logger serilogLogger = SerilogLogger.ConfigureSimpleFileLogger(ThreadingType.SingleThreaded);
            SingleThreadTestsCaseRunner.Run(
                LoggingLib.Serilog,  
                LogFileType.SimpleFile,
                (runNr, logNr) => serilogLogger.Information($"Run #{runNr} - Log #{logNr}"));

            #endregion
            
            SingleThreadTestsCaseRunner.ReportTestResults(ThreadingType.SingleThreaded, LogFileType.SimpleFile);
            
        }

        [Test]
        public void SingleThread_RollingSizeLogFile_Test()
        {
            SingleThreadTestsCaseRunner.ReportStartedTest(ThreadingType.SingleThreaded, LogFileType.RollingSizeFile);


            #region Log4net

            Log4NetLogger.ConfigureRollingSizeFileLogger(ThreadingType.SingleThreaded);
            SingleThreadTestsCaseRunner.Run(
                LoggingLib.Log4Net,
                LogFileType.RollingSizeFile,
                (runNr, logNr) => Log4NetLog.Info($"Run #{runNr} - Log #{logNr}"));
            Log4NetLogger.DisableLogger();

            #endregion

            SleepBeforeNextTestCaseRun();

            #region NLog

            NLogLogger.ConfigureRollingSizeFileLogger(ThreadingType.SingleThreaded);
            SingleThreadTestsCaseRunner.Run(
                LoggingLib.NLog,  
                LogFileType.RollingSizeFile,
                (runNr, logNr) => NLogLog.Info($"Run #{runNr} - Log #{logNr}"));

            #endregion

            SleepBeforeNextTestCaseRun();

            #region Serilog

            Serilog.Core.Logger serilogLog = SerilogLogger.ConfigureRollingSizeFileLogger(ThreadingType.SingleThreaded);
            SingleThreadTestsCaseRunner.Run(
                LoggingLib.Serilog,  
                LogFileType.RollingSizeFile,
                (runNr, logNr) => serilogLog.Information($"Run #{runNr} - Log #{logNr}"));

            #endregion
            

            SingleThreadTestsCaseRunner.ReportTestResults(ThreadingType.SingleThreaded, LogFileType.RollingSizeFile);
        }

        [Test]
        public void SingleThread_Optimized_SimpleLogFile_Test()
        {
            SingleThreadTestsCaseRunner.ReportStartedTest(ThreadingType.SingleThreaded, LogFileType.OptimizedSimpleFile);

            #region Log4net

            Log4NetLogger.ConfigureOptimizedSimpleFileLogger(ThreadingType.SingleThreaded);
            SingleThreadTestsCaseRunner.Run(
                LoggingLib.Log4Net,
                LogFileType.OptimizedSimpleFile,
                (runNr, logNr) => Log4NetLog.Info($"Run #{runNr} - Log #{logNr}"));
            Log4NetLogger.DisableLogger();

            #endregion

            SleepBeforeNextTestCaseRun();

//            #region NLog
//
//            NLogLogger.ConfigureSimpleFileLogger(ThreadingType.SingleThreaded);
//            SingleThreadTestsCaseRunner.Run(
//                LoggingLib.NLog,  
//                LogFileType.SimpleFile,
//                (runNr, logNr) => NLogLog.Info($"Run #{runNr} - Log #{logNr}"));
//
//            #endregion
//
//            SleepBeforeNextTestCaseRun();
//
//            #region Serilog
//
//            Serilog.Core.Logger serilogLogger = SerilogLogger.ConfigureSimpleFileLogger(ThreadingType.SingleThreaded);
//            SingleThreadTestsCaseRunner.Run(
//                LoggingLib.Serilog,  
//                LogFileType.SimpleFile,
//                (runNr, logNr) => serilogLogger.Information($"Run #{runNr} - Log #{logNr}"));
//
//            #endregion
            
            SingleThreadTestsCaseRunner.ReportTestResults(ThreadingType.SingleThreaded, LogFileType.OptimizedSimpleFile);
            
        }
    }
}
