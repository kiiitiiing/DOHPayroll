namespace PayrollSystem.Models
{
     internal class SalaryChargeModel
     {
          private decimal absences;
          private decimal cfi;
          private decimal dbp;
          private decimal disallowances;
          private decimal gross_income;
          private decimal gsis_consoloan;
          private decimal gsis_edu;
          private decimal gsis_eml;
          private decimal gsis_help;
          private decimal gsis_policy_loan;
          private decimal gsis_premium;
          private decimal gsis_uoli;
          private decimal half_net_received;
          private decimal hazard_deduction;
          private decimal hazard_net;
          private decimal hwmpc;
          private string id;
          private decimal lp_disallowance;
          private decimal lp_total;
          private decimal mp2;
          private string name;
          private decimal pagibig_loan;
          private decimal pagibig_premium;
          private decimal philhealth;
          private string position;
          private decimal rel;
          private decimal salary;
          private decimal simc;
          private object subsistence;
          private decimal subsistence_deduction;
          private decimal tax;
          private decimal total_deductions;
          private string v1;
          private string v2;
          private string v3;
          private string v4;

          public SalaryChargeModel(string id, string name, string position, decimal salary, object subsistence, decimal gross_income, decimal tax, decimal gsis_premium, decimal gsis_policy_loan, decimal gsis_uoli, decimal pagibig_premium, decimal cfi, decimal hwmpc, decimal total_deductions, decimal half_net_received, string v1, decimal subsistence_deduction, string v2, decimal gsis_consoloan, decimal gsis_eml, decimal gsis_edu, decimal pagibig_loan, decimal simc, decimal disallowances, decimal absences, decimal lp_total, string v3, decimal gsis_help, decimal lp_disallowance, decimal rel, decimal philhealth, decimal hazard_net, decimal hazard_deduction, string v4, decimal dbp, decimal mp2)
          {
               this.id = id;
               this.name = name;
               this.position = position;
               this.salary = salary;
               this.subsistence = subsistence;
               this.gross_income = gross_income;
               this.tax = tax;
               this.gsis_premium = gsis_premium;
               this.gsis_policy_loan = gsis_policy_loan;
               this.gsis_uoli = gsis_uoli;
               this.pagibig_premium = pagibig_premium;
               this.cfi = cfi;
               this.hwmpc = hwmpc;
               this.total_deductions = total_deductions;
               this.half_net_received = half_net_received;
               this.v1 = v1;
               this.subsistence_deduction = subsistence_deduction;
               this.v2 = v2;
               this.gsis_consoloan = gsis_consoloan;
               this.gsis_eml = gsis_eml;
               this.gsis_edu = gsis_edu;
               this.pagibig_loan = pagibig_loan;
               this.simc = simc;
               this.disallowances = disallowances;
               this.absences = absences;
               this.lp_total = lp_total;
               this.v3 = v3;
               this.gsis_help = gsis_help;
               this.lp_disallowance = lp_disallowance;
               this.rel = rel;
               this.philhealth = philhealth;
               this.hazard_net = hazard_net;
               this.hazard_deduction = hazard_deduction;
               this.v4 = v4;
               this.dbp = dbp;
               this.mp2 = mp2;
          }
     }
}