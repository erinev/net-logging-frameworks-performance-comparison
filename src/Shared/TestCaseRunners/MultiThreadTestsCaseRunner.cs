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
        public static void Run(LoggingLib lib, LogFileType logFileType, Action<long, int> logAction)
        {
            int numberOfRuns = Parameters.NumberOfRuns + 1;
            int logsCountPerRun = Parameters.NumberOfLogsPerRun;

            string testCaseName = $"{lib} -> {ThreadingType.MultiThreaded} -> {logFileType}";

            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"'{testCaseName}' case STARTED. Number of runs: '{numberOfRuns - 1}', logs per run: '{logsCountPerRun}'");

            var stopWatch = new Stopwatch();

            long totalElapsedMs = 0;

            Parallel.For((long) 1, numberOfRuns, index => 
            { 
                stopWatch.Restart();

                for (int j = 1; j <= logsCountPerRun; j++)
                {
                    logAction(index, j);
                }

                stopWatch.Stop();

                string runNumber = index < 10 ? $"0{index}" : index.ToString();

                Console.WriteLine($"Run #{runNumber} completed in: {stopWatch.ElapsedMilliseconds} ms");
                    
                Interlocked.Add(ref totalElapsedMs, stopWatch.ElapsedMilliseconds);
            });

            int totalNumberOfLogsWritten = lib == LoggingLib.NoLoggingLib ? 0 : (numberOfRuns - 1) * logsCountPerRun;

            Console.WriteLine($"'{testCaseName}' case FINISHED. Total logs written: '{totalNumberOfLogsWritten}', total elapsed: {totalElapsedMs} ms");
        }
    }
}
