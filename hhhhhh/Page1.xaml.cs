using System;
using System.Windows.Controls;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.ResourceMonitoring;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using LiveCharts;
using LiveCharts.Wpf;
using System.Management;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows;

namespace hhhhhh
{








    public partial class Page1 : Page
    {









        private static System.Timers.Timer _timer;




        public Page1()
        {
            InitializeComponent();
         
            _timer = new System.Timers.Timer();
            _timer.Interval = 3000;
            _timer.Elapsed += OntimedEvent;
            _timer.AutoReset = true;














        }
        private void OntimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            int cpuvalue = GetCpuValue();
            int memvalue = GetMemValue();

            Dispatcher.Invoke(() =>
            {
                ProgressBarCpu.Value = cpuvalue;
                ProgressBarMem.Value = memvalue;
            });
        }

        private int GetCpuValue()
        {
            var CpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            CpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            int returnvalue = (int)CpuCounter.NextValue();
            return returnvalue;
        }

        private int GetMemValue()
        {
            var MemCounter = new PerformanceCounter("Memory", "% Committed Bytes in Use");
            int returnvalue = (int)MemCounter.NextValue();
            return returnvalue;
        }


        private void buttonstart__Click(object sender, RoutedEventArgs e)
        {
            _timer.Enabled = true;
        }


    }

}

