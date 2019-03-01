/*
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;

namespace PayrollSystem.Database
{
    public class DatabaseConnect
    {
        public static MySqlConnection sql_payroll = null;
        public static MySqlConnection pis = null;
        public static MySqlConnection dts = null;
        public static MySqlConnection dtr = null;

        public static String message = "";
        public static string search = "";
        public static string server;
        public static string database;
        public static string uid;
        public static string password;
        public static int start;
        public static int end;
        public static string max_size;

        private static DatabaseConnect instance;

        private DatabaseConnect()
        {
        }

        //Singleton Pattern
        public static DatabaseConnect Instance

        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseConnect();
                    instance.Initialize();

                }
                return instance;
            }
        }

        //SQL DB COMMENT
        //Initialize values
        public void Initialize()
        {

            start = 0;
            end = 0;
            max_size = "0";
            if (sql_payroll == null)
            {
                server = "192.168.100.17";
                database = "payroll";
                uid = "doh7payroll";
                password = "doh7payroll";
                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                        database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + "; pooling = false;SslMode=none;convert zero datetime=True";

                sql_payroll = new MySqlConnection(connectionString);
            }
            if (pis == null)
            {
                server = "192.168.100.17";
                database = "pis";
                uid = "doh7payroll";
                password = "doh7payroll";
                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                        database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + "; pooling = false; SslMode=none; convert zero datetime=True";

                pis = new MySqlConnection(connectionString);
            }
            if (dts == null)
            {
                server = "192.168.100.17";
                database = "dts";
                uid = "doh7payroll";
                password = "doh7payroll";
                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                        database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + "; pooling = false; SslMode=none; convert zero datetime=True";

                dts = new MySqlConnection(connectionString);
            }
            if (dtr == null)
            {
                server = "192.168.100.17";
                database = "dohdtr";
                uid = "doh7payroll";
                password = "doh7payroll";
                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                        database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + "; pooling = false; SslMode=none; convert zero datetime=True";

                dtr = new MySqlConnection(connectionString);
            }
        }

        //open connection to database
        private bool OpenConnection()
        {
            bool ok = false;
            try
            {
                if (sql_payroll.State == ConnectionState.Closed)
                {
                    sql_payroll.Open();
                }
                if (dts.State == ConnectionState.Closed)
                {
                    dts.Open();
                }
                if (pis.State == ConnectionState.Closed)
                {
                    pis.Open();
                }
                if (dtr.State == ConnectionState.Closed)
                {
                    dtr.Open();
                }
                ok = true;

            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        //                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        //                      MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                Console.WriteLine(ex.Message);
                ok = false;
            }
            return ok;
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                sql_payroll.Close();
                dts.Close();
                pis.Close();
                dtr.Close();
                return true;
            }
            catch
            {


            }
            return false;
        }


        //Insert Record

        public String InsertPDF(String filePath, String type, String userid, String start_date, String end_date, String disbursement, String in_charge)
        {
            String message = "";
            if (!checkPdf(filePath, userid))
            {
                String query = "INSERT INTO payroll_pdf VALUES('0',now(),'" + start_date + "','" + end_date + "','" + userid + "','" + filePath + "','" + type + "','" + disbursement + "','" + in_charge + "',NULL,NULL)";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    //Create a data reader and Execute the command
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
                message = "PDF Successfully Generated";
            }
            else
            {
                String query = "UPDATE payroll_pdf SET date_created = now() WHERE file_path = '" + filePath + "' ";
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    //Create a data reader and Execute the command
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
                message = "PDF Successfully Updated";
            }
            return message;
        }

        public static String getMonthName(int number)
        {
            String month = "";
            switch (number)
            {
                case 1:
                    month = "January";
                    break;
                case 2:
                    month = "February";
                    break;
                case 3:
                    month = "March";
                    break;
                case 4:
                    month = "April";
                    break;
                case 5:
                    month = "May";
                    break;
                case 6:
                    month = "June";
                    break;
                case 7:
                    month = "July";
                    break;
                case 8:
                    month = "August";
                    break;
                case 9:
                    month = "September";
                    break;
                case 10:
                    month = "October";
                    break;
                case 11:
                    month = "November";
                    break;
                case 12:
                    month = "December";
                    break;
            }
            return month;
        }

        public Boolean checkPdf(String filpath, String userid)
        {

            Boolean found = false;
            String query = "SELECT id FROM payroll_pdf WHERE userid = '" + userid + "' AND file_path = '" + filpath + "'";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {

                    found = true;
                    break;
                }
                //Create a data reader and Execute the command
                dataReader.Close();
                this.CloseConnection();

            }
            return found;
        }


        public String UpdatePayroll(Payroll payroll)
        {

            String query = "UPDATE payroll SET start_date = '" + payroll.StartDate + "', end_date = '" + payroll.EndDate + "' , absent_days = '" + payroll.DaysAbsent +
                    "',remarks = '" + payroll.Remarks + "',adjustment = '" + payroll.Adjustment + "' ,working_days = '" + payroll.WorkDays +
                    "', month_salary = '" + payroll.Salary + "',minutes_late = '" + payroll.MinutesLate + "',coop = '" + payroll.Coop +
                    "',phic = '" + payroll.Phic + "',disallowance = '" + payroll.Disallowance + "',gsis = '" +
                    payroll.Gsis + "',pagibig = '" + payroll.Pagibig + "',excess_mobile = '" + payroll.ExcessMobile + "', tax = '" + payroll.Tax +
                    "',other_adjustment = '" + payroll.OtherAdjustment + "' WHERE id = '" + payroll.Id + "'";
            if (!checkPayroll(payroll.Id, payroll.Employee.PersonnelID, payroll.StartDate, payroll.EndDate))
            {
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    //Create a data reader and Execute the command
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    DatabaseConnect.start = 0;
                }
                return "Update Successfully";
            }

            return "Date already exists";
        }

        public String UpdatePayrollRegular(PayrollRegular payroll)
        {

            String query = "UPDATE regular_payroll SET month = '" + payroll.Month + "', year = '" + payroll.Year + "' , absent_days = '" + payroll.DaysAbsent +
                    "',working_days = '" + payroll.WorkDays + "',month_salary = '" + payroll.Salary + "' ,pera = '" + payroll.Pera +
                    "', minutes_late = '" + payroll.MinutesLate + "',tax = '" + payroll.Tax + "',cfi = '" + payroll.CFI +
                    "',gsis_premium = '" + payroll.GSIS_Premium + "',gsis_consoloan = '" + payroll.GSIS_Consoloan + "',gsis_policy_loan = '" +
                    payroll.GSIS_PolicyLoan + "',gsis_eml = '" + payroll.GSIS_EML + "',gsis_uoli = '" + payroll.GSIS_UOLI + "', gsis_edu = '" + payroll.GSIS_EDU +
                    "',gsis_help = '" + payroll.GSIS_Help + "', gsis_rel = '" + payroll.GSIS_REL + "', pagibig_premium = '" + payroll.Pagibig_Premium + "', pagibig_loan = '" +
                    payroll.Pagibig_Loan + "', disallowances = '" + payroll.Disallowances + "',philhealth = '" + payroll.PhilHealth + "',simc = '" + payroll.SIMC + "', hwmpc = '" + payroll.HWMPC + "', dbp = '" + payroll.DBP + "' , pagibig_mp2 = '" + payroll.MP2 + "'  WHERE id = '" + payroll.Id + "'";
            if (!checkPayrollRegular(payroll.Id, payroll.Employee.PersonnelID, payroll.Month, payroll.Year))
            {
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    //Create a data reader and Execute the command
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    DatabaseConnect.start = 0;
                }
                return "Update Successfully";
            }

            return "Date already exists";
        }

        public String UpdateCommunicable(CellphoneAllowance allowance)
        {

            String query = "UPDATE communicable_allowance SET month = '" + allowance.Month + "', year = '" + allowance.Year + "' ,remarks = '" + allowance.Remarks +
                    "',amount = '" + allowance.Amount + "' ,nov_billing= '" + allowance.NovBilling + "' WHERE id = '" + allowance.ID + "'";
            if (!checkCommunicable(allowance.ID, allowance.PersonnelID, allowance.Month, allowance.Year))
            {
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    //Create a data reader and Execute the command
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    DatabaseConnect.start = 0;
                }
                return "Update Successfully";
            }

            return "Date already exists";

        }

        public String UpdateLongevity(Longevity longevity)
        {

            String query = "UPDATE longevity SET month = '" + longevity.Month + "', year = '" + longevity.Year + "' ,salary_1 = '" + longevity.Salary1 + "',salary_2 = '" +
                    longevity.Salary2 + "' ,salary_3= '" + longevity.Salary3 + "',salary_4= '" + longevity.Salary4 + "',salary_5= '" + longevity.Salary5 + "',disallowance= '" +
                    longevity.Disallowance + "' WHERE id = '" + longevity.ID + "'";
            if (!checkLongevity(longevity.ID, longevity.PersonnelID, longevity.Month, longevity.Year))
            {
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    //Create a data reader and Execute the command
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    DatabaseConnect.start = 0;
                }
                return "Update Successfully";
            }

            return "Date already exists";

        }

        public String UpdateHazardPay(HazardPay hazardPay)
        {

            String query = "UPDATE hazard_pay SET month = '" + hazardPay.Month + "', year = '" + hazardPay.Year + "' ,hazard_pay = '" + hazardPay.Pay + "',hwmpc_loan = '" +
                    hazardPay.HWMPC + "' ,mortuary= '" + hazardPay.Mortuary + "',digitel_billing= '" + hazardPay.DigitelBilling + "',days_oo= '" + hazardPay.DaysOO + "',days_leave= '" +
                    hazardPay.DaysLeave + "' WHERE id = '" + hazardPay.ID + "'";
            if (!checkHazard(hazardPay.ID, hazardPay.PersonnelID, hazardPay.Month, hazardPay.Year))
            {
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    //Create a data reader and Execute the command
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    DatabaseConnect.start = 0;
                }
                return "Update Successfully";
            }

            return "Date already exists";
        }

        public String UpdateSubsistence(Subsistence subsistence)
        {

            String query = "UPDATE subsistence SET month = '" + subsistence.Month + "', year = '" + subsistence.Year + "' ,subsistence_allowance = '" +
                    subsistence.SubsistenceAllowance + "',laundry_allowance = '" + subsistence.LaundryAllowance + "' ,absences= '" + subsistence.Absences +
                    "',hwmcp= '" + subsistence.HWMCP + "',remarks= '" + subsistence.Remarks + "',no_days= '" + subsistence.NoDays + "' WHERE id = '" + subsistence.ID + "'";
            if (!checkSubsistence(subsistence.ID, subsistence.PersonnelID, subsistence.Month, subsistence.Year))
            {
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    //Create a data reader and Execute the command
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    DatabaseConnect.start = 0;
                }
                return "Update Successfully";
            }

            return "Date already exists";
        }

        public String UpdateRata(Rata rata)
        {

            String query = "UPDATE rata SET month = '" + rata.Month + "', year = '" + rata.Year + "' ,ra= '" + rata.Ra + "',ta = '" +
                    rata.Ta + "' ,deduction= '" + rata.Deductions + "',remarks= '" + rata.Remarks + "' WHERE id = '" + rata.ID + "'";
            if (!checkHazard(rata.ID, rata.PersonnelID, rata.Month, rata.Year))
            {
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    //Create a data reader and Execute the command
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    DatabaseConnect.start = 0;
                }
                return "Update Successfully";
            }

            return "Date already exists";

        }

        public String UpdateOnInsert(Payroll payroll)
        {

            String query = "UPDATE payroll SET start_date = '" + payroll.StartDate + "', end_date = '" + payroll.EndDate + "' , absent_days = '" +
                    payroll.DaysAbsent + "',remarks = '" + payroll.Remarks + "',adjustment = '" + payroll.Adjustment + "' ,working_days = '" + payroll.WorkDays +
                    "', month_salary = '" + payroll.Salary + "',minutes_late = '" + payroll.MinutesLate + "',coop = '" + payroll.Coop + "',phic = '" + payroll.Phic +
                    "',disallowance = '" + payroll.Disallowance + "',gsis = '" + payroll.Gsis + "',pagibig = '" + payroll.Pagibig +
                    "',excess_mobile = '" + payroll.ExcessMobile + "',tax = '" + payroll.Tax + "',other_adjustment = '" + payroll.OtherAdjustment + "' WHERE userid = '" + payroll.Employee.PersonnelID + "' AND start_date = '" + payroll.StartDate + "' AND end_date = '" + payroll.EndDate + "'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                DatabaseConnect.start = 0;
            }
            return "Update Successfully";
        }

        public String InsertCommunicable(CellphoneAllowance allowance)
        {
            String query = "INSERT INTO communicable_allowance VALUES('0','" + allowance.PersonnelID + "','" + allowance.Amount +
                    "','" + allowance.NovBilling + "',@remarks,'" + allowance.Month + "','" + allowance.Year + "')";
            if (!checkCommunicable(allowance.ID, allowance.PersonnelID, allowance.Month, allowance.Year))
            {
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    cmd.Parameters.AddWithValue("@remarks", allowance.Remarks);
                    //Create a data reader and Execute the command
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    DatabaseConnect.start = 0;
                }
                return "Insert Successfully";
            }
            return "Date already exists";
        }

        public String InsertSubsistence(Subsistence subsistence)
        {
            String query = "INSERT INTO subsistence VALUES('0','" + subsistence.PersonnelID + "','" + subsistence.SubsistenceAllowance + "','" + subsistence.LaundryAllowance +
                    "','" + subsistence.Absences + "','" + subsistence.HWMCP + "','" + subsistence.NoDays + "',@remarks,'" + subsistence.Month + "','" + subsistence.Year + "')";

            if (!checkSubsistence(subsistence.ID, subsistence.PersonnelID, subsistence.Month, subsistence.Year))
            {
                if (this.OpenConnection() == true)
                {

                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    cmd.Parameters.AddWithValue("@remarks", subsistence.Remarks);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    DatabaseConnect.start = 0;
                }
                return "Insert Successfully";
            }
            return "Date already exists";
        }

        public String InsertPayroll(Payroll payroll)
        {
            if (!checkPayroll(payroll.Id, payroll.Employee.PersonnelID, payroll.StartDate, payroll.EndDate))
            {
                String query = "INSERT INTO payroll VALUES('0','" + payroll.Employee.PersonnelID + "','" + payroll.StartDate + "','" + payroll.EndDate + "','" +
                        payroll.DaysAbsent + "','" + payroll.WorkDays + "','" + payroll.Salary + "','" + payroll.Adjustment + "','" + payroll.MinutesLate + "','" +
                        payroll.Coop + "','" + payroll.Phic + "','" + payroll.Disallowance + "','" + payroll.Gsis + "','" + payroll.Pagibig + "','" + payroll.ExcessMobile
                        + "',@remarks,NULL,NULL,'" + payroll.Tax + "','" + payroll.OtherAdjustment + "')";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    cmd.Parameters.AddWithValue("@remarks", payroll.Remarks);
                    //Create a data reader and Execute the command
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    DatabaseConnect.start = 0;
                }
                if (decimal.Parse(payroll.Pagibig) > 0)
                {
                    IncrementRemittance("pagibig_remittance", payroll.Employee.PersonnelID);
                }
                if (decimal.Parse(payroll.Coop) > 0)
                {
                    IncrementRemittance("coop_remittance", payroll.Employee.PersonnelID);
                }
                if (decimal.Parse(payroll.Phic) > 0)
                {
                    IncrementRemittance("phic_remittance", payroll.Employee.PersonnelID);
                }
                if (decimal.Parse(payroll.Disallowance) > 0)
                {
                    IncrementRemittance("disallowance_remittance", payroll.Employee.PersonnelID);
                }
                if (decimal.Parse(payroll.Gsis) > 0)
                {
                    IncrementRemittance("gsis_remittance", payroll.Employee.PersonnelID);
                }
                if (decimal.Parse(payroll.ExcessMobile) > 0)
                {
                    IncrementRemittance("excess_remittance", payroll.Employee.PersonnelID);
                }
                return "Insert Successfully";
            }
            else
            {
                return "Date already exists";
            }
        }

        public String InsertPayrollRegular(PayrollRegular payroll)
        {
            if (!checkPayrollRegular(payroll.Id, payroll.Employee.PersonnelID, payroll.Month, payroll.Year))
            {
                String query = "INSERT INTO regular_payroll VALUES('0','" + payroll.Employee.PersonnelID + "','" + payroll.Month + "','" + payroll.Year + "','" +
                        payroll.DaysAbsent + "','" + payroll.WorkDays + "','" + payroll.Salary + "','" + payroll.Pera + "','" + payroll.MinutesLate + "','" + payroll.Tax + "','" +
                        payroll.CFI + "','" + payroll.GSIS_Premium + "','" + payroll.GSIS_Consoloan + "','" + payroll.GSIS_PolicyLoan + "','" + payroll.GSIS_EML + "','" +
                        payroll.GSIS_UOLI + "','" + payroll.GSIS_EDU + "','" + payroll.GSIS_Help + "','" + payroll.GSIS_REL + "','" + payroll.Pagibig_Premium + "','" +
                        payroll.Pagibig_Loan + "','" + payroll.Disallowances + "','" + payroll.PhilHealth + "','" + payroll.SIMC + "','" + payroll.HWMPC + "','" + payroll.DBP + "','" + payroll.MP2 + "')";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    //Create a data reader and Execute the command
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    DatabaseConnect.start = 0;
                }
                /*
                if (decimal.Parse(payroll.Pagibig) > 0)
                {
                    IncrementRemittance("pagibig_remittance", payroll.Employee.PersonnelID);
                }
                if (decimal.Parse(payroll.Coop) > 0)
                {
                    IncrementRemittance("coop_remittance", payroll.Employee.PersonnelID);
                }
                if (decimal.Parse(payroll.Phic) > 0)
                {
                    IncrementRemittance("phic_remittance", payroll.Employee.PersonnelID);
                }
                if (decimal.Parse(payroll.Disallowance) > 0)
                {
                    IncrementRemittance("disallowance_remittance", payroll.Employee.PersonnelID);
                }
                if (decimal.Parse(payroll.Gsis) > 0)
                {
                    IncrementRemittance("gsis_remittance", payroll.Employee.PersonnelID);
                }
                if (decimal.Parse(payroll.ExcessMobile) > 0)
                {
                    IncrementRemittance("excess_remittance", payroll.Employee.PersonnelID);
                }   
             
                return "Insert Successfully";
            }
            else
            {
                return "Date already exists";
            }
        }


        public String InsertHazardPay(HazardPay hazardPay)
        {
            String query = "INSERT INTO hazard_pay VALUES('0','" + hazardPay.PersonnelID + "','" + hazardPay.Pay + "','" + hazardPay.HWMPC + "','" + hazardPay.Mortuary + "','"
                    + hazardPay.DigitelBilling + "','" + hazardPay.Month + "','" + hazardPay.Year + "','" + hazardPay.DaysLeave + "','" + hazardPay.DaysOO + "')";
            if (!checkHazard(hazardPay.ID, hazardPay.PersonnelID, hazardPay.Month, hazardPay.Year))
            {
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    //Create a data reader and Execute the command
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    DatabaseConnect.start = 0;
                }
                return "Insert Successfully";
            }
            return "Date already exists";
        }


        public String InsertLongevity(Longevity longevity)
        {
            String query = "INSERT INTO longevity VALUES('0','" + longevity.PersonnelID + "','" + longevity.Salary1 + "','" + longevity.Salary2 + "','" + longevity.Salary3
                    + "','" + longevity.Salary4 + "','" + longevity.Salary5 + "','" + longevity.Disallowance + "','" + longevity.Month + "','" + longevity.Year + "')";
            if (!checkLongevity(longevity.ID, longevity.PersonnelID, longevity.Month, longevity.Year))
            {
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    //Create a data reader and Execute the command
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    DatabaseConnect.start = 0;
                }
                return "Insert Successfully";
            }
            return "Date already exists";
        }

        public String InsertRata(Rata rata)
        {
            String query = "INSERT INTO rata VALUES('0','" + rata.PersonnelID + "','" + rata.Ra + "','" + rata.Ta + "','" + rata.Deductions + "',@remarks,'"
                    + rata.Month + "','" + rata.Year + "')";
            if (!checkRata(rata.ID, rata.PersonnelID, rata.Month, rata.Year))
            {
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    cmd.Parameters.AddWithValue("@remarks", rata.Remarks);
                    //Create a data reader and Execute the command
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    DatabaseConnect.start = 0;
                }
                return "Insert Successfully";
            }
            return "Date already exists";
        }

        public Boolean checkPayroll(String payID, String userid, String start_date, String end_date)
        {

            Boolean found = false;
            String query = "SELECT id FROM payroll WHERE userid = '" + userid + "' AND start_date = '" + start_date + "' AND end_date = '" + end_date + "' AND id <> '" + payID + "'";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {

                    found = true;
                    break;
                }
                //Create a data reader and Execute the command
                dataReader.Close();
                this.CloseConnection();

            }
            return found;
        }

        public Boolean checkPayrollRegular(String payID, String userid, int month, int year)
        {

            Boolean found = false;
            String query = "SELECT id FROM regular_payroll WHERE userid = '" + userid + "' AND month = '" + month + "' AND year = '" + year + "' AND id <> '" + payID + "'";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {

                    found = true;
                    break;
                }
                //Create a data reader and Execute the command
                dataReader.Close();
                this.CloseConnection();

            }
            return found;
        }

        public Boolean checkCommunicable(String cellid, String userid, int month, int year)
        {

            Boolean found = false;
            String query = "SELECT id FROM communicable_allowance WHERE userid = '" + userid + "' AND month = '" + month + "' AND year= '" + year + "' AND id <> '" + cellid + "'";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {

                    found = true;
                    break;
                }
                //Create a data reader and Execute the command
                dataReader.Close();
                this.CloseConnection();

            }
            return found;
        }

        public Boolean checkHazard(String hazard_id, String userid, int month, int year)
        {

            Boolean found = false;
            String query = "SELECT id FROM hazard_pay WHERE userid = '" + userid + "' AND month = '" + month + "' AND year= '" + year + "' AND id <> '" + hazard_id + "'";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {

                    found = true;
                    break;
                }
                //Create a data reader and Execute the command
                dataReader.Close();
                this.CloseConnection();

            }
            return found;
        }

        public Boolean checkSubsistence(String subsistence_id, String userid, int month, int year)
        {

            Boolean found = false;
            String query = "SELECT id FROM subsistence WHERE userid = '" + userid + "' AND month = '" + month + "' AND year= '" + year + "' AND id <> '" + subsistence_id + "'";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {

                    found = true;
                    break;
                }
                //Create a data reader and Execute the command
                dataReader.Close();
                this.CloseConnection();

            }
            return found;
        }

        public Boolean checkLongevity(String hazard_id, String userid, int month, int year)
        {

            Boolean found = false;
            String query = "SELECT id FROM longevity WHERE userid = '" + userid + "' AND month = '" + month + "' AND year= '" + year + "' AND id <> '" + hazard_id + "'";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {

                    found = true;
                    break;
                }
                //Create a data reader and Execute the command
                dataReader.Close();
                this.CloseConnection();

            }
            return found;
        }


        public Boolean checkRata(String rata_id, String userid, int month, int year)
        {

            Boolean found = false;
            String query = "SELECT id FROM rata WHERE userid = '" + userid + "' AND month = '" + month + "' AND year= '" + year + "' AND id <> '" + rata_id + "'";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {

                    found = true;
                    break;
                }
                //Create a data reader and Execute the command
                dataReader.Close();
                this.CloseConnection();

            }
            return found;
        }

        public String GetEmployeeNameByID(String divisionID)
        {
            String name = "";
            String query = "SELECT p.fname,p.mname,p.lname FROM division d LEFT JOIN users p ON p.id = d.head WHERE d.id = '" + divisionID + "' GROUP BY d.head";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, dts);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    String Firstname = dataReader["fname"].ToString();
                    String Lastname = dataReader["lname"].ToString();
                    String MiddleName = dataReader["mname"].ToString();
                    name = Firstname + " " + MiddleName + " " + Lastname;
                }
                //Create a data reader and Execute the command
                dataReader.Close();
                this.CloseConnection();
            }
            return name;
        }

        public Employee GetEmployeeByID(String id)
        {
            Employee employee = null;
            String query = "SELECT fname,lname,mname,designation_id FROM pis.personal_information WHERE userid = '" + id + "'";
            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, pis);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        String Firstname = dataReader["fname"].ToString().ToUpper();
                        String Lastname = dataReader["lname"].ToString().ToUpper();
                        String MiddleName = dataReader["mname"].ToString().ToUpper();
                        String designation_id = dataReader["designation_id"].ToString().ToUpper();
                        String position = GetDesignationName(designation_id).ToUpper();
                        employee = new Employee(id, Firstname, Lastname, MiddleName, position, "", "", "", "", "", "", "", "", "");
                    }
                    //Create a data reader and Execute the command
                    dataReader.Close();
                }
                catch (MySqlException e)
                {

                }
            }

            return employee;
        }


        public String GetSalaryChargeByID(String ID)
        {
            String name = "";
            String query = "SELECT salary_charge FROM personal_information WHERE userid = '" + ID + "'";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, pis);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    name = dataReader["salary_charge"].ToString();
                }
                //Create a data reader and Execute the command
                dataReader.Close();
                this.CloseConnection();
            }
            return name;
        }

        //TRY
        public Employee Login(String userid, String pin)
        {

            Employee employee = null;
            String query = "SELECT u.usertype,u.username,u.pin,i.job_status,i.userid,i.fname,i.lname,i.mname,i.employee_status,s.description FROM pis.users u LEFT JOIN pis.personal_information i ON u.username = i.userid LEFT JOIN dts.section s ON i.section_id= s.id WHERE u.username = '" + userid + "' AND u.pin = '" + pin + "'";
            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, pis);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        String PersonnelID = dataReader["userid"].ToString();
                        String Firstname = dataReader["fname"].ToString();
                        String Lastname = dataReader["lname"].ToString();
                        String MiddleName = dataReader["mname"].ToString();
                        String JobType = dataReader["employee_status"].ToString();
                        String Tin = "";
                        String Section = dataReader["description"].ToString();
                        String UserType = dataReader["usertype"].ToString();
                        String PIN = dataReader["pin"].ToString();
                        String JobStatus = dataReader["job_status"].ToString();
                        employee = new Employee(PersonnelID, Firstname, Lastname, MiddleName, JobType, Tin, Section, "", "", UserType, pin, "", JobStatus, "");
                    }
                    //Create a data reader and Execute the command
                    dataReader.Close();
                    this.CloseConnection();
                }
                catch (MySqlException e)
                {
                    this.CloseConnection();
                }

            }
            return employee;
        }

        public String DummyifWeekend(String date)
        {
            DateTime dateTime = new DateTime(int.Parse(date.Split('/')[2]), int.Parse(date.Split('/')[0]), int.Parse(date.Split('/')[1]));
            DayOfWeek datez = dateTime.DayOfWeek;
            if ((datez == DayOfWeek.Saturday) || (datez == DayOfWeek.Sunday))
            {
                return "Weekend";
            }
            return "Not Weekend";
        }

        public String DummyifHoliday(String date)
        {
            String found = "Not Holiday";
            String month = (int.Parse(date.Split('/')[0]) > 9) ? date.Split('/')[0] : "0" + date.Split('/')[0];
            String day = (int.Parse(date.Split('/')[1]) > 9) ? date.Split('/')[1] : "0" + date.Split('/')[1];
            String year = date.Split('/')[2];

            date = year + "-" + month + "-" + day;

            List<Employee> list = new List<Employee>();
            String query = "SELECT id FROM calendar WHERE start <= '" + date + "' AND end > '" + date + "' AND status = '1'";
            //Create Command
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, dtr);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    found = "Holiday";
                }
                dataReader.Close();
                this.CloseConnection();
            }

            //close Data Reader
            return found;
        }

        public String DummyCalendar(String date)
        {
            String response = "";
            string query = "SELECT start FROM calendar WHERE status = '1' LIMIT 10";
            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, dtr);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        response += dataReader["start"].ToString().Split(' ')[0] + " ";
                    }
                    //Create a data reader and Execute the command
                    dataReader.Close();
                    this.CloseConnection();
                }
                catch (MySqlException e)
                {
                    response += " CATCH";
                    this.CloseConnection();
                }

            }
            return response;
        }

        public String DummyDateAdd()
        {
            String response = "";
            string query = "SELECT start,DATE_ADD(start,INTERVAL 1 DAY) as 'start_added' FROM calendar LIMIT 10";
            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, dtr);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        response += dataReader["start"].ToString().Split(' ')[0] + "-" + dataReader["start_added"].ToString() + " ";
                    }
                    //Create a data reader and Execute the command
                    dataReader.Close();
                    this.CloseConnection();
                }
                catch (MySqlException e)
                {
                    response += " CATCH";
                    this.CloseConnection();
                }

            }
            return response;
        }

        public String DummyDTR()
        {
            String response = "";
            string query = "SELECT * FROM dtr_file LIMIT 10";
            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, dtr);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        response += dataReader["datein"].ToString().Split(' ')[0] + " ";
                    }
                    //Create a data reader and Execute the command
                    dataReader.Close();
                    this.CloseConnection();
                }
                catch (MySqlException e)
                {
                    response += " CATCH";
                    this.CloseConnection();
                }

            }
            return response;
        }

        public String DummyCTO(String date)
        {
            String response = "";
            string query = "SELECT datein FROM cdo_logs";
            if (!date.Equals(""))
            {
                query = query + " WHERE datein = '" + date + "'";
            }
            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, dtr);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        response += dataReader["datein"].ToString().Split(' ')[0] + " ";
                    }
                    //Create a data reader and Execute the command
                    dataReader.Close();
                    this.CloseConnection();
                }
                catch (MySqlException e)
                {
                    response += " CATCH";
                    this.CloseConnection();
                }

            }
            return response;
        }

        public String DummySO(String date)
        {
            String response = "";
            string query = "SELECT datein FROM so_logs";
            if (!date.Equals(""))
            {
                query = query + " WHERE datein = '" + date + "'";
            }
            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, dtr);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        response += dataReader["datein"].ToString().Split(' ')[0] + " ";
                    }
                    //Create a data reader and Execute the command
                    dataReader.Close();
                    this.CloseConnection();
                }
                catch (MySqlException e)
                {
                    response += " CATCH";
                    this.CloseConnection();
                }

            }
            return response;
        }

        public String SectionDescription(String id)
        {
            String response = "";
            String query = "SELECT description FROM section WHERE id = '" + id + "' LIMIT 1";
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, dts);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {

                    response = dataReader["description"].ToString();
                }

                //close Data Reader
                dataReader.Close();
                //close Connection
                this.CloseConnection();

                //return list to be displayed

            }
            return response;
        }

        public List<PdfFile> FetchPdf(String type, String search, String mId, bool cashier)
        {
            int temp_start = start;
            int temp_end = end;
            int count = 0;
            if (type.Equals("1"))
            {
                if (start - 10 <= 0)
                {
                    start = 0;
                }
                else
                {
                    start -= 10;
                }
            }
            else if (type.Equals("0"))
            {
                start = end;
            }
            else
            {
                start = 0;
            }

            List<PdfFile> list = new List<PdfFile>();
            String query = "";
            if (!search.Equals(""))
            {
                String start_date = search.Split(' ')[0];
                String end_date = search.Split(' ')[2];
                if (!mId.Equals("0"))
                {
                    if (cashier)
                    {
                        query = "SELECT (SELECT COUNT(id) FROM payroll_pdf WHERE userid = '" + mId + "' AND start_date = '" + start_date + "' AND end_date = '" + end_date + "' AND file_path LIKE '1%') as 'MAX_SIZE',userid,type,id,date_created,start_date,end_date,disbursement_type,in_charge,file_path FROM payroll_pdf WHERE start_date = '" + start_date + "' AND end_date = '" + end_date + "'";
                        query = query + " AND userid = '" + mId + "'";
                        query = query + " AND file_path LIKE '1%'";
                    }
                    else
                    {
                        query = "SELECT (SELECT COUNT(id) FROM payroll_pdf WHERE userid = '" + mId + "' AND start_date = '" + start_date + "' AND end_date = '" + end_date + "') as 'MAX_SIZE',userid,type,id,date_created,start_date,end_date,disbursement_type,in_charge,file_path FROM payroll_pdf WHERE start_date = '" + start_date + "' AND end_date = '" + end_date + "'";
                        query = query + " AND userid = '" + mId + "'";
                    }


                }
                else
                {

                    if (cashier)
                    {
                        query = "SELECT (SELECT COUNT(id) FROM payroll_pdf WHERE start_date = '" + start_date + "' AND end_date = '" + end_date + "' AND file_path LIKE '1%') as 'MAX_SIZE',userid,type,id,date_created,start_date,end_date,disbursement_type,in_charge,file_path FROM payroll_pdf WHERE start_date = '" + start_date + "' AND end_date = '" + end_date + "'";
                        query = query + " AND file_path LIKE '1%'";
                    }
                    else
                    {
                        query = "SELECT (SELECT COUNT(id) FROM payroll_pdf WHERE start_date = '" + start_date + "' AND end_date = '" + end_date + "') as 'MAX_SIZE',userid,type,id,date_created,start_date,end_date,disbursement_type,in_charge,file_path FROM payroll_pdf WHERE start_date = '" + start_date + "' AND end_date = '" + end_date + "'";
                    }
                }
            }
            else
            {
                if (!mId.Equals("0"))
                {
                    if (cashier)
                    {
                        query = "SELECT (SELECT COUNT(id) FROM payroll_pdf WHERE userid = '" + mId + "' AND file_path LIKE '1%') as 'MAX_SIZE',userid,type,id,date_created,disbursement_type,in_charge,start_date,end_date,file_path FROM payroll_pdf";
                        query = query + " WHERE userid = '" + mId + "'";
                        query = query + " AND file_path LIKE '1%'";
                    }
                    else
                    {
                        query = "SELECT (SELECT COUNT(id) FROM payroll_pdf WHERE userid = '" + mId + "') as 'MAX_SIZE',userid,type,id,date_created,disbursement_type,in_charge,start_date,end_date,file_path FROM payroll_pdf";
                        query = query + " WHERE userid = '" + mId + "'";
                    }


                }
                else
                {
                    if (cashier)
                    {
                        query = "SELECT (SELECT COUNT(id) FROM payroll_pdf WHERE file_path LIKE '1%') as 'MAX_SIZE',userid,type,id,date_created,start_date,end_date,file_path,disbursement_type,in_charge FROM payroll_pdf";
                        query = query + " WHERE file_path LIKE '1%'";
                    }
                    else
                    {
                        query = "SELECT (SELECT COUNT(id) FROM payroll_pdf) as 'MAX_SIZE',userid,type,id,date_created,start_date,end_date,file_path,disbursement_type,in_charge FROM payroll_pdf";
                    }

                }
            }
            query = query + " ORDER BY date_created DESC LIMIT 10 OFFSET " + DatabaseConnect.start;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    max_size = dataReader["MAX_SIZE"].ToString();
                    count += 1;
                    String id = dataReader["id"].ToString();
                    String userid = dataReader["userid"].ToString();
                    String date = dataReader["date_created"].ToString().Split(' ')[0];
                    String start_date = dataReader["start_date"].ToString();
                    String end_date = dataReader["end_date"].ToString();
                    String path = dataReader["file_path"].ToString();
                    String disbursement = dataReader["disbursement_type"].ToString();
                    String in_charge = dataReader["in_charge"].ToString();
                    PdfFile pdf = new PdfFile(id, userid, start_date, end_date, path, date, disbursement, in_charge);
                    list.Add(pdf);
                }
                //Create a data reader and Execute the command
                end = (start + count);
                dataReader.Close();
                this.CloseConnection();
            }
            if (count == 0)
            {
                start = 0;
                end = 0;
                max_size = "0";
            }
            return list;
        }

        public List<PdfFile> FetchPacsVal(String type, String search, String mId, bool cashier)
        {
            int temp_start = start;
            int temp_end = end;
            int count = 0;
            if (type.Equals("1"))
            {
                if (start - 10 <= 0)
                {
                    start = 0;
                }
                else
                {
                    start -= 10;
                }
            }
            else if (type.Equals("0"))
            {
                start = end;
            }
            else
            {
                start = 0;
            }

            List<PdfFile> list = new List<PdfFile>();
            String query = "";
            if (!search.Equals(""))
            {
                String start_date = search.Split(' ')[0];
                String end_date = search.Split(' ')[2];

                query = "SELECT (SELECT COUNT(DISTINCT start_date) FROM payroll_pdf WHERE start_date = '" + start_date + "' AND end_date = '" + end_date + "' AND (disbursement_type = 'CASH_CARD' OR disbursement_type = 'ATM') AND type = '1') as 'MAX_SIZE',userid,type,id,date_created,start_date,end_date,disbursement_type,in_charge,file_path FROM payroll_pdf WHERE start_date = '" + start_date + "' AND end_date = '" + end_date + "' AND (disbursement_type = 'CASH_CARD' OR disbursement_type = 'ATM') AND type = '1'";
            }
            else
            {
                query = "SELECT (SELECT COUNT(DISTINCT start_date) FROM payroll_pdf WHERE (disbursement_type = 'CASH_CARD' OR disbursement_type = 'ATM') AND type = '1') as 'MAX_SIZE',userid,type,id,date_created,disbursement_type,in_charge,start_date,end_date,file_path FROM payroll_pdf WHERE (disbursement_type = 'CASH_CARD' OR disbursement_type = 'ATM') AND type = '1'";

            }
            query = query + " GROUP BY start_date,end_date,disbursement_type,type ORDER BY date_created DESC LIMIT 10 OFFSET " + DatabaseConnect.start;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    max_size = dataReader["MAX_SIZE"].ToString();
                    count += 1;
                    String id = dataReader["id"].ToString();
                    String userid = dataReader["userid"].ToString();
                    String date = dataReader["date_created"].ToString().Split(' ')[0];
                    String start_date = dataReader["start_date"].ToString();
                    String end_date = dataReader["end_date"].ToString();
                    String path = dataReader["file_path"].ToString();
                    String disbursement = dataReader["disbursement_type"].ToString();
                    String in_charge = dataReader["in_charge"].ToString();
                    PdfFile pdf = new PdfFile(id, userid, start_date, end_date, path, date, disbursement, in_charge);
                    list.Add(pdf);
                }
                //Create a data reader and Execute the command
                end = (start + count);
                dataReader.Close();
                this.CloseConnection();
            }
            if (count == 0)
            {
                start = 0;
                end = 0;
                max_size = "0";
            }
            return list;
        }


        //Select statement
        public List<InCharge> GetInCharge(String id)
        {

            String query = "SELECT d.id,d.description,d.head,p.fname,p.mname,p.lname FROM division d LEFT JOIN users p ON p.id = d.head";
            if (!id.Equals(""))
            {
                query = query + " WHERE d.id = '" + id + "'";
            }
            query = query + " GROUP BY d.head";
            List<InCharge> list = new List<InCharge>();


            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, dts);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    String division_id = dataReader["id"].ToString();
                    String description = dataReader["description"].ToString();
                    String emp_ID = dataReader["head"].ToString();
                    String fname = dataReader["fname"].ToString();
                    String mname = dataReader["mname"].ToString();
                    String lname = dataReader["lname"].ToString();
                    InCharge myObject = new InCharge(emp_ID, fname, lname, mname, division_id, description);
                    list.Add(myObject);
                }

                //close Data Reader
                dataReader.Close();
                //close Connection
                this.CloseConnection();

                //return list to be displayed

            }
            return list;
        }

        public List<Sections> GetSection()
        {

            String query = "SELECT id,division,description,head FROM section ORDER BY description ASC";
            List<Sections> list = new List<Sections>();


            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, dts);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    String sectionID = dataReader["id"].ToString();
                    String divisionID = dataReader["division"].ToString();
                    String description = dataReader["description"].ToString();
                    String headID = dataReader["head"].ToString();
                    Sections section = new Sections(sectionID, divisionID, description, headID);
                    list.Add(section);
                }

                //close Data Reader
                dataReader.Close();
                //close Connection
                this.CloseConnection();

                //return list to be displayed

            }
            return list;
        }

        public String GetDesignationName(String id)
        {
            String name = "";
            String query = "SELECT description FROM dts.designation WHERE id = '" + id + "' LIMIT 1";
            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, dts);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        name = dataReader["description"].ToString();
                    }
                    //Create a data reader and Execute the command
                    dataReader.Close();
                    //this.CloseConnection();
                }
                catch (MySqlException e)
                {
                    //this.CloseConnection();
                }
            }
            return name;
        }

        public List<Employee> GetEmployee(String type, String search, String desc, String disbursement)
        {
            int temp_start = DatabaseConnect.start;
            int temp_end = end;
            int count = 0;
            if (type.Equals("1"))
            {
                if (DatabaseConnect.start - 10 <= 0)
                {
                    DatabaseConnect.start = 0;
                }
                else
                {
                    DatabaseConnect.start -= 10;
                }
            }
            else if (type.Equals("0"))
            {
                DatabaseConnect.start = end;
            }
            else
            {
                DatabaseConnect.start = 0;
            }
            string query = "";
            if (!search.Equals(""))
            {
                if (!disbursement.Equals(""))
                {
                    query = "SELECT (SELECT COUNT(userid) FROM personal_information WHERE job_status = '" + desc + "' AND (position <> 'Health Aiders' OR position IS NULL) AND employee_status = 'Active' AND (fname LIKE '" + search + "%' OR lname LIKE '" + search + "%' OR userid LIKE '" + search + "%') AND disbursement_type = '" + disbursement + "') as 'MAX_SIZE'";
                }
                else
                {
                    query = "SELECT (SELECT COUNT(userid) FROM personal_information WHERE job_status = '" + desc + "' AND (position <> 'Health Aiders' OR position IS NULL) AND employee_status = 'Active' AND (fname LIKE '" + search + "%' OR lname LIKE '" + search + "%' OR userid LIKE '" + search + "%')) as 'MAX_SIZE'";
                }
                query = query + " , position,tin_no,userid,fname,lname,mname,job_status,designation_id,sched FROM personal_information WHERE job_status = '" + desc + "' AND employee_status = 'Active' AND (position <> 'Health Aiders' OR position IS NULL) ";
                query = query + " AND (fname LIKE '" + search + "%' OR lname LIKE '" + search + "%' OR userid LIKE '" + search + "%')";
                if (!disbursement.Equals(""))
                {
                    query = query + " AND disbursement_type = '" + disbursement + "'";
                }
            }
            else
            {
                query = "SELECT (SELECT COUNT(userid) FROM personal_information WHERE job_status = '" + desc + "' AND employee_status = 'Active' AND (position <> 'Health Aiders' OR position IS NULL) AND disbursement_type = '" + disbursement + "') as 'MAX_SIZE' , position,tin_no,userid,fname,lname,mname,job_status,designation_id,sched FROM personal_information WHERE job_status = '" + desc + "' AND employee_status = 'Active' AND (position <> 'Health Aiders' OR position IS NULL)";
                if (!disbursement.Equals(""))
                {
                    query = query + " AND disbursement_type = '" + disbursement + "'";
                }
            }
            query = query + " ORDER BY lname,fname LIMIT 10 OFFSET " + DatabaseConnect.start;

            //Create a list to store the result
            List<Employee> list = new List<Employee>();


            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, pis);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    DatabaseConnect.max_size = dataReader["MAX_SIZE"].ToString();
                    count += 1;


                    String userid = dataReader["userid"].ToString();
                    String fname = dataReader["fname"].ToString();
                    String lname = dataReader["lname"].ToString();
                    String mname = dataReader["mname"].ToString();
                    String desig_id = dataReader["designation_id"].ToString();
                    String tin = dataReader["tin_no"].ToString();
                    String sched = dataReader["sched"].ToString();
                    String emptype = GetDesignationName(desig_id);

                    Employee employee = new Employee(userid, fname, lname, mname, emptype, tin, "", "", "", "", "", sched, "", "");
                    list.Add(employee);
                }

                //close Data Reader
                end = (start + count);
                dataReader.Close();
                DatabaseConnect.end = (DatabaseConnect.start + count);
                //close Connection
                this.CloseConnection();

                //return list to be displayed

            }
            if (count == 0)
            {
                start = 0;
                end = 0;
                max_size = "0";
            }
            return list;
        }

        public String GetDivisionNameByID(String id)
        {
            String division = "";
            String query = "SELECT description FROM division WHERE id = '" + id + "'";

            //Create a list to store the result

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, dts);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    division = dataReader["description"].ToString().Split('-')[0];
                }

                //close Data Reader
                dataReader.Close();
                //close Connection
                this.CloseConnection();

                //return list to be displayed

            }
            return division;
        }

        public String GetSalary(String id)
        {
            String salary = "0";
            String query = "SELECT monthly_salary FROM work_experience WHERE userid = '" + id + "' AND date_to = 'Present' ORDER BY date_to DESC LIMIT 1";

            //Create a list to store the result
            List<Employee> list = new List<Employee>();


            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, pis);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    salary = dataReader["monthly_salary"].ToString();
                    salary = salary.Replace(",", "");
                    if (salary.Equals(""))
                    {
                        salary = "0";
                    }
                }

                //close Data Reader
                dataReader.Close();
                //close Connection
                this.CloseConnection();

                //return list to be displayed

            }
            return salary;
        }

        public List<CellphoneAllowance> GetCommunicableList(String userid, String type, String search)
        {
            int temp_start = DatabaseConnect.start;
            int temp_end = end;
            int count = 0;
            int month = 0;
            int year = 0;

            if (type.Equals("1"))
            {
                if (DatabaseConnect.start - 10 <= 0)
                {
                    DatabaseConnect.start = 0;
                }
                else
                {
                    DatabaseConnect.start -= 10;
                }
            }
            else if (type.Equals("0"))
            {
                DatabaseConnect.start = end;
            }
            else
            {
                DatabaseConnect.start = 0;
            }
            string query = "";
            if (!search.Equals(""))
            {
                month = int.Parse(search.Split('-')[0]);
                year = int.Parse(search.Split('-')[1]);
                query = "SELECT (SELECT count(id) FROM communicable_allowance WHERE userid = '" + userid + "' AND month = '" + month + "' AND year = '" + year + "') as 'MAX_SIZE', id,userid,amount,nov_billing,remarks,month,year FROM communicable_allowance WHERE userid = '" + userid + "' AND month = '" + month + "' AND year = '" + year + "'";
            }
            else
            {
                query = "SELECT (SELECT count(id) FROM communicable_allowance WHERE userid = '" + userid + "') as 'MAX_SIZE', id,userid,amount,nov_billing,remarks,month,year FROM communicable_allowance WHERE userid  = '" + userid + "'";
            }
            query = query + " ORDER BY id LIMIT 10 OFFSET " + DatabaseConnect.start;

            //Create a list to store the result
            List<CellphoneAllowance> list = new List<CellphoneAllowance>();


            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    DatabaseConnect.max_size = dataReader["MAX_SIZE"].ToString();
                    if (!max_size.Equals("0"))
                    {
                        count += 1;
                        String ID = dataReader["id"].ToString();
                        String PersonnelID = dataReader["userid"].ToString();
                        String Amount = dataReader["amount"].ToString();
                        String NovBilling = dataReader["nov_billing"].ToString();
                        String Remarks = dataReader["remarks"].ToString();
                        int Month = int.Parse(dataReader["month"].ToString());
                        int Year = int.Parse(dataReader["year"].ToString());

                        CellphoneAllowance allowance = new CellphoneAllowance(ID, PersonnelID, Amount, NovBilling, Remarks, Month, Year);
                        list.Add(allowance);
                    }
                }

                //close Data Reader
                end = (start + count);
                dataReader.Close();
                DatabaseConnect.end = (DatabaseConnect.start + count);
                //close Connection
                this.CloseConnection();

                //return list to be displayed

            }
            if (count == 0)
            {
                start = 0;
                end = 0;
                max_size = "0";
            }
            return list;
        }

        public List<HazardPay> GetHazardPayList(String userid, String type, String search)
        {
            int temp_start = DatabaseConnect.start;
            int temp_end = end;
            int count = 0;
            int month = 0;
            int year = 0;
            if (!search.Equals(""))

                if (type.Equals("1"))
                {
                    if (DatabaseConnect.start - 10 <= 0)
                    {
                        DatabaseConnect.start = 0;
                    }
                    else
                    {
                        DatabaseConnect.start -= 10;
                    }
                }
                else if (type.Equals("0"))
                {
                    DatabaseConnect.start = end;
                }
                else
                {
                    DatabaseConnect.start = 0;
                }
            string query = "";
            if (!search.Equals(""))
            {
                month = int.Parse(search.Split('-')[0]);
                year = int.Parse(search.Split('-')[1]);
                query = "SELECT (SELECT count(id) FROM hazard_pay WHERE userid = '" + userid + "' AND month = '" + month + "' AND year = '" + year + "') as 'MAX_SIZE', id,userid,hazard_pay,hwmpc_loan,digitel_billing,mortuary,month,year,days_leave,days_oo FROM hazard_pay WHERE userid  = '" + userid + "' AND month = '" + month + "' AND year = '" + year + "'";
            }
            else
            {
                query = "SELECT (SELECT count(id) FROM hazard_pay WHERE userid = '" + userid + "') as 'MAX_SIZE', id,userid,hazard_pay,hwmpc_loan,mortuary,digitel_billing,month,year,days_leave,days_oo FROM hazard_pay WHERE userid  = '" + userid + "'";
            }
            query = query + " ORDER BY id LIMIT 10 OFFSET " + DatabaseConnect.start;

            //Create a list to store the result
            List<HazardPay> list = new List<HazardPay>();


            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    DatabaseConnect.max_size = dataReader["MAX_SIZE"].ToString();
                    if (!max_size.Equals("0"))
                    {
                        count += 1;
                        String ID = dataReader["id"].ToString();
                        String PersonnelID = dataReader["userid"].ToString();
                        String Pay = dataReader["hazard_pay"].ToString();
                        String HWMPC = dataReader["hwmpc_loan"].ToString();
                        String Mortuary = dataReader["mortuary"].ToString();
                        String DigitelBilling = dataReader["digitel_billing"].ToString();
                        int Month = int.Parse(dataReader["month"].ToString());
                        int Year = int.Parse(dataReader["year"].ToString());
                        int DaysLeave = int.Parse(dataReader["days_leave"].ToString());
                        int DaysOO = int.Parse(dataReader["days_oo"].ToString());
                        HazardPay allowance = new HazardPay(ID, PersonnelID, Pay, HWMPC, Mortuary, DigitelBilling, Month, Year, DaysLeave, DaysOO);
                        list.Add(allowance);
                    }
                }

                //close Data Reader
                end = (start + count);
                dataReader.Close();
                DatabaseConnect.end = (DatabaseConnect.start + count);
                //close Connection
                this.CloseConnection();

                //return list to be displayed

            }
            if (count == 0)
            {
                start = 0;
                end = 0;
                max_size = "0";
            }
            return list;
        }

        public List<Longevity> GetLongevityList(String userid, String type, String search)
        {
            int temp_start = DatabaseConnect.start;
            int temp_end = end;
            int count = 0;
            int month = 0;
            int year = 0;

            if (type.Equals("1"))
            {
                if (DatabaseConnect.start - 10 <= 0)
                {
                    DatabaseConnect.start = 0;
                }
                else
                {
                    DatabaseConnect.start -= 10;
                }
            }
            else if (type.Equals("0"))
            {
                DatabaseConnect.start = end;
            }
            else
            {
                DatabaseConnect.start = 0;
            }
            string query = "";
            if (!search.Equals(""))
            {
                month = int.Parse(search.Split('-')[0]);
                year = int.Parse(search.Split('-')[1]);
                query = "SELECT (SELECT count(id) FROM longevity WHERE userid = '" + userid + "' AND month = '" + month + "' AND year = '" + year + "') as 'MAX_SIZE', id,userid,salary_1,salary_2,salary_3,salary_4,salary_5,disallowance,month,year FROM longevity WHERE userid  = '" + userid + "' AND month = '" + month + "' AND year = '" + year + "'";
            }
            else
            {
                query = "SELECT (SELECT count(id) FROM longevity WHERE userid = '" + userid + "') as 'MAX_SIZE', id,userid,salary_1,salary_2,salary_3,salary_4,salary_5,disallowance,month,year FROM longevity WHERE userid  = '" + userid + "'";
            }
            query = query + " ORDER BY id LIMIT 10 OFFSET " + DatabaseConnect.start;

            //Create a list to store the result
            List<Longevity> list = new List<Longevity>();


            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    DatabaseConnect.max_size = dataReader["MAX_SIZE"].ToString();
                    if (!max_size.Equals("0"))
                    {
                        count += 1;
                        String ID = dataReader["id"].ToString();
                        String PersonnelID = dataReader["userid"].ToString();
                        String Salary1 = dataReader["salary_1"].ToString();
                        String Salary2 = dataReader["salary_2"].ToString();
                        String Salary3 = dataReader["salary_3"].ToString();
                        String Salary4 = dataReader["salary_4"].ToString();
                        String Salary5 = dataReader["salary_5"].ToString();
                        String Disallowance = dataReader["disallowance"].ToString();

                        int Month = int.Parse(dataReader["month"].ToString());
                        int Year = int.Parse(dataReader["year"].ToString());

                        Longevity longevity = new Longevity(ID, PersonnelID, Salary1, Salary2, Salary3, Salary4, Salary5, Disallowance, Month, Year);
                        list.Add(longevity);
                    }
                }

                //close Data Reader
                end = (start + count);
                dataReader.Close();
                DatabaseConnect.end = (DatabaseConnect.start + count);
                //close Connection
                this.CloseConnection();

                //return list to be displayed

            }
            if (count == 0)
            {
                start = 0;
                end = 0;
                max_size = "0";
            }
            return list;
        }

        public List<Subsistence> GetSubsistenceList(String userid, String type, String search)
        {
            int temp_start = DatabaseConnect.start;
            int temp_end = end;
            int count = 0;
            int month = 0;
            int year = 0;

            if (type.Equals("1"))
            {
                if (DatabaseConnect.start - 10 <= 0)
                {
                    DatabaseConnect.start = 0;
                }
                else
                {
                    DatabaseConnect.start -= 10;
                }
            }
            else if (type.Equals("0"))
            {
                DatabaseConnect.start = end;
            }
            else
            {
                DatabaseConnect.start = 0;
            }
            string query = "";
            if (!search.Equals(""))
            {
                month = int.Parse(search.Split('-')[0]);
                year = int.Parse(search.Split('-')[1]);
                query = "SELECT (SELECT count(id) FROM subsistence WHERE userid = '" + userid + "' AND month = '" + month + "' AND year = '" + year + "') as 'MAX_SIZE', id,userid,subsistence_allowance,laundry_allowance,absences,hwmcp,no_days,remarks,month,year FROM subsistence WHERE userid  = '" + userid + "' AND month = '" + month + "' AND year = '" + year + "'";
            }
            else
            {
                query = "SELECT (SELECT count(id) FROM subsistence WHERE userid = '" + userid + "') as 'MAX_SIZE', id,userid,subsistence_allowance,laundry_allowance,absences,hwmcp,no_days,remarks,month,year FROM subsistence WHERE userid  = '" + userid + "'";
            }
            query = query + " ORDER BY id DESC LIMIT 10 OFFSET " + DatabaseConnect.start;

            //Create a list to store the result
            List<Subsistence> list = new List<Subsistence>();


            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    DatabaseConnect.max_size = dataReader["MAX_SIZE"].ToString();
                    if (!max_size.Equals("0"))
                    {
                        count += 1;
                        String ID = dataReader["id"].ToString();
                        String PersonnelID = dataReader["userid"].ToString();
                        String SubsistenceAllowance = dataReader["subsistence_allowance"].ToString();
                        String LaundryAllowance = dataReader["laundry_allowance"].ToString();
                        String Absences = dataReader["absences"].ToString();
                        String HWMCP = dataReader["hwmcp"].ToString();
                        String Remarks = dataReader["remarks"].ToString();

                        int NoDays = int.Parse(dataReader["no_days"].ToString());
                        int Month = int.Parse(dataReader["month"].ToString());
                        int Year = int.Parse(dataReader["year"].ToString());

                        Subsistence subsistence = new Subsistence(ID, PersonnelID, SubsistenceAllowance, LaundryAllowance, Absences, HWMCP, Remarks, NoDays, Month, Year);
                        list.Add(subsistence);
                    }
                }

                //close Data Reader
                end = (start + count);
                dataReader.Close();
                DatabaseConnect.end = (DatabaseConnect.start + count);
                //close Connection
                this.CloseConnection();

                //return list to be displayed

            }
            if (count == 0)
            {
                start = 0;
                end = 0;
                max_size = "0";
            }
            return list;
        }

        public List<Rata> GetRataList(String userid, String type, String search)
        {
            int temp_start = DatabaseConnect.start;
            int temp_end = end;
            int count = 0;
            int month = 0;
            int year = 0;

            if (type.Equals("1"))
            {
                if (DatabaseConnect.start - 10 <= 0)
                {
                    DatabaseConnect.start = 0;
                }
                else
                {
                    DatabaseConnect.start -= 10;
                }
            }
            else if (type.Equals("0"))
            {
                DatabaseConnect.start = end;
            }
            else
            {
                DatabaseConnect.start = 0;
            }
            string query = "";
            if (!search.Equals(""))
            {
                month = int.Parse(search.Split('-')[0]);
                year = int.Parse(search.Split('-')[1]);
                query = "SELECT (SELECT count(id) FROM rata WHERE userid = '" + userid + "' AND month = '" + month + "' AND year = '" + year + "') as 'MAX_SIZE', id,userid,ra,ta,deduction,remarks,month,year FROM rata WHERE userid  = '" + userid + "' AND month = '" + month + "' AND year = '" + year + "'";
            }
            else
            {
                query = "SELECT (SELECT count(id) FROM rata WHERE userid = '" + userid + "') as 'MAX_SIZE', id,userid,ra,ta,deduction,remarks,month,year FROM rata WHERE userid  = '" + userid + "'";
            }
            query = query + " ORDER BY id LIMIT 10 OFFSET " + DatabaseConnect.start;

            //Create a list to store the result
            List<Rata> list = new List<Rata>();


            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    DatabaseConnect.max_size = dataReader["MAX_SIZE"].ToString();
                    if (!max_size.Equals("0"))
                    {
                        count += 1;
                        String ID = dataReader["id"].ToString();
                        String PersonnelID = dataReader["userid"].ToString();
                        String Ra = dataReader["ra"].ToString();
                        String Ta = dataReader["ta"].ToString();
                        String Deductions = dataReader["deduction"].ToString();
                        String Remarks = dataReader["remarks"].ToString();
                        int Month = int.Parse(dataReader["month"].ToString());
                        int Year = int.Parse(dataReader["year"].ToString());
                        Rata rata = new Rata(ID, PersonnelID, Ra, Ta, Deductions, Remarks, Month, Year);
                        list.Add(rata);
                    }
                }

                //close Data Reader
                end = (start + count);
                dataReader.Close();
                DatabaseConnect.end = (DatabaseConnect.start + count);
                //close Connection
                this.CloseConnection();

                //return list to be displayed

            }
            if (count == 0)
            {
                start = 0;
                end = 0;
                max_size = "0";
            }
            return list;
        }


        public List<Payroll> GetPayrollMobile(String id, String offset, String search)
        {

            string query = "";
            if (!search.Equals(""))
            {
                String start_date = search.Split(' ')[0];
                String end_date = search.Split(' ')[2];
                query = "SELECT (SELECT COUNT(p.userid) FROM payroll.payroll p LEFT JOIN pis.personal_information i ON p.userid = i.userid WHERE p.userid = '" + id + "' AND p.start_date = '" + start_date + "' AND p.end_date = '" + end_date + "') as 'MAX_SIZE' ,p.id,CAST(p.start_date as char) as 'start_date',CAST(p.end_date as char) as 'end_date',i.position,i.tin_no,p.userid,p.adjustment,p.remarks,p.absent_days,i.fname,i.lname,i.mname,i.job_status,p.working_days,p.month_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile,p.tax,p.other_adjustment FROM payroll.payroll p LEFT JOIN pis.personal_information i ON p.userid = i.userid WHERE p.userid = '" + id + "' AND p.start_date = '" + start_date + "' AND p.end_date = '" + end_date + "'";
            }
            else
            {
                query = "SELECT (SELECT COUNT(p.userid) FROM payroll.payroll p LEFT JOIN pis.personal_information i ON p.userid = i.userid WHERE p.userid = '" + id + "') as 'MAX_SIZE' ,p.id, i.position,i.tin_no,CAST(p.start_date as char) as 'start_date',cAST(p.end_date as char) as 'end_date',p.userid,p.adjustment,p.remarks,p.absent_days,i.fname,i.lname,i.mname,i.job_status,p.working_days,p.month_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile,p.tax,p.other_adjustment FROM payroll.payroll p LEFT JOIN pis.personal_information i ON p.userid = i.userid WHERE p.userid = '" + id + "'";
            }
            query = query + " ORDER BY p.id DESC LIMIT 10 OFFSET " + offset;

            //Create a list to store the result
            List<Payroll> list = new List<Payroll>();


            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    DatabaseConnect.max_size = dataReader["MAX_SIZE"].ToString();

                    String payroll_id = dataReader["id"].ToString();
                    String userid = dataReader["userid"].ToString();
                    String fname = dataReader["fname"].ToString();
                    String lname = dataReader["lname"].ToString();
                    String mname = dataReader["mname"].ToString();
                    String emptype = dataReader["position"].ToString();
                    //String start_date = dataReader.GetDateTime(dataReader.GetOrdinal("start_date")).ToString("YYYY-MM-DD");
                    //String end_date = dataReader.GetDateTime(dataReader.GetOrdinal("end_date")).ToString("YYYY-MM-DD");
                    String start_date = dataReader["start_date"].ToString();
                    String end_date = dataReader["end_date"].ToString();
                    String tin = dataReader["tin_no"].ToString();
                    String flag = "1";

                    String minutes_late = dataReader["minutes_late"].ToString();
                    if (minutes_late.Equals("") || minutes_late.Equals("NULL"))
                    {
                        minutes_late = "0";
                    }
                    String working_days = dataReader["working_days"].ToString();
                    if (working_days.Equals("") || working_days.Equals("NULL"))
                    {
                        working_days = "0";
                    }
                    String monthly_salary = dataReader["month_salary"].ToString();
                    if (monthly_salary.Equals("") || monthly_salary.Equals("NULL") || monthly_salary.Equals("Null") || monthly_salary.Equals(null))
                    {
                        flag = "0";
                        monthly_salary = "0.00";
                    }
                    String adjustment = dataReader["adjustment"].ToString();
                    if (adjustment.Equals("") || adjustment.Equals("NULL"))
                    {
                        adjustment = "0.00";
                    }
                    String remarks = dataReader["remarks"].ToString();
                    if (remarks.Equals("") || remarks.Equals("NULL"))
                    {
                        remarks = "";
                    }
                    String absent_days = dataReader["absent_days"].ToString();
                    if (absent_days.Equals("") || absent_days.Equals("NULL"))
                    {
                        absent_days = "";
                    }
                    String coop = dataReader["coop"].ToString();
                    if (coop.Equals("") || coop.Equals("NULL"))
                    {
                        coop = "0.00";
                    }
                    String phic = dataReader["phic"].ToString();
                    if (phic.Equals("") || phic.Equals("NULL"))
                    {
                        phic = "0.00";
                    }
                    String disallowance = dataReader["disallowance"].ToString();
                    if (disallowance.Equals("") || disallowance.Equals("NULL"))
                    {
                        disallowance = "0.00";
                    }
                    String gsis = dataReader["gsis"].ToString();
                    if (gsis.Equals("") || gsis.Equals("NULL"))
                    {
                        gsis = "0.00";
                    }
                    String pagibig = dataReader["pagibig"].ToString();
                    if (pagibig.Equals("") || pagibig.Equals("NULL"))
                    {
                        pagibig = "0.00";
                    }
                    String excess_mobile = dataReader["excess_mobile"].ToString();
                    if (excess_mobile.Equals("") || excess_mobile.Equals("NULL"))
                    {
                        excess_mobile = "0.00";
                    }
                    String tax = dataReader["tax"].ToString();
                    if (tax.Equals("") || tax.Equals("NULL"))
                    {
                        tax = "0.00";
                    }

                    String other_adjustment = dataReader["other_adjustment"].ToString();
                    if (other_adjustment.Equals("") || other_adjustment.Equals("NULL"))
                    {
                        other_adjustment = "0.00";
                    }


                    Employee employee = new Employee(userid, fname, lname, mname, emptype, tin, "", "", "", "", "", "", "", "");

                    Payroll payroll = new Payroll(payroll_id, employee, start_date, end_date, adjustment, working_days, absent_days, monthly_salary, minutes_late, coop, phic
                            , disallowance, gsis, pagibig, excess_mobile, remarks, flag, tax, other_adjustment);


                    list.Add(payroll);
                }

                //close Data Reader

                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed

            }
            return list;
        }

        public List<Payroll> GetPayroll(String id, String type, String search)
        {
            int temp_start = DatabaseConnect.start;
            int temp_end = end;
            int count = 0;
            if (type.Equals("1"))
            {
                if (DatabaseConnect.start - 10 <= 0)
                {
                    DatabaseConnect.start = 0;
                }
                else
                {
                    DatabaseConnect.start -= 10;
                }
            }
            else if (type.Equals("0"))
            {
                DatabaseConnect.start = end;
            }
            else
            {
                DatabaseConnect.start = 0;
            }

            string query = "";
            if (!search.Equals(""))
            {
                String start_date = search.Split(' ')[0];
                String end_date = search.Split(' ')[2];
                query = "SELECT (SELECT COUNT(p.userid) FROM payroll.payroll p LEFT JOIN pis.personal_information i ON p.userid = i.userid WHERE p.userid = '" + id + "' AND p.start_date = '" + start_date + "' AND p.end_date = '" + end_date + "') as 'MAX_SIZE' ,p.id,CAST(p.start_date as char) as 'start_date',CAST(p.end_date as char) as 'end_date',i.position,i.tin_no,p.userid,p.adjustment,p.remarks,p.absent_days,i.fname,i.lname,i.mname,i.job_status,p.working_days,p.month_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile,p.tax,p.other_adjustment FROM payroll.payroll p LEFT JOIN pis.personal_information i ON p.userid = i.userid WHERE p.userid = '" + id + "' AND p.start_date = '" + start_date + "' AND p.end_date = '" + end_date + "'";
            }
            else
            {
                query = "SELECT (SELECT COUNT(p.userid) FROM payroll.payroll p LEFT JOIN pis.personal_information i ON p.userid = i.userid WHERE p.userid = '" + id + "') as 'MAX_SIZE' ,p.id, i.position,i.tin_no,CAST(p.start_date as char) as 'start_date',cAST(p.end_date as char) as 'end_date',p.userid,p.adjustment,p.remarks,p.absent_days,i.fname,i.lname,i.mname,i.job_status,p.working_days,p.month_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile,p.tax,p.other_adjustment FROM payroll.payroll p LEFT JOIN pis.personal_information i ON p.userid = i.userid WHERE p.userid = '" + id + "'";
            }
            query = query + " ORDER BY p.id DESC LIMIT 10 OFFSET " + DatabaseConnect.start;

            //Create a list to store the result
            List<Payroll> list = new List<Payroll>();


            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    DatabaseConnect.max_size = dataReader["MAX_SIZE"].ToString();
                    count += 1;

                    String payroll_id = dataReader["id"].ToString();
                    String userid = dataReader["userid"].ToString();
                    String fname = dataReader["fname"].ToString();
                    String lname = dataReader["lname"].ToString();
                    String mname = dataReader["mname"].ToString();
                    String emptype = dataReader["position"].ToString();
                    //String start_date = dataReader.GetDateTime(dataReader.GetOrdinal("start_date")).ToString("YYYY-MM-DD");
                    //String end_date = dataReader.GetDateTime(dataReader.GetOrdinal("end_date")).ToString("YYYY-MM-DD");
                    String start_date = dataReader["start_date"].ToString();
                    String end_date = dataReader["end_date"].ToString();
                    String tin = dataReader["tin_no"].ToString();
                    String flag = "1";

                    String minutes_late = dataReader["minutes_late"].ToString();
                    if (minutes_late.Equals("") || minutes_late.Equals("NULL"))
                    {
                        minutes_late = "0";
                    }
                    String working_days = dataReader["working_days"].ToString();
                    if (working_days.Equals("") || working_days.Equals("NULL"))
                    {
                        working_days = "0";
                    }
                    String monthly_salary = dataReader["month_salary"].ToString();
                    if (monthly_salary.Equals("") || monthly_salary.Equals("NULL") || monthly_salary.Equals("Null") || monthly_salary.Equals(null))
                    {
                        flag = "0";
                        monthly_salary = "0.00";
                    }
                    String adjustment = dataReader["adjustment"].ToString();
                    if (adjustment.Equals("") || adjustment.Equals("NULL"))
                    {
                        adjustment = "0.00";
                    }
                    String remarks = dataReader["remarks"].ToString();
                    if (remarks.Equals("") || remarks.Equals("NULL"))
                    {
                        remarks = "";
                    }
                    String absent_days = dataReader["absent_days"].ToString();
                    if (absent_days.Equals("") || absent_days.Equals("NULL"))
                    {
                        absent_days = "";
                    }
                    String coop = dataReader["coop"].ToString();
                    if (coop.Equals("") || coop.Equals("NULL"))
                    {
                        coop = "0.00";
                    }
                    String phic = dataReader["phic"].ToString();
                    if (phic.Equals("") || phic.Equals("NULL"))
                    {
                        phic = "0.00";
                    }
                    String disallowance = dataReader["disallowance"].ToString();
                    if (disallowance.Equals("") || disallowance.Equals("NULL"))
                    {
                        disallowance = "0.00";
                    }
                    String gsis = dataReader["gsis"].ToString();
                    if (gsis.Equals("") || gsis.Equals("NULL"))
                    {
                        gsis = "0.00";
                    }
                    String pagibig = dataReader["pagibig"].ToString();
                    if (pagibig.Equals("") || pagibig.Equals("NULL"))
                    {
                        pagibig = "0.00";
                    }
                    String excess_mobile = dataReader["excess_mobile"].ToString();
                    if (excess_mobile.Equals("") || excess_mobile.Equals("NULL"))
                    {
                        excess_mobile = "0.00";
                    }
                    String tax = dataReader["tax"].ToString();
                    if (tax.Equals("") || tax.Equals("NULL"))
                    {
                        tax = "0.00";
                    }

                    String other_adjustment = dataReader["other_adjustment"].ToString();
                    if (other_adjustment.Equals("") || other_adjustment.Equals("NULL"))
                    {
                        other_adjustment = "0.00";
                    }


                    Employee employee = new Employee(userid, fname, lname, mname, emptype, tin, "", "", "", "", "", "", "", "");

                    Payroll payroll = new Payroll(payroll_id, employee, start_date, end_date, adjustment, working_days, absent_days, monthly_salary, minutes_late, coop, phic
                            , disallowance, gsis, pagibig, excess_mobile, remarks, flag, tax, other_adjustment);


                    list.Add(payroll);
                }

                //close Data Reader
                end = (start + count);
                dataReader.Close();
                DatabaseConnect.end = (DatabaseConnect.start + count);
                //close Connection
                this.CloseConnection();

                //return list to be displayed

            }
            if (count == 0)
            {
                start = 0;
                end = 0;
                max_size = "0";
            }
            return list;
        }

        public List<PayrollRegular> GetPayrollRegular(String id, String type, String search)
        {
            int temp_start = DatabaseConnect.start;
            int temp_end = end;
            int count = 0;
            if (type.Equals("1"))
            {
                if (DatabaseConnect.start - 10 <= 0)
                {
                    DatabaseConnect.start = 0;
                }
                else
                {
                    DatabaseConnect.start -= 10;
                }
            }
            else if (type.Equals("0"))
            {
                DatabaseConnect.start = end;
            }
            else
            {
                DatabaseConnect.start = 0;
            }

            string query = "";
            if (!search.Equals(""))
            {
                int month = int.Parse(search.Split('-')[0]);
                int year = int.Parse(search.Split('-')[1]);
                query = "SELECT (SELECT COUNT(p.userid) FROM payroll.regular_payroll p LEFT JOIN pis.personal_information i ON p.userid = i.userid WHERE p.userid = '" + id + "' AND p.month = '" + month + "' AND p.year = '" + year + "') as 'MAX_SIZE' ,p.id,p.month,p.year,i.position,i.tin_no,p.userid,p.absent_days,p.working_days,p.month_salary,p.pera,p.minutes_late,p.tax,p.cfi,p.gsis_premium,p.gsis_consoloan,p.gsis_policy_loan,p.gsis_eml,p.gsis_uoli,p.gsis_edu,p.gsis_help,p.gsis_rel,p.pagibig_premium,p.pagibig_loan,p.pagibig_mp2,p.disallowances,p.philhealth,p.simc,p.hwmpc,p.dbp,i.fname,i.lname,i.mname,i.job_status FROM payroll.regular_payroll p LEFT JOIN pis.personal_information i ON p.userid = i.userid WHERE p.userid = '" + id + "' AND p.month = '" + month + "' AND p.year = '" + year + "'";
            }
            else
            {
                query = "SELECT (SELECT COUNT(p.userid) FROM payroll.regular_payroll p LEFT JOIN pis.personal_information i ON p.userid = i.userid WHERE p.userid = '" + id + "') as 'MAX_SIZE' ,p.id,p.month,p.year,i.position,i.tin_no,p.userid,p.absent_days,p.working_days,p.month_salary,p.pera,p.minutes_late,p.tax,p.cfi,p.gsis_premium,p.gsis_consoloan,p.gsis_policy_loan,p.gsis_eml,p.gsis_uoli,p.gsis_edu,p.gsis_help,p.gsis_rel,p.pagibig_premium,p.pagibig_loan,p.pagibig_mp2,p.disallowances,p.philhealth,p.simc,p.hwmpc,p.dbp,i.fname,i.lname,i.mname,i.job_status FROM payroll.regular_payroll p LEFT JOIN pis.personal_information i ON p.userid = i.userid WHERE p.userid = '" + id + "'";
            }
            query = query + " ORDER BY p.id DESC LIMIT 10 OFFSET " + DatabaseConnect.start;

            //Create a list to store the result
            List<PayrollRegular> list = new List<PayrollRegular>();


            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    DatabaseConnect.max_size = dataReader["MAX_SIZE"].ToString();
                    count += 1;

                    String payroll_id = dataReader["id"].ToString();
                    String userid = dataReader["userid"].ToString();
                    String fname = dataReader["fname"].ToString();
                    String lname = dataReader["lname"].ToString();
                    String mname = dataReader["mname"].ToString();
                    String emptype = dataReader["position"].ToString();
                    //String start_date = dataReader.GetDateTime(dataReader.GetOrdinal("start_date")).ToString("YYYY-MM-DD");
                    //String end_date = dataReader.GetDateTime(dataReader.GetOrdinal("end_date")).ToString("YYYY-MM-DD");
                    String month = dataReader["month"].ToString();
                    String year = dataReader["year"].ToString();
                    String tin = dataReader["tin_no"].ToString();
                    String days_absent = dataReader["absent_days"].ToString();
                    String working_days = dataReader["working_days"].ToString();
                    String salary = dataReader["month_salary"].ToString();
                    String pera = dataReader["pera"].ToString();
                    String minutes_late = dataReader["minutes_late"].ToString();
                    String tax = dataReader["tax"].ToString();
                    String cfi = dataReader["cfi"].ToString();
                    String gsis_premium = dataReader["gsis_premium"].ToString();
                    String gsis_consoloan = dataReader["gsis_consoloan"].ToString();
                    String gsis_policy_loan = dataReader["gsis_policy_loan"].ToString();
                    String gsis_eml = dataReader["gsis_eml"].ToString();
                    String gsis_uoli = dataReader["gsis_uoli"].ToString();
                    String gsis_edu = dataReader["gsis_edu"].ToString();
                    String gsis_help = dataReader["gsis_help"].ToString();
                    String gsis_rel = dataReader["gsis_rel"].ToString();
                    String pagibig_premium = dataReader["pagibig_premium"].ToString();
                    String pagibig_loan = dataReader["pagibig_loan"].ToString();
                    String pagibig_mp2 = dataReader["pagibig_mp2"].ToString();
                    String disallowances = dataReader["disallowances"].ToString();
                    String philhealth = dataReader["philhealth"].ToString();
                    String simc = dataReader["simc"].ToString();
                    String hwmpc = dataReader["hwmpc"].ToString();
                    String dbp = dataReader["dbp"].ToString();


                    Employee employee = new Employee(userid, fname, lname, mname, emptype, tin, "", "", "", "", "", "", "", "");

                    PayrollRegular payroll = new PayrollRegular(payroll_id, employee, int.Parse(month), int.Parse(year), days_absent, working_days, salary, pera, int.Parse(minutes_late), tax, cfi, gsis_premium,
                            gsis_consoloan, gsis_policy_loan, gsis_eml, gsis_uoli, gsis_edu, gsis_help, gsis_rel, pagibig_premium, pagibig_loan, disallowances, philhealth, simc, hwmpc, dbp, pagibig_mp2);


                    list.Add(payroll);
                }

                //close Data Reader
                end = (start + count);
                dataReader.Close();
                DatabaseConnect.end = (DatabaseConnect.start + count);
                //close Connection
                this.CloseConnection();

                //return list to be displayed

            }
            if (count == 0)
            {
                start = 0;
                end = 0;
                max_size = "0";
            }
            return list;
        }

        public List<Payroll> GeneratePayroll(string start_date, string end_date, string selection,
                                             string disbursment, string sectionID, string custom)
        {
            List<Payroll> payroll = new List<Payroll>();
            string query = "SELECT i.designation_id,d.id,p.absent_days,p.adjustment,p.remarks,d.description,i.userid,i.mname,i.fname,i.lname,i.position,i.salary_charge,i.tin_no,i.account_number,p.working_days,p.month_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile,p.tax,p.other_adjustment FROM payroll.payroll p LEFT JOIN (pis.personal_information i LEFT JOIN dts.section d ON i.section_id = d.id) ON p.userid = i.userid WHERE p.start_date = '" + start_date + "' AND p.end_date = '" + end_date + "' AND i.disbursement_type = '" + disbursment + "'";
            switch (selection)
            {
                case "2":
                    query = query + " AND pagibig <> '0.00'";
                    break;
                case "3":
                    query = query + " AND coop <> '0.00'";
                    break;
                case "4":
                    query = query + " AND phic <> '0.00'";
                    break;
                case "5":
                    query = query + " AND gsis <> '0.00'";
                    break;
                case "6":
                    query = query + " AND excess_mobile <> '0.00'";
                    break;
            }
            if (!sectionID.Equals("ALL"))
            {
                query = query + " AND i.salary_charge = '" + sectionID + "'";
            }
            if (custom.Equals("custom"))
            {
                query = query + " ORDER BY i.lname,i.fname ASC";
            }
            else if (custom.Equals("non_custom"))
            {
                query = query + " ORDER BY i.salary_charge,i.lname,i.fname,d.description ASC";
            }
            else
            {
                query = query + " ORDER BY i.lname,i.fname ASC";
            }

            if (this.OpenConnection() == true)
            {
                //Create Command

                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    string tin = dataReader["tin_no"].ToString().ToUpper();
                    string account_number = dataReader["account_number"].ToString().ToUpper();
                    string desc = dataReader["salary_charge"].ToString().ToUpper();
                    string userid = dataReader["userid"].ToString().ToUpper();
                    string fname = dataReader["fname"].ToString().ToUpper();
                    string mname = dataReader["mname"].ToString().ToUpper();
                    string lname = dataReader["lname"].ToString().ToUpper();
                    string designation_id = dataReader["designation_id"].ToString().ToUpper();
                    string emptype = GetDesignationName(designation_id).ToUpper();

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
                    Employee employee = new Employee(userid, fname, lname, mname, emptype, tin, desc, "", "", "", "", "", "", account_number);
                    Payroll roll = new Payroll("0", employee, "", "", adjustment, working_days, absent_days
                            , monthly_salary, minutes_late, coop, phic, disallowance, gsis, pagibig, excess_mobile, remarks, "", tax, other_adjustment);
                    payroll.Add(roll);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed

            }
            return payroll;
        }

        public Payroll GeneratePayslip(String id, String start_date, String end_date)
        {
            Payroll payroll = null;
            string query = "SELECT p.absent_days,p.adjustment,p.remarks,s.description,i.userid,i.mname,i.fname,i.lname,i.disbursement_type,i.salary_charge,i.position,i.tin_no,p.working_days,p.month_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile,p.tax,p.other_adjustment FROM payroll.payroll p LEFT JOIN (pis.personal_information i LEFT JOIN dts.section s ON i.section_id = s.id) ON p.userid = i.userid WHERE p.userid = '" + id + "'";
            if (!start_date.Equals("") && !end_date.Equals(""))
            {
                query = query + " AND p.start_date = '" + start_date + "' AND p.end_date = '" + end_date + "'";
            }
            query = query + " ORDER BY s.description,i.fname,i.lname ASC LIMIT 1";

            if (this.OpenConnection() == true)
            {
                try
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        String tin = dataReader["tin_no"].ToString().ToUpper();
                        String desc = dataReader["description"].ToString().ToUpper();
                        String userid = dataReader["userid"].ToString().ToUpper();
                        String fname = dataReader["fname"].ToString().ToUpper();
                        String mname = dataReader["mname"].ToString().ToUpper();
                        String lname = dataReader["lname"].ToString().ToUpper();
                        String emptype = dataReader["position"].ToString().ToUpper();

                        String minutes_late = dataReader["minutes_late"].ToString();
                        if (minutes_late.Equals("") || minutes_late.Equals("NULL"))
                        {
                            minutes_late = "0";
                        }
                        String working_days = dataReader["working_days"].ToString();
                        if (working_days.Equals("") || working_days.Equals("NULL"))
                        {
                            working_days = "0";
                        }
                        String monthly_salary = dataReader["month_salary"].ToString();
                        if (monthly_salary.Equals("") || monthly_salary.Equals("NULL") || monthly_salary.Equals("Null") || monthly_salary.Equals(null))
                        {
                            monthly_salary = "0";
                        }
                        String coop = dataReader["coop"].ToString();
                        if (coop.Equals("") || coop.Equals("NULL"))
                        {
                            coop = "0";
                        }
                        String adjustment = dataReader["adjustment"].ToString();
                        if (adjustment.Equals("") || adjustment.Equals("NULL"))
                        {
                            adjustment = "0.00";
                        }
                        String remarks = dataReader["remarks"].ToString();
                        if (remarks.Equals("") || remarks.Equals("NULL"))
                        {
                            remarks = "";
                        }
                        String absent_days = dataReader["absent_days"].ToString();
                        if (absent_days.Equals("") || absent_days.Equals("NULL"))
                        {
                            absent_days = "";
                        }
                        String phic = dataReader["phic"].ToString();
                        if (phic.Equals("") || phic.Equals("NULL"))
                        {
                            phic = "0";
                        }
                        String disallowance = dataReader["disallowance"].ToString();
                        if (disallowance.Equals("") || disallowance.Equals("NULL"))
                        {
                            disallowance = "0";
                        }
                        String gsis = dataReader["gsis"].ToString();
                        if (gsis.Equals("") || gsis.Equals("NULL"))
                        {
                            gsis = "0";
                        }
                        String pagibig = dataReader["pagibig"].ToString();
                        if (pagibig.Equals("") || pagibig.Equals("NULL"))
                        {
                            pagibig = "0";
                        }
                        String excess_mobile = dataReader["excess_mobile"].ToString();
                        if (excess_mobile.Equals("") || excess_mobile.Equals("NULL"))
                        {
                            excess_mobile = "0";
                        }
                        String disbursement = dataReader["disbursement_type"].ToString();
                        if (disbursement.Equals("") || disbursement.Equals("NULL"))
                        {
                            disbursement = "";
                        }
                        String divisionID = dataReader["salary_charge"].ToString();
                        if (divisionID.Equals("") || divisionID.Equals("NULL"))
                        {
                            divisionID = "0";
                        }
                        String tax = dataReader["tax"].ToString();
                        if (tax.Equals("") || tax.Equals("NULL"))
                        {
                            tax = "0.00";
                        }

                        String other_adjustment = dataReader["other_adjustment"].ToString();
                        if (other_adjustment.Equals("") || other_adjustment.Equals("NULL"))
                        {
                            other_adjustment = "0.00";
                        }
                        Employee employee = new Employee(userid, fname, lname, mname, emptype, tin, desc, disbursement, divisionID, "", "", "", "", "");
                        payroll = new Payroll("0", employee, start_date, end_date, adjustment, working_days, absent_days
                                , monthly_salary, minutes_late, coop, phic, disallowance, gsis, pagibig, excess_mobile, remarks, "", tax, other_adjustment);

                    }

                    //close Data Reader
                    dataReader.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                }
                catch (Exception e)
                {

                }


            }
            return payroll;
        }

        public PayrollRegular GeneratePayslipRegular(String mId, String mMonth, String mYear)
        {
            PayrollRegular payrollRegular = null;
            string query = "SELECT id,userid,month,year,absent_days,working_days,month_salary,dbp,pera,minutes_late,tax,cfi,gsis_premium,gsis_consoloan,gsis_policy_loan,gsis_eml,gsis_uoli,gsis_edu,gsis_help,gsis_rel,pagibig_premium,pagibig_loan,pagibig_mp2,disallowances,philhealth,simc,hwmpc FROM regular_payroll WHERE userid = '" + mId + "'";

            if (this.OpenConnection() == true)
            {
                try
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        String id = dataReader["id"].ToString();
                        String userid = dataReader["userid"].ToString();
                        String month = dataReader["month"].ToString();
                        String year = dataReader["year"].ToString();
                        String absent_days = dataReader["absent_days"].ToString();
                        String working_days = dataReader["working_days"].ToString();
                        String month_salary = dataReader["month_salary"].ToString();
                        String pera = dataReader["pera"].ToString();
                        String minutes_late = dataReader["minutes_late"].ToString();
                        String tax = dataReader["tax"].ToString();
                        String cfi = dataReader["cfi"].ToString();
                        String gsis_premium = dataReader["gsis_premium"].ToString();
                        String gsis_consoloan = dataReader["gsis_consoloan"].ToString();
                        String gsis_policy_loan = dataReader["gsis_policy_loan"].ToString();
                        String gsis_eml = dataReader["gsis_eml"].ToString();
                        String gsis_uoli = dataReader["gsis_uoli"].ToString();
                        String gsis_edu = dataReader["gsis_edu"].ToString();
                        String gsis_help = dataReader["gsis_help"].ToString();
                        String gsis_rel = dataReader["gsis_rel"].ToString();
                        String pagibig_premium = dataReader["pagibig_premium"].ToString();
                        String pagibig_loan = dataReader["pagibig_loan"].ToString();
                        String pagibig_mp2 = dataReader["pagibig_mp2"].ToString();
                        String disallowances = dataReader["disallowances"].ToString();
                        String philhealth = dataReader["philhealth"].ToString();
                        String simc = dataReader["simc"].ToString();
                        String hwmpc = dataReader["hwmpc"].ToString();
                        String dbp = dataReader["dbp"].ToString();

                        payrollRegular = new PayrollRegular(id, null, int.Parse(month), int.Parse(year), absent_days, working_days, month_salary, pera, int.Parse(minutes_late),
                                tax, cfi, gsis_premium, gsis_consoloan, gsis_policy_loan, gsis_eml, gsis_uoli, gsis_edu, gsis_help, gsis_rel, pagibig_premium, pagibig_loan, disallowances, philhealth, simc, hwmpc, dbp, pagibig_mp2);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                }
                catch (Exception e)
                {

                }


            }
            return payrollRegular;
        }

        public List<PayrollRegular> GenerateSummaryRegularByDivision(String mMonth, String mYear, String mDivision)
        {
            List<PayrollRegular> list = new List<PayrollRegular>();
            string query = "SELECT p.id,p.userid,p.month,p.year,p.absent_days,p.working_days,p.month_salary,p.pera,p.minutes_late,p.tax,p.cfi,p.gsis_premium,p.gsis_consoloan,p.gsis_policy_loan,p.gsis_eml,p.gsis_uoli,p.gsis_edu,p.gsis_help,p.gsis_rel,p.pagibig_premium,p.pagibig_loan,p.pagibig_mp2,p.disallowances,p.philhealth,p.simc,p.hwmpc,p.dbp FROM payroll.regular_payroll p LEFT JOIN pis.personal_information i ON p.userid = i.userid WHERE p.month = '" + mMonth + "' AND p.year = '" + mYear + "'";
            if (!mDivision.Equals("ALL"))
            {
                query = query + " AND i.division_id = @division_id";
            }
            query = query + " ORDER BY i.lname,i.fname ASC";
            if (this.OpenConnection() == true)
            {
                try
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    cmd.Parameters.AddWithValue("@division_id", mDivision);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        String id = dataReader["id"].ToString();
                        String userid = dataReader["userid"].ToString();
                        String month = dataReader["month"].ToString();
                        String year = dataReader["year"].ToString();
                        String absent_days = dataReader["absent_days"].ToString();
                        String working_days = dataReader["working_days"].ToString();
                        String month_salary = dataReader["month_salary"].ToString();
                        String pera = dataReader["pera"].ToString();
                        String minutes_late = dataReader["minutes_late"].ToString();
                        String tax = dataReader["tax"].ToString();
                        String cfi = dataReader["cfi"].ToString();
                        String gsis_premium = dataReader["gsis_premium"].ToString();
                        String gsis_consoloan = dataReader["gsis_consoloan"].ToString();
                        String gsis_policy_loan = dataReader["gsis_policy_loan"].ToString();
                        String gsis_eml = dataReader["gsis_eml"].ToString();
                        String gsis_uoli = dataReader["gsis_uoli"].ToString();
                        String gsis_edu = dataReader["gsis_edu"].ToString();
                        String gsis_help = dataReader["gsis_help"].ToString();
                        String gsis_rel = dataReader["gsis_rel"].ToString();
                        String pagibig_premium = dataReader["pagibig_premium"].ToString();
                        String pagibig_loan = dataReader["pagibig_loan"].ToString();
                        String pagibig_mp2 = dataReader["pagibig_mp2"].ToString();
                        String disallowances = dataReader["disallowances"].ToString();
                        String philhealth = dataReader["philhealth"].ToString();
                        String simc = dataReader["simc"].ToString();
                        String hwmpc = dataReader["hwmpc"].ToString();
                        String dbp = dataReader["dbp"].ToString();
                        //198900036
                        Employee employee = GetEmployeeByID(userid);

                        PayrollRegular payrollRegular = new PayrollRegular(id, employee, int.Parse(month), int.Parse(year), absent_days, working_days, month_salary, pera, int.Parse(minutes_late),
                                tax, cfi, gsis_premium, gsis_consoloan, gsis_policy_loan, gsis_eml, gsis_uoli, gsis_edu, gsis_help, gsis_rel, pagibig_premium, pagibig_loan, disallowances, philhealth, simc, hwmpc, dbp, pagibig_mp2);
                        list.Add(payrollRegular);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                }
                catch (Exception e)
                {

                }


            }
            return list;
        }

        public List<PayrollRegular> GenerateSummaryRegular(String mMonth, String mYear, String mSection)
        {
            List<PayrollRegular> list = new List<PayrollRegular>();
            string query = "SELECT p.id,p.userid,p.month,p.year,p.absent_days,p.working_days,p.month_salary,p.pera,p.minutes_late,p.tax,p.cfi,p.gsis_premium,p.gsis_consoloan,p.gsis_policy_loan,p.gsis_eml,p.gsis_uoli,p.gsis_edu,p.gsis_help,p.gsis_rel,p.pagibig_premium,p.pagibig_loan,p.pagibig_mp2,p.disallowances,p.philhealth,p.simc,p.hwmpc,p.dbp FROM payroll.regular_payroll p LEFT JOIN pis.personal_information i ON p.userid = i.userid WHERE p.month = '" + mMonth + "' AND p.year = '" + mYear + "'";
            if (!mSection.Equals("ALL"))
            {
                query = query + " AND i.salary_charge = @salary_charge";
            }
            query = query + " ORDER BY i.lname,i.fname ASC";
            if (this.OpenConnection() == true)
            {
                try
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    cmd.Parameters.AddWithValue("@salary_charge", mSection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        String id = dataReader["id"].ToString();
                        String userid = dataReader["userid"].ToString();
                        String month = dataReader["month"].ToString();
                        String year = dataReader["year"].ToString();
                        String absent_days = dataReader["absent_days"].ToString();
                        String working_days = dataReader["working_days"].ToString();
                        String month_salary = dataReader["month_salary"].ToString();
                        String pera = dataReader["pera"].ToString();
                        String minutes_late = dataReader["minutes_late"].ToString();
                        String tax = dataReader["tax"].ToString();
                        String cfi = dataReader["cfi"].ToString();
                        String gsis_premium = dataReader["gsis_premium"].ToString();
                        String gsis_consoloan = dataReader["gsis_consoloan"].ToString();
                        String gsis_policy_loan = dataReader["gsis_policy_loan"].ToString();
                        String gsis_eml = dataReader["gsis_eml"].ToString();
                        String gsis_uoli = dataReader["gsis_uoli"].ToString();
                        String gsis_edu = dataReader["gsis_edu"].ToString();
                        String gsis_help = dataReader["gsis_help"].ToString();
                        String gsis_rel = dataReader["gsis_rel"].ToString();
                        String pagibig_premium = dataReader["pagibig_premium"].ToString();
                        String pagibig_loan = dataReader["pagibig_loan"].ToString();
                        String pagibig_mp2 = dataReader["pagibig_mp2"].ToString();
                        String disallowances = dataReader["disallowances"].ToString();
                        String philhealth = dataReader["philhealth"].ToString();
                        String simc = dataReader["simc"].ToString();
                        String hwmpc = dataReader["hwmpc"].ToString();
                        String dbp = dataReader["dbp"].ToString();
                        //198900036
                        Employee employee = GetEmployeeByID(userid);

                        PayrollRegular payrollRegular = new PayrollRegular(id, employee, int.Parse(month), int.Parse(year), absent_days, working_days, month_salary, pera, int.Parse(minutes_late),
                                tax, cfi, gsis_premium, gsis_consoloan, gsis_policy_loan, gsis_eml, gsis_uoli, gsis_edu, gsis_help, gsis_rel, pagibig_premium, pagibig_loan, disallowances, philhealth, simc, hwmpc, dbp, pagibig_mp2);
                        list.Add(payrollRegular);
                    }

                    //close Data Reader
                    dataReader.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                }
                catch (Exception e)
                {

                }


            }
            return list;
        }

        public Rata GenerateRata(String mId, String mMonth, String mYear)
        {

            string query = "SELECT id,userid,ra,ta,deduction,remarks,month,year FROM rata WHERE userid  = '" + mId + "' AND month = '" + mMonth + "' AND year = '" + mYear + "'";


            Rata rata = null;
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {

                    String ID = dataReader["id"].ToString();
                    String PersonnelID = dataReader["userid"].ToString();
                    String Ra = dataReader["ra"].ToString();
                    String Ta = dataReader["ta"].ToString();
                    String Deductions = dataReader["deduction"].ToString();
                    String Remarks = dataReader["remarks"].ToString();
                    int Month = int.Parse(dataReader["month"].ToString());
                    int Year = int.Parse(dataReader["year"].ToString());
                    rata = new Rata(ID, PersonnelID, Ra, Ta, Deductions, Remarks, Month, Year);
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return rata;
        }

        public HazardPay GenerateHazard(String mId, String mMonth, String mYear)
        {

            string query = "SELECT id,userid,hazard_pay,hwmpc_loan,digitel_billing,mortuary,month,year,days_leave,days_oo FROM hazard_pay WHERE userid  = '" + mId + "' AND month = '" + mMonth + "' AND year = '" + mYear + "'";
            HazardPay allowance = null;
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {

                    String ID = dataReader["id"].ToString();
                    String PersonnelID = dataReader["userid"].ToString();
                    String Pay = dataReader["hazard_pay"].ToString();
                    String HWMPC = dataReader["hwmpc_loan"].ToString();
                    String Mortuary = dataReader["mortuary"].ToString();
                    String DigitelBilling = dataReader["digitel_billing"].ToString();
                    int Month = int.Parse(dataReader["month"].ToString());
                    int Year = int.Parse(dataReader["year"].ToString());
                    int DaysLeave = int.Parse(dataReader["days_leave"].ToString());
                    int DaysOO = int.Parse(dataReader["days_oo"].ToString());
                    allowance = new HazardPay(ID, PersonnelID, Pay, HWMPC, Mortuary, DigitelBilling, Month, Year, DaysLeave, DaysOO);
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return allowance;
        }

        public CellphoneAllowance GenerateCommunicable(String mId, String mMonth, String mYear)
        {

            string query = "SELECT id,userid,amount,nov_billing,remarks,month,year FROM communicable_allowance WHERE userid = '" + mId + "' AND month = '" + mMonth + "' AND year = '" + mYear + "'";


            //Create a list to store the result
            CellphoneAllowance allowance = null;

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {

                    String ID = dataReader["id"].ToString();
                    String PersonnelID = dataReader["userid"].ToString();
                    String Amount = dataReader["amount"].ToString();
                    String NovBilling = dataReader["nov_billing"].ToString();
                    String Remarks = dataReader["remarks"].ToString();
                    int Month = int.Parse(dataReader["month"].ToString());
                    int Year = int.Parse(dataReader["year"].ToString());

                    allowance = new CellphoneAllowance(ID, PersonnelID, Amount, NovBilling, Remarks, Month, Year);
                }

                dataReader.Close();
                this.CloseConnection();

                //return list to be displayed
            }
            return allowance;
        }

        public Longevity GenerateLongevity(String mId, String mMonth, String mYear)
        {

            string query = "SELECT id,userid,salary_1,salary_2,salary_3,salary_4,salary_5,disallowance,month,year FROM longevity WHERE userid  = '" + mId + "' AND month = '" + mMonth + "' AND year = '" + mYear + "'";

            //Create a list to store the result
            Longevity longevity = null;

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {

                    String ID = dataReader["id"].ToString();
                    String PersonnelID = dataReader["userid"].ToString();
                    String Salary1 = dataReader["salary_1"].ToString();
                    String Salary2 = dataReader["salary_2"].ToString();
                    String Salary3 = dataReader["salary_3"].ToString();
                    String Salary4 = dataReader["salary_4"].ToString();
                    String Salary5 = dataReader["salary_5"].ToString();
                    String Disallowance = dataReader["disallowance"].ToString();

                    int Month = int.Parse(dataReader["month"].ToString());
                    int Year = int.Parse(dataReader["year"].ToString());

                    longevity = new Longevity(ID, PersonnelID, Salary1, Salary2, Salary3, Salary4, Salary5, Disallowance, Month, Year);

                }
                dataReader.Close();
                this.CloseConnection();

                //return list to be displayed

            }

            return longevity;
        }

        public Subsistence GenerateSubsistence(String mId, String mMonth, String mYear)
        {

            string query = "SELECT id,userid,subsistence_allowance,laundry_allowance,absences,hwmcp,no_days,remarks,month,year FROM subsistence WHERE userid  = '" + mId + "' AND month = '" + mMonth + "' AND year = '" + mYear + "'";


            //Create a list to store the result
            Subsistence subsistence = null;


            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    String ID = dataReader["id"].ToString();
                    String PersonnelID = dataReader["userid"].ToString();
                    String SubsistenceAllowance = dataReader["subsistence_allowance"].ToString();
                    String LaundryAllowance = dataReader["laundry_allowance"].ToString();
                    String Absences = dataReader["absences"].ToString();
                    String HWMCP = dataReader["hwmcp"].ToString();
                    String Remarks = dataReader["remarks"].ToString();

                    int NoDays = int.Parse(dataReader["no_days"].ToString());
                    int Month = int.Parse(dataReader["month"].ToString());
                    int Year = int.Parse(dataReader["year"].ToString());

                    subsistence = new Subsistence(ID, PersonnelID, SubsistenceAllowance, LaundryAllowance, Absences, HWMCP, Remarks, NoDays, Month, Year);
                }

                //close Data Reader

                dataReader.Close();

                this.CloseConnection();

                //return list to be displayed

            }

            return subsistence;
        }

        public Boolean IsHoliday(String date)
        {
            Boolean found = false;
            String month = (int.Parse(date.Split('/')[0]) > 9) ? date.Split('/')[0] : "0" + date.Split('/')[0];
            String day = (int.Parse(date.Split('/')[1]) > 9) ? date.Split('/')[1] : "0" + date.Split('/')[1];
            String year = date.Split('/')[2];

            String mDate = year + "-" + month + "-" + day;

            List<Employee> list = new List<Employee>();
            string query = "SELECT id FROM calendar WHERE start <= '" + mDate + "' AND end > '" + mDate + "' AND status = '1'";
            //Create Command
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, dtr);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    found = true;
                }
                dataReader.Close();
                this.CloseConnection();
            }

            //close Data Reader
            return found;
        }

        public Boolean ifWeekend(String date)
        {
            DateTime dateTime = new DateTime(int.Parse(date.Split('/')[2]), int.Parse(date.Split('/')[0]), int.Parse(date.Split('/')[1]));
            DayOfWeek datez = dateTime.DayOfWeek;
            if ((datez == DayOfWeek.Saturday) || (datez == DayOfWeek.Sunday))
            {
                return true;
            }
            return false;
        }

        public Boolean CheckCTO(String userid, String date, String time)
        {

            Boolean found = false;
            String month = (int.Parse(date.Split('/')[0]) > 9) ? date.Split('/')[0] : "0" + date.Split('/')[0];
            String day = (int.Parse(date.Split('/')[1]) > 9) ? date.Split('/')[1] : "0" + date.Split('/')[1];
            String year = date.Split('/')[2];

            String mDate = year + "-" + month + "-" + day;
            String query = "SELECT userid FROM cdo_logs WHERE userid = '" + userid + "' AND datein = '" + mDate + "' AND time = '" + time + "' LIMIT 1";
            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, dtr);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        found = true;
                    }
                    //Create a data reader and Execute the command
                    dataReader.Close();
                    //this.CloseConnection();
                }
                catch (MySqlException e)
                {
                    //this.CloseConnection();
                }

            }

            return found;
        }

        public String GetFlexTime(String schedule)
        {
            String flexTime = "08:00:00,12:00:00,13:00:00,17:00:00";
            String query = "SELECT am_in,am_out,pm_in,pm_out FROM work_sched WHERE id = '" + schedule + "' LIMIT 1";
            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, dtr);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        flexTime = dataReader["am_in"].ToString() + "," + dataReader["am_out"].ToString() + "," + dataReader["pm_in"].ToString() + "," + dataReader["pm_out"].ToString();
                    }
                    //close Data Reader
                    dataReader.Close();
                    //close Connection  
                    this.CloseConnection();
                }
                catch (MySqlException e)
                {
                    //this.CloseConnection();
                }

            }

            return flexTime;
        }

        public Boolean CheckSO(String userid, String date, String time)
        {

            Boolean found = false;
            String month = (int.Parse(date.Split('/')[0]) > 9) ? date.Split('/')[0] : "0" + date.Split('/')[0];
            String day = (int.Parse(date.Split('/')[1]) > 9) ? date.Split('/')[1] : "0" + date.Split('/')[1];
            String year = date.Split('/')[2];

            String mDate = year + "-" + month + "-" + day;
            String query = "SELECT userid FROM so_logs WHERE userid = '" + userid + "' AND datein = '" + mDate + "' AND time = '" + time + "' LIMIT 1";
            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, dtr);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        found = true;
                    }
                    //Create a data reader and Execute the command
                    dataReader.Close();
                    // this.CloseConnection();
                }
                catch (MySqlException e)
                {
                    // this.CloseConnection();
                }

            }
            return found;
        }

        public Boolean CheckLeave(String userid, String date, String time)
        {

            Boolean found = false;
            String month = (int.Parse(date.Split('/')[0]) > 9) ? date.Split('/')[0] : "0" + date.Split('/')[0];
            String day = (int.Parse(date.Split('/')[1]) > 9) ? date.Split('/')[1] : "0" + date.Split('/')[1];
            String year = date.Split('/')[2];

            String mDate = year + "-" + month + "-" + day;
            String query = "SELECT userid FROM leave_logs WHERE userid = '" + userid + "' AND datein = '" + mDate + "' AND time = '" + time + "' LIMIT 1";
            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, dtr);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        found = true;
                    }
                    //Create a data reader and Execute the command
                    dataReader.Close();
                    // this.CloseConnection();
                }
                catch (MySqlException e)
                {
                    // this.CloseConnection();
                }

            }
            return found;
        }

        public String SampleComputeLate(String am_in, String am_out, String pm_in, String pm_out)
        {
            int am_in_late = 0;
            int am_out_late = 0;
            int pm_in_late = 0;
            int pm_out_late = 0;
            //AM IN
            TimeSpan am_in_span = TimeSpan.Parse(am_in);
            TimeSpan am_in_subtrahend = TimeSpan.Parse("08:00:00");
            int am_in_second_subtrahend = (int)am_in_subtrahend.TotalSeconds;
            int am_in_second_timespan = (int)am_in_span.TotalSeconds;
            int result_am_in = (am_in_second_timespan - am_in_second_subtrahend) / 60;
            if (result_am_in > 0)
            {
                am_in_late += result_am_in;
            }
            //AM OUT
            TimeSpan am_out_span = TimeSpan.Parse(am_out);
            TimeSpan am_out_subtrahend = TimeSpan.Parse("12:00:00");
            int am_out_second_subtrahend = (int)am_out_subtrahend.TotalSeconds;
            int am_out_second_timespan = (int)am_out_span.TotalSeconds;
            int result_am_out = (am_out_second_subtrahend - am_out_second_timespan) / 60;
            if (result_am_out > 0)
            {
                am_out_late += result_am_out;
            }
            //PM IN
            TimeSpan pm_in_span = TimeSpan.Parse(pm_in);
            TimeSpan pm_in_subtrahend = TimeSpan.Parse("13:00:00");
            int pm_in_second_subtrahend = (int)pm_in_subtrahend.TotalSeconds;
            int pm_in_second_timespan = (int)pm_in_span.TotalSeconds;
            int result_pm_in = (pm_in_second_timespan - pm_in_second_subtrahend) / 60;
            if (result_pm_in > 0)
            {
                pm_in_late += result_pm_in;
            }
            // PM OUT
            TimeSpan pm_out_span = TimeSpan.Parse(pm_out);
            TimeSpan pm_out_subtrahend = TimeSpan.Parse("17:00:00");
            int pm_out_second_subtrahend = (int)pm_out_subtrahend.TotalSeconds;
            int pm_out_second_timespan = (int)pm_out_span.TotalSeconds;
            int result_pm_out = (pm_out_second_subtrahend - pm_out_second_timespan) / 60;
            if (result_pm_out > 0)
            {
                pm_out_late += result_pm_out;
            }
            return "am_in: " + am_in_late + "am_out: " + am_out_late + "pm_in: " + pm_in_late + "pm_in: " + pm_out_late;
        }


        public String GetMins(String id, String from, String to, String am_in, String am_out, String pm_in, String pm_out)
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
            String days_absent = "";


            for (int i = 0; i <= (to_days - from_days); i++)
            {
                days.Add((i + from_days));
                days_rendered++;
            }

            String query = "SELECT DISTINCT e.userid, datein,holiday,remark, (SELECT  CONCAT(t1.time, '_', t1.edited) FROM dtr_file t1 WHERE userid = d.userid and datein = d.datein and t1.time < '" + am_out + "' AND t1.event = 'IN' ORDER BY time ASC LIMIT 1) as am_in, (SELECT CONCAT(t2.time,'_',t2.edited) FROM dtr_file t2 WHERE userid = d.userid and datein = d.datein and (SELECT CONCAT(t1.time,'_',t1.edited) FROM dtr_file t1 WHERE userid = d.userid and datein = d.datein and t1.time < '" + am_out + "' AND t1.event = 'IN' ORDER BY time ASC LIMIT 1) and t2.time < '" + pm_in + "' AND t2.event = 'OUT' AND t2.time > '" + am_in + "' ORDER BY t2.time DESC LIMIT 1 ) as am_out,(SELECT CONCAT(t3.time,'_',t3.edited) FROM dtr_file t3 WHERE userid = d.userid AND datein = d.datein and t3.time > '" + am_out + "' and t3.time < '" + pm_out + "' AND t3.event = 'IN' ORDER BY t3.time ASC LIMIT 1) as pm_in,(SELECT CONCAT(t4.time,'_',t4.edited) FROM dtr_file t4 WHERE userid = d.userid AND datein = d.datein and t4.time >= '" + pm_in + "' AND t4.event = 'OUT' ORDER BY time DESC LIMIT 1) as pm_out FROM dtr_file d LEFT JOIN users e ON d.userid = e.userid and datein = d.datein or (datein between '" + from + "' AND '" + to + "' and holiday = '001') or (datein between '" + from + "' AND '" + to + "' and holiday = '002' and d.userid = e.userid) or (datein between '" + from + "' AND '" + to + "' and holiday = '003' and d.userid = e.userid) or (datein between '" + from + "' AND '" + to + "' and holiday = '004' and d.userid = e.userid) or (datein between '" + from + "' AND '" + to + "' and holiday = '005' and d.userid = e.userid) or (datein between '" + from + "' AND '" + to + "' and holiday = '006' and d.userid = e.userid) WHERE d.datein BETWEEN '" + from + "' AND '" + to + "' AND e.userid = '" + id + "' group by d.datein ORDER BY datein ASC";
            if (this.OpenConnection() == true)
            {
                //Create Command
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, dtr);
                    //format = "CALL GETLOGS('8:00:00','12:00:00','13:00:00','17:00:00','0001','2017-05-02','2017-05-02')";
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        //int day = int.Parse(dataReader["datein"].ToString().Split('-')[2]);
                        String holiday = dataReader["holiday"].ToString();
                        String date_in = dataReader["datein"].ToString().Split(' ')[0];
                        int day = int.Parse(date_in.Split('/')[1]);
                        String am_in1 = dataReader["am_in"].ToString();
                        if (!am_in1.Equals(""))
                        {
                            am_in1 = am_in1.Split('_')[0];
                        }
                        String am_out1 = dataReader["am_out"].ToString();
                        if (!am_out1.Equals(""))
                        {
                            am_out1 = am_out1.Split('_')[0];
                        }
                        String pm_in1 = dataReader["pm_in"].ToString();
                        if (!pm_in1.Equals(""))
                        {
                            pm_in1 = pm_in1.Split('_')[0];
                        }
                        String pm_out1 = dataReader["pm_out"].ToString();
                        if (!pm_out1.Equals(""))
                        {
                            pm_out1 = pm_out1.Split('_')[0];
                        }

                        // String mMonth = (int.Parse(date_in.Split('/')[0])>9)? int.Parse(date_in.Split('/')[0])+"" : "0"+int.Parse(date_in.Split('/')[0]);
                        // String mDay = (int.Parse(date_in.Split('/')[1]) > 9) ? int.Parse(date_in.Split('/')[1]) + "" : "0" + int.Parse(date_in.Split('/')[1]);

                        // String cto_date_format = date_in.Split('/')[2] + "-" + mMonth + "-" + mDay;

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

                    //close Data Reader
                    dataReader.Close();
                    //close Connection  
                    this.CloseConnection();
                    //CHECK
                    for (int i = 0; i < days.Count; i++)
                    {
                        //
                        String format = month + "/" + days[i] + "/" + year;

                        //  String mMonth = (month > 9) ? month + "" : "0" + month;
                        //  String mDay = (days[i] > 9) ? days[i]+ "" : "0" + days[i];
                        // String cto_date_format = year + "-" + mMonth + "-" + mDay;

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
                        String format = month + "/" + (i + 1) + "/" + year;
                        if (!ifWeekend(format) && !IsHoliday(format))
                        {
                            working_days++;
                        }
                    }
                }
                catch (Exception e)
                {
                    return "ERROR ERROR ERROR";
                }
            }

            return mins + " " + working_days + " " + days_absent + " " + days_rendered;
        }

        public Boolean allowAbsent()
        {

            return false;
        }


        public Boolean CheckUserRemittance(String table, String id)
        {
            String query = "SELECT amount FROM " + table + " WHERE userid = '" + id + "' LIMIT 1";
            Boolean found = false;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    found = true;
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return found;
        }

        public Boolean CheckUserTax(String id)
        {
            String query = "SELECT tax FROM tax_remittance WHERE userid = '" + id + "' LIMIT 1";
            Boolean found = false;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    found = true;
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return found;
        }


        public Boolean CheckUserID(String id, String emp_status)
        {
            String query = "SELECT id FROM personal_information WHERE userid = '" + id + "' AND job_status = '" + emp_status + "' AND employee_status = 'Active' LIMIT 1";
            Boolean found = false;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, pis);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    found = true;
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return found;
        }

        public Remittance GetRemittance(String table, String id)
        {
            String query = "SELECT * FROM " + table + " WHERE userid = '" + id + "' LIMIT 1";
            Remittance remittance = new Remittance("0", "0", "0", "0", "0.00");
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    string max = dataReader["max"].ToString();
                    string count = dataReader["count"].ToString();
                    string amount = dataReader["amount"].ToString();
                    remittance = new Remittance("0", id, max, count, amount);
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return remittance;
        }

        public String GetTax(String id)
        {
            String query = "SELECT tax FROM tax_remittance WHERE userid = '" + id + "' LIMIT 1";
            String amount = "0.00";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    amount = dataReader["tax"].ToString();
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return amount;
        }

        public String DeleteRemittance(String table, String id)
        {
            String query = "DELETE FROM " + table + " WHERE userid = '" + id + "'";
            String message = "";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                message = "Successfully Deleted";
            }
            return message;
        }

        public String DeletePayroll(String id)
        {
            String query = "DELETE FROM payroll WHERE id = '" + id + "'";
            String message = "";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                message = "Successfully Deleted";
            }
            return message;
        }

        public String DeletePayrollRegular(String id)
        {
            String query = "DELETE FROM regular_payroll WHERE id = '" + id + "'";
            String message = "";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                message = "Successfully Deleted";
            }
            return message;
        }

        public String DeleteCommunicable(String id)
        {
            String query = "DELETE FROM communicable_allowance WHERE id = '" + id + "'";
            String message = "";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                message = "Successfully Deleted";
            }
            return message;
        }

        public String DeleteHazard(String id)
        {
            String query = "DELETE FROM hazard_pay WHERE id = '" + id + "'";
            String message = "";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                message = "Successfully Deleted";
            }
            return message;
        }

        public String DeleteSubsistence(String id)
        {
            String query = "DELETE FROM subsistence WHERE id = '" + id + "'";
            String message = "";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                message = "Successfully Deleted";
            }
            return message;
        }

        public String DeleteLongevity(String id)
        {
            String query = "DELETE FROM longevity WHERE id = '" + id + "'";
            String message = "";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                message = "Successfully Deleted";
            }
            return message;
        }

        public String DeleteRata(String id)
        {
            String query = "DELETE FROM rata WHERE id = '" + id + "'";
            String message = "";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                message = "Successfully Deleted";
            }
            return message;
        }

        public String DeletePayrollPDF(String id)
        {
            String query = "DELETE FROM payroll_pdf WHERE id = '" + id + "'";
            String message = "";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                message = "Successfully Deleted";
            }
            return message;
        }

        public String InsertRemittance(String table, Remittance reimttance)
        {
            if (decimal.Parse(reimttance.Amount) > 0)
            {
                if (!CheckUserRemittance(table, reimttance.UserID))
                {

                    String query = "INSERT INTO " + table + " VALUES('0','" + reimttance.UserID + "','" + reimttance.MaxCount + "','" + reimttance.Count + "','" + reimttance.Amount + "')";

                    if (this.OpenConnection() == true)
                    {
                        MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                        //Create a data reader and Execute the command
                        cmd.ExecuteNonQuery();
                        this.CloseConnection();
                    }
                    return "Insert Successfully";
                }
                else
                {
                    return UpdateRemittance(table, reimttance);
                }
            }
            else
            {
                return "Amount must be greater than 0";
            }

        }


        public String UpdateRemittance(String table, Remittance remittance)
        {

            String query = "UPDATE " + table + " SET count = '" + remittance.Count + "',max = '" + remittance.MaxCount + "', amount = '" + remittance.Amount + "' WHERE userid = '" + remittance.UserID + "'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                return "Updated Successfully";
            }
            return "Update Fail";
        }

        public String InsertTax(Tax tax)
        {
            if (!CheckUserTax(tax.UserID))
            {
                String query = "INSERT INTO tax_remittance VALUES('0','" + tax.UserID + "','" + tax.Amount + "')";

                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                    //Create a data reader and Execute the command
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                }
                return "Insert Successfully";
            }
            else
            {
                return UpdateTax(tax);
            }

        }

        public String UpdateTax(Tax tax)
        {

            String query = "UPDATE tax_remittance SET tax = '" + tax.Amount + "' WHERE userid = '" + tax.UserID + "'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                return "Updated Successfully";
            }
            return "Update Fail";
        }

        public String UpdatePIN(String pin, String userid)
        {

            String query = "UPDATE users SET pin = '" + pin + "' WHERE username= '" + userid + "'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, pis);
                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                return "Updated Successfully";
            }
            return "Update Fail";
        }

        public String IncrementRemittance(String table, String id)
        {

            String query = "UPDATE " + table + " SET count = (count + 1) WHERE userid = '" + id + "'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
            return "Incremented Successfully";
        }

        public String GetRemittanceCount(String table, String id)
        {
            String count = "0 0";
            String query = "SELECT count,max FROM " + table + " WHERE userid = '" + id + "' LIMIT 1";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    count = dataReader["count"].ToString() + " " + dataReader["max"].ToString();
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return count;
        }

        public List<Tax> GetTax(String type, String search)
        {
            int temp_start = start;
            int temp_end = end;
            int count = 0;
            if (type.Equals("1"))
            {
                if (start - 10 <= 0)
                {
                    start = 0;
                }
                else
                {
                    start -= 10;
                }
            }
            else if (type.Equals("0"))
            {
                start = end;
            }
            else
            {
                start = 0;
            }

            List<Tax> list = new List<Tax>();
            String query = "";
            if (!search.Equals(""))
            {
                query = "SELECT (SELECT COUNT(id) FROM tax_remittance WHERE userid = '" + search + "') as 'MAX_SIZE',id,userid,tax FROM tax_remittance WHERE userid = '" + search + "'";
            }
            else
            {
                query = "SELECT (SELECT COUNT(id) FROM tax_remittance) as 'MAX_SIZE',id,userid,tax FROM tax_remittance";
            }
            query = query + " ORDER BY id DESC LIMIT 10 OFFSET " + DatabaseConnect.start;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    max_size = dataReader["MAX_SIZE"].ToString();
                    count += 1;
                    String id = dataReader["id"].ToString();
                    String userid = dataReader["userid"].ToString();
                    String tax = dataReader["tax"].ToString();

                    Tax remmitance = new Tax(id, userid, tax);
                    list.Add(remmitance);
                }
                //Create a data reader and Execute the command
                end = (start + count);
                dataReader.Close();
                this.CloseConnection();
            }
            if (count == 0)
            {
                start = 0;
                end = 0;
                max_size = "0";
            }
            return list;
        }

        public List<Remittance> GetRemittance(String table, String type, String search)
        {
            int temp_start = start;
            int temp_end = end;
            int count = 0;
            if (type.Equals("1"))
            {
                if (start - 10 <= 0)
                {
                    start = 0;
                }
                else
                {
                    start -= 10;
                }
            }
            else if (type.Equals("0"))
            {
                start = end;
            }
            else
            {
                start = 0;
            }

            List<Remittance> list = new List<Remittance>();
            String query = "";
            if (!search.Equals(""))
            {
                query = "SELECT (SELECT COUNT(id) FROM " + table + " WHERE userid = '" + search + "') as 'MAX_SIZE',id,userid,max,count,amount FROM " + table + " WHERE userid = '" + search + "'";
            }
            else
            {
                query = "SELECT (SELECT COUNT(id) FROM " + table + ") as 'MAX_SIZE',id,userid,max,count,amount FROM " + table + "";
            }
            query = query + " ORDER BY id DESC LIMIT 10 OFFSET " + DatabaseConnect.start;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, sql_payroll);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    max_size = dataReader["MAX_SIZE"].ToString();
                    count += 1;
                    String id = dataReader["id"].ToString();
                    String userid = dataReader["userid"].ToString();
                    String max = dataReader["max"].ToString().Split(' ')[0];
                    String counter = dataReader["count"].ToString();
                    String amount = dataReader["amount"].ToString();

                    Remittance remmitance = new Remittance(id, userid, max, counter, amount);
                    list.Add(remmitance);
                }
                //Create a data reader and Execute the command
                end = (start + count);
                dataReader.Close();
                this.CloseConnection();
            }
            if (count == 0)
            {
                start = 0;
                end = 0;
                max_size = "0";
            }
            return list;
        }
    }
}
*/