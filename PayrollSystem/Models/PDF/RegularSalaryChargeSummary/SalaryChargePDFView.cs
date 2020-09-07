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
          public static PdfPTable RegularSalaryChargeOverall(SalaryPageTotal pageTotal)
          {
               float[] widths = { 2, 4, 5, 4, 5, 4, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 3, 3, 4, 4, 1, 3, 1, 4 };
               PdfPTable data = new PdfPTable(25);
               data.SetWidths(widths);
               data.TotalWidth = 400;
               data.WidthPercentage = 100;

               int font_size = 8;

               data.AddCell(new PdfPCell(new Phrase("TOTAL OVERALL >>>", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Colspan = 3,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (pageTotal.BasicTop.ToString("#,##0.00").Length >= 5 || pageTotal.BasicDown.ToString("#,##0.00").Length >= 5) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(pageTotal.BasicTop.ToString("#,##0.00") + "\n(" + pageTotal.BasicDown.ToString("#,##0.00") + ")", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (pageTotal.BenefitsTop.ToString("#,##0.00").Length >= 5 || pageTotal.BenefitsDown.ToString("#,##0.00").Length >= 5) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(pageTotal.BenefitsTop.ToString("#,##0.00") + "\n(" + pageTotal.BenefitsDown.ToString("#,##0.00") + ")", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (pageTotal.GrossTop.ToString("#,##0.00").Length >= 5 || pageTotal.GrossDown.ToString("#,##0.00").Length >= 5) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(pageTotal.GrossTop.ToString("#,##0.00") + "\n" + pageTotal.GrossDown.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (pageTotal.TaxTop.ToString("#,##0.00").Length >= 5 || pageTotal.TaxDown.ToString("#,##0.00").Length >= 5) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(pageTotal.TaxTop.ToString("#,##0.00") + "\n" + pageTotal.TaxDown.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (pageTotal.GsisTop.ToString("#,##0.00").Length >= 5) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(pageTotal.GsisTop.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Colspan = 6,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (pageTotal.PagibigTop.ToString("#,##0.00").Length >= 5) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(pageTotal.PagibigTop.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Colspan = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (pageTotal.OthersTop.ToString("#,##0.00").Length >= 5) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(pageTotal.OthersTop.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Colspan = 4,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (pageTotal.TotalDeductionTop.ToString("#,##0.00").Length >= 5) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(pageTotal.TotalDeductionTop.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (pageTotal.NetAmountTop.ToString("#,##0.00").Length >= 5 || pageTotal.NetAmountDown.ToString("#,##0.00").Length >= 5) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(pageTotal.NetAmountTop.ToString("#,##0.00") + "\n" + pageTotal.NetAmountDown.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Colspan = 2,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("159,816.00\n(0.00)", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
                    Colspan = 3,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               return data;
          }


          public static PdfPTable RegularSalaryChargeItem(int count, string id, string name, string position, RegularPayslipModel model)
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
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Rowspan = 6,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase(name, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Rowspan = 6,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(position, new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Rowspan = 6,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               font_size = (model.FirstHalf.BasicSalary.ToString("#,##0.00").Length >= 10) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.BasicSalary.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {

                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.Subsistence.ToString("#,##0.00").Length >= 12) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.Subsistence.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = ((model.FirstHalf.GetNetSubsistence() + model.FirstHalf.GetNetLongevity() + model.SecondHalf.GetNetHazard()).ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase((model.FirstHalf.GetNetSubsistence() + model.FirstHalf.GetNetLongevity() + model.SecondHalf.GetNetHazard()).ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.Tax.ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.Tax.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Rowspan = 3,
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.GsisPremium.ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.GsisPremium.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("POL", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.GsisPolicyLoan.ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.GsisPolicyLoan.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("REL", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.Rel.ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.Rel.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.PagibigPremium.ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.PagibigPremium.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("CFI", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.Cfi.ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.Cfi.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("HWMPC", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.Hwmpc.ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.Hwmpc.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.GetTotalDeduction().ToString("#,##0.00").Length >= 10) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.GetTotalDeduction().ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Rowspan = 6,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.GetTotalPay().ToString("#,##0.00").Length >= 10) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.GetTotalPay().ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
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
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Rowspan = 6,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Rowspan = 6,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase(name + "\n" + id + " - (No Picture)", new Font(FontFactory.GetFont("HELVETICA", 6, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Rowspan = 6,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_BOTTOM
               });

               //2nd ROW
               font_size = 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.Pera.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.SubsistenceDeduction.ToString("#,##0.00").Length >= 10) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase("(" + model.FirstHalf.SubsistenceDeduction.ToString("#,##0.00")+ ")", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.SecondHalf.Rata.ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase("RT "+model.SecondHalf.Rata.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", 6, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("CON", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.GsisConsoLoan.ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.GsisConsoLoan.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("EML", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.GsisEml.ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.GsisEml.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("EDU", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.GsisEdu.ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.GsisEdu.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("MPL", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.PagibigLoan.ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.PagibigLoan.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("SIMC", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.Simc.ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.Simc.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("DIS", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.Disallowance.ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.Disallowance.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
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
               font_size = (model.FirstHalf.Absences.ToString("#,##0.00").Length >= 10) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase("("+model.FirstHalf.Absences.ToString("#,##0.00")+")", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Rowspan = 4,
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.Longevity.ToString("#,##0.00").Length >= 12) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.Longevity.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.SecondHalf.CellphoneAllowance.ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase("C "+model.SecondHalf.CellphoneAllowance.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {

                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("HLP", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.GsisHelp.ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.GsisHelp.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("100.00", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("UOI", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("100.00", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("MP2", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.PagibigMp2.ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.PagibigMp2.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("REL", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.Rel.ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.Rel.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("DBP", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.Dbp.ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.Dbp.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });



               // 4th ROW
               font_size = (model.FirstHalf.LongevityDeduction.ToString("#,##0.00").Length >= 10) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase("(" + model.FirstHalf.LongevityDeduction.ToString("#,##0.00") + ")", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER

               });

               data.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Rowspan = 3,
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.FirstHalf.PhilHealth.ToString("#,##0.00").Length >= 9) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.FirstHalf.PhilHealth.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Rowspan = 3,
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.TOP_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (model.SecondHalf.GetTotalPay().ToString("#,##0.00").Length >= 10) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.SecondHalf.GetTotalPay().ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
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
               font_size = (model.SecondHalf.HazardPay.ToString("#,##0.00").Length >= 12) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(model.SecondHalf.HazardPay.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER

               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Border = PdfPCell.LEFT_BORDER,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("30", new Font(FontFactory.GetFont("HELVETICA", rate_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {

                    Border = 0,
                    Padding = 3,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               //6TH ROWS
               font_size = ((model.SecondHalf.Hwmpc + model.SecondHalf.Mortuary + model.SecondHalf.OtherPayable).ToString("#,##0.00").Length >= 10) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase("(" + (model.SecondHalf.Hwmpc + model.SecondHalf.Mortuary + model.SecondHalf.OtherPayable).ToString("#,##0.00")  + ")", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Padding = 3,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER

               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("PRM", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Padding = 2,
                    Border =  PdfPCell.BOTTOM_BORDER,
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
          public static PdfPTable RegularSalaryChargePage(SalaryPageTotal pageTotal)
          {
               float[] widths = { 2, 4, 5, 4, 5, 4, 3, 2, 3, 2, 3, 2, 3, 2, 3, 2, 3, 3, 3, 4, 4, 1, 3, 1, 4 };
               PdfPTable data = new PdfPTable(25);
               data.SetWidths(widths);
               data.TotalWidth = 400;
               data.WidthPercentage = 100;

               int font_size = 8;

               data.AddCell(new PdfPCell(new Phrase("TOTAL THIS PAGE >>>", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Colspan = 3,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (pageTotal.BasicTop.ToString("#,##0.00").Length >= 5 || pageTotal.BasicDown.ToString("#,##0.00").Length >= 5) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(pageTotal.BasicTop.ToString("#,##0.00") + "\n(" + pageTotal.BasicDown.ToString("#,##0.00") + ")", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (pageTotal.BenefitsTop.ToString("#,##0.00").Length >= 5 || pageTotal.BenefitsDown.ToString("#,##0.00").Length >= 5) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(pageTotal.BenefitsTop.ToString("#,##0.00") + "\n(" + pageTotal.BenefitsDown.ToString("#,##0.00") + ")", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (pageTotal.GrossTop.ToString("#,##0.00").Length >= 5 || pageTotal.GrossDown.ToString("#,##0.00").Length >= 5) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(pageTotal.GrossTop.ToString("#,##0.00") + "\n" + pageTotal.GrossDown.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (pageTotal.TaxTop.ToString("#,##0.00").Length >= 5 || pageTotal.TaxDown.ToString("#,##0.00").Length >= 5) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(pageTotal.TaxTop.ToString("#,##0.00") + "\n" + pageTotal.TaxDown.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (pageTotal.GsisTop.ToString("#,##0.00").Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(pageTotal.GsisTop.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Colspan = 6,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (pageTotal.PagibigTop.ToString("#,##0.00").Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(pageTotal.PagibigTop.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Colspan = 2,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (pageTotal.OthersTop.ToString("#,##0.00").Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(pageTotal.OthersTop.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Colspan = 4,
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (pageTotal.TotalDeductionTop.ToString("#,##0.00").Length >= 5) ? 7 : 8;
               data.AddCell(new PdfPCell(new Phrase(pageTotal.TotalDeductionTop.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               font_size = (pageTotal.NetAmountTop.ToString("#,##0.00").Length >= 5 || pageTotal.NetAmountDown.ToString("#,##0.00").Length >= 5) ? 6 : 8;
               data.AddCell(new PdfPCell(new Phrase(pageTotal.NetAmountTop.ToString("#,##0.00") + "\n" + pageTotal.NetAmountDown.ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Colspan = 2,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               data.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", font_size, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Border = PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.RIGHT_BORDER,
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

               //1st Row
               header.AddCell(new PdfPCell(new Phrase("NO.", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Border =0,
                    Colspan = 26,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               header.AddCell(new PdfPCell(new Phrase("NO.", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("NO.", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("NO.", new Font(FontFactory.GetFont("HELVETICA", 7, Font.NORMAL, new BaseColor(255, 255, 255)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("S  A  L  A  R  Y", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Colspan = 3,
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("D E D U C T I O N S", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Colspan = 13,
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("T", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(255, 255, 255)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("T", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(255, 255, 255)))))
               {
                    Colspan = 2,
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               header.AddCell(new PdfPCell(new Phrase("T", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(255, 255, 255)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("T", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(255, 255, 255)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("T", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(255, 255, 255)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               //2nd Row
               header.AddCell(new PdfPCell(new Phrase("NO.", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Rowspan = 2,
                    PaddingTop = 15,
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("NAME", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Rowspan = 2,
                    PaddingTop = 15,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("DESIGNATION SALARY", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Rowspan = 2,
                    PaddingTop = 15,
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("BASIC *", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    PaddingTop = 15,
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Rowspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("SUBS/\nLONGEVITY/\nHAZARD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("GROSS\nINCOME\nO.A.INC.", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("WTAX", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    PaddingTop = 8,
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("G  S  I  S", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    PaddingTop = 8,
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Colspan = 6,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("PAGIBIG", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    PaddingTop = 8,
                    Colspan = 2,
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("OTHERS", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    PaddingTop = 8,
                    Colspan = 4,
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               header.AddCell(new PdfPCell(new Phrase("TOTAL\nDEDUCTIONS", new Font(FontFactory.GetFont("HELVETICA", 6, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Rowspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("NET AMOUNT RECEIVED", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Colspan = 2,
                    Rowspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });

               header.AddCell(new PdfPCell(new Phrase("SIGNATURE", new Font(FontFactory.GetFont("HELVETICA", 5, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    PaddingTop = 8,
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Rowspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("IN\nIT\nIA\nL", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("REMARKS", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    PaddingTop = 8,
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Rowspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });


               header.AddCell(new PdfPCell(new Phrase("(Less Abs/Undertime)", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border =  PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    Colspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("PHIL HEALTH", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("COD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("AMOUNT", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("COD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("AMOUNT", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("COD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("AMOUNT", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("COD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("AMOUNT", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("COD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("AMOUNT", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("COD", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               header.AddCell(new PdfPCell(new Phrase("AMOUNT", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
               {
                    Border = PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.BOTTOM_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
               });
               return header;
          }


          public static void RegularSalaryChargeSummary(int month, int year, string salary_charge, string file_path, List<SalaryChargeItem> list)
          {

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

                    string m_page_subs = "";
                    string m_page_subs_deduction = "";

                    decimal page_gross = 0;
                    decimal page_rtc = 0;
                    decimal page_tax = 0;
                    decimal page_philhealth = 0;
                    decimal page_gsis = 0;
                    decimal page_pagibig = 0;
                    decimal page_others = 0;
                    decimal page_total_deduction = 0;
                    decimal page_first_half = 0;
                    decimal page_second_half = 0;

                    decimal overall_salary = 0;
                    decimal overall_salary_deduction = 0;

                    decimal overall_subs = 0;
                    decimal overall_subs_deduction = 0;

                    string m_overall_subs = "";
                    string m_overall_subs_deduction = "";

                    decimal overall_gross = 0;
                    decimal overall_rtc = 0;
                    decimal overall_tax = 0;
                    decimal overall_philhealth = 0;
                    decimal overall_gsis = 0;
                    decimal overall_pagibig = 0;
                    decimal overall_others = 0;
                    decimal overall_total_deduction = 0;
                    decimal overall_first_half = 0;
                    decimal overall_second_half = 0;

                    SalaryPageTotal pageTotal = new SalaryPageTotal(page_salary, page_salary_deduction, page_subs, page_subs_deduction, page_gross, page_rtc, page_tax, page_philhealth, page_gsis, page_pagibig, page_others, page_total_deduction, page_first_half, page_second_half);
                    SalaryPageTotal overallTotal = new SalaryPageTotal(overall_salary, overall_salary_deduction,overall_subs,overall_subs_deduction, overall_gross, overall_rtc, overall_tax,
                         overall_philhealth, overall_gsis, overall_pagibig, overall_others, overall_total_deduction, overall_first_half, overall_second_half);

                    foreach (var data in list)
                    {
                         if (main.CalculateHeights() >= MAX_ROW_SIZE)
                         {

                              page_count++;
                              MAX_ROW_SIZE = 390;

                              pageTotal = new SalaryPageTotal(page_salary, page_salary_deduction, page_subs, page_subs_deduction, page_gross, page_rtc, page_tax, page_philhealth, page_gsis, page_pagibig, page_others, page_total_deduction, page_first_half, page_second_half);

                              main.AddCell(new PdfPCell(RegularSalaryChargePage(pageTotal))
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
                              page_first_half = 0;
                              page_second_half = 0;

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

                         
                        
                         string id = data.ID;
                         string name = data.Fullname;
                         string position = data.Designation; 
                         
                         page_salary += data.Payslip.FirstHalf.BasicSalary + data.Payslip.FirstHalf.Pera;
                         page_salary_deduction += data.Payslip.FirstHalf.Absences;
                         
                         page_subs += data.Payslip.FirstHalf.Subsistence + data.Payslip.FirstHalf.Longevity + data.Payslip.SecondHalf.HazardPay;
                         page_subs_deduction += data.Payslip.FirstHalf.SubsistenceDeduction + data.Payslip.FirstHalf.LongevityDeduction + data.Payslip.SecondHalf.Mortuary + data.Payslip.SecondHalf.Hwmpc + data.Payslip.SecondHalf.OtherPayable;

                         m_page_subs += data.Payslip.FirstHalf.Subsistence +" "+ data.Payslip.FirstHalf.Longevity +" "+ data.Payslip.SecondHalf.HazardPay;
                         m_page_subs_deduction += data.Payslip.FirstHalf.SubsistenceDeduction +" "+ data.Payslip.FirstHalf.LongevityDeduction +" "+ data.Payslip.SecondHalf.Mortuary +" "+ data.Payslip.SecondHalf.Hwmpc +" "+data.Payslip.SecondHalf.OtherPayable;

                         page_gross += data.Payslip.FirstHalf.BasicSalary + data.Payslip.FirstHalf.Pera - data.Payslip.FirstHalf.Absences + data.Payslip.FirstHalf.GetNetSubsistence() + data.Payslip.FirstHalf.GetNetLongevity() + data.Payslip.SecondHalf.GetNetHazard();
                         page_rtc += data.Payslip.SecondHalf.Rata + data.Payslip.SecondHalf.GetNetCellphone();
                         page_tax += data.Payslip.FirstHalf.Tax;
                         page_philhealth += data.Payslip.FirstHalf.PhilHealth;
                         page_gsis += data.Payslip.FirstHalf.GsisConsoLoan + data.Payslip.FirstHalf.GsisEdu + data.Payslip.FirstHalf.GsisEml + data.Payslip.FirstHalf.GsisHelp + data.Payslip.FirstHalf.GsisPolicyLoan + data.Payslip.FirstHalf.GsisPremium + data.Payslip.FirstHalf.GsisUoli;
                         page_pagibig += data.Payslip.FirstHalf.PagibigLoan + data.Payslip.FirstHalf.PagibigMp2 + data.Payslip.FirstHalf.PagibigPremium;
                         page_others += data.Payslip.FirstHalf.Cfi + data.Payslip.FirstHalf.Simc + data.Payslip.FirstHalf.Rel + data.Payslip.FirstHalf.Disallowance + data.Payslip.FirstHalf.Hwmpc + data.Payslip.FirstHalf.Dbp;
                         page_total_deduction += data.Payslip.FirstHalf.GetTotalDeduction();
                         page_first_half += data.Payslip.FirstHalf.GetTotalPay();
                         page_second_half += data.Payslip.SecondHalf.GetTotalPay();

                         overall_salary += data.Payslip.FirstHalf.BasicSalary + data.Payslip.FirstHalf.Pera;
                         overall_salary_deduction += data.Payslip.FirstHalf.Absences;

                         overall_subs += data.Payslip.FirstHalf.Subsistence + data.Payslip.FirstHalf.Longevity + data.Payslip.SecondHalf.HazardPay;
                         overall_subs_deduction += data.Payslip.FirstHalf.SubsistenceDeduction + data.Payslip.FirstHalf.LongevityDeduction + data.Payslip.SecondHalf.Mortuary + data.Payslip.SecondHalf.Hwmpc + data.Payslip.SecondHalf.OtherPayable;

                         m_overall_subs += data.Payslip.FirstHalf.Subsistence + " " + data.Payslip.FirstHalf.Longevity + " " + data.Payslip.SecondHalf.HazardPay;
                         m_overall_subs_deduction += data.Payslip.FirstHalf.SubsistenceDeduction + " " + data.Payslip.FirstHalf.LongevityDeduction + " " + data.Payslip.SecondHalf.Mortuary + " " + data.Payslip.SecondHalf.Hwmpc + " " + data.Payslip.SecondHalf.OtherPayable;

                         overall_gross += data.Payslip.FirstHalf.BasicSalary + data.Payslip.FirstHalf.Pera - data.Payslip.FirstHalf.Absences + data.Payslip.FirstHalf.GetNetSubsistence() + data.Payslip.FirstHalf.GetNetLongevity() + data.Payslip.SecondHalf.GetNetHazard();
                         overall_rtc += data.Payslip.SecondHalf.Rata + data.Payslip.SecondHalf.GetNetCellphone();
                         overall_tax += data.Payslip.FirstHalf.Tax;
                         overall_philhealth += data.Payslip.FirstHalf.PhilHealth;
                         overall_gsis += data.Payslip.FirstHalf.GsisConsoLoan + data.Payslip.FirstHalf.GsisEdu + data.Payslip.FirstHalf.GsisEml + data.Payslip.FirstHalf.GsisHelp + data.Payslip.FirstHalf.GsisPolicyLoan + data.Payslip.FirstHalf.GsisPremium + data.Payslip.FirstHalf.GsisUoli;
                         overall_pagibig += data.Payslip.FirstHalf.PagibigLoan + data.Payslip.FirstHalf.PagibigMp2 + data.Payslip.FirstHalf.PagibigPremium;
                         overall_others += data.Payslip.FirstHalf.Cfi + data.Payslip.FirstHalf.Simc + data.Payslip.FirstHalf.Rel + data.Payslip.FirstHalf.Disallowance + data.Payslip.FirstHalf.Hwmpc + data.Payslip.FirstHalf.Dbp;
                         overall_total_deduction += data.Payslip.FirstHalf.GetTotalDeduction();
                         overall_first_half += data.Payslip.FirstHalf.GetTotalPay();
                         overall_second_half += data.Payslip.SecondHalf.GetTotalPay();

                         items = RegularSalaryChargeItem(++counter, id, name, position, data.Payslip);

                         main.AddCell(new PdfPCell(items)
                         {
                              Border = 0,
                              HorizontalAlignment = Element.ALIGN_CENTER,
                              VerticalAlignment = Element.ALIGN_CENTER
                         });
                    }

                    pageTotal = new SalaryPageTotal(page_salary, page_salary_deduction, page_subs, page_subs_deduction, page_gross, page_rtc, page_tax, page_philhealth, page_gsis, page_pagibig, page_others, page_total_deduction, page_first_half, page_second_half);
                    overallTotal = new SalaryPageTotal(overall_salary, overall_salary_deduction, overall_subs, overall_subs_deduction, overall_gross, overall_rtc, overall_tax,
                         overall_philhealth, overall_gsis, overall_pagibig, overall_others, overall_total_deduction, overall_first_half, overall_second_half);

                    main.AddCell(new PdfPCell(RegularSalaryChargePage(pageTotal))
                    {
                         Border = 0,
                         HorizontalAlignment = Element.ALIGN_CENTER,
                         VerticalAlignment = Element.ALIGN_CENTER
                    });
                    main.AddCell(new PdfPCell(RegularSalaryChargeOverall(overallTotal))
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
               }
          }
     }
}