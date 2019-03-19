using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayrollSystem.Database;
using PayrollSystem.Models;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace PayrollSystem.Models
{
     public class RegularDivisionPDFView
     {
          public static PdfPTable RegularDivisionSummaryTitle(int month, int year)
          {
               PdfPTable Table = new PdfPTable(1);
               Table.TotalWidth = 400;

               List<String> title = new List<String>();
               title.Add("GENERAL PAYROLL");
               title.Add("RD/ARD");
               title.Add("SUMMARY Sheet");
               title.Add("GRAND TOTALS");
               title.Add("For the month of " + DateUtility.GetMonthName(month, year) + " 1-" + DateTime.DaysInMonth(year, month) + ", 2019");

               //Used this type of loop to pinpoint the location of specific item in a list.
               int FontStyle = Font.BOLD;

               for (int i = 0; i < title.Count; i++)
               {
                    int FontSize = 9;
                    if (i == 0 || i == 4)
                    {
                         FontSize = 8;
                    }

                    Table.AddCell(new PdfPCell(new Phrase(title[i], new Font(FontFactory.GetFont("HELVETICA", FontSize, FontStyle, new BaseColor(0, 0, 0)))))
                    {
                         Border = 0,
                         HorizontalAlignment = Element.ALIGN_CENTER,
                         VerticalAlignment = Element.ALIGN_CENTER
                    });
               }

               Table.AddCell(new PdfPCell(new Phrase("BLANK", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    PaddingTop = 10,
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               return Table;
          }

          public static bool RegularDivisionSummary(string physical_file_path, int month, int year, string division)
          {
               List<RegularPayrollModel> list = PayrollDatabase.Instance.GenerateSummaryRegularByDivision(month, year, division);
               if (list.Count > 0)
               {
                    Document doc = new Document();
                    doc.SetMargins(5f, 5f, 45f, 5f);
                    doc.SetPageSize(PageSize.A4);

                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(physical_file_path, FileMode.Create));

                    doc.Open();
                    doc.Add(RegularDivisionSummaryTitle(month, year));
                    doc.Add(RegularDivisionSummaryBody(list));
                    doc.Close();
                    return true;
               }
               else
               {
                    return false;
               }

          }

          public static PdfPTable RegularDivisionSummaryBody(List<RegularPayrollModel> list)
          {
               float[] widths = { 1, 3, 2, 2, 2, 1 };

               Document doc = new Document();
               doc.SetMargins(5f, 5f, 45f, 5f);
               doc.SetPageSize(PageSize.A4);

               PdfPTable Table = new PdfPTable(6);
               Table.SetWidths(widths);
               Table.TotalWidth = 400;
               Table.WidthPercentage = 100;



               decimal basic_rate = 0;
               decimal pera = 0;
               decimal subsistence = 0;
               decimal hazard = 0;
               decimal ra = 0;
               decimal ta = 0;
               decimal tax = 0;
               decimal gsis_policy_loan = 0;
               decimal phic = 0;
               decimal pagibig_loan = 0;
               decimal pagibig_premium = 0;
               decimal pagibig_mp2 = 0;
               decimal gsis_help = 0;
               decimal gsis_eml = 0;
               decimal dbp = 0;
               decimal gsis_consol = 0;
               decimal simc = 0;
               decimal hwmpc = 0;
               decimal cfi = 0;
               decimal disallowances = 0;
               decimal edu = 0;
               decimal gross = 0;
               decimal total_deductions = 0;

               foreach (RegularPayrollModel data in list)
               {

                    //  string id = data.Employee.ID;

                    basic_rate += decimal.Parse(data.Salary);
                    pera += decimal.Parse(data.Pera);

                    ra += data.GetRata().Ra;
                    ta += data.GetRata().Ta;

                    hazard += data.GetHazard().NetAmount;

                    tax += decimal.Parse(data.Tax);
                    gsis_policy_loan += decimal.Parse(data.GSIS_PolicyLoan);
                    phic += decimal.Parse(data.PhilHealth);
                    pagibig_loan += decimal.Parse(data.Pagibig_Loan);
                    pagibig_premium += decimal.Parse(data.Pagibig_Premium);
                    pagibig_mp2 += decimal.Parse(data.MP2);
                    gsis_help += decimal.Parse(data.GSIS_Help);
                    gsis_eml += decimal.Parse(data.GSIS_EML);
                    dbp += decimal.Parse(data.DBP);
                    gsis_consol += decimal.Parse(data.GSIS_Consoloan);
                    simc += decimal.Parse(data.SIMC);
                    hwmpc += decimal.Parse(data.HWMPC);
                    cfi += decimal.Parse(data.CFI);
                    disallowances += decimal.Parse(data.Disallowances);
                    edu += decimal.Parse(data.GSIS_EDU);

               }
               gross = basic_rate + pera + ra + ta + hazard;
               total_deductions = tax + gsis_policy_loan + phic + pagibig_loan + pagibig_premium + pagibig_mp2 + gsis_help + gsis_eml + dbp + gsis_consol + simc + hwmpc + cfi + disallowances + edu;

               decimal half_net = (gross - total_deductions) / 2;

               List<RegularDivisionItem> pdf_item = RegularDivisionItem.Seeder(basic_rate, pera, subsistence, ra, ta, hazard, half_net, tax, gsis_policy_loan, phic, pagibig_loan,
                   pagibig_premium, pagibig_mp2, gsis_help, gsis_eml, dbp, gsis_consol, simc, hwmpc, cfi, disallowances, edu);

               foreach (RegularDivisionItem data in pdf_item)
               {
                    BaseColor LeftNameBaseColor = (data.LeftName.Equals("BLANK")) ? new BaseColor(255, 255, 255) : new BaseColor(0, 0, 0);
                    BaseColor LeftValueBaseColor = (data.LeftValue.Equals("BLANK")) ? new BaseColor(255, 255, 255) : new BaseColor(0, 0, 0);
                    BaseColor RightNameBaseColor = (data.RightName.Equals("BLANK")) ? new BaseColor(255, 255, 255) : new BaseColor(0, 0, 0);
                    BaseColor RightValueBaseColor = (data.RightValue.Equals("BLANK")) ? new BaseColor(255, 255, 255) : new BaseColor(0, 0, 0);

                    int TableBorder = 0;
                    int FontStyle = Font.NORMAL;

                    if (data.LeftName.Equals("GROSS :"))
                    {
                         TableBorder = PdfPCell.TOP_BORDER;
                         FontStyle = Font.BOLD;
                    }

                    if (data.LeftName.Equals("(--)DEDUCTIONS:"))
                    {
                         FontStyle = Font.BOLD;
                    }


                    Table.AddCell(new PdfPCell(new Phrase("BLANK", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
                    {
                         Padding = 2,
                         Border = 0,
                         HorizontalAlignment = Element.ALIGN_RIGHT,
                         VerticalAlignment = Element.ALIGN_CENTER
                    });

                    Table.AddCell(new PdfPCell(new Phrase(data.LeftName, new Font(FontFactory.GetFont("HELVETICA", 8, FontStyle, LeftNameBaseColor))))
                    {
                         Padding = 2,
                         Border = 0,
                         HorizontalAlignment = Element.ALIGN_RIGHT,
                         VerticalAlignment = Element.ALIGN_CENTER
                    });
                    Table.AddCell(new PdfPCell(new Phrase(data.LeftValue, new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, LeftValueBaseColor))))
                    {
                         Padding = 2,
                         BorderColor = new BaseColor(0, 0, 0),
                         BorderWidthTop = 2,
                         Border = TableBorder,
                         HorizontalAlignment = Element.ALIGN_RIGHT,
                         VerticalAlignment = Element.ALIGN_CENTER
                    });

                    Table.AddCell(new PdfPCell(new Phrase(data.RightName, new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, RightNameBaseColor))))
                    {
                         Padding = 2,
                         Border = 0,
                         HorizontalAlignment = Element.ALIGN_RIGHT,
                         VerticalAlignment = Element.ALIGN_CENTER
                    });
                    Table.AddCell(new PdfPCell(new Phrase(data.RightValue, new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, RightValueBaseColor))))
                    {
                         Padding = 2,
                         Border = 0,
                         HorizontalAlignment = Element.ALIGN_RIGHT,
                         VerticalAlignment = Element.ALIGN_CENTER
                    });
                    Table.AddCell(new PdfPCell(new Phrase("BLANK", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
                    {
                         Padding = 2,
                         Border = 0,
                         HorizontalAlignment = Element.ALIGN_LEFT,
                         VerticalAlignment = Element.ALIGN_CENTER
                    });
               }


               Table.AddCell(new PdfPCell(new Phrase("BLANK", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    PaddingTop = 10,
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               Table.AddCell(new PdfPCell(new Phrase("BLANK", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    BorderWidthTop = 2,
                    Border = PdfPCell.TOP_BORDER,
                    PaddingTop = 10,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               Table.AddCell(new PdfPCell(new Phrase("BLANK", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    PaddingTop = 10,
                    BorderWidthTop = 2,
                    Border = PdfPCell.TOP_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               Table.AddCell(new PdfPCell(new Phrase("Total Deductions :", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    PaddingTop = 10,
                    BorderWidthTop = 2,
                    Border = PdfPCell.TOP_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               Table.AddCell(new PdfPCell(new Phrase(total_deductions.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    PaddingTop = 10,
                    BorderWidthTop = 2,
                    Border = PdfPCell.TOP_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               Table.AddCell(new PdfPCell(new Phrase("BLANK", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    PaddingTop = 10,
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });


               Table.AddCell(new PdfPCell(new Phrase("Total Net :", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Colspan = 4,
                    PaddingTop = 20,
                    BorderWidthTop = 2,
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               Table.AddCell(new PdfPCell(new Phrase((gross - total_deductions).ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    PaddingTop = 20,
                    BorderWidthTop = 2,
                    Border = PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               Table.AddCell(new PdfPCell(new Phrase("BLANK", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    PaddingTop = 20,
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });


               return Table;
          }
     }
}