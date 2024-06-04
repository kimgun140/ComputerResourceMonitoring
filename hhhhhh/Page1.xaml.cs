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
using LiveCharts.Configurations;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using LiveCharts.Wpf.Charts.Base;

using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;








namespace hhhhhh
{


    public partial class Page1 : Page
    {


        private static System.Timers.Timer _timer;

        public ChartValues<double> C_Chart { get; set; }



        public Page1()
        {
            InitializeComponent();

            //_timer = new System.Timers.Timer();
            //_timer.Interval = 3000;
            //_timer.Elapsed += OntimedEvent;
            //_timer.AutoReset = true;
            //_timer.Enabled = true;

            //C_Chart = new ChartValues<double>();
            //Chart.DataContext = this;



  


        }

        //public void OntimedEvent(object source, System.Timers.ElapsedEventArgs e)
        //{
        //    int cpuvalue = GetCpuValue();
        //    int memvalue = GetMemValue();

        //    //Dispatcher.Invoke(() =>
        //    //{
        //    //    ProgressBarCpu.Value = cpuvalue;
        //    //    ProgressBarMem.Value = memvalue;
        //    //    memmm.Content = memvalue.ToString() + "%";
        //    //    cpuuu.Content = cpuvalue.ToString() + "%";


        //    //});
        //    if (ProgressBarCpu.Dispatcher.CheckAccess())
        //    {
        //        ProgressBarCpu.Value = cpuvalue;
        //        cpuuu.Content = cpuvalue.ToString() + "%";
        //        C_Chart.Add((int)cpuvalue);
        //    }
        //    else
        //    {
        //        ProgressBarCpu.Dispatcher.BeginInvoke(() =>
        //        {
        //            ProgressBarCpu.Value = cpuvalue;
        //            cpuuu.Content = cpuvalue.ToString + "%";
        //            C_Chart.Add((int)cpuvalue);
        //        });
        //    }
        //    if (ProgressBarMem.Dispatcher.CheckAccess())
        //    {
        //        ProgressBarMem.Value = memvalue;
        //        memmm.Content = memvalue.ToString() + "%";
        //    }
        //    else
        //    {
        //        ProgressBarMem.Dispatcher.BeginInvoke(() =>
        //        {
        //            ProgressBarMem.Value = memvalue;
        //            memmm.Content = memvalue.ToString() + "%";
                   
        //        });
        //    }
        //}

        //public int GetCpuValue()
        //{
        //    var CpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        //    CpuCounter.NextValue();
        //    System.Threading.Thread.Sleep(1000);
        //    int returnvalue = (int)CpuCounter.NextValue();
        //    return returnvalue;
        //}

        //public int GetMemValue()
        //{
        //    var MemCounter = new PerformanceCounter("Memory", "% Committed Bytes in Use");
        //    int returnvalue = (int)MemCounter.NextValue();
        //    return returnvalue;
        //}


        //private void buttonstart__Click(object sender, RoutedEventArgs e)
        //{
        //    _timer.Enabled = true;
        //}




    }

}

