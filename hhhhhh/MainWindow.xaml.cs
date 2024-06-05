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


        public class db
        {
            private string ip;
            private int port;
            private string pwd;
            private string dbname;
            private string uid;
            private string connectString;
            MySqlConnection conn = null;

            public db()
            {
                string ip = "127.0.0.1";
                int port = 3306;
                string uid = "Student";
                string pwd = "1234";
                string dbname = "com";
                string connectString = $"Server={ip};Port={port};Database={dbname};uid={uid};pwd={pwd};CharSet=utf8;";
                conn = new MySqlConnection(connectString);

            }
            public void cpudata(int cpuvalue)
            {
                try
                {
                    //연결 확인 
                    conn.Open();
                    conn.Ping();
                    //실행할 쿼리문 
                    string query = "INSERT INTO cpu_data VALUES (DEFAULT," + cpuvalue + ");";
                    //쿼리 명령 실행
                    
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                        
                    cmd.ExecuteNonQuery();// 결과 집합이 없는  sql 전송 (적용된 행의 수 리턴)
                    conn.Close();

                }
                catch (MySqlException e)
                {

                }

            }
            public void memdata(int memvalue)
            {
                try
                {
                    //연결확인
                    conn.Open();
                    conn.Ping();
                    // 실행할 쿼리문 
                    string query = "INSERT INTO mem_data VALUES (DEFAULT, " + memvalue + ");";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
                catch (MySqlException e)
                {

                }
            }
            public void procdata(double procvalue)
            {
                try
                {
                    //연결확인
                    conn.Open();
                    conn.Ping();
                    // 실행할 쿼리문 
                    string query = "INSERT INTO proc_data VALUES (DEFAULT, " + procvalue + ");";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
                catch (MySqlException e)
                {
                    

                }
            }
            //public string selectdata(int cpuvalue)
            //{
            //    try
            //    {
            //        //연결 확인 
            //        conn.Open();
            //        conn.Ping();
            //        //실행할 쿼리문 
            //        //string query = "INSERT INTO cpu_data VALUES (DEFAULT," + cpuvalue + ");";
            //        string query = "SELECT * FROM cpu_data;";

            //        //string query = "INSERT INTO cpu_data VALUES (date_format(NOW(), '%Y-%m-%d-%H-%i-%s')," + cpuvalue + ");";

            //        MySqlDataReader dr = null;
            //        //쿼리 담을 LIst
            //        List<string> result = new List<string>();
            //        //dataset를 사용하면 데이터 베이스를 통째로 가져올 수 있겠네 dataset안에 datatable의 이름으로 검색해서 datarow에 접근 할 수 있겠네 ?
            //        //쿼리 명령 실행
            //        MySqlCommand cmd = new MySqlCommand(query, conn);
            //        //cmd.ExecuteNonQuery();// 결과 집합이 없는  sql 전송 (적용된 행의 수 리턴)
            //        dr = cmd.ExecuteReader(); //결과 집합이 있는 sql 전송(결과 집합을 담은 reader 객체 리턴 (select)

            //        //조회 결과 select 문 쓸때 
            //        //while (dr.Read())
            //        //{
            //        //    //데이터 조회시 null 값이 있을 경우에는 예외처리 필요.
            //        //    result.Add($"{(dr[0].ToString())},{dr[1].ToString()}");
            //        //}
            //        //foreach (string item in result)
            //        //{
            //        //    Console.WriteLine(item);
            //        //}
            //        conn.Close();
            //        return "ok";

            //        //return 0;
            //    }
            //    catch (MySqlException e)
            //    {
            //        return e.Message; ;

            //    }

            //}
        }

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

            Chart.DataContext = this;
            Chart1.DataContext = this;
            Chart2.DataContext = this;

        }
        db cpudb = new db();//  디비 객체를 여기 만들어야하네

        public void OntimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            int cpuvalue = GetCpuValue();
            int memvalue = GetMemValue();
            int processvalue = int.Parse(GetProcessCpuUsage().ToString());
            //Dispatcher.Invoke(() =>
            //{
            //    ProgressBarCpu.Value = cpuvalue;
            //    ProgressBarMem.Value = memvalue;
            //    memmm.Content = memvalue.ToString() + "%";
            //    cpuuu.Content = cpuvalue.ToString() + "%";


            //});
            if (ProgressBarCpu.Dispatcher.CheckAccess())
            {
                ProgressBarCpu.Value = cpuvalue;
                cpuuu.Content = cpuvalue.ToString() + "%";
                C_Chart.Add((int)cpuvalue);
            }
            else
            {
                ProgressBarCpu.Dispatcher.BeginInvoke((System.Action)(() =>
                {
                    ProgressBarCpu.Value = cpuvalue;
                    cpuuu.Content = cpuvalue;
                    C_Chart.Add((int)cpuvalue);
                }));
            }
            if (ProgressBarMem.Dispatcher.CheckAccess())
            {
                ProgressBarMem.Value = memvalue;
                memmm.Content = memvalue.ToString() + "%";
                M_Chart.Add((int)memvalue);
            }
            else
            {
                ProgressBarMem.Dispatcher.BeginInvoke((System.Action)(() =>
                {
                    ProgressBarMem.Value = memvalue;
                    memmm.Content = memvalue.ToString() + "%";
                    M_Chart.Add((int)memvalue);

                }));
            }
            if (ProgressProcess.Dispatcher.CheckAccess())
            {
                ProgressProcess.Value = processvalue;
                processsss.Content = processvalue.ToString();
                P_Chart.Add((int)processvalue);
            }
            else
            {
                ProgressProcess.Dispatcher.BeginInvoke((System.Action)(() =>
                {
                    ProgressProcess.Value = processvalue;
                    processsss.Content = processvalue.ToString();
                    P_Chart.Add((int)processvalue);
                }));
            }



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

        private async void background()
        {

            await Task.Run(async () =>
            {
                while (true)
                {
                    int cpuvalue = GetCpuValue();
                     cpudb.cpudata(cpuvalue);
                    //cpudatatext.Text = a;
                    
                    int memvalue = GetMemValue();
                    cpudb.memdata(memvalue);
                    double procvalue = GetProcessCpuUsage();
                    cpudb.procdata(procvalue);

                    await Task.Delay(3000);
                }
            });
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //int cpuvalue = GetCpuValue();
            //string a = cpudb.cpudata(cpuvalue);
            //cpudatatext.Text = a;
            //int memvalue = GetMemValue();
            //string b = cpudb.memdata(memvalue);
            //memdatatext.Text = b;
            //double procvalue = GetProcessCpuUsage();
            //string c = cpudb.procdata(procvalue);
            //procdatatext.Text = c;
        }
    }
}
