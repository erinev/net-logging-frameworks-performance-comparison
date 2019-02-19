namespace Shared.Loggers
{
    public class Parameters
    {
        public static string RootLogsDirectory => 
            "C:\\Development\\Logging Frameworks POC\\net-logging-frameworks-performance-test\\logs";

        public static int ArchiveAboveBytes = 209715200;
        public static string Log4NetArchiveAboveString = "200MB";
        public static int MaxArchiveFiles = 5;

        public static int NumberOfRuns = 20;
        public static int NumberOfLogsPerRun = 50000;

        public static int SleepForMsBeforeNextTestCaseRunForCpuCalmDown = 10 * 1000;
    }
}
