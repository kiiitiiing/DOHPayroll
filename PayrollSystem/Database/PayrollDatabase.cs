using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;
using PayrollSystem.Models;
using System.Threading.Tasks;

namespace PayrollSystem.Database
{
    public sealed class PayrollDatabase
    {
        public static string connectionString;
        private static PayrollDatabase instance;

        private PayrollDatabase() { }

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
        string server = "localhost";//"192.168.110.17";
        string database = "payroll";
        string uid = "root";// "doh7payroll";
        string password = "admin";//"doh7payroll";
        connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + "; pooling = false;SslMode=none;convert zero datetime=True";

        }
        public bool InsertHazardPay(Hazard hazard)
        {
            string query = "REPLACE INTO hazard_pay VALUES('0','" + hazard.PersonnelID + "','" + hazard.Pay + "','" + hazard.HWMPC + "','" + hazard.Mortuary + "','"
                    + hazard.DigitelBilling + "','" + hazard.Month + "','" + hazard.Year + "','" + hazard.DaysLeave + "','" + hazard.DaysOO + "')";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        cmd.ExecuteNonQuery();
                }
                SqlConnection.Close();
            }
            return true;
        }
        public bool InsertRata(Rata rata)
        {
            string query = "REPLACE INTO rata VALUES('0','" + rata.PersonnelID + "','" + rata.Ra + "','" + rata.Ta + "','" + rata.Deduction + "',@remarks,'"
                + rata.Month + "','" + rata.Year + "')";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        cmd.Parameters.AddWithValue("@remarks", rata.Remarks);
                        cmd.ExecuteNonQuery();
                }
                SqlConnection.Close();
            }
            return true;
        }


        public List<PhicRemittanceModel> GeneratePhicRemittance(string job_status, string start_date, string end_date)
        {
            string query = "SELECT pay.start_date, pay.end_date, pay.userid, pay.phic, info.job_status, info.fname, info.lname, info.mname, info.phicno, info.disbursement_type " +
                "FROM payroll.payroll pay " +
                "LEFT JOIN pis.personal_information info on pay.userid = info.userid " +
                "WHERE pay.phic != 0 AND info.job_status = '" + job_status + "'  AND pay.start_date >= str_to_date('" + start_date + "', '%m/%d/%Y') AND pay.end_date <= str_to_date('" + end_date + "', '%m/%d/%Y')"; 

            List<PhicRemittanceModel> model = new List<PhicRemittanceModel>();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
            {
                sqlConnection.Open();

                using(MySqlCommand command = new MySqlCommand(query, sqlConnection))
                {
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while(dataReader.Read())
                    {
                        var remittance = new PhicRemittanceModel
                        {
                            UserId = dataReader["userid"].ToString(),
                            FirstName = dataReader["fname"].ToString(),
                            MiddleName = dataReader["mname"].ToString(),
                            LastName = dataReader["lname"].ToString(),
                            PhicNo = dataReader["phicno"].ToString(),
                            Ammount = float.Parse( dataReader["phic"].ToString()),
                            Type = dataReader["disbursement_type"].ToString(),
                        };

                        model.Add(remittance);
                    }
                    dataReader.Close();
                }
                sqlConnection.Close();
            }

            return model;
        }

        public List<PagibigRemittanceModel> GeneratePagIbigRemittance(string job_status, string start_date, string end_date)
        {
            string query = "SELECT pay.start_date, pay.end_date, pay.userid, pay.pagibig, info.job_status, info.fname, info.lname, info.mname, info.name_extension, info.pag_ibigno, info.disbursement_type " +
                "FROM payroll.payroll pay " +
                "LEFT JOIN pis.personal_information info on pay.userid = info.userid " +
                "WHERE pay.pagibig != 0 AND info.job_status = '" + job_status + "'  AND pay.start_date >= str_to_date('" + start_date + "', '%m/%d/%Y') AND pay.end_date <= str_to_date('" + end_date + "', '%m/%d/%Y')";

            List<PagibigRemittanceModel> model = new List<PagibigRemittanceModel>();
            using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (MySqlCommand command = new MySqlCommand(query, sqlConnection))
                {
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        var remittance = new PagibigRemittanceModel
                        {
                            AccountNo = "",
                            EEShare = float.Parse(dataReader["pagibig"].ToString()),
                            ERShare = 0,
                            FirstName = dataReader["fname"].ToString(),
                            MiddleName = dataReader["mname"].ToString(),
                            LastName = dataReader["lname"].ToString(),
                            NameExtension = dataReader["name_extension"].ToString(),
                            PagIbigNo = dataReader["pag_ibigno"].ToString(),
                            Membership = "",
                            PerCov = DateTime.Parse(dataReader["start_date"].ToString()).ToString("yyyyMM"),
                            Remarks = "",
                            UserId = dataReader["userid"].ToString(),
                            StartDate = DateTime.Parse(dataReader["start_date"].ToString()),
                            EndDate = DateTime.Parse(dataReader["end_date"].ToString())
                        };
                        model.Add(remittance);
                    }
                    dataReader.Close();
                }
                sqlConnection.Close();
            }

            return model;
        }

        public bool InsertSubsistence(Subsistence subsistence)
        {
            string query = "INSERT INTO subsistence VALUES('0','" + subsistence.PersonnelID + "','" + subsistence.SubsistenceAllowance + "','" + subsistence.LaundryAllowance +
                    "','" + subsistence.Absences + "','" + subsistence.Hwmpc + "','" + subsistence.NoDays + "',@remarks,'" + subsistence.Month + "','" + subsistence.Year + "')";

            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        cmd.Parameters.AddWithValue("@remarks", subsistence.Remarks);
                        cmd.ExecuteNonQuery();
                }
                SqlConnection.Close();
            }
            return true;
        }
        public bool InsertCell(Cellphone cell)
        {
            string query = "REPLACE INTO communicable_allowance VALUES('0','" + cell.PersonnelID + "','" + cell.Amount + "','" + cell.Less + "',@remarks,'"
                + cell.Month + "','" + cell.Year + "')";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        cmd.Parameters.AddWithValue("@remarks", cell.Remarks);
                        cmd.ExecuteNonQuery();
                }
                SqlConnection.Close();
            }
            return true;
        }

        public bool InsertLongevity(Longevity longevity)
        {
            string query = "REPLACE INTO longevity VALUES('0','" + longevity.PersonnelID + "','" + longevity.Salary1 + "','" + longevity.Salary2 + "','" + longevity.Salary3
                    + "','" + longevity.Salary4 + "','" + longevity.Salary5 + "','" + longevity.Disallowance + "','" + longevity.Month + "','" + longevity.Year + "')";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        cmd.ExecuteNonQuery();
                }
                SqlConnection.Close();
            }
            return true;
        }


        public string IncrementRemittance(string table, string id)
        {

            string query = "UPDATE " + table + " SET count = (count + 1) WHERE userid = '" + id + "'";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        cmd.ExecuteNonQuery();
                }
                SqlConnection.Close();
            }
            return "Incremented Successfully";
        }

        public bool DeleteHazard(string id)
        {
            bool ok = false;
            string query = "DELETE FROM hazard_pay WHERE id = '" + id + "'";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        cmd.ExecuteNonQuery();
                        ok = true;
                }
                SqlConnection.Close();
            }
            return ok;
        }
        public bool DeleteRata(string id)
        {
            bool ok = false;
            string query = "DELETE FROM rata WHERE id = '" + id + "'";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        cmd.ExecuteNonQuery();
                        ok = true;
                }
                SqlConnection.Close();
            }
            return ok;
        }
        public bool DeleteSubsistence(string id)
        {
            bool ok = false;
            string query = "DELETE FROM subsistence WHERE id = '" + id + "'";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        cmd.ExecuteNonQuery();
                        ok = true;
                }
                SqlConnection.Close();
            }
            return ok;
        }

        public bool DeleteCell(string id)
        {
            bool ok = false;
            string query = "DELETE FROM communicable_allowance WHERE id = '" + id + "'";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        cmd.ExecuteNonQuery();
                        ok = true;
                }
                SqlConnection.Close();
            }
            return ok;
        }

        

        public bool DeleteLongevity(string id)
        {
            bool ok = false;
            string query = "DELETE FROM longevity WHERE id = '" + id + "'";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        cmd.ExecuteNonQuery();
                        ok = true;
                }
                SqlConnection.Close();
            }
            return ok;
        }

        public bool InsertRegularPayroll(RegularPayrollModel payroll)
        {

            string query = "REPLACE INTO regular_payroll VALUES('0','" + payroll.Employee.ID + "','" + payroll.Month + "','" + payroll.Year + "','" +
                        payroll.DaysAbsent + "','" + payroll.WorkDays + "','" + payroll.Salary + "','" + payroll.Pera + "','" + payroll.MinutesLate + "','" +
                        payroll.Tax + "','" + payroll.CFI + "','" + payroll.GSIS_Premium + "','" + payroll.GSIS_Consoloan + "','" + payroll.GSIS_PolicyLoan + "','" + payroll.GSIS_EML
                        + "','" + payroll.GSIS_UOLI + "','" + payroll.GSIS_EDU + "','" + payroll.GSIS_Help + "','" + payroll.GSIS_REL + "','" + payroll.Pagibig_Premium + "','" +
                        payroll.Pagibig_Loan + "','" + payroll.Disallowances + "','" + payroll.PhilHealth + "','" + payroll.SIMC + "','" + payroll.HWMPC + "','" + payroll.DBP + "','" + payroll.MP2 + "')";

            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
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
                }
                SqlConnection.Close();
            }
            return true;
        }

        public bool InsertJoPayroll(JobOrderPayrollModel payroll, bool isUpdate)
        {

            string query = "REPLACE INTO payroll.payroll VALUES('"+payroll.Id+"','" + payroll.Employee.ID + "', STR_TO_DATE('" + payroll.StartDate + "','%m/%d/%Y'), STR_TO_DATE('" + payroll.EndDate + "','%m/%d/%Y'),'" +
                    payroll.DaysAbsent + "','" + payroll.WorkDays + "','" + payroll.Salary + "','" + payroll.Adjustment + "','" + payroll.MinutesLate + "','" +
                    payroll.Coop + "','" + payroll.Phic + "','" + payroll.Disallowance + "','" + payroll.Gsis + "','" + payroll.Pagibig + "','" + payroll.ExcessMobile
                    + "',@remarks,NULL,NULL,'" + payroll.Tax + "','" + payroll.OtherAdjustment + "')";

            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        cmd.Parameters.AddWithValue("@remarks", payroll.Remarks);
                        cmd.ExecuteNonQuery();

                        if (!isUpdate)
                        {
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
                        }     
                }
                SqlConnection.Close();
            }
            return true;
        }

        public bool UpdateRemittance(string table, Remittance remittance)
        {
            string query = "REPLACE INTO " + table + " (userid, max, count,amount) VALUES('" + remittance.UserID + "', '" + remittance.MaxCount + "','" + remittance.Count + "','" + remittance.Amount + "')";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        cmd.ExecuteNonQuery();
                }
                SqlConnection.Close();
            }

            return true;    
        }
        
        public bool AddRemittanceLog(string table, RemittanceLogModel model)
        {
            string query = "REPLACE INTO " + table + "(type_remittance, userid, max, count, amount, created_at, updated_at) " +
                "VALUES ('" + model.RemittanceType + "', '" + model.UserId + "', '" + model.Max + "', '" + model.Count + "', '" + model.Amount + "', '" + model.CreatedAt.ToString("yyyy-MM-dd HH-mm-ss") + "', '" + model.UpdateAt.ToString("yyyy-MM-dd HH-mm-ss") + "')";
           
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                    cmd.ExecuteNonQuery();
                }
                SqlConnection.Close();
            }

            return true;
        }

        public bool UpdateTax(string UserID, string Amount)
        {
            string query = "REPLACE INTO tax_remittance (userid, tax) VALUES('" + UserID + "', '" + Amount + "')";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        cmd.ExecuteNonQuery();
                }
                SqlConnection.Close();
            }
            return true;
        }
        public bool DeletePayroll(string id)
        {
            string query = "DELETE FROM payroll WHERE id = '" + id + "'";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        cmd.ExecuteNonQuery();
                }
                SqlConnection.Close();
            }
            return true;
        }
        public bool DeleteRegularPayroll(string id)
        {
            string query = "DELETE FROM regular_payroll WHERE id = '" + id + "'";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        cmd.ExecuteNonQuery();
                }
                SqlConnection.Close();
            }
            return true;
        }

        public List<HazardListViewModel> GetHazardList(string EmpID, string Firstname, string Lastname)
        {
            List<HazardListViewModel> list = new List<HazardListViewModel>();
            string query = "SELECT * FROM hazard_pay WHERE userid = '" + EmpID + "'";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        MySqlDataReader dataReader = cmd.ExecuteReader();

                        while (dataReader.Read())
                        {
                            string id = dataReader["id"].ToString();
                            decimal hazard_pay = decimal.Parse(dataReader["hazard_pay"].ToString());
                            decimal hwmpc_loan = decimal.Parse(dataReader["hwmpc_loan"].ToString());
                            decimal mortuary = decimal.Parse(dataReader["mortuary"].ToString());
                            decimal digitel_billing = decimal.Parse(dataReader["digitel_billing"].ToString());
                            int month = int.Parse(dataReader["month"].ToString());
                            int year = int.Parse(dataReader["year"].ToString());
                            int days_leave = int.Parse(dataReader["days_leave"].ToString());
                            int days_oo = int.Parse(dataReader["days_oo"].ToString());

                            decimal NetAmount = hazard_pay - hwmpc_loan - mortuary - digitel_billing;
                            HazardListViewModel hazard = new HazardListViewModel(id, EmpID, Firstname, Lastname, month, year, NetAmount);
                            list.Add(hazard);
                        }
                        dataReader.Close();
                }
                SqlConnection.Close();
            }
            return list;
        }
        public List<RataListViewModel> GetRataList(string EmpID, string Firstname, string Lastname)
        {
            List<RataListViewModel> list = new List<RataListViewModel>();
            string query = "SELECT * FROM rata WHERE userid = '" + EmpID + "'";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        MySqlDataReader dataReader = cmd.ExecuteReader();

                        while (dataReader.Read())
                        {
                            string id = dataReader["id"].ToString();
                            decimal ra = decimal.Parse(dataReader["ra"].ToString());
                            decimal ta = decimal.Parse(dataReader["ta"].ToString());
                            decimal deduction = decimal.Parse(dataReader["deduction"].ToString());
                            int month = int.Parse(dataReader["month"].ToString());
                            int year = int.Parse(dataReader["year"].ToString());

                            decimal total = ra + ta - deduction;
                            RataListViewModel rata = new RataListViewModel(id, EmpID, Firstname, Lastname, month, year, total);
                            list.Add(rata);
                        }
                        dataReader.Close();
                }
                SqlConnection.Close();
            }
            return list;
        }

        public List<CellListViewModel> GetCellList(string EmpID, string Firstname, string Lastname)
        {
            List<CellListViewModel> list = new List<CellListViewModel>();
            string query = "SELECT * FROM communicable_allowance WHERE userid = '" + EmpID + "'";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        MySqlDataReader dataReader = cmd.ExecuteReader();

                        while (dataReader.Read())
                        {
                            string id = dataReader["id"].ToString();
                            decimal amount = decimal.Parse(dataReader["amount"].ToString());
                            decimal less = decimal.Parse(dataReader["nov_billing"].ToString());
                            int month = int.Parse(dataReader["month"].ToString());
                            int year = int.Parse(dataReader["year"].ToString());

                            decimal total = amount - less;
                            CellListViewModel cell = new CellListViewModel(id, EmpID, Firstname, Lastname, month, year, total);
                            list.Add(cell);
                        }
                        dataReader.Close();
                }
                SqlConnection.Close();
            }
            return list;
        }

        public List<SubsistenceListViewModel> GetSubsistenceList(string EmpID, string Firstname, string Lastname)
        {
            List<SubsistenceListViewModel> list = new List<SubsistenceListViewModel>();
            string query = "SELECT * FROM subsistence WHERE userid = '" + EmpID + "'";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        MySqlDataReader dataReader = cmd.ExecuteReader();

                        while (dataReader.Read())
                        {
                            string id = dataReader["id"].ToString();
                            decimal subsistence_allowance = decimal.Parse(dataReader["subsistence_allowance"].ToString());
                            decimal laundry_allowance = decimal.Parse(dataReader["laundry_allowance"].ToString());
                            decimal absences = decimal.Parse(dataReader["absences"].ToString());
                            decimal hwmpc = decimal.Parse(dataReader["hwmcp"].ToString());

                            int month = int.Parse(dataReader["month"].ToString());
                            int year = int.Parse(dataReader["year"].ToString());

                            decimal total = subsistence_allowance + laundry_allowance - absences - hwmpc;
                            SubsistenceListViewModel cell = new SubsistenceListViewModel(id, EmpID, Firstname, Lastname, month, year, total);
                            list.Add(cell);
                        }
                        dataReader.Close();
                }
                SqlConnection.Close();
            }
            return list;
        }

        public List<LongevityListViewModel> GetLongevityList(string EmpID, string Firstname, string Lastname)
        {
            List<LongevityListViewModel> list = new List<LongevityListViewModel>();
            string query = "SELECT * FROM longevity WHERE userid = '" + EmpID + "'";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        MySqlDataReader dataReader = cmd.ExecuteReader();

                        while (dataReader.Read())
                        {
                            string id = dataReader["id"].ToString();
                            decimal salary1 = decimal.Parse(dataReader["salary_1"].ToString());
                            decimal salary2 = decimal.Parse(dataReader["salary_2"].ToString());
                            decimal salary3 = decimal.Parse(dataReader["salary_3"].ToString());
                            decimal salary4 = decimal.Parse(dataReader["salary_4"].ToString());
                            decimal salary5 = decimal.Parse(dataReader["salary_5"].ToString());
                            decimal disallowance = decimal.Parse(dataReader["disallowance"].ToString());



                            int month = int.Parse(dataReader["month"].ToString());
                            int year = int.Parse(dataReader["year"].ToString());

                            decimal total = (salary1 * (decimal)0.05) + (salary2 * (decimal)0.05) + (salary3 * (decimal)0.05) + (salary4 * (decimal)0.05) + (salary5 * (decimal)0.05) - disallowance;
                            LongevityListViewModel cell = new LongevityListViewModel(id, EmpID, Firstname, Lastname, month, year, total);
                            list.Add(cell);
                        }
                        dataReader.Close();
                }
                SqlConnection.Close();
            }
            return list;
        }

        public List<RegularPayrollModel> GetRegularPayrollByID(string EmpID, string Firstname, string Lastname)
        {
            List<RegularPayrollModel> list = new List<RegularPayrollModel>();
            string query = "SELECT * FROM regular_payroll WHERE userid = '" + EmpID + "'";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
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

                            Employee employee = new Employee(userid, Firstname, Lastname, "", "", "", "", "", "");

                            RegularPayrollModel payroll = new RegularPayrollModel(payroll_id, employee, int.Parse(month), int.Parse(year), days_absent, working_days, salary, pera, int.Parse(minutes_late), tax, cfi, gsis_premium,
                                    gsis_consoloan, gsis_policy_loan, gsis_eml, gsis_uoli, gsis_edu, gsis_help, gsis_rel, pagibig_premium, pagibig_loan, disallowances, philhealth, simc, hwmpc, dbp, pagibig_mp2);


                            list.Add(payroll);
                        }
                        dataReader.Close();
                }
                SqlConnection.Close();
            }
            return list;
        }

        public List<JobOrderPayrollModel> GetJobOrderPayrollByID(string EmpID, string Firstname, string Lastname)
        {
            List<JobOrderPayrollModel> list = new List<JobOrderPayrollModel>();
            string query = "SELECT * FROM payroll WHERE userid = '" + EmpID + "'";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        MySqlDataReader dataReader = cmd.ExecuteReader();

                        while (dataReader.Read())
                        {
                             
                                string id = dataReader["id"].ToString();
                                string start_date = DateTime.Parse(dataReader["start_date"].ToString()).ToString("MM/dd/yyyy").ToUpper();
                                string end_date = DateTime.Parse(dataReader["end_date"].ToString()).ToString("MM/dd/yyyy").ToUpper();
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

                                Employee employee = new Employee(EmpID, Firstname, Lastname, "", "", "", "", "", "");

                                JobOrderPayrollModel joborder = new JobOrderPayrollModel(id, employee, start_date, end_date, adjustment, working_days, absent_days
                                        , monthly_salary, minutes_late, coop, phic, disallowance, gsis, pagibig, excess_mobile, remarks, "", tax, other_adjustment);
                                list.Add(joborder);
                        }
                        dataReader.Close();
                }
                SqlConnection.Close();
            }
            return list;
        }

        public List<ReportPDF> GetPDF(string searchQuery)
        {
            string query = "SELECT * FROM pdf";
            if (!searchQuery.Equals(""))
            {
                query = query + " WHERE document LIKE '" + searchQuery + "%' || disbursement LIKE '" + searchQuery + "%' || salary_charge LIKE '" + searchQuery + "%' || division LIKE '" + searchQuery + "%'";
            }
            query = query + " ORDER BY date_created DESC";

            List<ReportPDF> list = new List<ReportPDF>();
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
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

                        ReportPDF data = new ReportPDF(id, from, to, document, disbursement, salary_charge, division, file_path);
                        list.Add(data);
                    }

                    dataReader.Close();
                }
                SqlConnection.Close();
            }
            return list;
        }

        public List<JobOrderPayrollModel> GenerateJoSummary(string start_date, string end_date, string document,
                                            string disbursment, string salary_charge)
        {
            List<JobOrderPayrollModel> JobOrderList = new List<JobOrderPayrollModel>();
            string query = "SELECT d.id,p.absent_days,p.adjustment,p.remarks,d.description as designation,i.userid,i.mname,i.fname,i.lname,i.position,i.salary_charge,i.tin_no,i.account_number,p.working_days,p.month_salary,p.minutes_late,p.coop,p.phic,p.disallowance,p.gsis,p.pagibig,p.excess_mobile,p.tax,p.other_adjustment " +
                            "FROM payroll p LEFT JOIN (pis.personal_information i LEFT JOIN dts.designation d ON i.designation_id = d.id) ON p.userid = i.userid " +
                            "WHERE p.start_date = STR_TO_DATE('" + start_date + "','%m/%d/%Y')  AND p.end_date = STR_TO_DATE('" + end_date + "','%m/%d/%Y') AND i.disbursement_type = '" + disbursment + "'";
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
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
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

                                Employee employee = new Employee(userid, fname, lname, mname, designation, "", salary_charge_data, division, disbursment);

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
                }
                SqlConnection.Close();
            }
            return JobOrderList;
        }

        public List<RegularPayrollModel> GenerateSummaryRegularByDivision(int month, int year, string division)
        {
            List<RegularPayrollModel> list = new List<RegularPayrollModel>();
            string query = "SELECT r.ra,r.ta,r.deduction,s.subsistence_allowance,s.laundry_allowance,s.absences,s.hwmcp ,p.id,p.userid,p.month,p.year,p.absent_days,p.working_days,p.month_salary,p.pera,p.minutes_late,p.tax,p.cfi,p.gsis_premium,p.gsis_consoloan,p.gsis_policy_loan,p.gsis_eml,p.gsis_uoli,p.gsis_edu,p.gsis_help,p.gsis_rel,p.pagibig_premium,p.pagibig_loan,p.pagibig_mp2,p.disallowances,p.philhealth,p.simc,p.hwmpc,p.dbp " +
                            "FROM payroll.regular_payroll p " +
                            "LEFT JOIN pis.personal_information i ON p.userid = i.userid " +
                            "LEFT JOIN payroll.hazard_pay h ON p.userid = h.userid AND h.month = '" + month + "' AND H.year = '" + year + "'" +
                            "LEFT JOIN payroll.rata r ON p.userid = r.userid AND r.month = '" + month + "' AND r.year = '" + year + "'" +
                            "LEFT JOIN payroll.subsistence s ON p.userid = s.userid AND s.month = '" + month + "' AND s.year = '" + year + "'" +
                            "WHERE p.month = '" + month + "' AND p.year = '" + year + "' ";
            if (!division.Equals("ALL"))
            {
                query = query + "AND i.division_id = @division_id ";
            }
            query = query + "ORDER BY i.lname,i.fname ASC";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        cmd.Parameters.AddWithValue("@division_id", division);
                        MySqlDataReader dataReader = cmd.ExecuteReader();

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


                                decimal subsistence_allowance = dataReader["subsistence_allowance"].ToString() == "" ? 0 : decimal.Parse(dataReader["subsistence_allowance"].ToString());
                                decimal laundry_allowance = dataReader["laundry_allowance"].ToString() == "" ? 0 : decimal.Parse(dataReader["laundry_allowance"].ToString());
                                decimal absences = dataReader["absences"].ToString() == "" ? 0 : decimal.Parse(dataReader["absences"].ToString());
                                decimal hwmcp = dataReader["hwmcp"].ToString() == "" ? 0 : decimal.Parse(dataReader["hwmcp"].ToString());

                                decimal subsistence_net = subsistence_allowance + laundry_allowance;
                                decimal subsistence_deduction = absences + hwmcp;

                                HazardViewModel hazard = new HazardViewModel(subsistence_net, subsistence_deduction);

                                decimal ra = dataReader["ra"].ToString() == "" ? 0 : decimal.Parse(dataReader["ra"].ToString());
                                decimal ta = dataReader["ta"].ToString() == "" ? 0 : decimal.Parse(dataReader["ta"].ToString());
                                decimal deduction = dataReader["deduction"].ToString() == "" ? 0 : decimal.Parse(dataReader["deduction"].ToString());

                                RataViewModel rata = new RataViewModel(ra, ta, deduction);

                                Employee employee = null; //PISDatabase.Instance.GetEmployeeByID(userid);

                                RegularPayrollModel payrollRegular = new RegularPayrollModel(id, employee, month, year, absent_days, working_days, month_salary, pera, int.Parse(minutes_late),
                                        tax, cfi, gsis_premium, gsis_consoloan, gsis_policy_loan, gsis_eml, gsis_uoli, gsis_edu, gsis_help, gsis_rel, pagibig_premium, pagibig_loan, disallowances, philhealth, simc, hwmpc, dbp, pagibig_mp2);

                                payrollRegular.SetHazard(hazard);
                                payrollRegular.SetRata(rata);

                                list.Add(payrollRegular);
                        }
                        dataReader.Close();
                }
                SqlConnection.Close();
            }

            return list;
        }

        public List<RegularPayrollModel> GenerateSummaryRegularBySalaryCharge(int month, int year, string salary_charge)
        {
            List<RegularPayrollModel> list = new List<RegularPayrollModel>();
            string query = "SELECT i.fname,i.lname,i.mname,i.job_status,q.description as division,e.description as designation," +
                            "r.ra,r.ta,r.deduction as rata_deduction," +
                            "c.amount as cell_amount,c.nov_billing," +
                            "s.subsistence_allowance + s.laundry_allowance as subsistence_total,s.absences + s.hwmcp as subsistence_deduction," +
                            "h.hazard_pay as hazard_total,h.hwmpc_loan + h.mortuary + h.digitel_billing as hazard_deduction," +
                            "l.salary_1 + l.salary_2 + l.salary_3 + l.salary_4 + l.salary_5 as longevity_total,l.disallowance as longevity_deduction," +
                            "p.id,p.userid,p.month,p.year,p.absent_days,p.working_days,p.month_salary,p.pera,p.minutes_late,p.tax,p.cfi,p.gsis_premium,p.gsis_consoloan,p.gsis_policy_loan,p.gsis_eml,p.gsis_uoli,p.gsis_edu,p.gsis_help,p.gsis_rel,p.pagibig_premium,p.pagibig_loan,p.pagibig_mp2,p.disallowances,p.philhealth,p.simc,p.hwmpc,p.dbp " +
                            "FROM payroll.regular_payroll p " +
                            "LEFT JOIN (pis.personal_information i LEFT JOIN dts.division q ON q.id = i.division_id LEFT JOIN dts.designation e ON e.id = i.designation_id) ON p.userid = i.userid " +
                            "LEFT JOIN payroll.hazard_pay h ON p.userid = h.userid AND h.month = '" + month + "' AND h.year = '" + year + "'" +
                            "LEFT JOIN payroll.rata r ON p.userid = r.userid AND r.month = '" + month + "' AND r.year = '" + year + "'" +
                            "LEFT JOIN payroll.longevity l ON p.userid = l.userid AND l.month = '" + month + "' AND l.year = '" + year + "'" +
                            "LEFT JOIN payroll.communicable_allowance c ON p.userid = c.userid AND c.month = '" + month + "' AND c.year = '" + year + "'" +
                            "LEFT JOIN payroll.subsistence s ON p.userid = s.userid AND s.month = '" + month + "' AND s.year = '" + year + "'" +
                            "WHERE p.month = '" + month + "' AND p.year = '" + year + "' ";
            if (!salary_charge.Equals("ALL"))
            {
                query = query + "AND i.salary_charge = @salary_charge ";
            }
            query = query + "ORDER BY i.salary_charge,i.lname,i.fname ASC";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                try
                {
                        SqlConnection.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                        {
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
                                Employee employee = new Employee(userid, fname, lname, mname, designation, "", salary_charge, division, "");

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

                                decimal subsistence_total = dataReader["subsistence_total"].ToString() == "" ? 0 : decimal.Parse(dataReader["subsistence_total"].ToString());
                                decimal subsistence_deduction = dataReader["subsistence_deduction"].ToString() == "" ? 0 : decimal.Parse(dataReader["subsistence_deduction"].ToString());

                                HazardViewModel subsistence = new HazardViewModel(subsistence_total, subsistence_deduction);

                                decimal longevity_total = dataReader["longevity_total"].ToString() == "" ? 0 : decimal.Parse(dataReader["longevity_total"].ToString());
                                decimal longevity_deduction = dataReader["longevity_deduction"].ToString() == "" ? 0 : decimal.Parse(dataReader["longevity_deduction"].ToString());

                                HazardViewModel longevity = new HazardViewModel(longevity_total, longevity_deduction);

                                decimal hazard_total = dataReader["hazard_total"].ToString() == "" ? 0 : decimal.Parse(dataReader["hazard_total"].ToString());
                                decimal hazard_deduction = dataReader["hazard_deduction"].ToString() == "" ? 0 : decimal.Parse(dataReader["hazard_deduction"].ToString());

                                HazardViewModel hazard = new HazardViewModel(hazard_total, hazard_deduction);

                                decimal cell_amount = dataReader["cell_amount"].ToString() == "" ? 0 : decimal.Parse(dataReader["cell_amount"].ToString());
                                decimal nov_billing = dataReader["nov_billing"].ToString() == "" ? 0 : decimal.Parse(dataReader["nov_billing"].ToString());

                                HazardViewModel cellphone = new HazardViewModel(cell_amount, nov_billing);

                                //RATA DATAREADER
                                decimal ra = dataReader["ra"].ToString() == "" ? 0 : decimal.Parse(dataReader["ra"].ToString());
                                decimal ta= dataReader["ta"].ToString() == "" ? 0 : decimal.Parse(dataReader["ta"].ToString());
                                decimal deduction = dataReader["rata_deduction"].ToString() == "" ? 0 : decimal.Parse(dataReader["rata_deduction"].ToString());
                                RataViewModel rata = new RataViewModel(ra, ta, deduction);


                                RegularPayrollModel payrollRegular = new RegularPayrollModel(id, employee, month, year, absent_days, working_days, month_salary, pera, int.Parse(minutes_late),
                                        tax, cfi, gsis_premium, gsis_consoloan, gsis_policy_loan, gsis_eml, gsis_uoli, gsis_edu, gsis_help, gsis_rel, pagibig_premium, pagibig_loan, disallowances, philhealth, simc, hwmpc, dbp, pagibig_mp2);

                                payrollRegular.SetHazard(hazard);
                                payrollRegular.SetRata(rata);
                                payrollRegular.SetSubsistence(subsistence);
                                payrollRegular.SetLongevity(longevity);
                                payrollRegular.SetCellphone(cellphone);

                                list.Add(payrollRegular);
                            }
                            dataReader.Close();
                        }
                        SqlConnection.Close();
                }
                catch (Exception e)
                {

                }
            }

            return list;
        }

        public string InsertPDF(string from, string to, string document, string disbursement, string salary_charge, string division, string file_path)
        {
            string query = "REPLACE INTO pdf VALUES('0',@date_from,@date_to,@document,@disbursement,@salary_charge,@division,@file_path,NOW())";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        cmd.Parameters.AddWithValue("@date_from", from);
                        cmd.Parameters.AddWithValue("@date_to", to);
                        cmd.Parameters.AddWithValue("@document", document);
                        cmd.Parameters.AddWithValue("@disbursement", disbursement);
                        cmd.Parameters.AddWithValue("@salary_charge", salary_charge);
                        cmd.Parameters.AddWithValue("@division", division);
                        cmd.Parameters.AddWithValue("@file_path", file_path);
                        cmd.ExecuteNonQuery();
                }
                SqlConnection.Close();
            }
            return "Inserted/Updated Successfully";
        }

        public string DeletePDF(string id)
        {
            string query = "DELETE FROM pdf WHERE id = '" + id + "'";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        cmd.ExecuteNonQuery();
                }
                SqlConnection.Close();
            }

            return "Success";
        }

        public RegularPayslipModel GetRegularPayslipById(int month, int year, string userid)
        {
            RegularPayslipModel PayslipModel = null;
            string query = "SELECT l.salary_1,l.salary_2,l.salary_3,l.salary_4,l.salary_5,l.disallowance as long_disallowance,c.amount as cell_amount,c.nov_billing,h.hazard_pay, h.hwmpc_loan,h.mortuary,h.digitel_billing,r.ra,r.ta,r.deduction,s.subsistence_allowance,s.laundry_allowance,s.absences,s.hwmcp ,p.id,p.userid,p.month,p.year,p.absent_days,p.working_days,p.month_salary,p.pera,p.minutes_late,p.tax,p.cfi,p.gsis_premium,p.gsis_consoloan,p.gsis_policy_loan,p.gsis_eml,p.gsis_uoli,p.gsis_edu,p.gsis_help,p.gsis_rel,p.pagibig_premium,p.pagibig_loan,p.pagibig_mp2,p.disallowances,p.philhealth,p.simc,p.hwmpc,p.dbp " +
                            "FROM payroll.regular_payroll p " +
                            "LEFT JOIN pis.personal_information i ON p.userid = i.userid " +
                            "LEFT JOIN payroll.hazard_pay h ON p.userid = h.userid AND h.month = '" + month + "' AND h.year = '" + year + "'" +
                            "LEFT JOIN payroll.longevity l ON p.userid = l.userid AND l.month = '" + month + "' AND l.year = '" + year + "'" +
                            "LEFT JOIN payroll.communicable_allowance c ON p.userid = c.userid AND c.month = '" + month + "' AND c.year = '" + year + "'" +
                            "LEFT JOIN payroll.rata r ON p.userid = r.userid AND r.month = '" + month + "' AND r.year = '" + year + "'" +
                            "LEFT JOIN payroll.subsistence s ON p.userid = s.userid AND s.month = '" + month + "' AND s.year = '" + year + "'" +
                            "WHERE p.month = '" + month + "' AND p.year = '" + year + "' AND p.userid = '" + userid + "'";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        MySqlDataReader dataReader = cmd.ExecuteReader();

                        //Read the data and store them in the list
                        while (dataReader.Read())
                        {
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

                             
                            decimal hazard_pay = dataReader["hazard_pay"].ToString() == "" ? 0 : decimal.Parse(dataReader["hazard_pay"].ToString());
                            decimal hwmpc_loan = dataReader["hwmpc_loan"].ToString() == "" ? 0 : decimal.Parse(dataReader["hwmpc_loan"].ToString());
                            decimal mortuary = dataReader["mortuary"].ToString() == "" ? 0 : decimal.Parse(dataReader["mortuary"].ToString());
                            decimal digitel_billing = dataReader["digitel_billing"].ToString() == "" ? 0 : decimal.Parse(dataReader["digitel_billing"].ToString());

                            decimal cell_amount = dataReader["cell_amount"].ToString() == "" ? 0 : decimal.Parse(dataReader["cell_amount"].ToString());
                            decimal nov_billing = dataReader["nov_billing"].ToString() == "" ? 0 : decimal.Parse(dataReader["nov_billing"].ToString());

                            decimal subsistence_allowance = dataReader["subsistence_allowance"].ToString() == "" ? 0 : decimal.Parse(dataReader["subsistence_allowance"].ToString());
                            decimal laundry_allowance = dataReader["laundry_allowance"].ToString() == "" ? 0 : decimal.Parse(dataReader["laundry_allowance"].ToString());
                            decimal absences = dataReader["absences"].ToString() == "" ? 0 : decimal.Parse(dataReader["absences"].ToString());
                            decimal hwmcp = dataReader["hwmcp"].ToString() == "" ? 0 : decimal.Parse(dataReader["hwmcp"].ToString());
                            decimal subsistence = subsistence_allowance + laundry_allowance;
                            decimal subsistence_deduction = absences + hwmcp;

                            decimal salary_1 = dataReader["salary_1"].ToString() == "" ? 0 : decimal.Parse(dataReader["salary_1"].ToString());
                            decimal salary_2 = dataReader["salary_2"].ToString() == "" ? 0 : decimal.Parse(dataReader["salary_2"].ToString());
                            decimal salary_3 = dataReader["salary_3"].ToString() == "" ? 0 : decimal.Parse(dataReader["salary_3"].ToString());
                            decimal salary_4 = dataReader["salary_4"].ToString() == "" ? 0 : decimal.Parse(dataReader["salary_4"].ToString());
                            decimal salary_5 = dataReader["salary_5"].ToString() == "" ? 0 : decimal.Parse(dataReader["salary_5"].ToString());
                            decimal long_disallowance = dataReader["long_disallowance"].ToString() == "" ? 0 : decimal.Parse(dataReader["long_disallowance"].ToString());

                            decimal total_salary = (salary_1 * (decimal)0.05) + (salary_2 * (decimal)0.05) + (salary_3 * (decimal)0.05) + (salary_4 * (decimal)0.05) + (salary_5 * (decimal)0.05);
                            decimal ra = dataReader["ra"].ToString() == "" ? 0 : decimal.Parse(dataReader["ra"].ToString());
                            decimal ta = dataReader["ta"].ToString() == "" ? 0 : decimal.Parse(dataReader["ta"].ToString());
                            decimal deduction = dataReader["deduction"].ToString() == "" ? 0 : decimal.Parse(dataReader["deduction"].ToString());

                            RegularFirstHalf FirstHalf = new RegularFirstHalf(month_salary, pera, Tardiness, tax, cfi, gsis_premium, gsis_consoloan, gsis_policy_loan, gsis_eml, gsis_uoli, gsis_edu, rel, gsis_help, pagibig_premium, pagibig_loan, pagibig_mp2,
                                philhealth, simc, hwmpc, disallowance, subsistence, subsistence_deduction, total_salary, long_disallowance);

                            decimal mid_year = 0;
                            decimal year_end = 0;
                            if (month == 5) mid_year = month_salary;
                            if (month == 11) year_end = month_salary;

                            RegularSecondHalf SecondHalf = new RegularSecondHalf(FirstHalf.GetTotalPay(), hazard_pay, mortuary, hwmpc_loan, digitel_billing, cell_amount, nov_billing, mid_year, year_end, ra + ta);
                            PayslipModel = new RegularPayslipModel(FirstHalf, SecondHalf);

                        }

                        dataReader.Close();
                }
                SqlConnection.Close();
            }
            return PayslipModel;
        }

        public List<SalaryChargeItem> GenerateSalaryChargeSummary(int month, int year)
        {
            List<SalaryChargeItem> PayslipModelList = new List<SalaryChargeItem>();
            string query = "SELECT i.fname,i.lname,i.mname,i.job_status,q.description as division,e.description as designation," +
                            "l.salary_1,l.salary_2,l.salary_3,l.salary_4,l.salary_5,l.disallowance as long_disallowance,"+
                            "c.amount as cell_amount,c.nov_billing,h.hazard_pay, h.hwmpc_loan,h.mortuary,h.digitel_billing,"+
                            "r.ra,r.ta,r.deduction,s.subsistence_allowance,s.laundry_allowance,s.absences,s.hwmcp,"+
                            "p.id,p.userid,p.month,p.year,p.absent_days,p.working_days,p.month_salary,p.pera,p.minutes_late,p.tax,p.cfi,p.gsis_premium,p.gsis_consoloan,p.gsis_policy_loan,p.gsis_eml,p.gsis_uoli,p.gsis_edu,p.gsis_help,p.gsis_rel,p.pagibig_premium,p.pagibig_loan,p.pagibig_mp2,p.disallowances,p.philhealth,p.simc,p.hwmpc,p.dbp " +
                            "FROM payroll.regular_payroll p " +
                            "LEFT JOIN (pis.personal_information z LEFT JOIN dts.division q ON q.id = z.division_id LEFT JOIN dts.designation e ON e.id = z.designation_id) ON p.userid = z.userid " +
                            "LEFT JOIN pis.personal_information i ON p.userid = i.userid " +
                            "LEFT JOIN payroll.hazard_pay h ON p.userid = h.userid AND h.month = '" + month + "' AND h.year = '" + year + "'" +
                            "LEFT JOIN payroll.longevity l ON p.userid = l.userid AND l.month = '" + month + "' AND l.year = '" + year + "'" +
                            "LEFT JOIN payroll.communicable_allowance c ON p.userid = c.userid AND c.month = '" + month + "' AND c.year = '" + year + "'" +
                            "LEFT JOIN payroll.rata r ON p.userid = r.userid AND r.month = '" + month + "' AND r.year = '" + year + "'" +
                            "LEFT JOIN payroll.subsistence s ON p.userid = s.userid AND s.month = '" + month + "' AND s.year = '" + year + "'" +
                            "WHERE p.month = '" + month + "' AND p.year = '" + year + "'";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        MySqlDataReader dataReader = cmd.ExecuteReader();

                        //Read the data and store them in the list
                        while (dataReader.Read())
                        {
                            string id = dataReader["id"].ToString();
                              
                            string userid = dataReader["userid"].ToString();
                            string fname = dataReader["fname"].ToString().ToUpper();
                            string lname = dataReader["lname"].ToString().ToUpper();
                            string mname = dataReader["mname"].ToString().ToUpper();
                            string designation = dataReader["designation"].ToString().ToUpper();
                            string division = dataReader["division"].ToString().ToUpper();


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


                            decimal hazard_pay = dataReader["hazard_pay"].ToString() == "" ? 0 : decimal.Parse(dataReader["hazard_pay"].ToString());
                            decimal hwmpc_loan = dataReader["hwmpc_loan"].ToString() == "" ? 0 : decimal.Parse(dataReader["hwmpc_loan"].ToString());
                            decimal mortuary = dataReader["mortuary"].ToString() == "" ? 0 : decimal.Parse(dataReader["mortuary"].ToString());
                            decimal digitel_billing = dataReader["digitel_billing"].ToString() == "" ? 0 : decimal.Parse(dataReader["digitel_billing"].ToString());

                            decimal cell_amount = dataReader["cell_amount"].ToString() == "" ? 0 : decimal.Parse(dataReader["cell_amount"].ToString());
                            decimal nov_billing = dataReader["nov_billing"].ToString() == "" ? 0 : decimal.Parse(dataReader["nov_billing"].ToString());

                            decimal subsistence_allowance = dataReader["subsistence_allowance"].ToString() == "" ? 0 : decimal.Parse(dataReader["subsistence_allowance"].ToString());
                            decimal laundry_allowance = dataReader["laundry_allowance"].ToString() == "" ? 0 : decimal.Parse(dataReader["laundry_allowance"].ToString());
                            decimal absences = dataReader["absences"].ToString() == "" ? 0 : decimal.Parse(dataReader["absences"].ToString());
                            decimal hwmcp = dataReader["hwmcp"].ToString() == "" ? 0 : decimal.Parse(dataReader["hwmcp"].ToString());

                            decimal subsistence = subsistence_allowance + laundry_allowance;
                            decimal subsistence_deduction = absences + hwmcp;

                            decimal salary_1 = dataReader["salary_1"].ToString() == "" ? 0 : decimal.Parse(dataReader["salary_1"].ToString());
                            decimal salary_2 = dataReader["salary_2"].ToString() == "" ? 0 : decimal.Parse(dataReader["salary_2"].ToString());
                            decimal salary_3 = dataReader["salary_3"].ToString() == "" ? 0 : decimal.Parse(dataReader["salary_3"].ToString());
                            decimal salary_4 = dataReader["salary_4"].ToString() == "" ? 0 : decimal.Parse(dataReader["salary_4"].ToString());
                            decimal salary_5 = dataReader["salary_5"].ToString() == "" ? 0 : decimal.Parse(dataReader["salary_5"].ToString());

                            decimal long_disallowance = dataReader["long_disallowance"].ToString() == "" ? 0 : decimal.Parse(dataReader["long_disallowance"].ToString());

                            decimal total_salary = (salary_1 * (decimal)0.05) + (salary_2 * (decimal)0.05) + (salary_3 * (decimal)0.05) + (salary_4 * (decimal)0.05) + (salary_5 * (decimal)0.05);

                            decimal ra = dataReader["ra"].ToString() == "" ? 0 : decimal.Parse(dataReader["ra"].ToString());
                            decimal ta = dataReader["ta"].ToString() == "" ? 0 : decimal.Parse(dataReader["ta"].ToString());
                            decimal deduction = dataReader["deduction"].ToString() == "" ? 0 : decimal.Parse(dataReader["deduction"].ToString());

                            RegularFirstHalf FirstHalf = new RegularFirstHalf(month_salary, pera, Tardiness, tax, cfi, gsis_premium, gsis_consoloan, gsis_policy_loan, gsis_eml, gsis_uoli, gsis_edu, rel, gsis_help, pagibig_premium, pagibig_loan, pagibig_mp2,
                                philhealth, simc, hwmpc, disallowance, subsistence, subsistence_deduction, total_salary, long_disallowance);

                            decimal mid_year = 0;
                            decimal year_end = 0;

                            if (month == 5) mid_year = month_salary;
                            if (month == 11) year_end = month_salary;

                            RegularSecondHalf SecondHalf = new RegularSecondHalf(FirstHalf.GetTotalPay(), hazard_pay, mortuary, hwmpc_loan, digitel_billing, cell_amount, nov_billing, mid_year, year_end, ra + ta);
                            RegularPayslipModel PayslipModel = new RegularPayslipModel(FirstHalf, SecondHalf);

                            PayslipModelList.Add(new SalaryChargeItem(userid,fname+" "+mname+" "+lname,designation, PayslipModel));

                        }

                        dataReader.Close();
                }
                SqlConnection.Close();
            }
            return PayslipModelList;
        }

        public JoPayslipModel GetPayslipByEmployeeID(string userid, string from_date, string to_date)
        {
            JoPayslipModel data = null;

            string query = "SELECT absent_days,adjustment,working_days,month_salary,minutes_late,coop,phic,disallowance,gsis,pagibig,excess_mobile,tax,other_adjustment FROM payroll.payroll p " +
                            "WHERE userid = '" + userid + "' AND start_date = STR_TO_DATE('" + from_date + "','%m/%d/%y') AND end_date = STR_TO_DATE('" + to_date + "','%m/%d/%y')";
            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                        MySqlDataReader dataReader = cmd.ExecuteReader();

                        //Read the data and store them in the list
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
                }
                SqlConnection.Close();
            }

            return data;
        }
    }
}