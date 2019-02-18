namespace Shared.Loggers
{
    public class Constants
    {
        public static string RootLogsDirectory => 
            "C:\\Development\\Logging Frameworks POC\\net-logging-frameworks-performance-test\\logs";

        public static int ArchiveAboveBytes = 209715200;

        public static string Log4NetArchiveAboveString = "200MB";

        public static int MaxArchiveFiles = 5;
    }
}
