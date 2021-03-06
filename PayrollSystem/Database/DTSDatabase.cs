﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace PayrollSystem.Database
{
     public sealed class DTSDatabase
     {

          public static string connectionString;
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
                         instance.Initialize(0);

                    }
                    return instance;
               }
          }

          public void Initialize(int index)
          {
            string server = InitializeConnectionString.Servers[index];
            string database = "dts";
            string uid = InitializeConnectionString.UIDs[index];
            string password = InitializeConnectionString.Password[index];
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + "; pooling = false; SslMode=none; convert zero datetime=True";
          }


          public string GetDivisionNameByID(string id)
          {
               string division = "";
               string query = "SELECT description FROM division WHERE id = '" + id + "'";
               using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
               {
                    SqlConnection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                    {
                         MySqlDataReader dataReader = cmd.ExecuteReader();
                         while (dataReader.Read())
                         {
                              division = dataReader["description"].ToString().Split('-')[0];
                         }
                         dataReader.Close();
                    }
                    SqlConnection.Close();
               }

               return division;
          }

          public string GetDesignationName(string id)
          {
               string name = "";
               string query = "SELECT description FROM dts.designation WHERE id = '" + id + "' LIMIT 1";
               using (MySqlConnection SqlConnection = new MySqlConnection(connectionString))
               {
                    SqlConnection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, SqlConnection))
                    {
                         MySqlDataReader dataReader = cmd.ExecuteReader();
                         while (dataReader.Read())
                         {
                              name = dataReader["description"].ToString();
                         }
                         dataReader.Close();
                    }
                    SqlConnection.Close();
               }
               return name;
          }
     }
}