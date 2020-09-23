using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using PayrollSystem.Models;
using BCrypt.Net;

namespace PayrollSystem.Database
{
    public sealed class DTRDatabase
     {
        public static string connectionString;

        private static DTRDatabase instance;

        private DTRDatabase() { }

        //Singleton Pattern
        public static DTRDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                        instance = new DTRDatabase();
                        instance.Initialize();
                }
                return instance;
            }
        }

        public void Initialize()
        {
            string server = "localhost";// "192.168.110.17";
            string database = "dohdtr";
            string uid = "root";// "doh7payroll";
            string password = "admin";//"doh7payroll";
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                    database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + "; pooling = false; SslMode=none; convert zero datetime=True";
        }

        public CookiesModel VerifyUser(string userId, string password)
        {
            string query = "SELECT du.username, du.fname, du.mname, du.lname, du.password, du.emptype " +
                            "FROM dohdtr.users du " +
                            "WHERE du.username = '" + userId + "'";

            CookiesModel user = null;
            using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (MySqlCommand command = new MySqlCommand(query, sqlConnection))
                {
                    MySqlDataReader dataReader = command.ExecuteReader();

                    if (dataReader.Read())
                    {
                        if(!string.IsNullOrEmpty(dataReader["username"].ToString()))
                        {
                            user = new CookiesModel
                            {
                                ID = dataReader["username"].ToString(),
                                Firstname = dataReader["fname"].ToString(),
                                Middlename = dataReader["mname"].ToString(),
                                Lastname = dataReader["lname"].ToString(),
                                Password = dataReader["password"].ToString(),
                                Role = dataReader["username"].ToString().Contains("admin")? "ADMIN" : dataReader["emptype"].ToString()
                            };
                        }
                    }

                    dataReader.Close();
                }

                sqlConnection.Close();
            }
            return user;
        }


        public bool CheckCTO(string userid, string date, string time)
        {

            bool found = false;
            string month = (int.Parse(date.Split('/')[0]) > 9) ? date.Split('/')[0] : "0" + date.Split('/')[0];
            string day = (int.Parse(date.Split('/')[1]) > 9) ? date.Split('/')[1] : "0" + date.Split('/')[1];
            string year = date.Split('/')[2];

            string mDate = year + "-" + month + "-" + day;
            string query = "SELECT userid FROM cdo_logs WHERE userid = '" + userid + "' AND datein = '" + mDate + "' AND time = '" + time + "' LIMIT 1";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        MySqlDataReader dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            found = true;
                        }
                        dataReader.Close();
                }
                SqlConnection.Close();
            }
            return found;
        }

        public bool CheckSO(string userid, string date, string time)
        {

            bool found = false;
            string month = (int.Parse(date.Split('/')[0]) > 9) ? date.Split('/')[0] : "0" + date.Split('/')[0];
            string day = (int.Parse(date.Split('/')[1]) > 9) ? date.Split('/')[1] : "0" + date.Split('/')[1];
            string year = date.Split('/')[2];

            string mDate = year + "-" + month + "-" + day;
            string query = "SELECT userid FROM so_logs WHERE userid = '" + userid + "' AND datein = '" + mDate + "' AND time = '" + time + "' LIMIT 1";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        MySqlDataReader dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            found = true;
                        }
                        dataReader.Close();
                }
                SqlConnection.Close();
            }
            return found;
        }

        public bool CheckLeave(string userid, string date, string time)
        {

            bool found = false;
            string month = (int.Parse(date.Split('/')[0]) > 9) ? date.Split('/')[0] : "0" + date.Split('/')[0];
            string day = (int.Parse(date.Split('/')[1]) > 9) ? date.Split('/')[1] : "0" + date.Split('/')[1];
            string year = date.Split('/')[2];

            string mDate = year + "-" + month + "-" + day;
            string query = "SELECT userid FROM leave_logs WHERE userid = '" + userid + "' AND datein = '" + mDate + "' AND time = '" + time + "' LIMIT 1";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        MySqlDataReader dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            found = true;
                        }
                        dataReader.Close();
                }
                SqlConnection.Close();
            }
            return found;
        }

        public bool IsHoliday(string date)
        {
            bool found = false;
            string month = (int.Parse(date.Split('/')[0]) > 9) ? date.Split('/')[0] : "0" + date.Split('/')[0];
            string day = (int.Parse(date.Split('/')[1]) > 9) ? date.Split('/')[1] : "0" + date.Split('/')[1];
            string year = date.Split('/')[2];

            string mDate = year + "-" + month + "-" + day;

            string query = "SELECT id FROM calendar WHERE start <= '" + mDate + "' AND end > '" + mDate + "' AND status = '1'";

            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        MySqlDataReader dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            found = true;
                        }
                        dataReader.Close();
                }
                SqlConnection.Close();
            }
            return found;
        }

        public bool ifWeekend(string date)
        {
            DateTime dateTime = new DateTime(int.Parse(date.Split('/')[2]), int.Parse(date.Split('/')[0]), int.Parse(date.Split('/')[1]));
            DayOfWeek datez = dateTime.DayOfWeek;
            if ((datez == DayOfWeek.Saturday) || (datez == DayOfWeek.Sunday))
            {
                return true;
            }
            return false;
        }

        public string GetMins(string id, string from, string to, string am_in, string am_out, string pm_in, string pm_out)
        {
            List<int> days = new List<int>();
            int month = int.Parse(from.Split('-')[1]);
            int year = int.Parse(from.Split('-')[0]);
            int from_days = int.Parse(from.Split('-')[2]);
            int to_days = int.Parse(to.Split('-')[2]);
            int no_days = DateTime.DaysInMonth(year, month);
            int working_days = 0;
            int mins = 0;
            int days_rendered = 0;
            string days_absent = "";


            for (int i = 0; i <= (to_days - from_days); i++)
            {
                days.Add((i + from_days));
                days_rendered++;
            }

            string query = "SELECT DISTINCT e.userid, datein,holiday,remark, (SELECT  CONCAT(t1.time, '_', t1.edited) " +
                "FROM dtr_file t1 WHERE userid = d.userid and datein = d.datein and t1.time < '" + am_out + "' AND t1.event = 'IN' ORDER BY time ASC LIMIT 1) " +
                "as am_in, (SELECT CONCAT(t2.time,'_',t2.edited) " +
                "FROM dtr_file t2 WHERE userid = d.userid and datein = d.datein and (SELECT CONCAT(t1.time,'_',t1.edited) " +
                "FROM dtr_file t1 WHERE userid = d.userid and datein = d.datein and t1.time < '" + am_out + "' AND t1.event = 'IN' ORDER BY time ASC LIMIT 1) " +
                "and t2.time < '" + pm_in + "' AND t2.event = 'OUT' AND t2.time > '" + am_in + "' ORDER BY t2.time DESC LIMIT 1 ) as am_out,(SELECT CONCAT(t3.time,'_',t3.edited) " +
                "FROM dtr_file t3 WHERE userid = d.userid AND datein = d.datein and t3.time > '" + am_out + "' and t3.time < '" + pm_out + "' AND t3.event = 'IN' ORDER BY t3.time ASC LIMIT 1) " +
                "as pm_in,(SELECT CONCAT(t4.time,'_',t4.edited) " +
                "FROM dtr_file t4 WHERE userid = d.userid AND datein = d.datein and t4.time >= '" + pm_in + "' AND t4.event = 'OUT' ORDER BY time DESC LIMIT 1) as pm_out " +
                "FROM dtr_file d LEFT JOIN users e ON d.userid = e.userid and datein = d.datein or (datein between '" + from + "' AND '" + to + "' and holiday = '001') " +
                "or (datein between '" + from + "' AND '" + to + "' and holiday = '002' and d.userid = e.userid) or (datein between '" + from + "' AND '" + to + "' and holiday = '003' and d.userid = e.userid) " +
                "or (datein between '" + from + "' AND '" + to + "' and holiday = '004' and d.userid = e.userid) or (datein between '" + from + "' AND '" + to + "' and holiday = '005' and d.userid = e.userid) " +
                "or (datein between '" + from + "' AND '" + to + "' and holiday = '006' and d.userid = e.userid) WHERE d.datein BETWEEN '" + from + "' AND '" + to + "' AND e.userid = '" + id + "' group by d.datein ORDER BY datein ASC";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        MySqlDataReader dataReader = cmd.ExecuteReader();
                        while (dataReader.Read())
                        {
                            //int day = int.Parse(dataReader["datein"].ToString().Split('-')[2]);
                            string holiday = dataReader["holiday"].ToString();
                            string date_in = dataReader["datein"].ToString().Split(' ')[0];
                            int day = int.Parse(date_in.Split('/')[1]);
                            string am_in1 = dataReader["am_in"].ToString();
                            if (!am_in1.Equals(""))
                            {
                                am_in1 = am_in1.Split('_')[0];
                            }
                            string am_out1 = dataReader["am_out"].ToString();
                            if (!am_out1.Equals(""))
                            {
                                am_out1 = am_out1.Split('_')[0];
                            }
                            string pm_in1 = dataReader["pm_in"].ToString();
                            if (!pm_in1.Equals(""))
                            {
                                pm_in1 = pm_in1.Split('_')[0];
                            }
                            string pm_out1 = dataReader["pm_out"].ToString();
                            if (!pm_out1.Equals(""))
                            {
                                pm_out1 = pm_out1.Split('_')[0];
                            }

                            ///CASE 1 WHOLEDAY
                            if (!am_in1.Equals("") && !am_out1.Equals("") && !pm_in1.Equals("") && !pm_out1.Equals(""))
                            {

                                //AM IN
                                TimeSpan am_in_span = TimeSpan.Parse(am_in1);
                                TimeSpan am_in_subtrahend = TimeSpan.Parse("08:00:00");
                                int am_in_second_subtrahend = (int)am_in_subtrahend.TotalSeconds;
                                int am_in_second_timespan = (int)am_in_span.TotalSeconds;
                                int result_am_in = (am_in_second_timespan - am_in_second_subtrahend) / 60;
                                if (result_am_in > 0)
                                {
                                    mins += result_am_in;
                                }
                                //AM OUT
                                TimeSpan am_out_span = TimeSpan.Parse(am_out1);
                                TimeSpan am_out_subtrahend = TimeSpan.Parse("12:00:00");
                                int am_out_second_subtrahend = (int)am_out_subtrahend.TotalSeconds;
                                int am_out_second_timespan = (int)am_out_span.TotalSeconds;
                                int result_am_out = (am_out_second_subtrahend - am_out_second_timespan) / 60;
                                if (result_am_out > 0)
                                {
                                    mins += result_am_out;
                                }
                                //PM IN
                                TimeSpan pm_in_span = TimeSpan.Parse(pm_in1);
                                TimeSpan pm_in_subtrahend = TimeSpan.Parse("13:00:00");
                                int pm_in_second_subtrahend = (int)pm_in_subtrahend.TotalSeconds;
                                int pm_in_second_timespan = (int)pm_in_span.TotalSeconds;
                                int result_pm_in = (pm_in_second_timespan - pm_in_second_subtrahend) / 60;
                                if (result_pm_in > 0)
                                {
                                    mins += result_pm_in;
                                }
                                // PM OUT
                                TimeSpan pm_out_span = TimeSpan.Parse(pm_out1);
                                TimeSpan pm_out_subtrahend = TimeSpan.Parse("17:00:00");
                                int pm_out_second_subtrahend = (int)pm_out_subtrahend.TotalSeconds;
                                int pm_out_second_timespan = (int)pm_out_span.TotalSeconds;
                                int result_pm_out = (pm_out_second_subtrahend - pm_out_second_timespan) / 60;
                                if (result_pm_out > 0)
                                {
                                    mins += result_pm_out;
                                }
                            }
                            ///CASE 2 
                            else if (!am_in1.Equals("") && !am_out1.Equals("") && !pm_in1.Equals("") && pm_out1.Equals(""))
                            {

                                //AM IN
                                TimeSpan am_in_span = TimeSpan.Parse(am_in1);
                                TimeSpan am_in_subtrahend = TimeSpan.Parse("08:00:00");
                                int am_in_second_subtrahend = (int)am_in_subtrahend.TotalSeconds;
                                int am_in_second_timespan = (int)am_in_span.TotalSeconds;
                                int result_am_in = (am_in_second_timespan - am_in_second_subtrahend) / 60;
                                if (result_am_in > 0)
                                {
                                    mins += result_am_in;
                                }
                                //AM OUT
                                TimeSpan am_out_span = TimeSpan.Parse(am_out1);
                                TimeSpan am_out_subtrahend = TimeSpan.Parse("12:00:00");
                                int am_out_second_subtrahend = (int)am_out_subtrahend.TotalSeconds;
                                int am_out_second_timespan = (int)am_out_span.TotalSeconds;
                                int result_am_out = (am_out_second_subtrahend - am_out_second_timespan) / 60;
                                if (result_am_out > 0)
                                {
                                    mins += result_am_out;
                                }
                                if (!CheckCTO(id, date_in, "13:00:00") || !CheckSO(id, date_in, "13:00:00") || !CheckLeave(id, date_in, "13:00:00"))
                                {
                                    mins += 240;
                                }
                            }
                            ///CASE 3 
                            else if (!am_in1.Equals("") && !am_out1.Equals("") && pm_in1.Equals("") && pm_out1.Equals(""))
                            {

                                //AM IN
                                TimeSpan am_in_span = TimeSpan.Parse(am_in1);
                                TimeSpan am_in_subtrahend = TimeSpan.Parse("08:00:00");
                                int am_in_second_subtrahend = (int)am_in_subtrahend.TotalSeconds;
                                int am_in_second_timespan = (int)am_in_span.TotalSeconds;
                                int result_am_in = (am_in_second_timespan - am_in_second_subtrahend) / 60;
                                if (result_am_in > 0)
                                {
                                    mins += result_am_in;
                                }
                                //AM OUT
                                TimeSpan am_out_span = TimeSpan.Parse(am_out1);
                                TimeSpan am_out_subtrahend = TimeSpan.Parse("12:00:00");
                                int am_out_second_subtrahend = (int)am_out_subtrahend.TotalSeconds;
                                int am_out_second_timespan = (int)am_out_span.TotalSeconds;
                                int result_am_out = (am_out_second_subtrahend - am_out_second_timespan) / 60;
                                if (result_am_out > 0)
                                {
                                    mins += result_am_out;
                                }

                                if (!CheckCTO(id, date_in, "13:00:00") || !CheckSO(id, date_in, "13:00:00") || !CheckLeave(id, date_in, "13:00:00"))
                                {
                                    mins += 240;
                                }


                            }
                            ///CASE 4 
                            else if (!am_in1.Equals("") && am_out1.Equals("") && pm_in1.Equals("") && pm_out1.Equals(""))
                            {

                                //AM IN
                                TimeSpan am_in_span = TimeSpan.Parse(am_in1);
                                TimeSpan am_in_subtrahend = TimeSpan.Parse("08:00:00");
                                int am_in_second_subtrahend = (int)am_in_subtrahend.TotalSeconds;
                                int am_in_second_timespan = (int)am_in_span.TotalSeconds;
                                int result_am_in = (am_in_second_timespan - am_in_second_subtrahend) / 60;
                                if (result_am_in > 0)
                                {
                                    mins += result_am_in;
                                }
                                if (!CheckCTO(id, date_in, "13:00:00") || !CheckSO(id, date_in, "13:00:00") || !CheckLeave(id, date_in, "13:00:00"))
                                {
                                    mins += 240;
                                }


                            }
                            ///CASE 5
                            else if (!am_in1.Equals("") && am_out1.Equals("") && pm_in1.Equals("") && !pm_out1.Equals(""))
                            {

                                //AM IN
                                TimeSpan am_in_span = TimeSpan.Parse(am_in1);
                                TimeSpan am_in_subtrahend = TimeSpan.Parse("08:00:00");
                                int am_in_second_subtrahend = (int)am_in_subtrahend.TotalSeconds;
                                int am_in_second_timespan = (int)am_in_span.TotalSeconds;
                                int result_am_in = (am_in_second_timespan - am_in_second_subtrahend) / 60;
                                if (result_am_in > 0)
                                {
                                    mins += result_am_in;
                                }
                                // PM OUT
                                TimeSpan pm_out_span = TimeSpan.Parse(pm_out1);
                                TimeSpan pm_out_subtrahend = TimeSpan.Parse("17:00:00");
                                int pm_out_second_subtrahend = (int)pm_out_subtrahend.TotalSeconds;
                                int pm_out_second_timespan = (int)pm_out_span.TotalSeconds;
                                int result_pm_out = (pm_out_second_subtrahend - pm_out_second_timespan) / 60;
                                if (result_pm_out > 0)
                                {
                                    mins += result_pm_out;
                                }

                            }
                            ///CASE 6
                            else if (am_in1.Equals("") && am_out1.Equals("") && !pm_in1.Equals("") && !pm_out1.Equals(""))
                            {

                                if (!CheckCTO(id, date_in, "08:00:00") || !CheckSO(id, date_in, "08:00:00") || !CheckLeave(id, date_in, "08:00:00"))
                                {
                                    mins += 240;
                                }

                                //PM IN
                                TimeSpan pm_in_span = TimeSpan.Parse(pm_in1);
                                TimeSpan pm_in_subtrahend = TimeSpan.Parse("13:00:00");
                                int pm_in_second_subtrahend = (int)pm_in_subtrahend.TotalSeconds;
                                int pm_in_second_timespan = (int)pm_in_span.TotalSeconds;
                                int result_pm_in = (pm_in_second_timespan - pm_in_second_subtrahend) / 60;
                                if (result_pm_in > 0)
                                {
                                    mins += result_pm_in;
                                }
                                // PM OUT
                                TimeSpan pm_out_span = TimeSpan.Parse(pm_out1);
                                TimeSpan pm_out_subtrahend = TimeSpan.Parse("17:00:00");
                                int pm_out_second_subtrahend = (int)pm_out_subtrahend.TotalSeconds;
                                int pm_out_second_timespan = (int)pm_out_span.TotalSeconds;
                                int result_pm_out = (pm_out_second_subtrahend - pm_out_second_timespan) / 60;
                                if (result_pm_out > 0)
                                {
                                    mins += result_pm_out;
                                }

                            }
                            ///CASE 7
                            else if (am_in1.Equals("") && am_out1.Equals("") && !pm_in1.Equals("") && pm_out1.Equals(""))
                            {

                                if (!CheckCTO(id, date_in, "08:00:00") || !CheckSO(id, date_in, "08:00:00") || !CheckLeave(id, date_in, "08:00:00"))
                                {
                                    mins += 240;
                                }


                                //PM IN
                                TimeSpan pm_in_span = TimeSpan.Parse(pm_in1);
                                TimeSpan pm_in_subtrahend = TimeSpan.Parse("13:00:00");
                                int pm_in_second_subtrahend = (int)pm_in_subtrahend.TotalSeconds;
                                int pm_in_second_timespan = (int)pm_in_span.TotalSeconds;
                                int result_pm_in = (pm_in_second_timespan - pm_in_second_subtrahend) / 60;
                                if (result_pm_in > 0)
                                {
                                    mins += result_pm_in;
                                }
                                // PM OUT
                                TimeSpan pm_out_span = TimeSpan.Parse(pm_in1);
                                TimeSpan pm_out_subtrahend = TimeSpan.Parse("17:00:00");
                                int pm_out_second_subtrahend = (int)pm_out_subtrahend.TotalSeconds;
                                int pm_out_second_timespan = (int)pm_out_span.TotalSeconds;
                                int result_pm_out = (pm_out_second_subtrahend - pm_out_second_timespan) / 60;
                                if (result_pm_out > 0)
                                {
                                    mins += result_pm_out;
                                }
                            }

                            if (days.Contains(day))
                            {
                                days.Remove(day);
                            }
                        }

                        dataReader.Close();
                }
                SqlConnection.Close();
                for (int i = 0; i < days.Count; i++)
                {
                        string format = month + "/" + days[i] + "/" + year;

                        if (!ifWeekend(format) && !IsHoliday(format))
                        {
                            if ((CheckCTO(id, format, "08:00:00") && CheckCTO(id, format, "12:00:00") && CheckCTO(id, format, "13:00:00") && CheckCTO(id, format, "17:00:00")) ||
                                    (CheckSO(id, format, "08:00:00") && CheckSO(id, format, "12:00:00") && CheckSO(id, format, "13:00:00") && CheckSO(id, format, "17:00:00")) ||
                                    (CheckLeave(id, format, "08:00:00") && CheckLeave(id, format, "12:00:00") && CheckLeave(id, format, "13:00:00") && CheckLeave(id, format, "17:00:00")))
                            {
                                continue;
                            }
                            else if ((!CheckCTO(id, format, "08:00:00") && CheckCTO(id, format, "13:00:00")) || (!CheckSO(id, format, "08:00:00") && CheckSO(id, format, "13:00:00")) || (!CheckLeave(id, format, "08:00:00") && CheckLeave(id, format, "13:00:00")))
                            {
                                mins += 240;
                            }
                            else if ((CheckCTO(id, format, "08:00:00") && !CheckCTO(id, format, "13:00:00")) || (CheckSO(id, format, "08:00:00") && !CheckSO(id, format, "13:00:00")) || (CheckLeave(id, format, "08:00:00") && !CheckLeave(id, format, "13:00:00")))
                            {
                                mins += 240;
                            }
                            else
                            {
                                days_rendered--;
                                if (days_absent.Equals(""))
                                {
                                    days_absent += format;
                                }
                                else
                                {
                                    days_absent += "*" + format;
                                }
                            }
                        }
                        else
                        {
                            days_rendered--;
                        }
                }
                for (int i = 0; i < no_days; i++)
                {
                        string format = month + "/" + (i + 1) + "/" + year;
                        if (!ifWeekend(format) && !IsHoliday(format))
                        {
                            working_days++;
                        }
                }
            }
            return mins + " " + working_days + " " + days_absent + " " + days_rendered;
        }

        public int GetWorkingDays(string from, string to)
        {
            List<int> days = new List<int>();
            int month = int.Parse(from.Split('-')[1]);
            int year = int.Parse(from.Split('-')[0]);
            int no_days = DateTime.DaysInMonth(year, month);
            int working_days = 0;
              
            for (int i = 1; i <= no_days; i++)
            {
                string format = month + "/" + i + "/" + year;
                if (!ifWeekend(format) && !IsHoliday(format))
                {
                        working_days++;
                }
            }
            return working_days;
        }
     }
}