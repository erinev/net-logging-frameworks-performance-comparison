using System;
using System.IO;
using System.Management;
using ByteSizeLib;

namespace Shared
{
    public class EnvironmentInfoUtil
    {
        private static readonly string EnvironmentInfoSeparator = "---";
        private static readonly string ProcessorNameProperty = "Name";
        private static readonly string NumberOfPhysicalProcessorsProperty = "NumberOfProcessors";
        private static readonly string NumberOfCoresProperty = "NumberOfCores";
        private static readonly string NumberOfLogicalProcessorsProperty = "NumberOfLogicalProcessors";

        public static void OutputEnvironmentInfo()
        {
            Console.WriteLine();

            Console.WriteLine("Testing environment info (OS / HW):");
            Console.WriteLine(EnvironmentInfoSeparator);

            OutputOperatingSystemInfo();
            Console.WriteLine(EnvironmentInfoSeparator);

            OutputCpuInfo();
            Console.WriteLine(EnvironmentInfoSeparator);

            OutputMemoryInfo();
            Console.WriteLine(EnvironmentInfoSeparator);

            OutputDiskInfo();
            Console.WriteLine(EnvironmentInfoSeparator);
        }

        private static void OutputOperatingSystemInfo()
        {
            Console.WriteLine("OS:");

            Console.WriteLine($"Name: WINDOWS {Environment.OSVersion.Version.Major}");
            Console.WriteLine($"Full version string: {Environment.OSVersion.VersionString}");
        }

        private static void OutputCpuInfo()
        {
            Console.WriteLine("CPU:");

            foreach (var item in new ManagementObjectSearcher("SELECT * FROM Win32_Processor").Get())
            {
                Console.WriteLine($"Processor name: {item[ProcessorNameProperty]}");
            }
            
            foreach (var item in new ManagementObjectSearcher("SELECT * FROM Win32_Processor").Get())
            {
                float maxClockSpeed = 0.001f * (uint) item["MaxClockSpeed"];
                Console.WriteLine($"Maximum CPU frequency: {maxClockSpeed} GHz");
            }

            foreach (var item in new ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
            {
                Console.WriteLine($"Processors count: {item[NumberOfPhysicalProcessorsProperty]}");
            }

            int coreCount = 0;
            foreach (var item in new ManagementObjectSearcher("Select * from Win32_Processor").Get())
            {
                coreCount += int.Parse(item[NumberOfCoresProperty].ToString());
            }
            Console.WriteLine($"Cores count: {coreCount}");


            foreach (var item in new ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
            {
                Console.WriteLine($"Logical processors count: {item[NumberOfLogicalProcessorsProperty]}");
            }
        }

        private static void OutputMemoryInfo()
        {
            Console.WriteLine("RAM:");

            foreach (var item in new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get())
            {
                double ramSizeInKb = Convert.ToDouble(item["TotalVisibleMemorySize"]);
                double ramSizeInGb = Math.Round((ramSizeInKb / (1024*1024)), 2);
                Console.WriteLine($"Total RAM: {ramSizeInGb} GB");
            }
        }
        
        private static void OutputDiskInfo()
        {
            Console.WriteLine("DISK:");

            FileInfo currentDirectoryInfo = new FileInfo(Directory.GetCurrentDirectory());    
            string driveNameOfCurrentProcess = Path.GetPathRoot(currentDirectoryInfo.FullName);

            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives) {
                if (drive.Name == driveNameOfCurrentProcess)
                {
                    Console.WriteLine($"Model: {GetDiskModel(drive.Name)}");
                    Console.WriteLine($"Drive letter: {drive.Name}");
                    Console.WriteLine($"File system: {drive.DriveFormat}");

                    if (drive.IsReady)
                    {
                        int totalDiskSize = Convert.ToInt32(ByteSize.FromBytes(drive.TotalSize).GigaBytes);
                        Console.WriteLine($"Total size: {Convert.ToInt32(totalDiskSize)} GB");

                        int totalFreeDiskSpace = Convert.ToInt32(ByteSize.FromBytes(drive.AvailableFreeSpace).GigaBytes);
                        Console.WriteLine($"Free space: {totalFreeDiskSpace} GB");
                    }
                }
            }
        }

        static string GetDiskModel(string driveName)
        {
            string model = "Unresolved";
            string driveNameLetter = driveName[0].ToString();

            foreach (var diskDriveItem in new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive").Get())
            {
                var diskDrive = (ManagementObject) diskDriveItem;

                foreach (var partitionItem in diskDrive.GetRelated("Win32_DiskPartition"))
                {
                    var partition = (ManagementObject) partitionItem;

                    foreach (var logicalDiskItem in partition.GetRelated("Win32_LogicalDisk"))
                    {
                        if (logicalDiskItem["Name"].ToString().StartsWith(driveNameLetter))
                        {
                            model = diskDrive["Model"].ToString();
                        }
                    }
                }
            }

            return model;
        }
    }
}
