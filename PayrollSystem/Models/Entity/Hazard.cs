using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
     public class Hazard
     {
          public string ID { get; set; }
          public string PersonnelID { get; set; }
          public decimal Pay { get; set; }
          public decimal HWMPC { get; set; }
          public decimal Mortuary { get; set; }
          public decimal DigitelBilling { get; set; }
          public int Month { get; set; }
          public int Year { get; set; }
          public int DaysLeave { get; set; }
          public int DaysOO { get; set; }

          public Hazard(string ID, string PersonnelID, decimal Pay, decimal HWMPC, decimal Mortuary, decimal DigitelBilling, int Month, int Year, int DaysLeave, int DaysOO)
          {
               this.ID = ID;
               this.PersonnelID = PersonnelID;
               this.Pay = Pay;
               this.HWMPC = HWMPC;
               this.Mortuary = Mortuary;
               this.DigitelBilling = DigitelBilling;
               this.Month = Month;
               this.Year = Year;
               this.DaysLeave = DaysLeave;
               this.DaysOO = DaysOO;
          }
          public decimal GetNetAmount
          {
               get
               {
                    return Pay + HWMPC - Mortuary - DigitelBilling;
               }
          }

     }
}