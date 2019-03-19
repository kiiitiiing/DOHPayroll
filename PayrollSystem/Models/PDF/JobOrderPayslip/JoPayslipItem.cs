using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PayrollSystem.Models
{
     public class JoPayslipItem
     {
          public string LeftDescription { get; set; }
          public string LeftValue { get; set; }
          public int LeftPosition { get; set; }
          public BaseColor LeftBackground { get; set; }
          public BaseColor LeftBaseColor { get; set; }




          public JoPayslipItem(string LeftDescription, string LeftValue, int LeftPosition, BaseColor LeftBackground, BaseColor LeftBaseColor)
          {
               this.LeftDescription = LeftDescription;
               this.LeftValue = LeftValue;
               this.LeftPosition = LeftPosition;
               this.LeftBackground = LeftBackground;
               this.LeftBaseColor = LeftBaseColor;
          }



          public static List<JoPayslipItem> Seeder(JoPayslipModel data)
          {
               BaseColor black = new BaseColor(0, 0, 0);
               BaseColor white = new BaseColor(255, 255, 255);

               List<JoPayslipItem> list = new List<JoPayslipItem>()
            {
                new JoPayslipItem("Basic Salary:",data.BasicSalary.ToString("#,##0.00"),1,white,black),
                new JoPayslipItem("Adjustment (+):",data.Adjustment.ToString("#,##0.00"),1,white,black),
                new JoPayslipItem("Deductions:","",1,white,black),
                new JoPayslipItem("  Tardiness/Absences:",data.Tardiness.ToString("#,##0.00"),1,white,black),
                new JoPayslipItem("BLANK","BLANK",1,white,white),
                new JoPayslipItem("Net Amount:",data.GetNetAmount().ToString("#,##0.00"),1,black,white),

                new JoPayslipItem("BLANK","BLANK",1,white,white),

                new JoPayslipItem("Additional Option:","",1,white,black),
                new JoPayslipItem("  Adjustment:",data.OtherAdjustment.ToString("#,##0.00"),1,white,black),
                new JoPayslipItem("Deductions:","",1,white,black),
                new JoPayslipItem("  5% EWT:",data.EWT.ToString("#,##0.00"),1,white,black),
                new JoPayslipItem("  3% Professional Tax:",data.ProfTax.ToString("#,##0.00"),1,white,black),
                new JoPayslipItem("  HWMPC:",data.Hwmpc.ToString("#,##0.00"),1,white,black),
                new JoPayslipItem("  Pag-Ibig:",data.Pagibig.ToString("#,##0.00"),1,white,black),
                new JoPayslipItem("  PHIC:",data.Phic.ToString("#,##0.00"),1,white,black),
                new JoPayslipItem("  GSIS:",data.Gsis.ToString("#,##0.00"),1,white,black),
                new JoPayslipItem("  Other Payables (Digitel):",data.Digitel.ToString("#,##0.00"),1,white,black),

                new JoPayslipItem("BLANK","BLANK",1,white,white),

                new JoPayslipItem("Total Deductions:",data.GetTotalDeduction().ToString("#,##0.00"),1,black,white),

                new JoPayslipItem("BLANK","BLANK",1,white,white),

                new JoPayslipItem("Net Income:",data.GetNetIncome().ToString("#,##0.00"),1,black,white),

            };

               return list;
          }
     }
}
