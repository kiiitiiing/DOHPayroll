using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
    public class RegularDivisionItem
    {
        public string LeftName { get; set; }
        public string LeftValue { get; set; }
        public string RightName { get; set; }
        public string RightValue { get; set; }

        public RegularDivisionItem(string LeftName, string LeftValue, string RightName, string RightValue)
        {
            this.LeftName = LeftName;
            this.LeftValue = LeftValue;
            this.RightName = RightName;
            this.RightValue = RightValue;
        }


        public static List<RegularDivisionItem> Seeder(double tax, double gsis_policy_loan, double phic, double pagibig_loan, double pagibig_premium,
            double pagibig_mp2, double gsis_help, double gsis_eml, double dbp, double gsis_consol, double simc, double hwmpc, double cfi, double disallowances, double edu)
        {

            List<RegularDivisionItem> seeder = new List<RegularDivisionItem>();
            //BODY
            seeder.Add(new RegularDivisionItem("BLANK", "BLANK", "BLANK", "BLANK"));
            seeder.Add(new RegularDivisionItem("Basic Rate :", "0.00", "BLANK", "BLANK"));
            seeder.Add(new RegularDivisionItem("Basic Increment :", "0.00", "BLANK", "BLANK"));
            seeder.Add(new RegularDivisionItem("Basic Adjustment :", "0.00", "BLANK", "BLANK"));
            seeder.Add(new RegularDivisionItem("Pera :", "0.00", "BLANK", "BLANK"));
            seeder.Add(new RegularDivisionItem("Add'tl Comp. Allowances :", "0.00", "BLANK", "BLANK"));
            seeder.Add(new RegularDivisionItem("Laundry Allowances :", "0.00", "NET PAY FOR:", "HALF 1 : 0.00"));
            seeder.Add(new RegularDivisionItem("Subsistence :", "0.00", "", "HALF 2 : 0.00"));
            seeder.Add(new RegularDivisionItem("Hazard Pay :", "0.00", "BLANK", "BLANK"));
            seeder.Add(new RegularDivisionItem("Compensation 3(Ca) :", "0.00", "BLANK", "BLANK"));
            seeder.Add(new RegularDivisionItem("Compensation 4(Cb) :", "0.00", "BLANK", "BLANK"));
            seeder.Add(new RegularDivisionItem("Compensation 5(Cc) :", "0.00", "BLANK", "BLANK"));
            seeder.Add(new RegularDivisionItem("Comp. 6(Cd) RA :", "0.00", "BLANK", "BLANK"));
            seeder.Add(new RegularDivisionItem("Compensation 7(Ce) TA :", "0.00", "BLANK", "BLANK"));
            seeder.Add(new RegularDivisionItem("Comp. 8(Cf) Extra-Ord :", "0.00", "BLANK", "BLANK"));
            seeder.Add(new RegularDivisionItem("Comp. 9(Cg) :", "0.00", "BLANK", "BLANK"));
            seeder.Add(new RegularDivisionItem("Comp. 10(Ch) CellCard :", "0.00", "BLANK", "BLANK"));
            seeder.Add(new RegularDivisionItem("GROSS :", "0.00", "BLANK", "BLANK"));
            seeder.Add(new RegularDivisionItem("BLANK", "BLANK", "BLANK", "BLANK"));
            seeder.Add(new RegularDivisionItem("(--)DEDUCTIONS:", "BLANK", "BLANK", "BLANK"));
            seeder.Add(new RegularDivisionItem("BLANK", "BLANK", "BLANK", "BLANK"));
            //DEDUCTIONS
            seeder.Add(new RegularDivisionItem("Withholding Tax :", tax.ToString("#,##0.00"), "GSIS Policy Loan :", gsis_policy_loan.ToString("#,##0.00")));
            seeder.Add(new RegularDivisionItem("Phil-Health (Medicare) :", phic.ToString("#,##0.00"), "GSIS Salary Loan :", "0.00"));
            seeder.Add(new RegularDivisionItem("PAGIBIG FUND :", "0.00", "GSIS Optional Loan :", "0.00"));
            seeder.Add(new RegularDivisionItem("PAGIBIG Multi-Purpose :", pagibig_mp2.ToString("#,##0.00"), "GSIS Housing Loan :", "0.00"));
            seeder.Add(new RegularDivisionItem("PAGIBIG Housing :", "0.00", "GSIS Real Estate Loan :", "0.00"));
            seeder.Add(new RegularDivisionItem("PAGIBIG Additional 1 :", pagibig_loan.ToString("#,##0.00"), "GSIS PBHAY LOAN :", "0.00"));
            seeder.Add(new RegularDivisionItem("PAGIBIG Additional 2 :", pagibig_premium.ToString("#,##0.00"), "GSIS HELP :", gsis_help.ToString("#,##0.00")));
            seeder.Add(new RegularDivisionItem("GSIS Life_Retr. Insurance :", "0.00", "GSIS EML :", gsis_eml.ToString("#,##0.00")));
            seeder.Add(new RegularDivisionItem("GSIS Unlimited Opt. Ins. 1 :", "0.00", "GSIS UMID :", "0.00"));
            seeder.Add(new RegularDivisionItem("GSIS Unlimited Opt. Ins. 2 :", "0.00", "GSIS Unlimited Opt. Loan :", "0.00"));
            seeder.Add(new RegularDivisionItem("GSIS Optional Insurance 1 :", "0.00", "DBP Loan :", "0.00"));
            seeder.Add(new RegularDivisionItem("GSIS Optional Insurance 2 :", "0.00", "GENESIS PLUS :", "0.00"));
            seeder.Add(new RegularDivisionItem("GSIS Optional Insurance 3 :", "0.00", "GENESIS :", "0.00"));
            seeder.Add(new RegularDivisionItem("GSIS Optional Insurance 4 :", "0.00", "GSIS CONSOL :", "0.00"));
            seeder.Add(new RegularDivisionItem("GSIS Optional Insurance 5 :", "0.00", "COOP ENROL. LOAN :", "0.00"));
            seeder.Add(new RegularDivisionItem("GSIS Educational Ins. 1 :", "0.00", "LBP LOAN :", "0.00"));
            seeder.Add(new RegularDivisionItem("GSIS Educational Ins. 2 :", "0.00", "COOP ROSE PHARM :", "0.00"));
            seeder.Add(new RegularDivisionItem("GSIS Educational Ins. 3 :", "0.00", "COOP RICE LOAN :", "0.00"));
            seeder.Add(new RegularDivisionItem("GSIS Educational Ins. 4 :", "0.00", "COOP APPLIA. LOAN :", "0.00"));
            seeder.Add(new RegularDivisionItem("GSIS Educational Ins. 5 :", "0.00", "SIMC LOAN :", simc.ToString("#,##0.00")));
            seeder.Add(new RegularDivisionItem("GSIS Fire Insurance :", "0.00", "HWMPC LOAN :", hwmpc.ToString("#,##0.00")));
            seeder.Add(new RegularDivisionItem("GSIS Additional Insurance :", "0.00", "CFI LOAN :", cfi.ToString("#,##0.00")));
            seeder.Add(new RegularDivisionItem("DISALLOWANCE 1 :", disallowances.ToString("#,##0.00"), "COOP SPECIAL L :", "0.00"));
            seeder.Add(new RegularDivisionItem("DISALLOWANCE 2 :", "0.00", "DISALLOW 1 :", "0.00"));
            seeder.Add(new RegularDivisionItem("DISALLOWANCE 3 :", "0.00", "GSIS ADJUSTMENT :", "0.00"));
            seeder.Add(new RegularDivisionItem("GSIS Hospital Insurance :", "0.00", "GSIS ADJUSTMENT :", "0.00"));
            seeder.Add(new RegularDivisionItem("GSIS EDU LOAN :", edu.ToString("#,##0.00"), "DIGITEL (EXCESS) :", "0.00"));
            seeder.Add(new RegularDivisionItem("GSIS EL LOAN :", "0.00", "GSIS ECARD PLUS :", "0.00"));
            seeder.Add(new RegularDivisionItem("GSIS S.O.S :", "0.00", "UCPB LOAN :", "0.00"));
            seeder.Add(new RegularDivisionItem("GSIS CONSO LOAN :", "0.00", "BLANK", "BLANK"));
            seeder.Add(new RegularDivisionItem("GSIS CASH ADV L :", "0.00", "BLANK", "BLANK"));
            seeder.Add(new RegularDivisionItem("GSIS CASH ADV P:", "0.00", "BLANK", "BLANK"));

            return seeder;
        }
    }
}