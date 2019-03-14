using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PayrollSystem.Models
{
    public class RegularPayslipItem
    {
        public string LeftDescription { get; set; }
        public string LeftValue { get; set; }
        public int LeftPosition { get; set; }
        public BaseColor LeftBackground { get; set; }
        public BaseColor LeftBaseColor { get; set; }

        public string RightDescription { get; set; }
        public string RightValue { get; set; }
        public int RightPosition { get; set; }
        public BaseColor RightBackground { get; set; }
        public BaseColor RightBaseColor { get; set; }



        public RegularPayslipItem(string LeftDescription,string LeftValue, int LeftPosition, BaseColor LeftBackground, BaseColor LeftBaseColor,
            string RightDescription,string RightValue,int RightPosition, BaseColor RightBackground, BaseColor RightBaseColor)
        {
            this.LeftDescription = LeftDescription;
            this.LeftValue = LeftValue;
            this.LeftPosition = LeftPosition;
            this.LeftBackground = LeftBackground;
            this.LeftBaseColor = LeftBaseColor;
            this.RightDescription = RightDescription;
            this.RightValue = RightValue;
            this.RightPosition = RightPosition;
            this.RightBackground = RightBackground;
            this.RightBaseColor = RightBaseColor;
        }

      

        public static List<RegularPayslipItem> Seeder(RegularPayslipModel PayslipModel)
        {
            List<RegularPayslipItem> list = new List<RegularPayslipItem>();

            BaseColor black = new BaseColor(0,0,0);
            BaseColor white = new BaseColor(255,255,255);

            list.Add(new RegularPayslipItem("FIRST HALF SALARY", "", 0,black,white,"SECOND HALF SALARY","",0, black,white));
            list.Add(new RegularPayslipItem("BASIC SALARY", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.BasicSalary,false), 0, white,black, "SALARY & PERA/ACA", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.SecondHalf.Salary,false), 0, black,white));
            list.Add(new RegularPayslipItem("PERA / ACA", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.Pera,false), 0, white,black, "BLANK", "BLANK", 0, white,white));
            list.Add(new RegularPayslipItem("TOTAL SALARY", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.GetTotalSalary(),false), 0, black,white, "HAZARD PAY", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.SecondHalf.HazardPay,false), 0, white,black));
            list.Add(new RegularPayslipItem("DEDUCTIONS", "", 2, black,white, "MORTUARY",ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.SecondHalf.Mortuary,true), 2, white,black));
            list.Add(new RegularPayslipItem("DEDUCTION (Late)", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.Absences,true), 2, white,black, "HWMPC", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.SecondHalf.Hwmpc,true), 2, white,black));
            list.Add(new RegularPayslipItem("TAX", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.Tax,true), 2, white,black, "OTHER ACCOUNTS PAYABLE", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.SecondHalf.OtherPayable,true), 2, white,black));
            list.Add(new RegularPayslipItem("PHILHEALTH", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.PhilHealth,true), 2, white,black, "NET HAZARD PAY", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.SecondHalf.GetNetHazard(),false), 0,black,white));
            list.Add(new RegularPayslipItem("GSIS PREMIUM", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.GsisPremium,true), 2, white,black, "CELLPHONE COMMUNICATION", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.SecondHalf.CellphoneAllowance,false), 0,white,black));
            list.Add(new RegularPayslipItem("UOLI PREM. GSIS", "(0.00)", 2, white,black, "DEDUCTION", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.SecondHalf.CellphoneDeduction,true), 2,white,black));
            list.Add(new RegularPayslipItem("MEM. GSIS", "(0.00)", 2, white,black, "NET CELLPHONE COMM.", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.SecondHalf.GetNetCellphone(),false), 0,black,white));
            list.Add(new RegularPayslipItem("ED. GSIS", "(0.00)",2, white,black, "ANNIVERSARY ALLOWANCE", "0.00", 0, white,black));
            list.Add(new RegularPayslipItem("GSIS CONSOLOAN", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.GsisConsoLoan,true), 2, white,black, "UNIFORM AND CLOTHING ALL", "0.00",0, white,black));
            list.Add(new RegularPayslipItem("GSIS POLICY LOAN", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.GsisPolicyLoan,true), 2,white,black, "MASTERAL", "0.00", 0, white,black));
            list.Add(new RegularPayslipItem("GSIS EML", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.GsisEml,true), 2, white,black, "DEDUCTION", "(0.00)", 2, white,black));
            list.Add(new RegularPayslipItem("GSIS UOLI", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.GsisUoli,true), 2, white,black, "NET MASTERAL", "0.00", 0, black,white));
            list.Add(new RegularPayslipItem("PAGIBIG PREMIUM", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.PagibigPremium,true), 2, white,black, "LOYALTY BONUS", "0.00",0, white,black));
            list.Add(new RegularPayslipItem("PAGIBIG LOAN", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.PagibigLoan,true), 2, white,black, "MID-YEAR BONUS", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.SecondHalf.MidYearBonus,false), 0, white,black));
            list.Add(new RegularPayslipItem("CFI", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.Cfi,true), 2, white,black, "DEDUCTION", "(0.00)", 2, white,black));
            list.Add(new RegularPayslipItem("SIMC", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.Simc,true), 2, white,black, "NET MID-YEAR BONUS", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.SecondHalf.MidYearBonus,false), 0,black,white));
            list.Add(new RegularPayslipItem("HWMPC", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.Hwmpc,true), 2, white,black, "YEAR-END BONUS", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.SecondHalf.YearEndBonus,false), 0,white,black));
            list.Add(new RegularPayslipItem("GSIS EDU ASST", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.GsisEdu,true), 2, white,black, "DEDUCTION", "(0.00)",2, white,black));
            list.Add(new RegularPayslipItem("DISALLOWANCES", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.Disallowance,true), 2, white,black, "NET-YEAR END BONUS", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.SecondHalf.YearEndBonus,false), 0,black,white));
            list.Add(new RegularPayslipItem("DBP", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.Dbp,true), 2, white,black, "MONETIZATION", "0.00", 0, white,black));
            list.Add(new RegularPayslipItem("GSIS HELP", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.GsisHelp,true), 2, white,black, "DEDUCTION", "(0.00)", 2, white,black));
            list.Add(new RegularPayslipItem("REL", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.Rel,true), 2, white,black, "NET MONETIZATION", "0.00", 0, black,white));
            list.Add(new RegularPayslipItem("TOTAL DEDUCTIONS", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.GetTotalDeduction(), true), 2, black,white, "PBB (Performance Based Bonus)", "0.00", 0, white,black));
            list.Add(new RegularPayslipItem("NET SALARY & PERA/ACA", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.GetNetSalary(), false), 0, black, white, "ONA", "0.00", 0, white,black));
            list.Add(new RegularPayslipItem("SUBSISTENCE", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.Subsistence, false), 0, white, black, "DEDUCTION", "(0.00)", 2, white,black));
            list.Add(new RegularPayslipItem("DEDUCTION (Absences)", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.SubsistenceDeduction,true), 2, white,black, "NET ONA", "0.00", 0, black,white));
            list.Add(new RegularPayslipItem("NET SUBSISTENCE", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.GetNetSubsistence(),false), 0, black,white, "PEI", "0.00", 0, white,black));
            list.Add(new RegularPayslipItem("LONGEVITY", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.Longevity,false), 0, white,black, "LONGEVITY DIFFERENTIAL", "0.00", 0, white,black));
            list.Add(new RegularPayslipItem("DEDUCTION", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.LongevityDeduction, true), 2, white, black, "RATA", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.SecondHalf.Rata, false), 0, white,black));
            list.Add(new RegularPayslipItem("NET LONGEVITY", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.GetNetLongevity(),false), 0, black,white, "OTHERS", "0.00",0, white,black));
            list.Add(new RegularPayslipItem("HAZARD DIFFERENTIAL", "0.00", 0, white,black, "BLANK", "0.00", 0, white,white));
            list.Add(new RegularPayslipItem("SALARY DIFFERENTIAL", "0.00", 0, white, black, "BLANK", "0.00", 0, white,white));

            list.Add(new RegularPayslipItem("BLANK DIFFERENTIAL", "0.00", 0, white,white, "BLANK", "0.00", 0, white,white));
            list.Add(new RegularPayslipItem("TOTAL PAY (1ST HALF)", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.FirstHalf.GetTotalPay(), false), 0, black,white, "TOTAL PAY (2ND HALF)", ValueFormatterUtility.ConvertToCurrencyFormat(PayslipModel.SecondHalf.GetTotalPay(),false), 0, black,white));

            return list;
        }
    }
}
