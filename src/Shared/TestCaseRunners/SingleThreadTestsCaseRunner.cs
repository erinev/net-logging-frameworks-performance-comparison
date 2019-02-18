using System;
using System.Diagnostics;
using Shared.Contracts;

namespace Shared.TestCaseRunners
{
    public class SingleThreadTestsCaseRunner
    {
        public static void Run(LoggingLib lib, LogFileType logFileType, Action<int, int> logAction, int numberOfRuns = 20, int logsCountPerRun = 100000)
        {
            string testCaseName = $"{lib} -> SingleThread -> {logFileType}";

            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"'{testCaseName}' case STARTED. Number of runs: '{numberOfRuns}', logs per run: '{logsCountPerRun}'");

            var stopWatch = new Stopwatch();

            long totalElapsedMs = 0;

            for (int i = 1; i <= numberOfRuns; i++)
            {
                stopWatch.Restart();

                for (int j = 1; j <= logsCountPerRun; j++)
                {
                    logAction(i, j);
                }

                stopWatch.Stop();

                string runNumber = i < 10 ? $"0{i}" : i.ToString();

                Console.WriteLine($"Run #{runNumber} completed in: {stopWatch.ElapsedMilliseconds} ms");
                totalElapsedMs += stopWatch.ElapsedMilliseconds;
            }

            int totalNumberOfLogsWritten = lib == LoggingLib.NoLoggingLib ? 0 : numberOfRuns * logsCountPerRun;

            Console.WriteLine($"'{testCaseName}' case FINISHED. Total logs written: '{totalNumberOfLogsWritten}', total elapsed: {totalElapsedMs} ms");
        }
    }
}
