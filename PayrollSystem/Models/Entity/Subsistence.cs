using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
     public class Subsistence
     {
          public string ID { get; set; }
          public string PersonnelID { get; set; }
          public decimal SubsistenceAllowance { get; set; }
          public decimal LaundryAllowance { get; set; }
          public decimal Absences { get; set; }
          public decimal Hwmpc { get; set; }
          public int NoDays { get; set; }
          public string Remarks { get; set; }
          public int Month { get; set; }
          public int Year { get; set; }
         

          public Subsistence(string ID, string PersonnelID, decimal SubsistenceAllowance, decimal LaundryAllowance, decimal Absences, decimal Hwmpc,int NoDays,string Remarks, int Month, int Year)
          {
               this.ID = ID;
               this.PersonnelID = PersonnelID;
               this.SubsistenceAllowance = SubsistenceAllowance;
               this.LaundryAllowance = LaundryAllowance;
               this.Absences = Absences;
               this.Hwmpc = Hwmpc;
               this.NoDays = NoDays;
               this.Remarks = Remarks;
               this.Month = Month;
               this.Year = Year;
              
          }
          public decimal GetNetAmount
          {
               get
               {
                    return SubsistenceAllowance + LaundryAllowance - Absences - Hwmpc;
               }
          }

     }
}