using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
     public class DateUtility
     {
          public static string GetMonthName(int month, int year)
          {
               return new DateTime(year, month, 1).ToString("MMM");
          }
          public static int GetMaximumDayOfMonth(int month, int year)
          {
               return DateTime.DaysInMonth(year, month);
          }
     }
}