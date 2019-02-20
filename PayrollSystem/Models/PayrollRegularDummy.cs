using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
    public class PayrollRegularDummy
    {
        public String Id { get; set; }
        public Employee Employee { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public String DaysAbsent { get; set; }
        public String WorkDays { get; set; }
        public String Salary { get; set; }
        public String Pera { get; set; }
        public int MinutesLate { get; set; }
        public String Tax { get; set; }
        public String CFI { get; set; }
        public String GSIS_Premium { get; set; }
        public String GSIS_Consoloan { get; set; }
        public String GSIS_PolicyLoan { get; set; }
        public String GSIS_EML { get; set; }
        public String GSIS_UOLI { get; set; }
        public String GSIS_EDU { get; set; }
        public String GSIS_Help { get; set; }
        public String GSIS_REL { get; set; }
        public String Pagibig_Premium { get; set; }
        public String Pagibig_Loan { get; set; }
        public String Disallowances { get; set; }
        public String PhilHealth { get; set; }
        public String SIMC { get; set; }
        public String HWMPC { get; set; }
        public String DBP { get; set; }
        public String MP2 { get; set; }
        public String Flag { get; set; }
        public PayrollRegularDummy() { }

        public PayrollRegularDummy(String Id, Employee Employee, int Month, int Year, String DaysAbsent, String WorkDays, String Salary, String Pera, int MinutesLate,
            String Tax, String CFI, String GSIS_Premium, String GSIS_Consoloan, String GSIS_PolicyLoan, String GSIS_EML, String GSIS_UOLI, String GSIS_EDU, String GSIS_Help, String GSIS_REL,
            String Pagibig_Premium, String Pagibig_Loan, String Disallowances, String PhilHealth, String SIMC, String HWMPC, String DBP, String MP2)
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
    }
}