using System;
using System.Threading;
using log4net;
using NLog;
using NUnit.Framework;
using Shared.Contracts;
using Shared.Loggers;
using Shared.TestCaseRunners;

namespace Performance.Tests.SingleThreadedTests
{
    [TestFixture]
    public class AllLoggingLibsPerformanceTests
    {
        private static readonly ILog Log4NetLog = log4net.LogManager.GetLogger("PerformanceTests");
        private static readonly Logger NLogLog = NLog.LogManager.GetLogger("PerformanceTests");

        private static void SleepBeforeNextTestCaseRun()
        {
            Thread.Sleep(Parameters.SleepForMsBeforeNextTestCaseRunForCpuCalmDown);
        }

        #region Single threaded

        [Test]
        public void AllLibs_SingleThread_SimpleLogFile_Test()
        {
            Console.WriteLine($"'All libs -> single thread -> simple log file' test STARTED: {DateTime.Now}");


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
            

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine($"'All libs -> single thread -> simple log file' test FINISHED: {DateTime.Now}");
        }

        [Test]
        public void AllLibs_SingleThread_RollingSizeLogFile_Test()
        {
            Console.WriteLine($"'All libs -> single thread -> rolling size log file' test STARTED: {DateTime.Now}");


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
            

            Console.WriteLine($"'All libs -> single thread -> rolling size log file' test FINISHED: {DateTime.Now}");
        }

        #endregion

        #region Multi threaded

        [Test]
        public void AllLibs_MultiThread_SimpleLogFile_Test()
        {
            Console.WriteLine($"'All libs -> multi thread -> simple log file' test STARTED: {DateTime.Now}");


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
            

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine($"'All libs -> multi thread -> simple log file' test FINISHED: {DateTime.Now}");
        }

        [Test]
        public void AllLibs_MultiThread_RollingSizeLogFile_Test()
        {
            Console.WriteLine($"'All libs -> multi thread -> rolling size log file' test STARTED: {DateTime.Now}");


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
            

            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine($"'All libs -> multi thread -> rolling size log file' test FINISHED: {DateTime.Now}");
        }

        #endregion
    }
}
