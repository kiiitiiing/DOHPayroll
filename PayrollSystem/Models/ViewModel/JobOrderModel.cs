using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
     public class JobOrderModel : Employee
     {
          public Remittance Coop { get; set; }
          public Remittance Pagibig { get; set; }
          public Remittance Gsis { get; set; }
          public Remittance Phic { get; set; }
          public Remittance Excess { get; set; }


          public JobOrderModel(Employee employee)
          {
               this.ID = employee.ID;
               this.Firstname = employee.Firstname;
               this.Lastname = employee.Lastname;
               this.Middlename = employee.Middlename;
               this.Designation = employee.Designation;
               this.Salary = employee.Salary;
               this.SalaryCharge = employee.SalaryCharge;
               this.Division = employee.Division;
               this.DisbursementType = employee.DisbursementType;
          }

          public void SetCoop(Remittance Coop)
          {
               this.Coop = Coop;
          }

          public Remittance GetCoop()
          {
               return Coop;
          }

          public void SetPagibig(Remittance Pagibig)
          {
               this.Pagibig = Pagibig;
          }

          public Remittance GetPagibig()
          {
               return Pagibig;
          }

          public void SetPhic(Remittance Phic)
          {
               this.Phic = Phic;
          }

          public Remittance GetPhic()
          {
               return Phic;
          }
          public void SetGsis(Remittance Gsis)
          {
               this.Gsis = Gsis;
          }

          public Remittance GetGsis()
          {
               return Gsis;
          }
          public void SetExcess(Remittance Excess)
          {
               this.Excess = Excess;
          }

          public Remittance GetExcess()
          {
               return Excess;
          }


          //VIEW MODEL

          public string CoopFormat()
          {
               return Coop.Amount + "/" + Coop.MaxCount + "/" + Coop.Count;
          }

          public string PagibigFormat()
          {
               return Pagibig.Amount + "/" + Pagibig.MaxCount + "/" + Pagibig.Count;
          }
          public string GsisFormat()
          {
               return Gsis.Amount + "/" + Gsis.MaxCount + "/" + Gsis.Count;
          }
          public string PhicFormat()
          {
               return Phic.Amount + "/" + Phic.MaxCount + "/" + Phic.Count;
          }

          public string ExcessFormat()
          {
               return Excess.Amount + "/" + Excess.MaxCount + "/" + Excess.Count;
          }
     }
}