using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
     public class Cellphone
     {
          public string ID { get; set; }
          public string PersonnelID { get; set; }
          public decimal Amount { get; set; }
          public decimal Less { get; set; }
          public int Month { get; set; }
          public int Year { get; set; }
          public string Remarks { get; set; }


          public Cellphone(string ID, string PersonnelID, decimal Amount, decimal Less,int Month, int Year,string Remarks)
          {
               this.ID = ID;
               this.PersonnelID = PersonnelID;
               this.Amount = Amount;
               this.Less = Less;
               this.Month = Month;
               this.Year = Year;
               this.Remarks = Remarks;
          }
     }
}