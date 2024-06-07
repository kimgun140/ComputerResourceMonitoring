using hhhhhh.NewFolder1;
using LiveCharts;
using LiveCharts.Wpf;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Crypto.Operators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static hhhhhh.NewFolder1.Computerdata;
using MySql.Data.MySqlClient;
using System.Windows.Threading;
using static hhhhhh.pooooooooo;
using System.Web.UI.DataVisualization.Charting;

namespace hhhhhh
{


    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {

        private static System.Timers.Timer _timer;

        public ChartValues<double> C_Chart { get; set; }
        public ChartValues<double> M_Chart { get; set; }
        public ChartValues<double> P_Chart { get; set; }
        public ChartValues<double> Save_Chart { get; set; }
        //cpu

        public ChartValues<double> Save_Chart1 { get; set; }
        //메모리
        public ChartValues<double> Save_Chart2 { get; set; }
        //process



        //public ChartArea chrartasdf { get; set; }

        db cpudb = new db();//  



        public MainWindow()
        {
            InitializeComponent();
            _timer = new System.Timers.Timer();
            _timer.Interval = 3000;
            background();
            _timer.Elapsed += OntimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = true;

            C_Chart = new ChartValues<double>();
            M_Chart = new ChartValues<double>();
            P_Chart = new ChartValues<double>();
            Save_Chart = new ChartValues<double>();
            Save_Chart1 = new ChartValues<double>();
            Save_Chart2 = new ChartValues<double>();


            ///*ChartArea*/ chrartasdf = new ChartArea("");


            Chart.DataContext = this;
            Chart1.DataContext = this;
            Chart2.DataContext = this;
            Chart3.DataContext = this;
            Chart4.DataContext = this;
            Chart5.DataContext = this;
            //chartasdfname.DataContext = this;
        }


        async public void OntimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            int cpuvalue = GetCpuValue();
            int memvalue = GetMemValue();
            int processvalue = int.Parse(GetProcessCpuUsage().ToString());
            //Dispatcher.BeginInvoke((Action)(() =>
            //{
            //    ProgressBarCpu.Value = cpuvalue;
            //    ProgressBarMem.Value = memvalue;
            //    memmm.Content = memvalue.ToString() + "%";
            //    cpuuu.Content = cpuvalue.ToString() + "%";
            //}));


            await ProgressBarCpu.Dispatcher.BeginInvoke((System.Action)(() =>
                {
                    ProgressBarCpu.Value = cpuvalue;
                    cpuuu.Content = cpuvalue.ToString() + "%";
                    if (C_Chart.Count > 5)
                    {
                        //C_Chart.Clear();
                        C_Chart.RemoveAt(0);
                        M_Chart.RemoveAt(0);
                        P_Chart.RemoveAt(0);
                    }
                    C_Chart.Add((int)cpuvalue);
                }));



            await ProgressBarMem.Dispatcher.BeginInvoke((System.Action)(() =>
             {
                 ProgressBarMem.Value = memvalue;
                 memmm.Content = memvalue.ToString() + "%";
                 M_Chart.Add((int)memvalue);

             }));



            await ProgressProcess.Dispatcher.BeginInvoke((System.Action)(() =>
             {
                 ProgressProcess.Value = processvalue;
                 processsss.Content = processvalue.ToString() + "%";
                 P_Chart.Add((int)processvalue);


             }));




        }

        public int GetCpuValue()
        {
            var CpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            CpuCounter.NextValue();
            System.Threading.Thread.Sleep(1000);
            int returnvalue = (int)CpuCounter.NextValue();

            return returnvalue;
        }

        public int GetMemValue()
        {
            var MemCounter = new PerformanceCounter("Memory", "% Committed Bytes in Use");
            int returnvalue = (int)MemCounter.NextValue();
            return returnvalue;
        }



        public double GetProcessCpuUsage()
        //
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

        public async void background()
        {

            await Task.Run(async () =>
            {
                while (true)
                {
                    int cpuvalue = GetCpuValue();
                    cpudb.cpudata(cpuvalue);
                    int memvalue = GetMemValue();
                    cpudb.memdata(memvalue);
                    double procvalue = GetProcessCpuUsage();
                    cpudb.procdata(procvalue);

                    await Task.Delay(3000);
                }
            });
        }
        async public void Button_Click(object sender, RoutedEventArgs e)
        {
            Save_Chart.Clear();
            Save_Chart1.Clear();
            Save_Chart2.Clear();
            await Task.Run(async () =>
            {
                //await Task.Delay(1000);

                List<string> cdata = cpudb.select_cpudata();
                List<string> mdata = cpudb.select_memdata();
                List<string> pdata = cpudb.select_procdata();
                for (int i = 0; i < pdata.Count - 1; i++)
                {

                    await Dispatcher.BeginInvoke((Action)(async () =>
                        {
                            Chart3.Visibility = Visibility.Visible;
                            Chart4.Visibility = Visibility.Visible;
                            Chart5.Visibility = Visibility.Visible;
                            await Task.Run(() =>
                            {
                                Save_Chart.Add(Convert.ToInt32(cdata[i])); // 
                                Save_Chart1.Add(Convert.ToInt32(mdata[i]));
                                Save_Chart2.Add(Convert.ToInt32(pdata[i]));
                            });
                        }));
                }
            });
        }

        async public void Button_Click_1(object sender, RoutedEventArgs e)
        {

            await Task.Run(cpudb.SAVE_data);


        }
    }
}
