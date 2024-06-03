using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace hhhhhh.NewFolder1
{
     public class Computerdata
    {

        public class UsageInfo
        {
            /// <summary>
            /// 전체 용량
            /// </summary>
            public ulong TotalSize { get; set; }

            /// <summary>
            /// 남은 용량
            /// </summary>
            public ulong FreeSize { get; set; }

            /// <summary>
            /// (readonly) 사용량
            /// </summary>
            public ulong UsedSize => TotalSize - FreeSize;

            /// <summary>
            /// (readonly) 사용률 
            /// </summary>
            public double Usage => ((double)(UsedSize) / (double)TotalSize) * 100;

            public static double GetProcessCpuUsage() // Process 사용률
            {
                try
                {
                    var wmi = new ManagementObjectSearcher($"select * from Win32_PerfFormattedData_PerfProc_Process where Name='{Process.GetCurrentProcess().ProcessName}'");
                    var procTime = wmi.Get().Cast<ManagementObject>().Select(mo => (long)(ulong)mo["PercentProcessorTime"]).FirstOrDefault();
                    var procUsage = procTime / Environment.ProcessorCount;

                    return (double)procUsage;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            public static double GetTotalCpuUsage() // 전체 CPU사용률
            {
                try
                {
                    var wmi = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor where Name != '_Total'");
                    var cpuUsages = wmi.Get().Cast<ManagementObject>().Select(mo => (long)(ulong)mo["PercentProcessorTime"]);
                    var totalUsage = cpuUsages.Average();

                    return (double)totalUsage;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            public static UsageInfo GetMemoryUsage() // 메모리 사용률
            {
                try
                {
                    var wmi = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
                    var info = wmi.Get().Cast<ManagementObject>().Select(mo => new UsageInfo()
                    {
                        TotalSize = ulong.Parse(mo["TotalVisibleMemorySize"].ToString()),
                        FreeSize = ulong.Parse(mo["FreePhysicalMemory"].ToString()),
                    }).FirstOrDefault();

                    return info;
                }
                catch (Exception)
                {
                    return null;
                }
            }


            public static UsageInfo GetHddUsage() // 디스크 사용률
            {
                try
                {
                    var driveName = Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory);
                    var d = new DriveInfo(driveName);
                    var info = new UsageInfo()
                    {
                        TotalSize = (ulong)d.TotalSize,
                        FreeSize = (ulong)(d.TotalFreeSpace)
                    };

                    return info;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
