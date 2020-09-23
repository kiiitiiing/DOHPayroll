using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Database
{
    public static class InitializeConnectionString
    {
        public static string[] Servers = { "localhost", "192.168.110.17" };
        public static string[] UIDs = { "root", "doh7payroll" };
        public static string[] Password = { "admin", "doh7payroll" };
    }
}