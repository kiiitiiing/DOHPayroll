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
    public class PISDatabase
    {
        public static string server;
        public static string database;
        public static string uid;
        public static string password;
        public static MySqlConnection SqlConnection = null;

        private static PISDatabase instance;

        private PISDatabase(){}

        //Singleton Pattern
        public static PISDatabase Instance

        {
            get
            {
                if (instance == null)
                {
                    instance = new PISDatabase();
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
                database = "pis";
                uid = "doh7payroll";
                password = "doh7payroll";
                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                        database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + "; pooling = false; SslMode=none; convert zero datetime=True";

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
            catch { }
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
            catch { }
            return false;
        }

        public string SafeGetString(MySqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }

        public List<Employee> GetEmployee(string SearchString, string JobStatus, string Disbursement)
        {
            
            string query = "SELECT coop.max as coop_payment,coop.count as coop_paid,coop.amount as coop_amount,"+
                           "pagibig.max as pagibig_payment,pagibig.count as pagibig_paid,pagibig.amount as pagibig_amount,"+
                           "gsis.max as gsis_payment,gsis.count as gsis_paid,gsis.amount as gsis_amount,"+
                           "phic.max as phic_payment,phic.count as phic_paid,phic.amount as phic_amount,"+
                           "excess.max as excess_payment,excess.count as excess_paid,excess.amount as excess_amount,"+
                           "w.monthly_salary,p.userid,p.fname,p.lname,p.mname,p.job_status,p.designation_id,"+
                           "p.salary_charge,d.description as division,e.description as designation FROM pis.personal_information p " +
                           "LEFT JOIN pis.work_experience w ON w.userid = p.userid AND w.date_to = 'Present'" +
                           "LEFT JOIN dts.division d ON d.id = p.division_id " +
                           "LEFT JOIN dts.designation e ON e.id = p.designation_id " +
                           "LEFT JOIN payroll.coop_remittance coop ON coop.userid = p.userid " +
                           "LEFT JOIN payroll.pagibig_remittance pagibig ON pagibig.userid = p.userid " +
                           "LEFT JOIN payroll.gsis_remittance gsis ON gsis.userid = p.userid " +
                           "LEFT JOIN payroll.phic_remittance phic ON phic.userid = p.userid " +
                           "LEFT JOIN payroll.excess_remittance excess ON excess.userid = p.userid " +
                           "WHERE p.job_status = '" + JobStatus + "' AND p.employee_status = 'Active' AND (p.position <> 'Health Aiders' OR p.position IS NULL) AND p.disbursement_type = '"+ Disbursement + "'";

            if (!SearchString.Equals(""))
            { 
                query = query + " AND (p.fname LIKE '" + SearchString + "%' OR p.lname LIKE '" + SearchString + "%' OR p.userid LIKE '" + SearchString + "%')";  
            }
            
            query = query + " ORDER BY p.lname,p.fname";

            //Create a list to store the result
            List<Employee> list = new List<Employee>();


            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
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

                    Employee employee = new Employee(userid, fname, lname, mname, designation,salary, salary_charge, division, Disbursement);
                    employee.SetCoop(coop);
                    employee.SetPagibig(pagibig);
                    employee.SetGsis(gsis);
                    employee.SetPhic(phic);
                    employee.SetExcess(excess);

                    list.Add(employee);
                }
                //close Data Reader
                dataReader.Close();
                this.CloseConnection();

                //return list to be displayed

            }
            return list;
        }

        public Employee GetEmployeeByID(string id)
        {
            Employee employee = null;
            string query = "SELECT p.userid,p.fname,p.lname,p.mname,p.job_status,p.designation_id,p.salary_charge,d.description as division,e.description as designation FROM pis.personal_information p " +
                           "LEFT JOIN pis.work_experience w ON w.userid = p.userid AND w.date_to = 'Present'" +
                           "LEFT JOIN dts.division d ON d.id = p.division_id " +
                           "LEFT JOIN dts.designation e ON e.id = p.designation_id " +
                           "WHERE userid = '" + id+ "'";
            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
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
                        
                        employee = new Employee(userid, fname, lname, mname, designation,salary, salary_charge, division, "");
                       
                    }
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


    }
}