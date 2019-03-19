using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
     public class RegularModel : Employee
     {

          public decimal Tax { get; set; }
          public Remittance cfi { get; set; }

          public Remittance gsisPremium { get; set; }

          public Remittance gsisConsoloan { get; set; }

          public Remittance gsisPolicyLoan { get; set; }

          public Remittance gsisEml { get; set; }

          public Remittance gsisUoli { get; set; }

          public Remittance gsisEdu { get; set; }

          public Remittance gsisHelp { get; set; }

          public Remittance gsisRel { get; set; }

          public Remittance pagibigPremium { get; set; }

          public Remittance pagibigLoan { get; set; }

          public Remittance pagibigMp2 { get; set; }

          public Remittance philHealth { get; set; }

          public Remittance simc { get; set; }

          public Remittance hwmpc { get; set; }

          public Remittance dbp { get; set; }

          public Remittance disallowances { get; set; }


          public RegularModel(Employee employee)
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

          public void SetTax(decimal Tax)
          {
               this.Tax = Tax;
          }
          public decimal GetTax()
          {
               return Tax;
          }

          public void SetCfi(Remittance cfi)
          {
               this.cfi = cfi;
          }

          public Remittance GetCfi()
          {
               return cfi;
          }
          public void SetGsisPremium(Remittance gsisPremium)
          {
               this.gsisPremium = gsisPremium;
          }

          public Remittance GetGsisPremium()
          {
               return gsisPremium;
          }
          public void SetGsisConsoloan(Remittance gsisConsoloan)
          {
               this.gsisConsoloan = gsisConsoloan;
          }

          public Remittance GetGsisConsoloan()
          {
               return gsisConsoloan;
          }
          public void SetGsisPolicyLoan(Remittance gsisPolicyLoan)
          {
               this.gsisPolicyLoan = gsisPolicyLoan;
          }

          public Remittance GetGsisPolicyLoan()
          {
               return gsisPolicyLoan;
          }
          public void SetGsisEml(Remittance gsisEml)
          {
               this.gsisEml = gsisEml;
          }

          public Remittance GetGsisEml()
          {
               return gsisEml;
          }
          public void SetGsisUoli(Remittance gsisUoli)
          {
               this.gsisUoli = gsisUoli;
          }

          public Remittance GetGsisUoli()
          {
               return gsisUoli;
          }

          public void SetGsisEdu(Remittance gsisEdu)
          {
               this.gsisEdu = gsisEdu;
          }

          public Remittance GetGsisEdu()
          {
               return gsisEdu;
          }
          public void SetGsisHelp(Remittance gsisHelp)
          {
               this.gsisHelp = gsisHelp;
          }

          public Remittance GetGsisHelp()
          {
               return gsisHelp;
          }
          public void SetGsisRel(Remittance gsisRel)
          {
               this.gsisRel = gsisRel;
          }

          public Remittance GetGsisRel()
          {
               return gsisRel;
          }
          public void SetPagibigPremium(Remittance pagibigPremium)
          {
               this.pagibigPremium = pagibigPremium;
          }

          public Remittance GetPagibigPremium()
          {
               return pagibigPremium;
          }
          public void SetPagibigLoan(Remittance pagibigLoan)
          {
               this.pagibigLoan = pagibigLoan;
          }

          public Remittance GetPagibigLoan()
          {
               return pagibigLoan;
          }
          public void SetPagibigMp2(Remittance pagibigMp2)
          {
               this.pagibigMp2 = pagibigMp2;
          }

          public Remittance GetPagibigMp2()
          {
               return pagibigMp2;
          }
          public void SetPhilHealth(Remittance philHealth)
          {
               this.philHealth = philHealth;
          }

          public Remittance GetPhilHealth()
          {
               return philHealth;
          }
          public void SetSimc(Remittance simc)
          {
               this.simc = simc;
          }

          public Remittance GetSimc()
          {
               return simc;
          }
          public void SetHwmpc(Remittance hwmpc)
          {
               this.hwmpc = hwmpc;
          }

          public Remittance GetHwmpc()
          {
               return hwmpc;
          }
          public void SetDbp(Remittance dbp)
          {
               this.dbp = dbp;
          }

          public Remittance GetDbp()
          {
               return dbp;
          }
          public void SetDisallowances(Remittance disallowances)
          {
               this.disallowances = disallowances;
          }

          public Remittance GetDisallowances()
          {
               return disallowances;
          }

          //VIEW MODEL
          public string CfiFormat()
          {
               return cfi.Amount + "/" + cfi.MaxCount + "/" + cfi.Count;
          }

          public string GsisConsoLoanFormat()
          {
               return gsisConsoloan.Amount + "/" + gsisConsoloan.MaxCount + "/" + gsisConsoloan.Count;
          }
          public string GsisPolicyLoanFormat()
          {
               return gsisPolicyLoan.Amount + "/" + gsisPolicyLoan.MaxCount + "/" + gsisPolicyLoan.Count;
          }
          public string GsisEmlLoanFormat()
          {
               return gsisEml.Amount + "/" + gsisEml.MaxCount + "/" + gsisEml.Count;
          }
          public string GsisUoliLoanFormat()
          {
               return gsisUoli.Amount + "/" + gsisUoli.MaxCount + "/" + gsisUoli.Count;
          }
          public string GsisEduLoanFormat()
          {
               return gsisEdu.Amount + "/" + gsisEdu.MaxCount + "/" + gsisEdu.Count;
          }
          public string GsisHelpLoanFormat()
          {
               return gsisEdu.Amount + "/" + gsisEdu.MaxCount + "/" + gsisEdu.Count;
          }
          public string RelFormat()
          {
               return gsisRel.Amount + "/" + gsisRel.MaxCount + "/" + gsisRel.Count;
          }
          public string PagibigLoanFormat()
          {
               return pagibigLoan.Amount + "/" + pagibigLoan.MaxCount + "/" + pagibigLoan.Count;
          }
          public string PagibigMp2Format()
          {
               return pagibigMp2.Amount + "/" + pagibigMp2.MaxCount + "/" + pagibigMp2.Count;
          }
          public string SimcFormat()
          {
               return simc.Amount + "/" + simc.MaxCount + "/" + simc.Count;
          }
          public string HwmpcFormat()
          {
               return hwmpc.Amount + "/" + hwmpc.MaxCount + "/" + hwmpc.Count;
          }
          public string DbpFormat()
          {
               return dbp.Amount + "/" + dbp.MaxCount + "/" + dbp.Count;
          }
          public string DisallowancesFormat()
          {
               return disallowances.Amount + "/" + disallowances.MaxCount + "/" + disallowances.Count;
          }
     }
}