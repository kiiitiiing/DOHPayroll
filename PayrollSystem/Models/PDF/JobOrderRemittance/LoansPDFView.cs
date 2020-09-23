using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Ajax.Utilities;
using PayrollSystem.Database;
using PayrollSystem.Models;

namespace PayrollSystem.Models.PDF.JobOrderRemittance
{
    public class LoansPDFView
    {
        public static bool LoansContainer(string physical_file_path, string job_status, string start_date, string end_date)
        {
            List<LoansModel> list = new List<LoansModel>(); //PayrollDatabase.Instance.GeneratePagIbigRemittance(job_status, start_date, end_date);
            if (list.Count > 0)
            {
                Document doc = new Document();
                doc.SetMargins(5f, 5f, 5f, 5f);
                doc.SetPageSize(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(physical_file_path, FileMode.Create));
                doc.Open();
                doc.Add(LoansPDFView.LoansTitle());
                doc.Add(LoansPDFView.LoansHeader());
                doc.Add(LoansPDFView.LoansBody(list));
                doc.Close();
                return true;
            }
            else
                return false;
        }

        public static PdfPTable LoansTitle()
        {
            float[] headers = { 12, 30 };
            PdfPTable Table = new PdfPTable(2);
            Table.SetWidths(headers);
            Table.TotalWidth = 400;

            //1
            Table.AddCell(new PdfPCell(new Phrase("EmployerID", new Font(FontFactory.GetFont("Times New Roman", 12, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase("2088-3884-0005", new Font(FontFactory.GetFont("Arial", 12, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            //2
            Table.AddCell(new PdfPCell(new Phrase("EmployerName", new Font(FontFactory.GetFont("Times New Roman", 12, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase("JOB ORDERS - DEPARTMENT OF HEALTH R7", new Font(FontFactory.GetFont("Arial", 12, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            //3
            Table.AddCell(new PdfPCell(new Phrase("Address", new Font(FontFactory.GetFont("Times New Roman", 12, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase("OSMENA BLVD.,CEBU CITY", new Font(FontFactory.GetFont("Arial", 12, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            return Table;
        }

        public static PdfPTable LoansHeader()
        {
            float[] headers = { 12, 6, 15, 14, 7, 14, 6, 8, 6 };
            PdfPTable Table = new PdfPTable(9);
            Table.SetWidths(headers);
            Table.TotalWidth = 400;

            Table.AddCell(new PdfPCell(new Phrase("pag-IBIG ID", new Font(FontFactory.GetFont("HELVETICA", 10, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("APPLICATION NO/AGREEMENT NO", new Font(FontFactory.GetFont("HELVETICA", 10, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("LASTNAME", new Font(FontFactory.GetFont("HELVETICA", 10, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("FIRST NAME", new Font(FontFactory.GetFont("HELVETICA", 10, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("NAME EXTENSION (JR, II)", new Font(FontFactory.GetFont("HELVETICA", 10, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("MIDDLE NAME", new Font(FontFactory.GetFont("HELVETICA", 10, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("LOAN TYPE (MPL, CAL)", new Font(FontFactory.GetFont("HELVETICA", 10, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("AMOUNT", new Font(FontFactory.GetFont("HELVETICA", 10, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("REMARKS", new Font(FontFactory.GetFont("HELVETICA", 10, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            return Table;
        }

        public static PdfPTable LoansBody(List<LoansModel> list)
        {
            float[] headers = { 12, 6, 15, 14, 7, 14, 6, 8, 6 };
            PdfPTable Table = new PdfPTable(9);
            Table.SetWidths(headers);
            Table.TotalWidth = 400;

            var newList = list.GroupBy(x => x.UserId);

            foreach (var item in newList)
            {
                Table.AddCell(new PdfPCell(new Phrase(item.First().PagIbigId, new Font(FontFactory.GetFont("Arial", 12, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
                Table.AddCell(new PdfPCell(new Phrase(item.First().ApplicationNo, new Font(FontFactory.GetFont("Arial", 12, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
                Table.AddCell(new PdfPCell(new Phrase(item.First().LastName, new Font(FontFactory.GetFont("Arial", 12, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
                Table.AddCell(new PdfPCell(new Phrase(item.First().FirstName, new Font(FontFactory.GetFont("Arial", 12, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
                Table.AddCell(new PdfPCell(new Phrase(item.First().NameExtension, new Font(FontFactory.GetFont("Arial", 12, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
                Table.AddCell(new PdfPCell(new Phrase(item.First().MiddleName, new Font(FontFactory.GetFont("Arial", 12, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
                Table.AddCell(new PdfPCell(new Phrase(item.First().LoanType, new Font(FontFactory.GetFont("Arial", 12, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });

                Table.AddCell(new PdfPCell(new Phrase(item.Sum(x => x.Amount).ToString("0.00"), new Font(FontFactory.GetFont("Arial", 12, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });

                Table.AddCell(new PdfPCell(new Phrase(item.First().Remarks, new Font(FontFactory.GetFont("Arial", 12, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
            }

            Table.AddCell(new PdfPCell(new Phrase("TOTAL STL AMORTIZATION", new Font(FontFactory.GetFont("Times New Roman", 10, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 6,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase(list.Sum(x => x.Amount).ToString("0.00"), new Font(FontFactory.GetFont("Arial", 14, Font.BOLDITALIC, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("Arial", 14, Font.BOLDITALIC, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER
            });
            return Table;
        }

        public static PdfPTable LoansFooter()
        {
            float[] headers = { 3, 12, 15, 18, 15, 13, 7 };
            PdfPTable Table = new PdfPTable(7);
            Table.SetWidths(headers);
            Table.TotalWidth = 400;

            Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                PaddingTop = 10,
                PaddingBottom = 15,
                Border = 0,
                Rowspan = 3,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase("Prepared:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                PaddingTop = 10,
                PaddingBottom = 15,
                Border = 0,
                Colspan = 3,
                Rowspan = 3,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase("Noted:", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                PaddingTop = 10,
                PaddingBottom = 15,
                Border = PdfPCell.RIGHT_BORDER,
                Colspan = 2,
                Rowspan = 3,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                PaddingTop = 10,
                PaddingBottom = 15,
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.LEFT_BORDER,
                Rowspan = 3,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            //next line

            Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                PaddingBottom = 10,
                Border = 0,
                Rowspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase("Heidi C. Borbon", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 3,
                Rowspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_BOTTOM
            });

            Table.AddCell(new PdfPCell(new Phrase("Theresa Q. Tragico", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER,
                Colspan = 2,
                Rowspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_BOTTOM
            });

            Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                PaddingBottom = 10,
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.LEFT_BORDER,
                Rowspan = 4,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            //positions

            Table.AddCell(new PdfPCell(new Phrase("Administrative Assistant I", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                PaddingBottom = 10,
                Border = 0,
                Colspan = 3,
                Rowspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_TOP
            });

            Table.AddCell(new PdfPCell(new Phrase("Administrative Officer V", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                PaddingBottom = 10,
                Border = PdfPCell.RIGHT_BORDER,
                Colspan = 2,
                Rowspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_TOP
            });

            return Table;
        }
    }
}