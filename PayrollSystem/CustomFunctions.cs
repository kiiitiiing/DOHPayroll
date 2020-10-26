using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem
{
    public static class CustomFunctions
    {
        public static string CheckValue(this string value, string defaultVal)
        {
            if (string.IsNullOrEmpty(value))
                return defaultVal;
            else
                return value;
        }

        public static string SetDtrUrl(string id)
        {
            return "http://192.168.81.7/dtr_api/logs/GetLogs/" + id;
        }
    }
}