using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;
using PayrollSystem.Models;
using BCrypt.Net;

namespace PayrollSystem.Database
{
    public class PISDatabase
     {

        public static string connectionString;
        //  public static MySqlConnection SqlConnection = null;

        private static PISDatabase instance;

        private PISDatabase() { }

        //Singleton Pattern
        public static PISDatabase Instance

        {
            get
            {
                if (instance == null)
                {
                    instance = new PISDatabase();
                    instance.Initialize(0);
                }
                return instance;
            }
        }

        public void Initialize(int index)
        {
            string server = InitializeConnectionString.Servers[index];
            string database = "pis";
            string uid = InitializeConnectionString.UIDs[index];
            string password = InitializeConnectionString.Password[index];
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                        database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + "; pooling = false; SslMode=none; convert zero datetime=True";
        }

        public CookiesModel VerifyUser(string userId, string password)
        {
            string query = "SELECT pu.username, du.fname, du.mname, du.lname, du.password " +
                            "FROM pis.users pu " +
                            "LEFT JOIN dohdtr.users du ON du.userid = pu.username " +
                            "WHERE pu.username = '" + userId + "'";

            CookiesModel user = null;
            using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (MySqlCommand command = new MySqlCommand(query, sqlConnection))
                {
                    MySqlDataReader dataReader = command.ExecuteReader();

                    if(dataReader.Read())
                    {
                        user = new CookiesModel
                        {
                            ID = dataReader["username"].ToString(),
                            Firstname = dataReader["fname"].ToString(),
                            Middlename = dataReader["mname"].ToString(),
                            Lastname = dataReader["lname"].ToString(),
                            Password = dataReader["password"].ToString()
                        };
                    }

                    dataReader.Close();
                }

                sqlConnection.Close();
            }

            return user;
        }



        public string SafeGetString(MySqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }

        public List<JobOrderModel> GetJoEmployee(string SearchString, string Disbursement)
        {

            string query = "SELECT coop.max as coop_payment,coop.count as coop_paid,coop.amount as coop_amount," +
                            "pagibig.max as pagibig_payment,pagibig.count as pagibig_paid,pagibig.amount as pagibig_amount," +
                            "gsis.max as gsis_payment,gsis.count as gsis_paid,gsis.amount as gsis_amount," +
                            "phic.max as phic_payment,phic.count as phic_paid,phic.amount as phic_amount," +
                            "excess.max as excess_payment,excess.count as excess_paid,excess.amount as excess_amount," +
                            "w.monthly_salary,p.userid,p.fname,p.lname,p.mname,p.job_status,p.designation_id," +
                            "p.salary_charge,d.description as division,e.description as designation FROM pis.personal_information p " +
                            "LEFT JOIN pis.work_experience w ON w.userid = p.userid AND w.date_to = 'Present'" +
                            "LEFT JOIN dts.division d ON d.id = p.division_id " +
                            "LEFT JOIN dts.designation e ON e.id = p.designation_id " +
                            "LEFT JOIN payroll.coop_remittance coop ON coop.userid = p.userid " +
                            "LEFT JOIN payroll.pagibig_remittance pagibig ON pagibig.userid = p.userid " +
                            "LEFT JOIN payroll.gsis_remittance gsis ON gsis.userid = p.userid " +
                            "LEFT JOIN payroll.phic_remittance phic ON phic.userid = p.userid " +
                            "LEFT JOIN payroll.excess_remittance excess ON excess.userid = p.userid " +
                            "WHERE p.job_status = 'Job Order' AND p.employee_status = '1' AND (p.position <> 'Health Aiders' OR p.position IS NULL) AND p.disbursement_type = '" + Disbursement + "'";

            if (!SearchString.Equals(""))
            {
                query = query + " AND (p.fname LIKE '%" + SearchString + "%' OR p.lname LIKE '%" + SearchString + "%' OR p.userid LIKE '%" + SearchString + "%')";
            }

            query = query + " ORDER BY p.lname,p.fname";

            List<JobOrderModel> list = new List<JobOrderModel>();
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
                            string mname = dataReader["mname"].ToString();
                            string designation = dataReader["designation"].ToString();
                            string salary = dataReader["monthly_salary"].ToString();
                            string salary_charge = dataReader["salary_charge"].ToString();
                            string division = dataReader["division"].ToString();

                            //COOP REMITTANCE
                            string coop_amount = (!dataReader.IsDBNull(dataReader.GetOrdinal("coop_amount"))) ? dataReader["coop_amount"].ToString() : "0.00";
                            string coop_payment = (!dataReader.IsDBNull(dataReader.GetOrdinal("coop_payment"))) ? dataReader["coop_payment"].ToString() : "0";
                            string coop_paid = (!dataReader.IsDBNull(dataReader.GetOrdinal("coop_paid"))) ? dataReader["coop_paid"].ToString() : "0";
                            Remittance coop = new Remittance("0", userid, coop_payment, coop_paid, coop_amount);

                            //PAGIBIG REMITTANCE
                            string pagibig_amount = (!dataReader.IsDBNull(dataReader.GetOrdinal("pagibig_amount"))) ? dataReader["pagibig_amount"].ToString() : "0.00";
                            string pagibig_payment = (!dataReader.IsDBNull(dataReader.GetOrdinal("pagibig_payment"))) ? dataReader["pagibig_payment"].ToString() : "0";
                            string pagibig_paid = (!dataReader.IsDBNull(dataReader.GetOrdinal("pagibig_paid"))) ? dataReader["pagibig_paid"].ToString() : "0";
                            Remittance pagibig = new Remittance("0", userid, pagibig_payment, pagibig_paid, pagibig_amount);

                            //GSIS REMITTANCE
                            string gsis_amount = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_amount"))) ? dataReader["gsis_amount"].ToString() : "0.00";
                            string gsis_payment = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_payment"))) ? dataReader["gsis_payment"].ToString() : "0";
                            string gsis_paid = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_paid"))) ? dataReader["gsis_paid"].ToString() : "0";
                            Remittance gsis = new Remittance("0", userid, gsis_payment, gsis_paid, gsis_amount);

                            //PHIC REMITTANCE
                            string phic_amount = (!dataReader.IsDBNull(dataReader.GetOrdinal("phic_amount"))) ? dataReader["phic_amount"].ToString() : "0.00";
                            string phic_payment = (!dataReader.IsDBNull(dataReader.GetOrdinal("phic_payment"))) ? dataReader["phic_payment"].ToString() : "0";
                            string phic_paid = (!dataReader.IsDBNull(dataReader.GetOrdinal("phic_paid"))) ? dataReader["phic_paid"].ToString() : "0";
                            Remittance phic = new Remittance("0", userid, phic_payment, phic_paid, phic_amount);

                            //EXCESS REMITTANCE
                            string excess_amount = (!dataReader.IsDBNull(dataReader.GetOrdinal("excess_amount"))) ? dataReader["excess_amount"].ToString() : "0.00";
                            string excess_payment = (!dataReader.IsDBNull(dataReader.GetOrdinal("excess_payment"))) ? dataReader["excess_payment"].ToString() : "0";
                            string excess_paid = (!dataReader.IsDBNull(dataReader.GetOrdinal("excess_paid"))) ? dataReader["excess_paid"].ToString() : "0";
                            Remittance excess = new Remittance("0", userid, excess_payment, excess_paid, excess_amount);

                            Employee employee = new Employee(userid, fname, lname, mname, designation, salary, salary_charge, division, Disbursement);
                            JobOrderModel jobOrderModel = new JobOrderModel(employee);

                            jobOrderModel.SetCoop(coop);
                            jobOrderModel.SetPagibig(pagibig);
                            jobOrderModel.SetGsis(gsis);
                            jobOrderModel.SetPhic(phic);
                            jobOrderModel.SetExcess(excess);

                            list.Add(jobOrderModel);
                        }
                        dataReader.Close();
                }
                SqlConnection.Close();
            }
            return list;
        }

        public List<RegularModel> GetRegularEmployee(string SearchString)
        {

            string query = "SELECT tax.tax,cfi.max as cfi_payment,cfi.count as cfi_paid,cfi.amount as cfi_amount," +
                            "gsis_consoloan.max as gsis_consoloan_payment,gsis_consoloan.count as gsis_consoloan_paid,gsis_consoloan.amount as gsis_consoloan_amount," +
                            "gsis_policyloan.max as gsis_policyloan_payment,gsis_policyloan.count as gsis_policyloan_paid,gsis_policyloan.amount as gsis_policyloan_amount," +
                            "gsis_eml.max as gsis_eml_payment,gsis_eml.count as gsis_eml_paid,gsis_eml.amount as gsis_eml_amount," +
                            "gsis_uoli.max as gsis_uoli_payment,gsis_uoli.count as gsis_uoli_paid,gsis_uoli.amount as gsis_uoli_amount," +
                            "gsis_edu.max as gsis_edu_payment,gsis_edu.count as gsis_edu_paid,gsis_edu.amount as gsis_edu_amount," +
                            "gsis_help.max as gsis_help_payment,gsis_help.count as gsis_help_paid,gsis_help.amount as gsis_help_amount," +
                            "rel.max as rel_payment,rel.count as rel_paid,rel.amount as rel_amount," +
                            "pagibig_loan.max as pagibig_loan_payment,pagibig_loan.count as pagibig_loan_paid,pagibig_loan.amount as pagibig_loan_amount," +
                            "pagibig_mp2.max as pagibig_mp2_payment,pagibig_mp2.count as pagibig_mp2_paid,pagibig_mp2.amount as pagibig_mp2_amount," +
                            "simc.max as simc_payment,simc.count as simc_paid,simc.amount as simc_amount," +
                            "hwmpc.max as hwmpc_payment,hwmpc.count as hwmpc_paid,hwmpc.amount as hwmpc_amount," +
                            "dbp.max as dbp_payment,dbp.count as dbp_paid,dbp.amount as dbp_amount," +
                            "disallowances.max as disallowances_payment,disallowances.count as disallowances_paid,disallowances.amount as disallowances_amount," +
                            "w.monthly_salary,p.userid,p.fname,p.lname,p.mname,p.job_status,p.designation_id," +
                            "p.salary_charge,d.description as division,e.description as designation FROM pis.personal_information p " +
                            "LEFT JOIN pis.work_experience w ON w.userid = p.userid AND w.date_to = 'Present'" +
                            "LEFT JOIN dts.division d ON d.id = p.division_id " +
                            "LEFT JOIN dts.designation e ON e.id = p.designation_id " +
                            "LEFT JOIN payroll.cfi_remittance cfi ON cfi.userid = p.userid " +
                            "LEFT JOIN payroll.gsis_consoloan_remittance gsis_consoloan ON gsis_consoloan.userid = p.userid " +
                            "LEFT JOIN payroll.gsis_policyloan_remittance gsis_policyloan ON gsis_policyloan.userid = p.userid " +
                            "LEFT JOIN payroll.gsis_eml_remittance gsis_eml ON gsis_eml.userid = p.userid " +
                            "LEFT JOIN payroll.gsis_uoli_remittance gsis_uoli ON gsis_uoli.userid = p.userid " +
                            "LEFT JOIN payroll.gsis_edu_remittance gsis_edu ON gsis_edu.userid = p.userid " +
                            "LEFT JOIN payroll.gsis_help_remittance gsis_help ON gsis_help.userid = p.userid " +
                            "LEFT JOIN payroll.rel_remittance rel ON rel.userid = p.userid " +
                            "LEFT JOIN payroll.pagibig_loan_remittance pagibig_loan ON pagibig_loan.userid = p.userid " +
                            "LEFT JOIN payroll.pagibig_mp2_remittance pagibig_mp2 ON pagibig_mp2.userid = p.userid " +
                            "LEFT JOIN payroll.simc_remittance simc ON simc.userid = p.userid " +
                            "LEFT JOIN payroll.coop_remittance hwmpc ON hwmpc.userid = p.userid " +
                            "LEFT JOIN payroll.dbp_remittance dbp ON dbp.userid = p.userid " +
                            "LEFT JOIN payroll.disallowance_remittance disallowances ON disallowances.userid = p.userid " +
                            "LEFT JOIN payroll.tax_remittance tax ON tax.userid = p.userid " +
                            "WHERE p.job_status = 'Permanent' AND p.employee_status = '1' AND (p.position <> 'Health Aiders' OR p.position IS NULL) AND p.disbursement_type = 'ATM'";

            if (!SearchString.Equals(""))
            {
                query = query + " AND (p.fname LIKE '%" + SearchString + "%' OR p.lname LIKE '%" + SearchString + "%' OR p.userid LIKE '%" + SearchString + "%')";
            }

            query = query + " ORDER BY p.lname,p.fname";

            List<RegularModel> list = new List<RegularModel>();

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
                            string mname = dataReader["mname"].ToString();
                            string designation = dataReader["designation"].ToString();
                            string salary = dataReader["monthly_salary"].ToString();
                            string salary_charge = dataReader["salary_charge"].ToString();
                            string division = dataReader["division"].ToString();

                            decimal tax = (!dataReader.IsDBNull(dataReader.GetOrdinal("tax"))) ? decimal.Parse(dataReader["tax"].ToString()) : 0;

                            string cfi_amount = (!dataReader.IsDBNull(dataReader.GetOrdinal("cfi_amount"))) ? dataReader["cfi_amount"].ToString() : "0.00";
                            string cfi_payment = (!dataReader.IsDBNull(dataReader.GetOrdinal("cfi_payment"))) ? dataReader["cfi_payment"].ToString() : "0";
                            string cfi_paid = (!dataReader.IsDBNull(dataReader.GetOrdinal("cfi_paid"))) ? dataReader["cfi_paid"].ToString() : "0";
                            Remittance cfi = new Remittance("0", userid, cfi_payment, cfi_paid, cfi_amount);

                            string gsis_consoloan_amount = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_consoloan_amount"))) ? dataReader["gsis_consoloan_amount"].ToString() : "0.00";
                            string gsis_consoloan_payment = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_consoloan_payment"))) ? dataReader["gsis_consoloan_payment"].ToString() : "0";
                            string gsis_consoloan_paid = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_consoloan_paid"))) ? dataReader["gsis_consoloan_paid"].ToString() : "0";
                            Remittance gsis_consoloan = new Remittance("0", userid, gsis_consoloan_payment, gsis_consoloan_paid, gsis_consoloan_amount);

                            string gsis_policyloan_amount = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_policyloan_amount"))) ? dataReader["gsis_policyloan_amount"].ToString() : "0.00";
                            string gsis_policyloan_payment = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_policyloan_payment"))) ? dataReader["gsis_policyloan_payment"].ToString() : "0";
                            string gsis_policyloan_paid = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_policyloan_paid"))) ? dataReader["gsis_policyloan_paid"].ToString() : "0";
                            Remittance gsis_policyloan = new Remittance("0", userid, gsis_policyloan_payment, gsis_policyloan_paid, gsis_policyloan_amount);

                            string gsis_eml_amount = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_eml_amount"))) ? dataReader["gsis_eml_amount"].ToString() : "0.00";
                            string gsis_eml_payment = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_eml_payment"))) ? dataReader["gsis_eml_payment"].ToString() : "0";
                            string gsis_eml_paid = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_eml_paid"))) ? dataReader["gsis_eml_paid"].ToString() : "0";
                            Remittance gsis_eml = new Remittance("0", userid, gsis_eml_payment, gsis_eml_paid, gsis_eml_amount);

                            string gsis_uoli_amount = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_uoli_amount"))) ? dataReader["gsis_uoli_amount"].ToString() : "0.00";
                            string gsis_uoli_payment = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_uoli_payment"))) ? dataReader["gsis_uoli_payment"].ToString() : "0";
                            string gsis_uoli_paid = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_uoli_paid"))) ? dataReader["gsis_uoli_paid"].ToString() : "0";
                            Remittance gsis_uoli = new Remittance("0", userid, gsis_uoli_payment, gsis_uoli_paid, gsis_uoli_amount);

                            string gsis_edu_amount = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_edu_amount"))) ? dataReader["gsis_edu_amount"].ToString() : "0.00";
                            string gsis_edu_payment = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_edu_payment"))) ? dataReader["gsis_edu_payment"].ToString() : "0";
                            string gsis_edu_paid = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_edu_paid"))) ? dataReader["gsis_edu_paid"].ToString() : "0";
                            Remittance gsis_edu = new Remittance("0", userid, gsis_edu_payment, gsis_edu_paid, gsis_edu_amount);

                            string gsis_help_amount = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_help_amount"))) ? dataReader["gsis_help_amount"].ToString() : "0.00";
                            string gsis_help_payment = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_help_payment"))) ? dataReader["gsis_help_payment"].ToString() : "0";
                            string gsis_help_paid = (!dataReader.IsDBNull(dataReader.GetOrdinal("gsis_help_paid"))) ? dataReader["gsis_help_paid"].ToString() : "0";
                            Remittance gsis_help = new Remittance("0", userid, gsis_help_payment, gsis_help_paid, gsis_help_amount);

                            string rel_amount = (!dataReader.IsDBNull(dataReader.GetOrdinal("rel_amount"))) ? dataReader["rel_amount"].ToString() : "0.00";
                            string rel_payment = (!dataReader.IsDBNull(dataReader.GetOrdinal("rel_payment"))) ? dataReader["rel_payment"].ToString() : "0";
                            string rel_paid = (!dataReader.IsDBNull(dataReader.GetOrdinal("rel_paid"))) ? dataReader["rel_paid"].ToString() : "0";
                            Remittance rel = new Remittance("0", userid, rel_payment, rel_paid, rel_amount);

                            string pagibig_loan_amount = (!dataReader.IsDBNull(dataReader.GetOrdinal("pagibig_loan_amount"))) ? dataReader["pagibig_loan_amount"].ToString() : "0.00";
                            string pagibig_loan_payment = (!dataReader.IsDBNull(dataReader.GetOrdinal("pagibig_loan_payment"))) ? dataReader["pagibig_loan_payment"].ToString() : "0";
                            string pagibig_loan_paid = (!dataReader.IsDBNull(dataReader.GetOrdinal("pagibig_loan_paid"))) ? dataReader["pagibig_loan_paid"].ToString() : "0";
                            Remittance pagibig_loan = new Remittance("0", userid, pagibig_loan_payment, pagibig_loan_paid, pagibig_loan_amount);

                            string pagibig_mp2_amount = (!dataReader.IsDBNull(dataReader.GetOrdinal("pagibig_mp2_amount"))) ? dataReader["pagibig_mp2_amount"].ToString() : "0.00";
                            string pagibig_mp2_payment = (!dataReader.IsDBNull(dataReader.GetOrdinal("pagibig_mp2_payment"))) ? dataReader["pagibig_mp2_payment"].ToString() : "0";
                            string pagibig_mp2_paid = (!dataReader.IsDBNull(dataReader.GetOrdinal("pagibig_mp2_paid"))) ? dataReader["pagibig_mp2_paid"].ToString() : "0";
                            Remittance pagibig_mp2 = new Remittance("0", userid, pagibig_mp2_payment, pagibig_mp2_paid, pagibig_mp2_amount);

                            string simc_amount = (!dataReader.IsDBNull(dataReader.GetOrdinal("simc_amount"))) ? dataReader["simc_amount"].ToString() : "0.00";
                            string simc_payment = (!dataReader.IsDBNull(dataReader.GetOrdinal("simc_payment"))) ? dataReader["simc_payment"].ToString() : "0";
                            string simc_paid = (!dataReader.IsDBNull(dataReader.GetOrdinal("simc_paid"))) ? dataReader["simc_paid"].ToString() : "0";
                            Remittance simc = new Remittance("0", userid, simc_payment, simc_paid, simc_amount);

                            string hwmpc_amount = (!dataReader.IsDBNull(dataReader.GetOrdinal("hwmpc_amount"))) ? dataReader["hwmpc_amount"].ToString() : "0.00";
                            string hwmpc_payment = (!dataReader.IsDBNull(dataReader.GetOrdinal("hwmpc_payment"))) ? dataReader["hwmpc_payment"].ToString() : "0";
                            string hwmpc_paid = (!dataReader.IsDBNull(dataReader.GetOrdinal("hwmpc_paid"))) ? dataReader["hwmpc_paid"].ToString() : "0";
                            Remittance hwmpc = new Remittance("0", userid, hwmpc_payment, hwmpc_paid, hwmpc_amount);

                            string dbp_amount = (!dataReader.IsDBNull(dataReader.GetOrdinal("dbp_amount"))) ? dataReader["dbp_amount"].ToString() : "0.00";
                            string dbp_payment = (!dataReader.IsDBNull(dataReader.GetOrdinal("dbp_payment"))) ? dataReader["dbp_payment"].ToString() : "0";
                            string dbp_paid = (!dataReader.IsDBNull(dataReader.GetOrdinal("dbp_paid"))) ? dataReader["dbp_paid"].ToString() : "0";
                            Remittance dbp = new Remittance("0", userid, dbp_payment, dbp_paid, dbp_amount);

                            string disallowances_amount = (!dataReader.IsDBNull(dataReader.GetOrdinal("disallowances_amount"))) ? dataReader["disallowances_amount"].ToString() : "0.00";
                            string disallowances_payment = (!dataReader.IsDBNull(dataReader.GetOrdinal("disallowances_payment"))) ? dataReader["disallowances_payment"].ToString() : "0";
                            string disallowances_paid = (!dataReader.IsDBNull(dataReader.GetOrdinal("disallowances_paid"))) ? dataReader["disallowances_paid"].ToString() : "0";
                            Remittance disallowances = new Remittance("0", userid, disallowances_payment, disallowances_paid, disallowances_amount);

                            Employee employee = new Employee(userid, fname, lname, mname, designation, salary, salary_charge, division, "ATM");

                            RegularModel regularModel = new RegularModel(employee);
                            regularModel.SetCfi(cfi);
                            regularModel.SetGsisConsoloan(gsis_consoloan);
                            regularModel.SetGsisPolicyLoan(gsis_policyloan);
                            regularModel.SetGsisEml(gsis_eml);
                            regularModel.SetGsisUoli(gsis_uoli);
                            regularModel.SetGsisEdu(gsis_edu);
                            regularModel.SetGsisHelp(gsis_help);
                            regularModel.SetGsisRel(rel);
                            regularModel.SetPagibigLoan(pagibig_loan);
                            regularModel.SetPagibigMp2(pagibig_mp2);
                            regularModel.SetSimc(simc);
                            regularModel.SetHwmpc(hwmpc);
                            regularModel.SetDbp(dbp);
                            regularModel.SetDisallowances(disallowances);
                            regularModel.SetTax(tax);

                            list.Add(regularModel);
                        }
                        dataReader.Close();
                }
                SqlConnection.Close();
            }
            return list;
        }

        public string GetSalary(string tranch, string grade, string step)
        {
            var query = "SELECT salary_amount as salary FROM pis.salary_grade WHERE salary_tranche = '" + tranch + "' AND salary_grade = '" + grade + "' AND salary_step = '" + step + "'";

            string salary = "0.00";

            using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
            {
                SqlConnection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                {
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        salary = dataReader["salary"].ToString();
                    }
                    dataReader.Close();
                }
                SqlConnection.Close();
            }
            return salary;
        }

        public bool UpdateEmployeeSalary(string salary, string userid, string tranch, string grade, string step)
        {
            var salaryGrade = tranch + " | " + grade + "-" + step;
            string query = "UPDATE work_experience SET monthly_salary = '" + salary + "', salary_grade = '" + salaryGrade + "' WHERE userid = '" + userid + "'";

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

        public Employee GetEmployeeByID(string id)
        {
            Employee employee = null;
            string query = "SELECT p.userid,p.fname,p.lname,p.mname,p.job_status,p.designation_id,p.salary_charge,d.description as division,e.description as designation FROM pis.personal_information p " +
                            "LEFT JOIN pis.work_experience w ON w.userid = p.userid AND w.date_to = 'Present'" +
                            "LEFT JOIN dts.division d ON d.id = p.division_id " +
                            "LEFT JOIN dts.designation e ON e.id = p.designation_id " +
                            "WHERE userid = '" + id + "'";
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
                            string mname = dataReader["mname"].ToString();
                            string designation = dataReader["designation"].ToString();
                            string salary = dataReader["monthly_salary"].ToString();
                            string salary_charge = dataReader["salary_charge"].ToString();

                            string division = dataReader["division"].ToString();

                            employee = new Employee(userid, fname, lname, mname, designation, salary, salary_charge, division, "");
                        }
                        dataReader.Close();
                }
                SqlConnection.Close();
            }
            return employee;
        }
     }
}