using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Linq;

using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
//using Resource_Monitoring.Models;
//using static Resource_Monitoring.Models.Memory;

//namespace Resource_Monitoring
//{
//    /// <summary>
//    /// Interaction logic for MainWindow.xaml
//    /// </summary>

//    public partial class MainWindow : Window
//    {
//        private static System.Timers.Timer _timer;

//        public ChartValues<double> C_Chart { get; set; }

//        public MainWindow()
//        {

//            InitializeComponent();
//            _timer = new System.Timers.Timer();
//            _timer.Interval = 3000;
//            _timer.Elapsed += OntimedEvent;
//            _timer.AutoReset = true;
//            C_Chart = new ChartValues<double>();
//            Chart.DataContext = this;


//        }

//        public void OntimedEvent(object source, System.Timers.ElapsedEventArgs e)
//        {
//            // CheckForIllegalCrossThreadCalls = false;


//            int cpuvalue = GetCpuValue();
//            int memvalue = GetMemValue();



//            if (ProgressBarCPU.Dispatcher.CheckAccess())
//            {
//                ProgressBarCPU.Value = cpuvalue;
//                lblCpuUsage.Content = cpuvalue.ToString() + "%";
//                C_Chart.Add((int)cpuvalue);

//            }
//            else
//            {
//                ProgressBarCPU.Dispatcher.BeginInvoke(() =>
//                {
//                    ProgressBarCPU.Value = cpuvalue;
//                    lblCpuUsage.Content = cpuvalue.ToString() + "%";
//                    C_Chart.Add((int)cpuvalue);

//                });
//            }
//            if (ProgressBarMem.Dispatcher.CheckAccess())
//            {
//                ProgressBarMem.Value = memvalue;
//                lblMemUsage.Content = memvalue.ToString() + "%";
//            }
//            else
//            {
//                ProgressBarMem.Dispatcher.BeginInvoke(() =>
//                {
//                    ProgressBarMem.Value = memvalue;
//                    lblMemUsage.Content = memvalue.ToString() + "%";
//                });
//            }

//        }

//        public int GetCpuValue()
//        {
//            var CpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
//            CpuCounter.NextValue();
//            System.Threading.Thread.Sleep(1000);
//            int returnvalue = (int)CpuCounter.NextValue();
//            return returnvalue;
//        }

//        public int GetMemValue()
//        {
//            var MemCounter = new PerformanceCounter("Memory", "% Committed Bytes in Use");
//            int returnvalue = (int)MemCounter.NextValue();
//            return returnvalue;
//        }

//        /*private void ButtonStart_Click(object sender, RoutedEventArgs e)
//        {
//            _timer.Enabled = true;
//        }*/

//        public void Window_Activated(object sender, EventArgs e)
//        {
//            _timer.Enabled = true;

//        }


//    }
//}






