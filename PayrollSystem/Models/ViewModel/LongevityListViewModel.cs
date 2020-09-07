using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
     public class LongevityListViewModel
     {
          public string Id { get; set; }
          public string Userid { get; set; }
          public string Firstname { get; set; }
          public string Lastname { get; set; }
          public int Month { get; set; }
          public int Year { get; set; }
          public decimal NetAmount { get; set; }

          public LongevityListViewModel(string Id,string Userid,string Firstname,string Lastname, int Month, int Year, decimal NetAmount)
          {
               this.Id = Id;
               this.Userid = Userid;
               this.Firstname = Firstname;
               this.Lastname = Lastname;
               this.Month = Month;
               this.Year = Year;
               this.NetAmount = NetAmount;
          }
          public string GetMonth() {
               return DateUtility.GetMonthName(Month,Year);
          }

          public string GetNetAmount()
          {
               return ValueFormatterUtility.ConvertToCurrencyFormat(NetAmount,false);
          }

     }
}