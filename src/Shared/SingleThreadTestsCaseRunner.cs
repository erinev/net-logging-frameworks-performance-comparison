using System;
using System.Diagnostics;

namespace Shared
{
    public class SingleThreadTestsCaseRunner
    {
        public static void Run(LoggingLib lib, LogFileType logFileType, Action<int, int> logAction, int numberOfRuns = 10, int logsCountPerRun = 50000)
        {
            string testCaseName = $"{lib}_SingleThread_{logFileType}";

            Console.WriteLine($"Test '{testCaseName}' STARTED. Number of runs: '{numberOfRuns}', logs per run: '{logsCountPerRun}'");

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

                Console.WriteLine($"Run #{i} completed in: {stopWatch.ElapsedMilliseconds} ms");
                totalElapsedMs += stopWatch.ElapsedMilliseconds;
            }

            Console.WriteLine($"Test '{testCaseName}' FINISHED. Total logs written: '{numberOfRuns * logsCountPerRun}', total elapsed: {totalElapsedMs} ms");
        }
    }
}
