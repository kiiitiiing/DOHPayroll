using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayrollSystem.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using PayrollSystem.Database;

namespace PayrollSystem.Models
{
     public class SalaryChargePDFView
     {
          public static PdfPTable RegularSalaryChargeOverall(String salary, String salary_deduction, String subs, String subs_deduction, String gross, String rtc, String tax, String philhealth, String gsis, String pagibig, String others, String total_deductions, String net_received)
          {
               float[] widths = { 2, 4, 5, 4, 5, 4, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 3, 3, 4, 4, 1, 3, 1, 4 };
               PdfPTable data = new PdfPTable(25);
               data.SetWidths(widths);
               data.TotalWidth = 400;
               data.WidthPercentage = 100;

               int font_size = 8;

               data.AddCell(new PdfPCell(new Phrase("TOTAL OVERALL >>>", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Colspan = 3,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (salary.Length >= 5 || salary_deduction.Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(salary + "\n(" + salary_deduction + ")", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (subs.Length >= 5 || subs_deduction.Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(subs + "\n(" + subs_deduction + ")", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (gross.Length >= 5 || rtc.Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(gross + "\n" + rtc, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (tax.Length >= 5 || philhealth.Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(tax + "\n" + philhealth, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (gsis.Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(gsis, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Colspan = 6,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (pagibig.Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(pagibig, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Colspan = 2,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (others.Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(others, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Colspan = 4,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (total_deductions.Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(total_deductions, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (net_received.Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(net_received + "\n" + net_received, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Colspan = 2,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("159,816.00\n(0.00)", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Colspan = 3,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               return data;
          }


          public static PdfPTable RegularSalaryChargeItem(int count, String id, String name, String position, String salary, String subsistence, String gross_income, String tax, String gsis_premium, String gsis_policy_loan, String gsis_uoli, String pagibig_premium,
                String cfi, String coop, String total_deduction, String netamount_received, String pera, String subsistence_deduction, String Rata, String gsis_consoloan, String gsis_eml, String gsis_eda, String pagibig_loan,
                String simc, String disallowance, String absences, String longevity, String raTa, String gsis_hel, String longevity_deduction, String rel, String philhealth, String hazard, String hazard_deduction, String cellphone, String dbp, String mp2)
          {
               float[] widths = { 2, 4, 5, 4, 5, 4, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 3, 3, 4, 4, 1, 3, 1, 4 };
               PdfPTable data = new PdfPTable(25);
               data.SetWidths(widths);
               data.TotalWidth = 400;
               data.WidthPercentage = 100;

               int font_size = 8;
               int rate_size = 6;

               data.AddCell(new PdfPCell(new Phrase(count + "", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Rowspan = 6,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase(name, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Rowspan = 6,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(position, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Rowspan = 6,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(salary, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.TOP_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(subsistence, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.TOP_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(gross_income, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(tax, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Rowspan = 3,
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.TOP_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.TOP_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(gsis_premium, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.TOP_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("POL", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.TOP_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(gsis_policy_loan, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.TOP_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("REL", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.TOP_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(gsis_uoli, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.TOP_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.TOP_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(pagibig_premium, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.TOP_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("CFI", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.TOP_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(cfi, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.TOP_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("HWMPC", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.TOP_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(coop, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.TOP_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase(total_deduction, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Rowspan = 6,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(netamount_received, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("15", new Font(FontFactory.GetFont("HELVETICA", rate_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {

                    Border = PdfPCell.BOTTOM_BORDER,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Rowspan = 6,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Rowspan = 6,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(name + "\n" + id + " - (No Picture)", new Font(FontFactory.GetFont("HELVETICA", 6, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Rowspan = 6,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_BOTTOM
               });

               //2nd ROW
               data.AddCell(new PdfPCell(new Phrase(pera, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("(" + subsistence_deduction + ")", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(raTa, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("CON", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(gsis_consoloan, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("EML", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(gsis_eml, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("EDU", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(gsis_eda, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("MPL", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(pagibig_loan, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("SIMC", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(simc, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("DIS", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(disallowance, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase(netamount_received, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Rowspan = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("15", new Font(FontFactory.GetFont("HELVETICA", rate_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Rowspan = 2,
                    Border = 0,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               //3rd ROWS
               data.AddCell(new PdfPCell(new Phrase(absences, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Rowspan = 4,
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(longevity, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(cellphone, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {

                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("HLP", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(gsis_hel, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("100.00", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("UOI", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("100.00", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("MP2", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(mp2, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("REL", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(rel, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("DBP", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(dbp, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });



               // 4th ROW
               data.AddCell(new PdfPCell(new Phrase("(" + longevity_deduction + ")", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER

               });
               data.AddCell(new PdfPCell(new Phrase(cellphone, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Rowspan = 3,
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(philhealth, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Rowspan = 3,
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.TOP_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase(netamount_received, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {

                    Border = PdfPCell.LEFT_BORDER,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("30", new Font(FontFactory.GetFont("HELVETICA", rate_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {

                    Border = PdfPCell.BOTTOM_BORDER,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               //5TH ROWS
               data.AddCell(new PdfPCell(new Phrase(hazard, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER

               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(netamount_received, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {

                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("30", new Font(FontFactory.GetFont("HELVETICA", rate_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {

                    Border = PdfPCell.BOTTOM_BORDER,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               //6TH ROWS
               data.AddCell(new PdfPCell(new Phrase("(" + hazard_deduction + ")", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER

               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               return data;
          }


          public static PdfPTable RegularSalaryChargeFooter()
          {
               float[] widths = { 2, 4, 5, 4, 5, 4, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 3, 4, 2, 3, 4, 3, 1, 5 };
               PdfPTable data = new PdfPTable(25);
               data.SetWidths(widths);
               data.TotalWidth = 400;
               data.WidthPercentage = 100;

               data.AddCell(new PdfPCell(new Phrase("New Line", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Border = 0,
                    Colspan = 25,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("% - PERA, R - Ra, T - Ta, C - Cellphone Allowance", new Font(FontFactory.GetFont("HELVETICA", 9, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = 0,
                    Colspan = 25,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               return data;
          }
          public static PdfPTable RegularSalaryChargePage(String salary, String salary_deduction, String subs, String subs_deduction, String gross, String rtc, String tax, String philhealth, String gsis, String pagibig, String others, String total_deductions, String net_received)
          {
               float[] widths = { 2, 4, 5, 4, 5, 4, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 3, 3, 4, 4, 1, 3, 1, 4 };
               PdfPTable data = new PdfPTable(25);
               data.SetWidths(widths);
               data.TotalWidth = 400;
               data.WidthPercentage = 100;

               int font_size = 8;

               data.AddCell(new PdfPCell(new Phrase("TOTAL THIS PAGE >>>", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Colspan = 3,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (salary.Length >= 5 || salary_deduction.Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(salary + "\n(" + salary_deduction + ")", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (subs.Length >= 5 || subs_deduction.Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(subs + "\n(" + subs_deduction + ")", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (gross.Length >= 5 || rtc.Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(gross + "\n" + rtc, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (tax.Length >= 5 || philhealth.Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(tax + "\n" + philhealth, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (gsis.Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(gsis, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Colspan = 6,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (pagibig.Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(pagibig, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Colspan = 2,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (others.Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(others, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Colspan = 4,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (total_deductions.Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(total_deductions, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {

                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (net_received.Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(net_received + "\n" + net_received, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.BOTTOM_BORDER,
                    Colspan = 2,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("159,816.00\n(0.00)", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Colspan = 3,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               return data;
          }
          public static PdfPTable RegularSalaryChargeHeader(int count, string month, int year, string salary_charge)
          {
               float[] widths = { 2, 4, 5, 4, 5, 4, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 3, 3, 4, 4, 1, 3, 1, 4 };

               PdfPTable header = new PdfPTable(25);
               header.SetWidths(widths);
               header.TotalWidth = 400;
               header.WidthPercentage = 100;
               switch (salary_charge)
               {
                    case "RD_ARD":
                         salary_charge = "REGIONAL DIRECTOR/ADMINISTRATIVE REGIONAL DIRECTOR";
                         break;

                    case "MSD":
                         salary_charge = "MANAGEMENT SUPPORT DIVISION";
                         break;

                    case "LHSD":
                         salary_charge = "LOCAL HEALTH SUPPORT DIVISION";
                         break;
                    case "RLED":
                         salary_charge = "REGULATION AND LICENSING ENFORCEMENT DIVISION";
                         break;
               }
               header.AddCell(new PdfPCell(new Phrase(salary_charge, new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
               {
                    Border = 0,
                    Colspan = 10,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("GENRAL PAYROLL", new Font(FontFactory.GetFont("HELVETICA", 11, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = 0,
                    Rowspan = 2,
                    Colspan = 9,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("Page No.: " + count, new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = 0,
                    Colspan = 7,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase(salary_charge, new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = 0,
                    Colspan = 10,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("Journal Voucher No. ______________", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = 0,
                    Colspan = 7,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               header.AddCell(new PdfPCell(new Phrase("WE HEREBY ACKNOWLEDGE to have received from the DOH - RO7, Osmeña Blvd, Cebu City, the sums therein specified opposite our respective names,", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = 0,
                    Colspan = 26,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("being in full compensation for our services for the period " + month + " 1-31, " + year + ", except as noted otherwise in the Remarks columns.", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = 0,
                    Colspan = 26,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               header.AddCell(new PdfPCell(new Phrase("NO.", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Border = 0,
                    Colspan = 26,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               header.AddCell(new PdfPCell(new Phrase("NO.", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("NO.", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("NO.", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("S  A  L  A  R  Y", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Colspan = 3,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("D E D U C T I O N S", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Colspan = 13,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("T", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(255, 255, 255)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,

                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("T", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(255, 255, 255)))))
               {
                    Colspan = 2,
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               header.AddCell(new PdfPCell(new Phrase("T", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(255, 255, 255)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("T", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(255, 255, 255)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("T", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(255, 255, 255)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               header.AddCell(new PdfPCell(new Phrase("NO.", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Rowspan = 2,
                    PaddingTop = 15,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("NAME", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Rowspan = 2,
                    PaddingTop = 15,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("DESIGNATION SALARY", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Rowspan = 2,
                    PaddingTop = 15,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("BASIC *", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    PaddingTop = 15,
                    Rowspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("SUBS/\nLONGEVITY/\nHAZARD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("GROSS\nINCOME\nO.A.INC.", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("WTAX", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    PaddingTop = 8,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("G  S  I  S", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    PaddingTop = 8,
                    Colspan = 6,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("PAGIBIG", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    PaddingTop = 8,
                    Colspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("OTHERS", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    PaddingTop = 8,
                    Colspan = 4,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               header.AddCell(new PdfPCell(new Phrase("TOTAL\nDEDUCTIONS", new Font(FontFactory.GetFont("HELVETICA", 6, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    Rowspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("NET AMOUNT RECEIVED", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    Colspan = 2,
                    Rowspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               header.AddCell(new PdfPCell(new Phrase("SIGNATURE", new Font(FontFactory.GetFont("HELVETICA", 5, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    PaddingTop = 8,
                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    Rowspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("IN\nIT\nIA\nL", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    Rowspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("REMARKS", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    PaddingTop = 8,
                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    Rowspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });


               header.AddCell(new PdfPCell(new Phrase("(Less Abs/Undertime)", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.LEFT_BORDER,
                    Colspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("PHIL HEALTH", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("COD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("AMOUNT", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("COD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("AMOUNT", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("COD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("AMOUNT", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("COD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("AMOUNT", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("COD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("AMOUNT", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("COD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("AMOUNT", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               return header;
          }


          public static bool RegularSalaryChargeSummary(int month, int year, string salary_charge, string file_path)
          {



               List<RegularPayrollModel> list = PayrollDatabase.Instance.GenerateSummaryRegularBySalaryCharge(month, year, salary_charge);

               if (list.Count > 0)
               {
                    Document doc = new Document();
                    doc.SetMargins(15, 15, 15, 15);
                    doc.SetPageSize(PageSize.LEGAL.Rotate());

                    PdfPTable main = new PdfPTable(1);
                    main.TotalWidth = 400;
                    main.WidthPercentage = 100;

                    float[] widths = { 2, 5, 5, 4, 4, 4, 3, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 3, 4, 1, 2, 3, 1, 4, 1, 4 };
                    PdfPTable header = new PdfPTable(26);
                    header.SetWidths(widths);
                    header.TotalWidth = 400;
                    header.WidthPercentage = 100;

                    PdfPTable items = new PdfPTable(26);
                    items.SetWidths(widths);
                    items.TotalWidth = 400;
                    items.WidthPercentage = 100;

                    int page_count = 1;

                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(file_path, FileMode.Create));
                    doc.Open();

                    main.AddCell(new PdfPCell(RegularSalaryChargeHeader(page_count, DateUtility.GetMonthName(month, year), year, salary_charge))
                    {
                         Border = 0,
                         HorizontalAlignment = Element.ALIGN_CENTER,
                         VerticalAlignment = Element.ALIGN_CENTER
                    });

                    doc.Add(main);
                    main.FlushContent();
                    int MAX_ROW_SIZE = 300;

                    int counter = 0;
                    decimal page_salary = 0;
                    decimal page_salary_deduction = 0;
                    decimal page_subs = 0;
                    decimal page_subs_deduction = 0;
                    decimal page_gross = 0;
                    decimal page_rtc = 0;
                    decimal page_tax = 0;
                    decimal page_philhealth = 0;
                    decimal page_gsis = 0;
                    decimal page_pagibig = 0;
                    decimal page_others = 0;
                    decimal page_total_deduction = 0;
                    decimal page_net_received = 0;

                    decimal overall_salary = 0;
                    decimal overall_salary_deduction = 0;
                    decimal overall_subs = 0;
                    decimal overall_subs_deduction = 0;
                    decimal overall_gross = 0;
                    decimal overall_rtc = 0;
                    decimal overall_tax = 0;
                    decimal overall_philhealth = 0;
                    decimal overall_gsis = 0;
                    decimal overall_pagibig = 0;
                    decimal overall_others = 0;
                    decimal overall_total_deduction = 0;
                    decimal overall_net_received = 0;

                    foreach (var data in list)
                    {
                         if (main.CalculateHeights() >= MAX_ROW_SIZE)
                         {

                              page_count++;
                              MAX_ROW_SIZE = 390;
                              main.AddCell(new PdfPCell(RegularSalaryChargePage(page_salary.ToString("#,##0.00"), page_salary_deduction.ToString("#,##0.00"), page_subs.ToString("#,##0.00"), page_subs_deduction.ToString("#,##0.00"),
                              page_gross.ToString("#,##0.00"), page_rtc.ToString("#,##0.00"), page_tax.ToString("#,##0.00"), page_philhealth.ToString("#,##0.00"), page_gsis.ToString("#,##0.00"),
                              page_pagibig.ToString("#,##0.00"), page_others.ToString("#,##0.00"), page_total_deduction.ToString("#,##0.00"), page_net_received.ToString("#,##0.00")))
                              {
                                   Border = 0,
                                   HorizontalAlignment = Element.ALIGN_CENTER,
                                   VerticalAlignment = Element.ALIGN_CENTER
                              });

                              main.AddCell(new PdfPCell(RegularSalaryChargeFooter())
                              {
                                   Border = 0,
                                   HorizontalAlignment = Element.ALIGN_CENTER,
                                   VerticalAlignment = Element.ALIGN_CENTER
                              });

                              page_salary = 0;
                              page_salary_deduction = 0;
                              page_subs = 0;
                              page_subs_deduction = 0;
                              page_gross = 0;
                              page_rtc = 0;
                              page_tax = 0;
                              page_philhealth = 0;
                              page_gsis = 0;
                              page_pagibig = 0;
                              page_others = 0;
                              page_total_deduction = 0;
                              page_net_received = 0;

                              doc.Add(main);
                              main.FlushContent();
                              doc.NewPage();
                              main.AddCell(new PdfPCell(RegularSalaryChargeHeader(page_count, DateUtility.GetMonthName(month, year), year, salary_charge))
                              {
                                   Border = 0,
                                   HorizontalAlignment = Element.ALIGN_CENTER,
                                   VerticalAlignment = Element.ALIGN_CENTER
                              });
                         }

                         String id = "";
                         String name = "";
                         String position = "";
                         if (data.Employee != null)
                         {
                              id = data.Employee.ID;
                              name = data.Employee.Firstname + " " + data.Employee.Middlename + " " + data.Employee.Lastname;
                              position = data.Employee.Designation;
                         }

                         decimal salary = decimal.Parse(data.Salary);

                         decimal pera = decimal.Parse(data.Pera);
                         decimal salary_pera = salary + pera;
                         int minutes_late = data.MinutesLate;
                         int count = data.DaysAbsent.Split(',').Length;
                         if (count > 0 && !data.DaysAbsent.Split(',')[0].Equals(""))
                         {
                              minutes_late += (480 * count);
                         }
                         int working_days = int.Parse(data.WorkDays);
                         decimal per_day = 0;
                         decimal absences = 0;
                         if (working_days != 0 && salary != 0)
                         {
                              per_day = salary / working_days;
                              absences = Math.Round((minutes_late * (((per_day) / 8) / 60)), 2, MidpointRounding.AwayFromZero);
                         }
                         decimal philhealth = decimal.Parse(data.PhilHealth);
                         decimal tax = decimal.Parse(data.Tax);
                         decimal gsis_premium = decimal.Parse(data.GSIS_Premium);
                         decimal gsis_consoloan = decimal.Parse(data.GSIS_Consoloan);
                         decimal gsis_policy_loan = decimal.Parse(data.GSIS_PolicyLoan);
                         decimal gsis_eml = decimal.Parse(data.GSIS_EML);
                         decimal gsis_uoli = decimal.Parse(data.GSIS_UOLI);
                         decimal pagibig_premium = decimal.Parse(data.Pagibig_Premium);
                         decimal pagibig_loan = decimal.Parse(data.Pagibig_Loan);
                         decimal cfi = decimal.Parse(data.CFI);
                         decimal simc = decimal.Parse(data.SIMC);
                         decimal hwmpc = decimal.Parse(data.HWMPC);
                         decimal gsis_edu = decimal.Parse(data.GSIS_EDU);
                         decimal disallowances = decimal.Parse(data.Disallowances);
                         decimal gsis_help = decimal.Parse(data.GSIS_Help);
                         decimal rel = decimal.Parse(data.GSIS_REL);
                         decimal dbp = decimal.Parse(data.DBP);
                         decimal mp2 = decimal.Parse(data.MP2);

                         decimal total_deductions = tax + philhealth + gsis_premium + gsis_consoloan + gsis_policy_loan + gsis_eml + gsis_uoli + pagibig_premium + pagibig_loan
                         + cfi + simc + hwmpc + gsis_edu + disallowances + gsis_help + rel + dbp + mp2;

                         decimal net_salary_pera = salary_pera - total_deductions;

                         decimal subsistence_total = decimal.Parse("0.00");
                         decimal subsistence_deduction = decimal.Parse("0.00");
                         decimal subsistence_total_deduction = decimal.Parse("0.00");

                         /*
                           if (subsistence != null)
                           {
                               subsistence_total = decimal.Parse(subsistence.SubsistenceAllowance) + decimal.Parse(subsistence.LaundryAllowance);
                               subsistence_deduction = decimal.Parse(subsistence.Absences) + decimal.Parse(subsistence.HWMCP);
                               subsistence_total_deduction = subsistence_total - subsistence_deduction;
                           }
                           */


                         String display_subsistence_total = subsistence_total.ToString("#,##0.00");
                         String display_subsistence_deduction = (subsistence_deduction > 0) ? "(" + subsistence_deduction.ToString("#,##0.00") + ")" : subsistence_deduction.ToString("#,##0.00");
                         String display_subsistence_total_deduction = subsistence_total_deduction.ToString("#,##0.00");

                         decimal lp_1 = decimal.Parse("0.00");
                         decimal lp_2 = decimal.Parse("0.00");
                         decimal lp_3 = decimal.Parse("0.00");
                         decimal lp_4 = decimal.Parse("0.00");
                         decimal lp_5 = decimal.Parse("0.00");
                         decimal lp_total = lp_1 + lp_2 + lp_3 + lp_4 + lp_5;
                         decimal lp_disallowance = decimal.Parse("0.00");
                         decimal lp_longevity_total = lp_total - lp_disallowance;

                         /*  Longevity longevity = DatabaseConnect.Instance.GenerateLongevity(id, month, year);
                           if (longevity != null)
                           {
                               lp_1 = decimal.Parse(longevity.Salary1) * decimal.Parse("0.05");
                               lp_2 = decimal.Parse(longevity.Salary2) * decimal.Parse("0.05");
                               lp_3 = decimal.Parse(longevity.Salary3) * decimal.Parse("0.05");
                               lp_4 = decimal.Parse(longevity.Salary4) * decimal.Parse("0.05");
                               lp_5 = decimal.Parse(longevity.Salary5) * decimal.Parse("0.05");
                               lp_total = Math.Round(lp_1 + lp_2 + lp_3 + lp_4 + lp_5, 2, MidpointRounding.AwayFromZero);
                               lp_disallowance = decimal.Parse(longevity.Disallowance);
                               lp_longevity_total = lp_total - lp_disallowance;
                           }
                           */

                         string display_lp_total = lp_total.ToString("#,##0.00");
                         string display_lp_disallowance = (lp_disallowance > 0) ? "(" + lp_disallowance.ToString("#,##0.00") + ")" : lp_disallowance.ToString("#,##0.00");
                         string display_lp_longevity_total = lp_longevity_total.ToString("#,##0.00");

                         decimal cell_amount = decimal.Parse("0.00");
                         decimal cell_nov_billing = decimal.Parse("0.00");
                         decimal cell_net_amount = cell_amount - cell_nov_billing;

                         /*CellphoneAllowance cellphoneAllowance = DatabaseConnect.Instance.GenerateCommunicable(id, month, year);
                         if (cellphoneAllowance != null)
                         {
                             cell_amount = decimal.Parse(cellphoneAllowance.Amount);
                             cell_nov_billing = decimal.Parse(cellphoneAllowance.NovBilling);
                             cell_net_amount = cell_amount - cell_nov_billing;
                         }*/


                         string display_cell_amount = cell_amount.ToString("#,##0.00");
                         string display_cell_nov_billing = (cell_nov_billing > 0) ? "(" + cell_nov_billing.ToString("#,##0.00") + ")" : cell_nov_billing.ToString("#,##0.00");
                         string display_cell_net_amount = cell_net_amount.ToString("#,##0.00");

                         decimal hazard_net = decimal.Parse("0.00");
                         decimal hazard_deduction = decimal.Parse("0.00");

                         HazardViewModel hazard = data.GetHazard();
                         if (hazard != null)
                         {
                              hazard_deduction = hazard.Deductions;
                              hazard_net = hazard.NetAmount;
                         }

                         decimal gross_income = (salary_pera - absences) + (subsistence_total_deduction) + (lp_longevity_total) + (hazard_net);

                         decimal rata_net = decimal.Parse("0.00") + decimal.Parse("0.00") - decimal.Parse("0.00");
                         decimal rata_deduction = decimal.Parse("0.00");
                         decimal rata_total = decimal.Parse("0.00") + decimal.Parse("0.00");
                         decimal ra = decimal.Parse("0.00");
                         decimal ta = decimal.Parse("0.00");

                         RataViewModel rata = data.GetRata();
                         if (rata != null)
                         {
                              rata_deduction = rata.Deduction;
                              ra = rata.Ra;
                              ta = rata.Ta;
                         }

                         rata_total = ra + ta;
                         rata_net = rata_total - rata_deduction;

                         String display_rata_net = rata_net.ToString("#,##0.00");
                         String display_rata_deduction = (rata_deduction > 0) ? "(" + rata_deduction.ToString("#,##0.00") + ")" : rata_deduction.ToString("#,##0.00");
                         decimal mid_year = decimal.Parse("0.00");
                         decimal end_year = decimal.Parse("0.00");
                         decimal mid_deduction = decimal.Parse("0.00");

                         decimal overall_pay = salary_pera + subsistence_total + lp_total + cell_amount + hazard_net + rata_total + mid_year;
                         decimal overall_deduction = total_deductions + subsistence_deduction + lp_disallowance + cell_nov_billing + hazard_deduction + rata_deduction + mid_deduction;
                         String display_overall_deduction = (overall_deduction > 0) ? "(" + overall_deduction.ToString("#,##0.00") + ")" : overall_deduction.ToString("#,##0.00");

                         decimal overall_net = overall_pay - overall_deduction;
                         String display_overall_net = (overall_net > 0) ? "(" + overall_net.ToString("#,##0.00") + ")" : overall_net.ToString("#,##0.00");

                         decimal total_pay_1st = (net_salary_pera / 2) + subsistence_total_deduction + lp_longevity_total;
                         decimal total_pay_2nd = (net_salary_pera / 2) + hazard_net + cell_net_amount + mid_year + rata_total;
                         decimal net_received = (gross_income + rata_net + cell_net_amount) - total_deductions;

                         decimal half_net_received = net_received / 2;

                         page_salary += salary + pera;
                         page_salary_deduction += absences;
                         page_subs += subsistence_total + lp_total + hazard_net;
                         page_subs_deduction += subsistence_deduction + lp_disallowance + hazard_deduction;
                         page_gross += gross_income;
                         page_rtc += rata_net + cell_net_amount;
                         page_tax += tax;
                         page_philhealth += philhealth;
                         page_gsis += gsis_consoloan + gsis_edu + gsis_eml + gsis_help + gsis_policy_loan + gsis_premium + gsis_uoli;
                         page_pagibig += pagibig_loan + pagibig_premium + mp2;
                         page_others += cfi + simc + rel + disallowances + hwmpc + dbp;
                         page_total_deduction += total_deductions;
                         page_net_received += half_net_received;

                         overall_salary += salary + pera;
                         overall_salary_deduction += absences;
                         overall_subs += subsistence_total + lp_total + hazard_net;
                         overall_subs_deduction += subsistence_deduction + lp_disallowance + hazard_deduction;
                         overall_gross += gross_income; ;
                         overall_rtc += rata_net + cell_net_amount;
                         overall_tax += tax;
                         overall_philhealth += philhealth;
                         overall_gsis += gsis_consoloan + gsis_edu + gsis_eml + gsis_help + gsis_policy_loan + gsis_premium + gsis_uoli;
                         overall_pagibig += pagibig_loan + pagibig_premium + mp2;
                         overall_others += cfi + simc + rel + disallowances + hwmpc;
                         overall_total_deduction += total_deductions;
                         overall_net_received += half_net_received;



                         items = RegularSalaryChargeItem(++counter, id, name, position, salary.ToString("#,##0.00"), subsistence_total.ToString("#,##0.00"), gross_income.ToString("#,##0.00"), tax.ToString("#,##0.00"),
                         gsis_premium.ToString("#,##0.00"), gsis_policy_loan.ToString("#,##0.00"), gsis_uoli.ToString("#,##0.00"), pagibig_premium.ToString("#,##0.00"), cfi.ToString("#,##0.00"),
                         hwmpc.ToString("#,##0.00"), total_deductions.ToString("#,##0.00"), half_net_received.ToString("#,##0.00"), "% " + pera.ToString("#,##0.00"), subsistence_deduction.ToString("#,##0.00"),
                         "RT " + rata_net.ToString("#,##0.00"), gsis_consoloan.ToString("#,##0.00"), gsis_eml.ToString("#,##0.00"), gsis_edu.ToString("#,##0.00"), pagibig_loan.ToString("#,##0.00"), simc.ToString("#,##0.00"),
                         disallowances.ToString("#,##0.00"), "(" + absences.ToString("#,##0.00") + ")", lp_total.ToString("#,##0.00"), "RT " + rata_net.ToString("#,##0.00"), gsis_help.ToString("#,##0.00"), lp_disallowance.ToString("#,##0.00"),
                         rel.ToString("#,##0.00"), philhealth.ToString("#,##0.00"), hazard_net.ToString("#,##0.00"), hazard_deduction.ToString("#,##0.00"), "C " + cell_net_amount.ToString("#,##0.00"), dbp.ToString("#,##0.00"), mp2.ToString("#,##0.00"));

                         main.AddCell(new PdfPCell(items)
                         {
                              Border = 0,
                              HorizontalAlignment = Element.ALIGN_CENTER,
                              VerticalAlignment = Element.ALIGN_CENTER
                         });
                    }

                    main.AddCell(new PdfPCell(RegularSalaryChargePage(page_salary.ToString("#,##0.00"), page_salary_deduction.ToString("#,##0.00"), page_subs.ToString("#,##0.00"), page_subs_deduction.ToString("#,##0.00"),
                    page_gross.ToString("#,##0.00"), page_rtc.ToString("#,##0.00"), page_tax.ToString("#,##0.00"), page_philhealth.ToString("#,##0.00"), page_gsis.ToString("#,##0.00"),
                    page_pagibig.ToString("#,##0.00"), page_others.ToString("#,##0.00"), page_total_deduction.ToString("#,##0.00"), page_net_received.ToString("#,##0.00")))
                    {
                         Border = 0,
                         HorizontalAlignment = Element.ALIGN_CENTER,
                         VerticalAlignment = Element.ALIGN_CENTER
                    });
                    main.AddCell(new PdfPCell(RegularSalaryChargeOverall(overall_salary.ToString("#,##0.00"), overall_salary_deduction.ToString("#,##0.00"), overall_subs.ToString("#,##0.00"), overall_subs_deduction.ToString("#,##0.00"),
                    overall_gross.ToString("#,##0.00"), overall_rtc.ToString("#,##0.00"), overall_tax.ToString("#,##0.00"), overall_philhealth.ToString("#,##0.00"), overall_gsis.ToString("#,##0.00"),
                    overall_pagibig.ToString("#,##0.00"), overall_others.ToString("#,##0.00"), overall_total_deduction.ToString("#,##0.00"), overall_net_received.ToString("#,##0.00")))
                    {
                         Border = 0,
                         HorizontalAlignment = Element.ALIGN_CENTER,
                         VerticalAlignment = Element.ALIGN_CENTER
                    });
                    main.AddCell(new PdfPCell(RegularSalaryChargeFooter())
                    {
                         Border = 0,
                         HorizontalAlignment = Element.ALIGN_CENTER,
                         VerticalAlignment = Element.ALIGN_CENTER
                    });


                    doc.Add(main);
                    doc.Close();
                    return true;
               }
               else
               {
                    return false;
               }
          }
     }
}