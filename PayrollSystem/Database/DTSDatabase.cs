using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace PayrollSystem.Database
{
    public sealed class DTSDatabase
    {
        public static string server;
        public static string database;
        public static string uid;
        public static string password;
        public MySqlConnection SqlConnection = null;

        private static DTSDatabase instance;

        private DTSDatabase() { }

        //Singleton Pattern
        public static DTSDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DTSDatabase();
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
                database = "dts";
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

        public string GetDivisionNameByID(string id)
        {
            String division = "";
            String query = "SELECT description FROM division WHERE id = '" + id + "'";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, SqlConnection); 
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    division = dataReader["description"].ToString().Split('-')[0];
                }
                dataReader.Close();
                this.CloseConnection();
            }
            return division;
        }

        public string GetDesignationName(string id)
        {
            string name = "";
            string query = "SELECT description FROM dts.designation WHERE id = '" + id + "' LIMIT 1";
            if (this.OpenConnection() == true)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, SqlConnection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        name = dataReader["description"].ToString();
                    }
                    dataReader.Close();
                    this.CloseConnection();
                }
                catch (MySqlException e)
                {
                    this.CloseConnection();
                }
            }
            return name;
        }
    }
}