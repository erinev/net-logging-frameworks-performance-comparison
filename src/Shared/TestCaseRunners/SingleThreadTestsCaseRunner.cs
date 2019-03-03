using System;
using System.Diagnostics;
using Shared.Contracts;
using Shared.Loggers;

namespace Shared.TestCaseRunners
{
    public class SingleThreadTestsCaseRunner
    {
        private static readonly Stopwatch TestCaseStopWatch = new Stopwatch();
        private static readonly Stopwatch SingleRunStopWatch = new Stopwatch();

        public static void Run(LoggingLib lib, LogFileType logFileType, Action<int, int> logAction)
        {
            int numberOfRuns = Parameters.NumberOfRuns;
            int logsCountPerRun = Parameters.NumberOfLogsPerSyncRun;

            string testCaseName = $"{lib} -> {ThreadingType.SingleThreaded} -> {logFileType}";

            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"'{testCaseName}' case STARTED. Number of runs: '{numberOfRuns}', logs per run: '{logsCountPerRun}'");

            long sumOfAllRunsElapsedMs = 0;

            TestCaseStopWatch.Reset();
            TestCaseStopWatch.Start();

            for (int i = 1; i <= numberOfRuns; i++)
            {
                SingleRunStopWatch.Reset();
                SingleRunStopWatch.Start();

                for (int j = 1; j <= logsCountPerRun; j++)
                {
                    logAction(i, j);
                }

                SingleRunStopWatch.Stop();

                string runNumber = i < 10 ? $"0{i}" : i.ToString();

                int singleRunTimeInMs = Convert.ToInt32(SingleRunStopWatch.Elapsed.TotalMilliseconds);
                Console.WriteLine($"Run #{runNumber} completed in: {singleRunTimeInMs} ms");
                sumOfAllRunsElapsedMs += singleRunTimeInMs;
            }

            TestCaseStopWatch.Stop();

            int totalNumberOfLogsWritten = lib == LoggingLib.NoLoggingLib ? 0 : numberOfRuns * logsCountPerRun;

            int totalRunTimeInMs = Convert.ToInt32(TestCaseStopWatch.Elapsed.TotalMilliseconds);
            Console.WriteLine($"'{testCaseName}' case FINISHED. Total logs written: '{totalNumberOfLogsWritten}', whole test case run time: {totalRunTimeInMs} ms, sum of all single runs time: {sumOfAllRunsElapsedMs} ms");
        }

        public static void ReportStartedTest(ThreadingType threadingType, LogFileType logFileType)
        {
            Console.WriteLine($"'{threadingType} -> {logFileType}' test STARTED: {DateTime.Now}");
        }

        public static void ReportTestResults(ThreadingType threadingType, LogFileType logFileType)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine($"'{threadingType} -> {logFileType}' test FINISHED: {DateTime.Now}");

            if (Parameters.OutputEnvironmentInfoAfterRun)
            {
                EnvironmentInfoUtil.OutputEnvironmentInfo();
            }
        }
    }
}
