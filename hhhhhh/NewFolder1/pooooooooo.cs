﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hhhhhh
{
    public class pooooooooo
    {
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
            public string cpudata(int cpuvalue)
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
                    return "ok";

                }
                catch (MySqlException e)
                {
                    return e.Message; ;

                }

            }
            public string memdata(int memvalue)
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
                    return "memok";

                }
                catch (MySqlException e)
                {
                    return e.Message;

                }
            }
            public string procdata(double procvalue)
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
                    return "procok";

                }
                catch (MySqlException e)
                {
                    return e.Message;

                }
            }
            public List<string> select_cpudata()
            {
                try
                {
                    conn.Close();
                    //conn.Open();
                    var datasett = new DataSet();
                    //실행할 쿼리문 
                    string query = "SELECT * FROM cpu_data;";
                    //command executereader로 가져온 데이터를 dr에 담아줄거임
                    //MySqlDataReader dr = null; // 타입을 var로 설정해줄  수 있네 mysqlreader로 직접안해줘도 ㄱㅊ
                    //쿼리 담을 LIst
                    List<string> result = new List<string>();
                    //dataset를 사용하면 데이터 베이스를 통째로 가져올 수 있겠네 dataset안에 datatable의 이름으로 검색해서 datarow에 접근 할 수 있겠네 ?
                    //쿼리 명령 실행
                    //MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    adapter.Fill(datasett, "cpu_data");

                    foreach (var row in datasett.Tables["cpu_data"].AsEnumerable())
                    {
                        result.Add(row["save_cpu"].ToString());
                    }
                    return result; // 리스트 반환 
                    //cmd.ExecuteNonQuery();// 결과 집합이 없는  sql 전송 (적용된 행의 수 리턴)
                    //dr = cmd.ExecuteReader(); //결과 집합이 있는 sql 전송(결과 집합을 담은 reader 객체 리턴 (select)

                    //조회 결과 select 문 쓸때 
                    //while (dr.Read())
                    //{
                    //    //데이터 조회시 null 값이 있을 경우에는 예외처리 필요.
                    //    result.Add($"{(dr[0].ToString())},{dr[1].ToString()}");
                    //}
                    //foreach (string item in result)
                    //{
                    //    Console.WriteLine(item);
                    //}
                    //conn.Close();
                    //return "ok";

                    //return 0;
                }
                catch (MySqlException e)
                {
                    return null;

                }

            }
            public List<string> select_memdata()
            {
                try
                {
                    conn.Close();
                    //conn.Open();
                    var datasett = new DataSet();
                    //실행할 쿼리문 
                    string query = "SELECT * FROM mem_data;";
                    //command executereader로 가져온 데이터를 dr에 담아줄거임
                    //MySqlDataReader dr = null; // 타입을 var로 설정해줄  수 있네 mysqlreader로 직접안해줘도 ㄱㅊ
                    //쿼리 담을 LIst
                    List<string> result = new List<string>();
                    //dataset를 사용하면 데이터 베이스를 통째로 가져올 수 있겠네 dataset안에 datatable의 이름으로 검색해서 datarow에 접근 할 수 있겠네 ?
                    //쿼리 명령 실행
                    //MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    adapter.Fill(datasett, "mem_data");

                    foreach (var row in datasett.Tables["mem_data"].AsEnumerable())
                    {
                        result.Add(row["save_mem"].ToString());
                    }
                    return result; // 리스트 반환 

                }
                catch (MySqlException e)
                {
                    return null;

                }

            }
            public List<string> select_procdata()
            {
                try
                {
                    conn.Close();
                    //conn.Open();
                    var datasett = new DataSet();
                    //실행할 쿼리문 
                    string query = "SELECT * FROM proc_data;";
                    //command executereader로 가져온 데이터를 dr에 담아줄거임
                    //MySqlDataReader dr = null; // 타입을 var로 설정해줄  수 있네 mysqlreader로 직접안해줘도 ㄱㅊ
                    //쿼리 담을 LIst
                    List<string> result = new List<string>();
                    //dataset를 사용하면 데이터 베이스를 통째로 가져올 수 있겠네 dataset안에 datatable의 이름으로 검색해서 datarow에 접근 할 수 있겠네 ?
                    //쿼리 명령 실행
                    //MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    adapter.Fill(datasett, "proc_data");

                    foreach (var row in datasett.Tables["proc_data"].AsEnumerable())
                    {
                        result.Add(row["save_proc"].ToString());
                    }
                    return result; // 리스트 반환 

                }
                catch (MySqlException e)
                {
                    return null;

                }

            }
        }
    }
}
