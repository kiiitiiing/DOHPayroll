using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
     public class JobOrderPayrollModel
     {
          public string Id { get; set; }
          public Employee Employee { get; set; }
          public string WorkDays { get; set; }
          public string StartDate { get; set; }
          public string EndDate { get; set; }
          public string Salary { get; set; }
          public string Adjustment { get; set; }
          public string Remarks { get; set; }
          public string DaysAbsent { get; set; }
          public string MinutesLate { get; set; }
          public string Coop { get; set; }
          public string Phic { get; set; }
          public string Disallowance { get; set; }
          public string Gsis { get; set; }
          public string Pagibig { get; set; }
          public string ExcessMobile { get; set; }
          public string Tax { get; set; }
          public string OtherAdjustment { get; set; }

          public string Flag { get; set; }

          public JobOrderPayrollModel() { }

          public JobOrderPayrollModel(string Id,string UserID, string StartDate, string EndDate, string Adjustment, string WorkDays, string DaysAbsent, string Salary, string MinutesLate, string Coop,
              string Phic, string Disallowance, string Gsis, string Pagibig, string ExcessMobile, string Remarks, string Tax, string OtherAdjustment)
          {
               this.Id = Id;
               this.Employee = new Employee();
               this.Employee.ID = UserID;
               this.StartDate = StartDate;
               this.EndDate = EndDate;
               this.Adjustment = Adjustment;
               this.WorkDays = WorkDays;
               this.DaysAbsent = DaysAbsent;
               this.Salary = Salary;
               this.MinutesLate = MinutesLate;
               this.Coop = Coop;
               this.Phic = Phic;
               this.Disallowance = Disallowance;
               this.Gsis = Gsis;
               this.Pagibig = Pagibig;
               this.ExcessMobile = ExcessMobile;
               this.Remarks = Remarks;
               this.Tax = Tax;
               this.OtherAdjustment = OtherAdjustment;
          }


          public JobOrderPayrollModel(string Id, Employee Employee, string StartDate, string EndDate, string Adjustment, string WorkDays, string DaysAbsent, string Salary, string MinutesLate, string Coop,
              string Phic, string Disallowance, string Gsis, string Pagibig, string ExcessMobile, string Remarks, string Flag, string Tax, string OtherAdjustment)
          {
               this.Id = Id;
               this.Employee = Employee;
               this.StartDate = StartDate;
               this.EndDate = EndDate;
               this.WorkDays = WorkDays;
               this.Salary = Salary;
               this.MinutesLate = MinutesLate;
               this.Coop = Coop;
               this.Phic = Phic;
               this.Disallowance = Disallowance;
               this.Gsis = Gsis;
               this.Pagibig = Pagibig;
               this.ExcessMobile = ExcessMobile;
               this.Flag = Flag;
               this.Adjustment = Adjustment;
               this.Remarks = Remarks;
               this.DaysAbsent = DaysAbsent;
               this.Tax = Tax;
               this.OtherAdjustment = OtherAdjustment;
          }

     }
}