using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
     public class Rata
     {
          public string ID { get; set; }
          public string PersonnelID { get; set; }
          public decimal Ra { get; set; }
          public decimal Ta { get; set; }
          public decimal Deduction { get; set; }
          public int Month { get; set; }
          public int Year { get; set; }
          public string Remarks { get; set; }


          public Rata(string ID, string PersonnelID, decimal Ra, decimal Ta, decimal Deduction,int Month, int Year,string Remarks)
          {
               this.ID = ID;
               this.PersonnelID = PersonnelID;
               this.Ra = Ra;
               this.Ta = Ta;
               this.Deduction = Deduction;
               this.Month = Month;
               this.Year = Year;
               this.Remarks = Remarks;
          }
     }
}