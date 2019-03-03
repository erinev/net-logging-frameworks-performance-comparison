namespace Shared.Loggers
{
    public class Parameters
    {
        public static string RootLogsDirectory => 
            "C:\\Development\\Logging Frameworks POC\\net-logging-frameworks-performance-test\\logs";

        public static bool OutputEnvironmentInfoAfterRun = false;

        public static int ArchiveAboveBytes = 209715200;
        public static string Log4NetArchiveAboveString = "200MB";
        public static int MaxArchiveFiles = 5;

        public static int NumberOfRuns = 20;
        public static int NumberOfLogsPerParallelRun = 250000;
        public static int NumberOfLogsPerSyncRun = 100000;

        public static int SleepForMsBeforeNextParallelRun = 15 * 1000;
        public static int SleepForMsBeforeNextSyncRun = 3 * 1000;
    }
}
