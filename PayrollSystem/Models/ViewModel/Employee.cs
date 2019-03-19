using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
     public class Employee
     {
          public string ID { get; set; }
          public string Firstname { get; set; }
          public string Lastname { get; set; }
          public string Middlename { get; set; }
          public string Designation { get; set; }
          public string Salary { get; set; }
          public string SalaryCharge { get; set; }
          public string Division { get; set; }
          public string DisbursementType { get; set; }
          public string Tin { get; set; }
          public string WorkSchedule { get; set; }

          public Employee() { }


          public Employee(string ID, string Firstname, string Lastname, string Middlename, string Designation, string Salary, string SalaryCharge, string Division, string DisbursementType)
          {
               this.ID = ID;
               this.Firstname = Firstname;
               this.Lastname = Lastname;
               this.Middlename = Middlename;
               this.Designation = Designation;
               this.Salary = Salary;
               this.SalaryCharge = SalaryCharge;
               this.Division = Division;
               this.DisbursementType = DisbursementType;
          }

          public void SetTin(string Tin)
          {
               this.Tin = Tin;
          }
          public string GetTin()
          {
               return Tin;
          }
     }
}