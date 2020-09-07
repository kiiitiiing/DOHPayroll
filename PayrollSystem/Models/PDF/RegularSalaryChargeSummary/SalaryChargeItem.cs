using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
     public class SalaryChargeItem
     {
          public string ID { get; set; }
          public string Fullname { get; set; }
          public string Designation { get; set; }
          public RegularPayslipModel Payslip { get; set; }

          public SalaryChargeItem(string ID, string Fullname, string Designation, RegularPayslipModel Payslip)
          {
               this.ID = ID;
               this.Fullname = Fullname;
               this.Payslip = Payslip;
          }
     }
}