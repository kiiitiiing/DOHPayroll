using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
     public class RegularPayrollModel
     {
          public string Id { get; set; }
          public Employee Employee { get; set; }
          public int Month { get; set; }
          public int Year { get; set; }
          public string DaysAbsent { get; set; }
          public string WorkDays { get; set; }
          public string Salary { get; set; }
          public string Pera { get; set; }
          public int MinutesLate { get; set; }
          public string Tax { get; set; }
          public string CFI { get; set; }
          public string GSIS_Premium { get; set; }
          public string GSIS_Consoloan { get; set; }
          public string GSIS_PolicyLoan { get; set; }
          public string GSIS_EML { get; set; }
          public string GSIS_UOLI { get; set; }
          public string GSIS_EDU { get; set; }
          public string GSIS_Help { get; set; }
          public string GSIS_REL { get; set; }
          public string Pagibig_Premium { get; set; }
          public string Pagibig_Loan { get; set; }
          public string Disallowances { get; set; }
          public string PhilHealth { get; set; }
          public string SIMC { get; set; }
          public string HWMPC { get; set; }
          public string DBP { get; set; }
          public string MP2 { get; set; }
          public string Flag { get; set; }

          public HazardViewModel hazard { get; set; }
          public RataViewModel rata { get; set; }

          //public RegularPayrollModel() { }

          public RegularPayrollModel(string Id, Employee Employee, int Month, int Year, string DaysAbsent, string WorkDays, string Salary, string Pera, int MinutesLate,
              string Tax, string CFI, string GSIS_Premium, string GSIS_Consoloan, string GSIS_PolicyLoan, string GSIS_EML, string GSIS_UOLI, string GSIS_EDU, string GSIS_Help, string GSIS_REL,
              string Pagibig_Premium, string Pagibig_Loan, string Disallowances, string PhilHealth, string SIMC, string HWMPC, string DBP, string MP2)
          {
               this.Id = Id;
               this.Employee = Employee;
               this.Month = Month;
               this.Year = Year;
               this.DaysAbsent = DaysAbsent;
               this.WorkDays = WorkDays;
               this.Salary = Salary;
               this.Pera = Pera;
               this.MinutesLate = MinutesLate;
               this.Tax = Tax;
               this.CFI = CFI;
               this.GSIS_Premium = GSIS_Premium;
               this.GSIS_Consoloan = GSIS_Consoloan;
               this.GSIS_PolicyLoan = GSIS_PolicyLoan;
               this.GSIS_EML = GSIS_EML;
               this.GSIS_UOLI = GSIS_UOLI;
               this.GSIS_EDU = GSIS_EDU;
               this.GSIS_Help = GSIS_Help;
               this.GSIS_REL = GSIS_REL;
               this.Pagibig_Premium = Pagibig_Premium;
               this.Pagibig_Loan = Pagibig_Loan;
               this.Disallowances = Disallowances;
               this.PhilHealth = PhilHealth;
               this.SIMC = SIMC;
               this.HWMPC = HWMPC;
               this.DBP = DBP;
               this.MP2 = MP2;
          }

          public string MonthFormat()
          {
               return DateUtility.GetMonthName(Month, Year);
          }

          public void SetHazard(HazardViewModel hazard)
          {
               this.hazard = hazard;
          }
          public HazardViewModel GetHazard()
          {
               return hazard;
          }

          public void SetRata(RataViewModel rata)
          {
               this.rata = rata;
          }
          public RataViewModel GetRata()
          {
               return rata;
          }
     }
}
