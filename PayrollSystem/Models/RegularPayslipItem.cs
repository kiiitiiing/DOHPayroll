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

      

        public static List<RegularPayslipItem> Seeder()
        {
            List<RegularPayslipItem> list = new List<RegularPayslipItem>();

            list.Add(new RegularPayslipItem("FIRST HALF SALARY", "", 0,new BaseColor(0,0,0),new BaseColor(255,255,255),"SECOND HALF SALARY","",0, new BaseColor(0, 0, 0), new BaseColor(255, 255, 255)));
            list.Add(new RegularPayslipItem("BASIC SALARY", "2,000.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "SALARY & PERA/ACA", "9,461.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("PERA / ACA", "2,000.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "BLANK", "BLANK", 0, new BaseColor(255, 255, 255), new BaseColor(255, 255, 255)));
            list.Add(new RegularPayslipItem("TOTAL SALARY", "2,000.00", 0, new BaseColor(0, 0, 0), new BaseColor(255, 255, 255), "HAZARD PAY", "0.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("DEDUCTIONS", "", 2, new BaseColor(0, 0, 0), new BaseColor(255,255,255), "MORTUARY", "0.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("DEDUCTION (Late)", "2,000.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "HWMPC", "0.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("TAX", "(2,000.00)", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "OTHER ACCOUNTS PAYABLE", "0.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("PHILHEALTH", "2,000.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "NET HAZARD PAY", "0.00", 0, new BaseColor(0, 0, 0), new BaseColor(255, 255, 255)));
            list.Add(new RegularPayslipItem("GSIS PREMIUM", "2,000.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "CELLPHONE COMMUNICATION", "0.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("UOLI PREM. GSIS", "2,000.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "DEDUCTION", "0.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("MEM. GSIS", "2,000.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "NET CELLPHONE COMM.", "0.00",0, new BaseColor(0, 0, 0), new BaseColor(255, 255, 255)));
            list.Add(new RegularPayslipItem("ED. GSIS", "2,000.00",2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "ANNIVERSARY ALLOWANCE", "0.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("GSIS CONSOLOAN", "2,000.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "UNIFORM AND CLOTHING ALL", "0.00",0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("GSIS POLICY LOAN", "2,000.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "MASTERAL", "0.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("GSIS EML", "2,000.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "DEDUCTION", "0.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("GSIS UOLI", "2,000.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "NET MASTERAL", "0.00", 0, new BaseColor(0, 0, 0), new BaseColor(255, 255, 255)));
            list.Add(new RegularPayslipItem("PAGIBIG PREMIUM", "2,000.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "LOYALTY BONUS", "0.00",0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("PAGIBIG LOAN", "2,000.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "MID-YEAR BONUS", "0.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("CFI", "2,000.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "DEDUCTION", "0.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("SIMC", "2,000.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "NET MID-YEAR BONUS", "0.00", 0, new BaseColor(0, 0, 0), new BaseColor(255, 255, 255)));
            list.Add(new RegularPayslipItem("HWMPC", "2,000.00",2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "YEAR-END BONUS", "0.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("GSIS EDU ASST", "2,000.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "DEDUCTION", "0.00",0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("DISALLOWANCES", "2,000.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "NET-YEAR END BONUS", "0.00", 0, new BaseColor(0, 0, 0), new BaseColor(255, 255, 255)));
            list.Add(new RegularPayslipItem("DBP", "2,000.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "MONETIZATION", "0.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("GSIS HELP", "2,000.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "DEDUCTION", "0.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("REL", "2,000.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "NET MONETIZATION", "0.00", 0, new BaseColor(0, 0, 0), new BaseColor(255, 255, 255)));
            list.Add(new RegularPayslipItem("TOTAL DEDUCTIONS", "2,000.00",2, new BaseColor(00, 0, 0), new BaseColor(255,255,255), "PBB (Performance Based Bonus)", "0.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("NET SALARY & PERA/ACA", "2,000.00", 0, new BaseColor(0, 0, 0), new BaseColor(255, 255, 255), "ONA", "0.00",0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("SUBSISTENCE", "2,000.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "DEDUCTION", "0.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("DEDUCTION (Absences)", "2,000.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "NET ONA", "0.00", 0, new BaseColor(0, 0, 0), new BaseColor(255, 255, 255)));
            list.Add(new RegularPayslipItem("NET SUBSISTENCE", "2,000.00", 0, new BaseColor(0, 0, 0), new BaseColor(255, 255, 255), "PEI", "0.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("LONGEVITY", "2,000.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "LONGEVITY DIFFERENTIAL", "0.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("DEDUCTION", "2,000.00", 2, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "RATA", "0.00",0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("NET LONGEVITY", "2,000.00", 0, new BaseColor(0, 0, 0), new BaseColor(255, 255, 255), "OTHERS", "0.00",0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0)));
            list.Add(new RegularPayslipItem("HAZARD DIFFERENTIAL", "2,000.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "BLANK", "0.00", 0, new BaseColor(255, 255, 255), new BaseColor(255, 255, 255)));
            list.Add(new RegularPayslipItem("SALARY DIFFERENTIAL", "2,000.00", 0, new BaseColor(255, 255, 255), new BaseColor(0, 0, 0), "BLANK", "0.00", 0, new BaseColor(255, 255, 255), new BaseColor(255, 255, 255)));

            list.Add(new RegularPayslipItem("BLANK DIFFERENTIAL", "2,000.00", 0, new BaseColor(255, 255, 255), new BaseColor(255, 255, 255), "BLANK", "0.00", 0, new BaseColor(255, 255, 255), new BaseColor(255, 255, 255)));
            list.Add(new RegularPayslipItem("TOTAL PAY (1ST HALF)", "2,000.00", 0, new BaseColor(0, 0, 0), new BaseColor(255, 255, 255), "TOTAL PAY (2ND HALF)", "0.00", 0, new BaseColor(0, 0, 0), new BaseColor(255, 255, 255)));

            return list;
        }
    }
}
