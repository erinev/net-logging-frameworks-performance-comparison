using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Shared.Contracts;
using Shared.Loggers;

namespace Shared.TestCaseRunners
{
    public class MultiThreadTestsCaseRunner
    {
        private static readonly Stopwatch TestCaseStopWatch = new Stopwatch();
        static readonly ThreadLocal<Stopwatch> LocalSingleRunStopwatch = new ThreadLocal<Stopwatch>(() => new Stopwatch());

        public static void Run(LoggingLib lib, LogFileType logFileType, Action<long, int> logAction)
        {
            int numberOfRuns = Parameters.NumberOfRuns + 1;
            int logsCountPerRun = Parameters.NumberOfLogsPerParallelRun;

            string testCaseName = $"{lib} -> {ThreadingType.MultiThreaded} -> {logFileType}";

            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"'{testCaseName}' case STARTED. Number of runs: '{numberOfRuns - 1}', logs per run: '{logsCountPerRun}'");

            long sumOfAllRunsElapsedMs = 0;

            TestCaseStopWatch.Reset();
            TestCaseStopWatch.Start();

            Parallel.For((long) 1, numberOfRuns, index => 
            { 
                LocalSingleRunStopwatch.Value.Reset();
                LocalSingleRunStopwatch.Value.Start();

                Task task = Task.Factory.StartNew(() =>
                {
                    for (int j = 1; j <= logsCountPerRun; j++)
                    {
                        logAction(index, j);
                    }
                });

                task.Wait();

                LocalSingleRunStopwatch.Value.Stop();

                string runNumber = index < 10 ? $"0{index}" : index.ToString();

                int singleRunTimeInMs = Convert.ToInt32(LocalSingleRunStopwatch.Value.Elapsed.TotalMilliseconds);
                Console.WriteLine($"Run #{runNumber} completed in: {singleRunTimeInMs} ms");
                    
                Interlocked.Add(ref sumOfAllRunsElapsedMs, singleRunTimeInMs);
            });

            TestCaseStopWatch.Stop();

            int totalNumberOfLogsWritten = lib == LoggingLib.NoLoggingLib ? 0 : (numberOfRuns - 1) * logsCountPerRun;

            int totalRunTimeInMs = Convert.ToInt32(TestCaseStopWatch.Elapsed.TotalMilliseconds);
            Console.WriteLine($"'{testCaseName}' case FINISHED. Total logs written: '{totalNumberOfLogsWritten}', whole test case run time: {totalRunTimeInMs} ms, sum of all parallel runs time: {sumOfAllRunsElapsedMs} ms");
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
