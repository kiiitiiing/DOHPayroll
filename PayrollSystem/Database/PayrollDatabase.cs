using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;
using PayrollSystem.Models;

namespace PayrollSystem.Database
{
    public sealed class PayrollDatabase
    {
        public static string server;
        public static string database;
        public static string uid;
        public static string password;
        public static MySqlConnection SqlConnection = null;

        private static PayrollDatabase instance;

        private PayrollDatabase()
        {
        }

        //Singleton Pattern
        public static PayrollDatabase Instance

        {
            get
            {
                if (instance == null)
                {
                    instance = new PayrollDatabase();
                    instance.Initialize();

                }
                return instance;
            }
        }

        public void Initialize()
        {

           
            if (SqlConnection == null)
            {
                server = "192.168.100.17";
                database = "payroll";
                uid = "doh7payroll";
                password = "doh7payroll";
                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                        database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + "; pooling = false;SslMode=none;convert zero datetime=True";

                SqlConnection = new MySqlConnection(connectionString);
            }
        }

        private bool OpenConnection()
        {
          
            try
            {
                if (SqlConnection.State == ConnectionState.Closed)
                {
                    SqlConnection.Open();
                }
                return true;

            }
            catch {}
            return false;
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                SqlConnection.Close();
                return true;
            }
            catch{}
            return false;
        }

        public string IncrementRemittance(string table, string id)
        {

            string query = "UPDATE " + table + " SET count = (count + 1) WHERE userid = '" + id + "'";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                cmd.ExecuteNonQuery();
            }
            return "Incremented Successfully";
        }

        public bool InsertRegularPayroll(RegularPayrollModel payroll)
        {
            
                string query = "REPLACE INTO regular_payroll VALUES('0','" + payroll.Employee.ID + "','" + payroll.Month + "','" + payroll.Year+ "','" +
                        payroll.DaysAbsent + "','" + payroll.WorkDays + "','" + payroll.Salary + "','" + payroll.Pera+ "','" + payroll.MinutesLate + "','" +
                        payroll.Tax+ "','" + payroll.CFI+ "','" + payroll.GSIS_Premium + "','" + payroll.GSIS_Consoloan+ "','" + payroll.GSIS_PolicyLoan + "','" + payroll.GSIS_EML
                        + "','" + payroll.GSIS_UOLI + "','" + payroll.GSIS_EDU + "','" + payroll.GSIS_Help + "','" + payroll.GSIS_REL+ "','" + payroll.Pagibig_Premium+ "','" + 
                        payroll.Pagibig_Loan+ "','" + payroll.Disallowances + "','" + payroll.PhilHealth + "','" + payroll.SIMC+ "','" + payroll.HWMPC+ "','" + payroll.DBP+ "','" + payroll.MP2 + "')";

                if (OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                    cmd.ExecuteNonQuery();


                if (decimal.Parse(payroll.CFI) > 0)
                {
                    IncrementRemittance("cfi_remittance", payroll.Employee.ID);
                }
                if (decimal.Parse(payroll.GSIS_Consoloan) > 0)
                {
                    IncrementRemittance("gsis_consoloan_remittance", payroll.Employee.ID);
                }
                if (decimal.Parse(payroll.GSIS_PolicyLoan) > 0)
                {
                    IncrementRemittance("gsis_policyloan_remittance", payroll.Employee.ID);
                }
                if (decimal.Parse(payroll.GSIS_EML) > 0)
                {
                    IncrementRemittance("gsis_eml_remittance", payroll.Employee.ID);
                }
                if (decimal.Parse(payroll.GSIS_UOLI) > 0)
                {
                    IncrementRemittance("gsis_uoli_remittance", payroll.Employee.ID);
                }
                if (decimal.Parse(payroll.GSIS_EDU) > 0)
                {
                    IncrementRemittance("gsis_edu_remittance", payroll.Employee.ID);
                }
                if (decimal.Parse(payroll.GSIS_Help) > 0)
                {
                    IncrementRemittance("gsis_edu_remittance", payroll.Employee.ID);
                }
                if (decimal.Parse(payroll.GSIS_REL) > 0)
                {
                    IncrementRemittance("rel_remittance", payroll.Employee.ID);
                }
                if (decimal.Parse(payroll.Pagibig_Loan) > 0)
                {
                    IncrementRemittance("pagibig_loan_remittance", payroll.Employee.ID);
                }
                if (decimal.Parse(payroll.MP2) > 0)
                {
                    IncrementRemittance("pagibig_mp2_remittance", payroll.Employee.ID);
                }
                if (decimal.Parse(payroll.SIMC) > 0)
                {
                    IncrementRemittance("simc_remittance", payroll.Employee.ID);
                }
                if (decimal.Parse(payroll.HWMPC) > 0)
                {
                    IncrementRemittance("coop_remittance", payroll.Employee.ID);
                }
                if (decimal.Parse(payroll.DBP) > 0)
                {
                    IncrementRemittance("dbp_remittance", payroll.Employee.ID);
                }
                if (decimal.Parse(payroll.Disallowances) > 0)
                {
                    IncrementRemittance("disallowance_remittance", payroll.Employee.ID);
                }

                CloseConnection();

                return true;
            }

            return false;
        }

        public bool InsertJoPayroll(JobOrderPayrollModel payroll)
        {

            string query = "REPLACE INTO payroll VALUES('0','" + payroll.Employee.ID + "','" + payroll.StartDate + "','" + payroll.EndDate + "','" +
                    payroll.DaysAbsent + "','" + payroll.WorkDays + "','" + payroll.Salary + "','" + payroll.Adjustment + "','" + payroll.MinutesLate + "','" +
                    payroll.Coop + "','" + payroll.Phic + "','" + payroll.Disallowance + "','" + payroll.Gsis + "','" + payroll.Pagibig + "','" + payroll.ExcessMobile
                    + "',@remarks,NULL,NULL,'" + payroll.Tax + "','" + payroll.OtherAdjustment + "')";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                cmd.Parameters.AddWithValue("@remarks", payroll.Remarks);
                cmd.ExecuteNonQuery();


                if (decimal.Parse(payroll.Pagibig) > 0)
                {
                    IncrementRemittance("pagibig_remittance", payroll.Employee.ID);
                }
                if (decimal.Parse(payroll.Coop) > 0)
                {
                    IncrementRemittance("coop_remittance", payroll.Employee.ID);
                }
                if (decimal.Parse(payroll.Phic) > 0)
                {
                    IncrementRemittance("phic_remittance", payroll.Employee.ID);
                }
                if (decimal.Parse(payroll.Disallowance) > 0)
                {
                    IncrementRemittance("disallowance_remittance", payroll.Employee.ID);
                }
                if (decimal.Parse(payroll.Gsis) > 0)
                {
                    IncrementRemittance("gsis_remittance", payroll.Employee.ID);
                }
                if (decimal.Parse(payroll.ExcessMobile) > 0)
                {
                    IncrementRemittance("excess_remittance", payroll.Employee.ID);
                }

                CloseConnection();

                return true;
            }

            return false;
        }

        public bool UpdateRemittance(string table, Remittance remittance)
        {
            string query = "REPLACE INTO "+table+" (userid, max, count,amount) VALUES('"+remittance.UserID+ "', '" + remittance.MaxCount + "','" + remittance.Count + "','" + remittance.Amount + "')";

            if (OpenConnection() == true)   
            {
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                cmd.ExecuteNonQuery();
                CloseConnection();
                return true;
            }
            return false;
        }
        public bool UpdateTax(string UserID, string Amount)
        {
            string query = "REPLACE INTO tax_remittance (userid, tax) VALUES('" + UserID + "', '" + Amount + "')";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                cmd.ExecuteNonQuery();
                CloseConnection();
                return true;
            }
            return false;
        }
        public bool DeletePayroll(string id)
        {
            string query = "DELETE FROM payroll WHERE id = '"+id+"'";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                cmd.ExecuteNonQuery();
                CloseConnection();
                return true;
            }
            return false;
        }
        public bool DeleteRegularPayroll(string id)
        {
            string query = "DELETE FROM regular_payroll WHERE id = '" + id + "'";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                cmd.ExecuteNonQuery();
                CloseConnection();
                return true;
            }
            return false;
        }

        public List<RegularPayrollModel> GetRegularPayrollByID(string EmpID, string Firstname, string Lastname, string Middlename)
        {
            List<RegularPayrollModel> list = new List<RegularPayrollModel>();
            string query = "SELECT * FROM regular_payroll WHERE userid = '" + EmpID + "'";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    string payroll_id = dataReader["id"].ToString();
                    string userid = dataReader["userid"].ToString();
                    
                    //string emptype = dataReader["position"].ToString();
                    //String start_date = dataReader.GetDateTime(dataReader.GetOrdinal("start_date")).ToString("YYYY-MM-DD");
                    //String end_date = dataReader.GetDateTime(dataReader.GetOrdinal("end_date")).ToString("YYYY-MM-DD");
                    string month = dataReader["month"].ToString();
                    string year = dataReader["year"].ToString();
                    //string tin = dataReader["tin_no"].ToString();
                    string days_absent = dataReader["absent_days"].ToString();
                    string working_days = dataReader["working_days"].ToString();
                    string salary = dataReader["month_salary"].ToString();
                    string pera = dataReader["pera"].ToString();
                    string tax = dataReader["tax"].ToString();
                    string minutes_late = dataReader["minutes_late"].ToString();
                    string cfi = dataReader["cfi"].ToString();
                    string gsis_premium = dataReader["gsis_premium"].ToString();
                    string gsis_consoloan = dataReader["gsis_consoloan"].ToString();
                    string gsis_policy_loan = dataReader["gsis_policy_loan"].ToString();
                    string gsis_eml = dataReader["gsis_eml"].ToString();
                    string gsis_uoli = dataReader["gsis_uoli"].ToString();
                    string gsis_edu = dataReader["gsis_edu"].ToString();
                    string gsis_help = dataReader["gsis_help"].ToString();
                    string gsis_rel = dataReader["gsis_rel"].ToString();
                    string pagibig_premium = dataReader["pagibig_premium"].ToString();
                    string pagibig_loan = dataReader["pagibig_loan"].ToString();
                    string pagibig_mp2 = dataReader["pagibig_mp2"].ToString();
                    string disallowances = dataReader["disallowances"].ToString();
                    string philhealth = dataReader["philhealth"].ToString();
                    string simc = dataReader["simc"].ToString();
                    string hwmpc = dataReader["hwmpc"].ToString();
                    string dbp = dataReader["dbp"].ToString();

                    Employee employee = new Employee(userid, Firstname, Lastname, Middlename, "", "", "", "", "");

                    RegularPayrollModel payroll = new RegularPayrollModel(payroll_id, employee, int.Parse(month), int.Parse(year), days_absent, working_days, salary, pera, int.Parse(minutes_late), tax, cfi, gsis_premium,
                            gsis_consoloan, gsis_policy_loan, gsis_eml, gsis_uoli, gsis_edu, gsis_help, gsis_rel, pagibig_premium, pagibig_loan, disallowances, philhealth, simc, hwmpc, dbp, pagibig_mp2);


                    list.Add(payroll);
                }
                dataReader.Close();
                CloseConnection();
            }
            return list;
        }

        public List<JobOrderPayrollModel> GetJobOrderPayrollByID(string EmpID,string Firstname, string Lastname, string Middlename)
        {
            List<JobOrderPayrollModel> list = new List<JobOrderPayrollModel>();
            string query = "SELECT * FROM payroll WHERE userid = '"+ EmpID + "'";
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    string id = dataReader["id"].ToString();
                    string start_date = dataReader["start_date"].ToString().ToUpper();
                    string end_date  = dataReader["end_date"].ToString().ToUpper();
                    string minutes_late = dataReader["minutes_late"].ToString().ToUpper();
                    if (minutes_late.Equals("") || minutes_late.Equals("NULL"))
                    {
                        minutes_late = "0";
                    }
                    string working_days = dataReader["working_days"].ToString();
                    if (working_days.Equals("") || working_days.Equals("NULL"))
                    {
                        working_days = "0";
                    }
                    string monthly_salary = dataReader["month_salary"].ToString();
                    if (monthly_salary.Equals("") || monthly_salary.Equals("NULL") || monthly_salary.Equals("Null") || monthly_salary.Equals(null))
                    {
                        monthly_salary = "0";
                    }
                    string coop = dataReader["coop"].ToString();
                    if (coop.Equals("") || coop.Equals("NULL"))
                    {
                        coop = "0";
                    }
                    string adjustment = dataReader["adjustment"].ToString();
                    if (adjustment.Equals("") || adjustment.Equals("NULL"))
                    {
                        adjustment = "0.00";
                    }
                    string remarks = dataReader["remarks"].ToString();
                    if (remarks.Equals("") || remarks.Equals("NULL"))
                    {
                        remarks = "";
                    }
                    string absent_days = dataReader["absent_days"].ToString();
                    if (absent_days.Equals("") || absent_days.Equals("NULL"))
                    {
                        absent_days = "";
                    }
                    string phic = dataReader["phic"].ToString();
                    if (phic.Equals("") || phic.Equals("NULL"))
                    {
                        phic = "0";
                    }
                    string disallowance = dataReader["disallowance"].ToString();
                    if (disallowance.Equals("") || disallowance.Equals("NULL"))
                    {
                        disallowance = "0";
                    }
                    string gsis = dataReader["gsis"].ToString();
                    if (gsis.Equals("") || gsis.Equals("NULL"))
                    {
                        gsis = "0";
                    }
                    string pagibig = dataReader["pagibig"].ToString();
                    if (pagibig.Equals("") || pagibig.Equals("NULL"))
                    {
                        pagibig = "0";
                    }
                    string excess_mobile = dataReader["excess_mobile"].ToString();
                    if (excess_mobile.Equals("") || excess_mobile.Equals("NULL"))
                    {
                        excess_mobile = "0";
                    }
                    string tax = dataReader["tax"].ToString();
                    if (tax.Equals("") || tax.Equals("NULL"))
                    {
                        tax = "0.00";
                    }

                    string other_adjustment = dataReader["other_adjustment"].ToString();
                    if (other_adjustment.Equals("") || other_adjustment.Equals("NULL"))
                    {
                        other_adjustment = "0.00";
                    }

                    Employee employee = new Employee(EmpID,Firstname,Lastname,Middlename,"","","","","");

                    JobOrderPayrollModel joborder = new JobOrderPayrollModel(id, employee,start_date, end_date, adjustment, working_days, absent_days
                            , monthly_salary, minutes_late, coop, phic, disallowance, gsis, pagibig, excess_mobile, remarks, "", tax, other_adjustment);
                    list.Add(joborder);
                }
                dataReader.Close();
                CloseConnection();
            }
            return list;
        }

        public List<ReportPDF> GetPDF(string searchQuery)
        {
            string query = "SELECT * FROM pdf";
            if (!searchQuery.Equals(""))
            {
                query = query + " WHERE document LIKE '"+searchQuery+"%' || disbursement LIKE '"+searchQuery+"%' || salary_charge LIKE '"+searchQuery+"%' || division LIKE '"+searchQuery+"%'" ;
            }
            query = query + " ORDER BY date_created DESC";

            List<ReportPDF> list = new List<ReportPDF>();
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                 
                    string id = dataReader["id"].ToString();
                    string from = dataReader["date_from"].ToString();
                    string to = dataReader["date_to"].ToString().Split(' ')[0];
                    string document = dataReader["document"].ToString();
                    string disbursement = dataReader["disbursement"].ToString();
                    string salary_charge = dataReader["salary_charge"].ToString();
                    string division = dataReader["division"].ToString();
                    string file_path = dataReader["file_path"].ToString();

                    ReportPDF data = new ReportPDF(id,from,to, document,disbursement, salary_charge,division,file_path);
                    list.Add(data);
                }
                dataReader.Close();
                this.CloseConnection();
            }    
            return list;
        }

        public List<JobOrderPayrollModel> GenerateJoSummary(string start_date, string end_date, string document,
                                            string disbursment, string salary_charge)
        {
            List<JobOrderPayrollModel> JobOrderList = new List<JobOrderPayrollModel>();
            string query = "SELECT d.id,p.absent_days,p.adjustment,p.remarks,d.description as designation,i.userid,i.mname,i.fname,i.lname,i.position,i.salary_charge,i.tin_no,i.account_number,p.working_days,p.month_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile,p.tax,p.other_adjustment "+
                           "FROM payroll.payroll p LEFT JOIN (pis.personal_information i LEFT JOIN dts.designation d ON i.designation_id = d.id) ON p.userid = i.userid "+
                           "WHERE p.start_date = '" + start_date + "' AND p.end_date = '" + end_date + "' AND i.disbursement_type = '" + disbursment + "'";
            switch (document)
            {
                case "PAGIBIG":
                    query = query + " AND pagibig <> '0.00'";
                    break;
                case "COOP":
                    query = query + " AND coop <> '0.00'";
                    break;
                case "PHIC":
                    query = query + " AND phic <> '0.00'";
                    break;
                case "GSIS":
                    query = query + " AND gsis <> '0.00'";
                    break;
                case "EXCESS":
                    query = query + " AND excess_mobile <> '0.00'";
                    break;
            }
            if (!salary_charge.Equals("ALL"))
            {
                query = query + " AND i.salary_charge = '" + salary_charge + "'";
            }

            query = query + " ORDER BY i.salary_charge,i.lname,i.fname,d.description ASC";

            if(OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);                
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {

                    string userid = dataReader["userid"].ToString();
                    string fname = dataReader["fname"].ToString();
                    string lname = dataReader["lname"].ToString();
                    string salary_charge_data = dataReader["salary_charge"].ToString();
                    string mname = dataReader["mname"].ToString();
                    string designation = dataReader["designation"].ToString();
                    string division = "";//NOT NECCESSARY;

                    Employee employee = new Employee(userid, fname, lname, mname, designation,"", salary_charge_data, division, disbursment);

                    //PAYROLL DATAREADER
                    string minutes_late = dataReader["minutes_late"].ToString().ToUpper();
                    if (minutes_late.Equals("") || minutes_late.Equals("NULL"))
                    {
                        minutes_late = "0";
                    }
                    string working_days = dataReader["working_days"].ToString();
                    if (working_days.Equals("") || working_days.Equals("NULL"))
                    {
                        working_days = "0";
                    }
                    string monthly_salary = dataReader["month_salary"].ToString();
                    if (monthly_salary.Equals("") || monthly_salary.Equals("NULL") || monthly_salary.Equals("Null") || monthly_salary.Equals(null))
                    {
                        monthly_salary = "0";
                    }
                    string coop = dataReader["coop"].ToString();
                    if (coop.Equals("") || coop.Equals("NULL"))
                    {
                        coop = "0";
                    }
                    string adjustment = dataReader["adjustment"].ToString();
                    if (adjustment.Equals("") || adjustment.Equals("NULL"))
                    {
                        adjustment = "0.00";
                    }
                    string remarks = dataReader["remarks"].ToString();
                    if (remarks.Equals("") || remarks.Equals("NULL"))
                    {
                        remarks = "";
                    }
                    string absent_days = dataReader["absent_days"].ToString();
                    if (absent_days.Equals("") || absent_days.Equals("NULL"))
                    {
                        absent_days = "";
                    }
                    string phic = dataReader["phic"].ToString();
                    if (phic.Equals("") || phic.Equals("NULL"))
                    {
                        phic = "0";
                    }
                    string disallowance = dataReader["disallowance"].ToString();
                    if (disallowance.Equals("") || disallowance.Equals("NULL"))
                    {
                        disallowance = "0";
                    }
                    string gsis = dataReader["gsis"].ToString();
                    if (gsis.Equals("") || gsis.Equals("NULL"))
                    {
                        gsis = "0";
                    }
                    string pagibig = dataReader["pagibig"].ToString();
                    if (pagibig.Equals("") || pagibig.Equals("NULL"))
                    {
                        pagibig = "0";
                    }
                    string excess_mobile = dataReader["excess_mobile"].ToString();
                    if (excess_mobile.Equals("") || excess_mobile.Equals("NULL"))
                    {
                        excess_mobile = "0";
                    }
                    string tax = dataReader["tax"].ToString();
                    if (tax.Equals("") || tax.Equals("NULL"))
                    {
                        tax = "0.00";
                    }

                    string other_adjustment = dataReader["other_adjustment"].ToString();
                    if (other_adjustment.Equals("") || other_adjustment.Equals("NULL"))
                    {
                        other_adjustment = "0.00";
                    }
                   
                    JobOrderPayrollModel joborder = new JobOrderPayrollModel("0", employee, "", "", adjustment, working_days, absent_days
                            , monthly_salary, minutes_late, coop, phic, disallowance, gsis, pagibig, excess_mobile, remarks, "", tax, other_adjustment);
                    JobOrderList.Add(joborder);
                }
                dataReader.Close();
                CloseConnection();

            }
            return JobOrderList;
        }

        public List<RegularPayrollModel> GenerateSummaryRegularByDivision(int month, int year, string division)
        {
            List<RegularPayrollModel> list = new List<RegularPayrollModel>();
            string query = "SELECT r.ra,r.ta,r.deduction,s.subsistence_allowance,s.laundry_allowance,s.absences,s.hwmcp ,p.id,p.userid,p.month,p.year,p.absent_days,p.working_days,p.month_salary,p.pera,p.minutes_late,p.tax,p.cfi,p.gsis_premium,p.gsis_consoloan,p.gsis_policy_loan,p.gsis_eml,p.gsis_uoli,p.gsis_edu,p.gsis_help,p.gsis_rel,p.pagibig_premium,p.pagibig_loan,p.pagibig_mp2,p.disallowances,p.philhealth,p.simc,p.hwmpc,p.dbp " +
                           "FROM payroll.regular_payroll p "+
                           "LEFT JOIN pis.personal_information i ON p.userid = i.userid "+
                           "LEFT JOIN payroll.hazard_pay h ON p.userid = h.userid AND h.month = '" + month + "' AND H.year = '" + year + "'" +
                           "LEFT JOIN payroll.rata r ON p.userid = r.userid AND r.month = '" + month + "' AND r.year = '" + year + "'" +
                           "LEFT JOIN payroll.subsistence s ON p.userid = s.userid AND s.month = '" + month + "' AND s.year = '" + year + "'" +
                           "WHERE p.month = '" + month + "' AND p.year = '" + year + "' ";
            if (!division.Equals("ALL"))
            {
                query = query + "AND i.division_id = @division_id ";
            }
            query = query + "ORDER BY i.lname,i.fname ASC";
            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                    cmd.Parameters.AddWithValue("@division_id", division);
                  
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        string id = dataReader["id"].ToString();
                        string userid = dataReader["userid"].ToString();
                        string absent_days = dataReader["absent_days"].ToString();
                        string working_days = dataReader["working_days"].ToString();
                        string month_salary = dataReader["month_salary"].ToString();
                        string pera = dataReader["pera"].ToString();
                        string minutes_late = dataReader["minutes_late"].ToString();
                        string tax = dataReader["tax"].ToString();
                        string cfi = dataReader["cfi"].ToString();
                        string gsis_premium = dataReader["gsis_premium"].ToString();
                        string gsis_consoloan = dataReader["gsis_consoloan"].ToString();
                        string gsis_policy_loan = dataReader["gsis_policy_loan"].ToString();
                        string gsis_eml = dataReader["gsis_eml"].ToString();
                        string gsis_uoli = dataReader["gsis_uoli"].ToString();
                        string gsis_edu = dataReader["gsis_edu"].ToString();
                        string gsis_help = dataReader["gsis_help"].ToString();
                        string gsis_rel = dataReader["gsis_rel"].ToString();
                        string pagibig_premium = dataReader["pagibig_premium"].ToString();
                        string pagibig_loan = dataReader["pagibig_loan"].ToString();
                        string pagibig_mp2 = dataReader["pagibig_mp2"].ToString();
                        string disallowances = dataReader["disallowances"].ToString();
                        string philhealth = dataReader["philhealth"].ToString();
                        string simc = dataReader["simc"].ToString();
                        string hwmpc = dataReader["hwmpc"].ToString();
                        string dbp = dataReader["dbp"].ToString();

                       
                        decimal subsistence_allowance = dataReader["subsistence_allowance"].ToString() == "" ?0: decimal.Parse(dataReader["subsistence_allowance"].ToString());
                        decimal laundry_allowance = dataReader["laundry_allowance"].ToString() == "" ? 0 : decimal.Parse(dataReader["laundry_allowance"].ToString());
                        decimal absences = dataReader["absences"].ToString() == "" ? 0 : decimal.Parse(dataReader["absences"].ToString());
                        decimal hwmcp = dataReader["hwmcp"].ToString() == "" ? 0 : decimal.Parse(dataReader["hwmcp"].ToString());

                        decimal subsistence_net = subsistence_allowance + laundry_allowance;
                        decimal subsistence_deduction = absences + hwmcp;

                        HazardViewModel hazard = new HazardViewModel(subsistence_net, subsistence_deduction);

                        decimal ra = dataReader["ra"].ToString() == "" ? 0 : decimal.Parse(dataReader["ra"].ToString());
                        decimal ta = dataReader["ta"].ToString() == "" ? 0 : decimal.Parse(dataReader["ta"].ToString());
                        decimal deduction = dataReader["deduction"].ToString() == "" ? 0 : decimal.Parse(dataReader["deduction"].ToString());

                        RataViewModel rata = new RataViewModel(ra,ta, deduction);

                        Employee employee = null; //PISDatabase.Instance.GetEmployeeByID(userid);

                        RegularPayrollModel payrollRegular = new RegularPayrollModel(id, employee, month, year, absent_days, working_days, month_salary, pera, int.Parse(minutes_late),
                                tax, cfi, gsis_premium, gsis_consoloan, gsis_policy_loan, gsis_eml, gsis_uoli, gsis_edu, gsis_help, gsis_rel, pagibig_premium, pagibig_loan, disallowances, philhealth, simc, hwmpc, dbp, pagibig_mp2);

                        payrollRegular.SetHazard(hazard);
                        payrollRegular.SetRata(rata);

                        list.Add(payrollRegular);
                    }

                    dataReader.Close();

                    this.CloseConnection();

                    //return list to be displayed
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.ToString());
                }


            }
            return list;
        }

        public List<RegularPayrollModel> GenerateSummaryRegularBySalaryCharge(int month, int year, string salary_charge)
        {
            List<RegularPayrollModel> list = new List<RegularPayrollModel>();
            string query = "SELECT i.fname,i.lname,i.mname,i.job_status,q.description as division,e.description as designation,r.ra,r.ta,r.deduction,s.subsistence_allowance,s.laundry_allowance,s.absences,s.hwmcp ,p.id,p.userid,p.month,p.year,p.absent_days,p.working_days,p.month_salary,p.pera,p.minutes_late,p.tax,p.cfi,p.gsis_premium,p.gsis_consoloan,p.gsis_policy_loan,p.gsis_eml,p.gsis_uoli,p.gsis_edu,p.gsis_help,p.gsis_rel,p.pagibig_premium,p.pagibig_loan,p.pagibig_mp2,p.disallowances,p.philhealth,p.simc,p.hwmpc,p.dbp " +
                           "FROM payroll.regular_payroll p " +
                           "LEFT JOIN (pis.personal_information i LEFT JOIN dts.division q ON q.id = i.division_id LEFT JOIN dts.designation e ON e.id = i.designation_id) ON p.userid = i.userid " +
                           "LEFT JOIN payroll.hazard_pay h ON p.userid = h.userid AND h.month = '" + month + "' AND H.year = '" + year + "'" +
                           "LEFT JOIN payroll.rata r ON p.userid = r.userid AND r.month = '" + month + "' AND r.year = '" + year + "'" +
                           "LEFT JOIN payroll.subsistence s ON p.userid = s.userid AND s.month = '" + month + "' AND s.year = '" + year + "'" +
                           "WHERE p.month = '" + month + "' AND p.year = '" + year + "' ";
            if (!salary_charge.Equals("ALL"))
            {
                query = query + "AND i.salary_charge = @salary_charge ";
            }
            query = query + "ORDER BY i.salary_charge,i.lname,i.fname ASC";
            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                    cmd.Parameters.AddWithValue("@salary_charge", salary_charge);

                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string id = dataReader["id"].ToString();
                        //EMPLOYEE DATAREADERS
                        string userid = dataReader["userid"].ToString();
                        string fname = dataReader["fname"].ToString();
                        string lname = dataReader["lname"].ToString();
                        string mname = dataReader["mname"].ToString();
                        string designation = dataReader["designation"].ToString();
                        string division = dataReader["division"].ToString();
                        Employee employee = new Employee(userid, fname, lname, mname, designation,"", salary_charge, division, "");

                        //PAYROLL DATAREADERS
                        string absent_days = dataReader["absent_days"].ToString();
                        string working_days = dataReader["working_days"].ToString();
                        string month_salary = dataReader["month_salary"].ToString();
                        string pera = dataReader["pera"].ToString();
                        string minutes_late = dataReader["minutes_late"].ToString();
                        string tax = dataReader["tax"].ToString();
                        string cfi = dataReader["cfi"].ToString();
                        string gsis_premium = dataReader["gsis_premium"].ToString();
                        string gsis_consoloan = dataReader["gsis_consoloan"].ToString();
                        string gsis_policy_loan = dataReader["gsis_policy_loan"].ToString();
                        string gsis_eml = dataReader["gsis_eml"].ToString();
                        string gsis_uoli = dataReader["gsis_uoli"].ToString();
                        string gsis_edu = dataReader["gsis_edu"].ToString();
                        string gsis_help = dataReader["gsis_help"].ToString();
                        string gsis_rel = dataReader["gsis_rel"].ToString();
                        string pagibig_premium = dataReader["pagibig_premium"].ToString();
                        string pagibig_loan = dataReader["pagibig_loan"].ToString();
                        string pagibig_mp2 = dataReader["pagibig_mp2"].ToString();
                        string disallowances = dataReader["disallowances"].ToString();
                        string philhealth = dataReader["philhealth"].ToString();
                        string simc = dataReader["simc"].ToString();
                        string hwmpc = dataReader["hwmpc"].ToString();
                        string dbp = dataReader["dbp"].ToString();

                        //SUBSISTENCE DATAREADER
                        decimal subsistence_allowance = dataReader["subsistence_allowance"].ToString() == "" ? 0 : decimal.Parse(dataReader["subsistence_allowance"].ToString());
                        decimal laundry_allowance = dataReader["laundry_allowance"].ToString() == "" ? 0 : decimal.Parse(dataReader["laundry_allowance"].ToString());
                        decimal absences = dataReader["absences"].ToString() == "" ? 0 : decimal.Parse(dataReader["absences"].ToString());
                        decimal hwmcp = dataReader["hwmcp"].ToString() == "" ? 0 : decimal.Parse(dataReader["hwmcp"].ToString());
                        decimal subsistence_net = subsistence_allowance + laundry_allowance;
                        decimal subsistence_deduction = absences + hwmcp;
                        HazardViewModel hazard = new HazardViewModel(subsistence_net, subsistence_deduction);

                        //RATA DATAREADER
                        decimal ra = dataReader["ra"].ToString() == "" ? 0 : decimal.Parse(dataReader["ra"].ToString());
                        decimal ta = dataReader["ta"].ToString() == "" ? 0 : decimal.Parse(dataReader["ta"].ToString());
                        decimal deduction = dataReader["deduction"].ToString() == "" ? 0 : decimal.Parse(dataReader["deduction"].ToString());
                        RataViewModel rata = new RataViewModel(ra, ta, deduction);


                        RegularPayrollModel payrollRegular = new RegularPayrollModel(id, employee, month, year, absent_days, working_days, month_salary, pera, int.Parse(minutes_late),
                                tax, cfi, gsis_premium, gsis_consoloan, gsis_policy_loan, gsis_eml, gsis_uoli, gsis_edu, gsis_help, gsis_rel, pagibig_premium, pagibig_loan, disallowances, philhealth, simc, hwmpc, dbp, pagibig_mp2);

                        payrollRegular.SetHazard(hazard);
                        payrollRegular.SetRata(rata);

                        list.Add(payrollRegular);
                    }

                    dataReader.Close();
                    CloseConnection();

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.ToString());
                    CloseConnection();
                }
            }
            return list;
        }

        public string InsertPDF(string from,string to,string document, string disbursement, string salary_charge, string division , string file_path)
        {
            String query = "INSERT INTO pdf VALUES('0',@date_from,@date_to,@document,@disbursement,@salary_charge,@division,@file_path,NOW())";
            if (this.OpenConnection() == true)
            {
              MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
              cmd.Parameters.AddWithValue("@date_from", from);
              cmd.Parameters.AddWithValue("@date_to", to);
              cmd.Parameters.AddWithValue("@document", document);
              cmd.Parameters.AddWithValue("@disbursement", disbursement);
              cmd.Parameters.AddWithValue("@salary_charge", salary_charge);
              cmd.Parameters.AddWithValue("@division", division);
              cmd.Parameters.AddWithValue("@file_path", file_path);
                    
              cmd.ExecuteNonQuery();
              this.CloseConnection();
                   
            }
            return "Insert Successfully";
        }

        public string DeletePDF(string id)
        {
            string query = "DELETE FROM pdf WHERE id = '"+id+"'";
           
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
               
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
            return "Success";
        }

        public RegularPayslipModel GetRegularPayslipById(int month, int year, string userid)
        {
            RegularPayslipModel PayslipModel = null;
            string query = "SELECT r.ra,r.ta,r.deduction,s.subsistence_allowance,s.laundry_allowance,s.absences,s.hwmcp ,p.id,p.userid,p.month,p.year,p.absent_days,p.working_days,p.month_salary,p.pera,p.minutes_late,p.tax,p.cfi,p.gsis_premium,p.gsis_consoloan,p.gsis_policy_loan,p.gsis_eml,p.gsis_uoli,p.gsis_edu,p.gsis_help,p.gsis_rel,p.pagibig_premium,p.pagibig_loan,p.pagibig_mp2,p.disallowances,p.philhealth,p.simc,p.hwmpc,p.dbp " +
                           "FROM payroll.regular_payroll p " +
                           "LEFT JOIN pis.personal_information i ON p.userid = i.userid " +
                           "LEFT JOIN payroll.hazard_pay h ON p.userid = h.userid AND h.month = '" + month + "' AND H.year = '" + year + "'" +
                           "LEFT JOIN payroll.rata r ON p.userid = r.userid AND r.month = '" + month + "' AND r.year = '" + year + "'" +
                           "LEFT JOIN payroll.subsistence s ON p.userid = s.userid AND s.month = '" + month + "' AND s.year = '" + year + "'" +
                           "WHERE p.month = '" + month + "' AND p.year = '" + year + "' AND p.userid = '"+userid+"'";
           
            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        string id = dataReader["id"].ToString();
                        decimal pera = decimal.Parse(dataReader["pera"].ToString());
                        decimal month_salary = decimal.Parse(dataReader["month_salary"].ToString());
                        if (month_salary.Equals("") || month_salary.Equals("NULL") || month_salary.Equals("Null") || month_salary.Equals(null))
                        {
                            month_salary = 0;
                        }

                        string MinutesLate = dataReader["minutes_late"].ToString();
                        if (MinutesLate.Equals("") || MinutesLate.Equals("NULL"))
                        {
                            MinutesLate = "0";
                        }

                        string DaysAbsent = dataReader["absent_days"].ToString();
                        if (DaysAbsent.Equals("") || DaysAbsent.Equals("NULL"))
                        {
                            DaysAbsent = "";
                        }

                        string WorkingDays = dataReader["working_days"].ToString();
                        if (WorkingDays.Equals("") || WorkingDays.Equals("NULL"))
                        {
                            WorkingDays = "0";
                        }

                        int minutes_late = int.Parse(MinutesLate);

                        int NumberOfDaysAbsent = DaysAbsent.Split(',').Length;
                        if (NumberOfDaysAbsent > 0 && !DaysAbsent.Split(',')[0].Equals(""))
                        {
                            minutes_late += (480 * NumberOfDaysAbsent);
                        }

                        int NumberWorkingDays = int.Parse(WorkingDays);
                        decimal DayRate = 0;
                        decimal Tardiness = 0;

                        if (NumberWorkingDays != 0 && month_salary != 0)
                        {
                            DayRate = month_salary / NumberWorkingDays;
                            Tardiness = (decimal)Math.Round((minutes_late * (((DayRate) / 8) / 60)), 2, MidpointRounding.AwayFromZero);
                        }

                        decimal tax = decimal.Parse(dataReader["tax"].ToString());
                        decimal cfi = decimal.Parse(dataReader["cfi"].ToString());
                        decimal gsis_premium = decimal.Parse(dataReader["gsis_premium"].ToString());
                        decimal gsis_consoloan = decimal.Parse(dataReader["gsis_consoloan"].ToString());
                        decimal gsis_policy_loan = decimal.Parse(dataReader["gsis_policy_loan"].ToString());
                        decimal gsis_eml = decimal.Parse(dataReader["gsis_eml"].ToString());
                        decimal gsis_uoli = decimal.Parse(dataReader["gsis_uoli"].ToString());
                        decimal gsis_edu = decimal.Parse(dataReader["gsis_edu"].ToString());
                        decimal gsis_help = decimal.Parse(dataReader["gsis_help"].ToString());
                        decimal rel = decimal.Parse(dataReader["gsis_rel"].ToString());
                        decimal pagibig_premium = decimal.Parse(dataReader["pagibig_premium"].ToString());
                        decimal pagibig_loan = decimal.Parse(dataReader["pagibig_loan"].ToString());
                        decimal pagibig_mp2 = decimal.Parse(dataReader["pagibig_mp2"].ToString());
                        decimal disallowance = decimal.Parse(dataReader["disallowances"].ToString());
                        decimal philhealth = decimal.Parse(dataReader["philhealth"].ToString());
                        decimal simc = decimal.Parse(dataReader["simc"].ToString());
                        decimal hwmpc = decimal.Parse(dataReader["hwmpc"].ToString());
                        decimal dbp = decimal.Parse(dataReader["dbp"].ToString());


                        decimal subsistence_allowance = dataReader["subsistence_allowance"].ToString() == "" ? 0 : decimal.Parse(dataReader["subsistence_allowance"].ToString());
                        decimal laundry_allowance = dataReader["laundry_allowance"].ToString() == "" ? 0 : decimal.Parse(dataReader["laundry_allowance"].ToString());
                        decimal absences = dataReader["absences"].ToString() == "" ? 0 : decimal.Parse(dataReader["absences"].ToString());
                        decimal hwmcp = dataReader["hwmcp"].ToString() == "" ? 0 : decimal.Parse(dataReader["hwmcp"].ToString());

                        decimal subsistence = subsistence_allowance + laundry_allowance;
                        decimal subsistence_deduction = absences + hwmcp;

                        HazardViewModel hazard = new HazardViewModel(subsistence, subsistence_deduction);

                        decimal ra = dataReader["ra"].ToString() == "" ? 0 : decimal.Parse(dataReader["ra"].ToString());
                        decimal ta = dataReader["ta"].ToString() == "" ? 0 : decimal.Parse(dataReader["ta"].ToString());
                        decimal deduction = dataReader["deduction"].ToString() == "" ? 0 : decimal.Parse(dataReader["deduction"].ToString());


                        RegularFirstHalf FirstHalf = new RegularFirstHalf(month_salary,pera, Tardiness, tax,cfi,gsis_premium,gsis_consoloan, gsis_policy_loan, gsis_eml,gsis_uoli,gsis_edu,rel,gsis_help,pagibig_premium,pagibig_loan,pagibig_mp2,
                            philhealth,simc,hwmpc,disallowance,subsistence,subsistence_deduction,0,0);

                        RegularSecondHalf SecondHalf = new RegularSecondHalf(FirstHalf.GetTotalPay(),0,0,0,0,0,0,0,0,ra+ta);
                        PayslipModel = new RegularPayslipModel(FirstHalf,SecondHalf);

                    }

                    dataReader.Close();
                    CloseConnection();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.ToString());
                    CloseConnection();
                }
            }
            return PayslipModel;
        }

        public JoPayslipModel GetPayslipByEmployeeID(string userid, string from_date, string to_date)
        {
            JoPayslipModel data = null;

            string query = "SELECT absent_days,adjustment,working_days,month_salary,minutes_late,coop,phic,disallowance,gsis,pagibig,excess_mobile,tax,other_adjustment FROM payroll p " +
                           "WHERE userid = '" + userid + "' AND start_date = '" + from_date + "' AND end_date = '" + to_date + "'";
           
            if (OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                
                    while (dataReader.Read())
                    {
                        decimal Adjustment = decimal.Parse(dataReader["adjustment"].ToString());
                        if (Adjustment.Equals("") || Adjustment.Equals("NULL"))
                        {
                            Adjustment = 0;
                        }

                        decimal BasicSalary = decimal.Parse(dataReader["month_salary"].ToString());
                        if (BasicSalary.Equals("") || BasicSalary.Equals("NULL") || BasicSalary.Equals("Null") || BasicSalary.Equals(null))
                        {
                            BasicSalary = 0;
                        }

                        string MinutesLate = dataReader["minutes_late"].ToString();
                        if (MinutesLate.Equals("") || MinutesLate.Equals("NULL"))
                        {
                            MinutesLate = "0";
                        }

                        string DaysAbsent = dataReader["absent_days"].ToString();
                        if (DaysAbsent.Equals("") || DaysAbsent.Equals("NULL"))
                        {
                            DaysAbsent = "";
                        }

                        string WorkingDays = dataReader["working_days"].ToString();
                        if (WorkingDays.Equals("") || WorkingDays.Equals("NULL"))
                        {
                            WorkingDays = "0";
                        }

                        int minutes_late = int.Parse(MinutesLate);

                        int NumberOfDaysAbsent = DaysAbsent.Split(',').Length;
                        if (NumberOfDaysAbsent > 0 && !DaysAbsent.Split(',')[0].Equals(""))
                        {
                            minutes_late += (480 * NumberOfDaysAbsent);
                        }

                        int NumberWorkingDays = int.Parse(WorkingDays);
                        decimal DayRate = 0;
                        decimal Tardiness = 0;

                        if (NumberWorkingDays != 0 && BasicSalary != 0)
                        {
                            DayRate = BasicSalary / NumberWorkingDays;
                            Tardiness = (decimal)Math.Round((minutes_late * (((DayRate) / 8) / 60)), 2, MidpointRounding.AwayFromZero);
                        }

                        decimal OtherAdjustment = decimal.Parse(dataReader["other_adjustment"].ToString());
                        if (OtherAdjustment.Equals("") || OtherAdjustment.Equals("NULL"))
                        {
                            OtherAdjustment = 0;
                        }

                        decimal EWT = decimal.Parse(dataReader["tax"].ToString());
                        if (EWT.Equals("") || EWT.Equals("NULL"))
                        {
                            EWT = 0;
                        }

                        decimal ProfTax = decimal.Parse(dataReader["disallowance"].ToString());
                        if (ProfTax.Equals("") || ProfTax.Equals("NULL"))
                        {
                            ProfTax = 0;
                        }

                        decimal Hwmpc = decimal.Parse(dataReader["coop"].ToString());
                        if (Hwmpc.Equals("") || Hwmpc.Equals("NULL"))
                        {
                            Hwmpc = 0;
                        }

                        decimal Pagibig = decimal.Parse(dataReader["pagibig"].ToString());
                        if (Pagibig.Equals("") || Pagibig.Equals("NULL"))
                        {
                            Pagibig = 0;
                        }

                        decimal Phic = decimal.Parse(dataReader["phic"].ToString());
                        if (Phic.Equals("") || Phic.Equals("NULL"))
                        {
                            Phic = 0;
                        }

                        decimal Gsis = decimal.Parse(dataReader["gsis"].ToString());
                        if (Gsis.Equals("") || Gsis.Equals("NULL"))
                        {
                            Gsis = 0;
                        }

                        decimal Digitel = decimal.Parse(dataReader["excess_mobile"].ToString());
                        if (Digitel.Equals("") || Digitel.Equals("NULL"))
                        {
                            Digitel = 0;
                        }

                        data = new JoPayslipModel(BasicSalary, Adjustment, Tardiness, OtherAdjustment, EWT, ProfTax, Hwmpc, Pagibig, Phic, Gsis, Digitel);
                    }
                    dataReader.Close();
                    CloseConnection();
                }
                catch (Exception e){ CloseConnection(); }
            }
            return data;
        }
    }
}