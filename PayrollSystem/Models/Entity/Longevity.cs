using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
     public class Longevity
     {
          public string ID { get; set; }
          public string PersonnelID { get; set; }
          public decimal Salary1 { get; set; }
          public decimal Salary2 { get; set; }
          public decimal Salary3 { get; set; }
          public decimal Salary4 { get; set; }
          public decimal Salary5 { get; set; }
          public decimal Disallowance { get; set; }
          public int Month { get; set; }
          public int Year { get; set; }
          


          public Longevity(string ID, string PersonnelID, decimal Salary1, decimal Salary2, decimal Salary3, decimal Salary4, decimal Salary5, decimal Disallowance, int Month, int Year)
          {
               this.ID = ID;
               this.PersonnelID = PersonnelID;
               this.Salary1 = Salary1;
               this.Salary2 = Salary2;
               this.Salary3 = Salary3;
               this.Salary4 = Salary4;
               this.Salary5 = Salary5;
               this.Disallowance = Disallowance;
               this.Month = Month;
               this.Year = Year;
          }
     }
}