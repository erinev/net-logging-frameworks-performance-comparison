using System;
using System.Diagnostics;
using Shared.Contracts;
using Shared.Loggers;

namespace Shared.TestCaseRunners
{
    public class SingleThreadTestsCaseRunner
    {
        private static readonly Stopwatch SingleRunStopWatch = new Stopwatch();

        public static void Run(LoggingLib lib, LogFileType logFileType, Action<int, int> logAction)
        {
            int numberOfRuns = Parameters.NumberOfRuns;
            int logsCountPerRun = Parameters.NumberOfLogsPerRun;

            string testCaseName = $"{lib} -> {ThreadingType.SingleThreaded} -> {logFileType}";

            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"'{testCaseName}' case STARTED. Number of runs: '{numberOfRuns}', logs per run: '{logsCountPerRun}'");

            long sumOfAllRunsElapsedMs = 0;

            var testCaseStopWatch = Stopwatch.StartNew();

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

                Console.WriteLine($"Run #{runNumber} completed in: {SingleRunStopWatch.Elapsed.Milliseconds} ms");
                sumOfAllRunsElapsedMs += SingleRunStopWatch.Elapsed.Milliseconds;
            }

            testCaseStopWatch.Stop();

            int totalNumberOfLogsWritten = lib == LoggingLib.NoLoggingLib ? 0 : numberOfRuns * logsCountPerRun;

            Console.WriteLine($"'{testCaseName}' case FINISHED. Total logs written: '{totalNumberOfLogsWritten}', whole test case  run time: {testCaseStopWatch.Elapsed.Milliseconds} ms");
        }
    }
}
