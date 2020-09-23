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
    public class JoPagIbigRemittancePDFView
    {
        public static bool JoPagIbigRemittanceContainer(string physical_file_path, string job_status, string start_date, string end_date)
        {
            List<PagibigRemittanceModel> list = PayrollDatabase.Instance.GeneratePagIbigRemittance(job_status, start_date, end_date);
            if (list.Count > 0)
            {
                Document doc = new Document();
                doc.SetMargins(5f, 5f, 5f, 5f);
                doc.SetPageSize(PageSize.A4.Rotate());
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(physical_file_path, FileMode.Create));
                doc.Open();
                doc.Add(JoPagIbigRemittancePDFView.JoPagIbigRemittanceTitle());
                doc.Add(JoPagIbigRemittancePDFView.JoPagIbigRemittanceHeader());
                doc.Add(JoPagIbigRemittancePDFView.JoPagIbigRemittanceBody(list));
                doc.Close();
                return true;
            }
            else
                return false;
        }

        public static PdfPTable JoPagIbigRemittanceTitle()
        {
            float[] headers = { 12,30,30 };
            PdfPTable Table = new PdfPTable(3);
            Table.SetWidths(headers);
            Table.WidthPercentage = 100;
            Table.TotalWidth = 400;

            //1
            Table.AddCell(new PdfPCell(new Phrase("EmployerID", new Font(FontFactory.GetFont("Times New Roman", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase("2088-3884-0005", new Font(FontFactory.GetFont("Arial", 11, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("HQP-PFF-053", new Font(FontFactory.GetFont("Times New Roman", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            //2
            Table.AddCell(new PdfPCell(new Phrase("EmployerName", new Font(FontFactory.GetFont("Times New Roman", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase("JOB ORDERS - DEPARTMENT OF HEALTH R7", new Font(FontFactory.GetFont("Arial", 11, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            //3
            Table.AddCell(new PdfPCell(new Phrase("Address", new Font(FontFactory.GetFont("Times New Roman", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase("OSMENA BLVD.,CEBU CITY", new Font(FontFactory.GetFont("Arial", 11, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("Times New Roman", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 3,
                PaddingBottom = 10,
            });
            return Table;
        }


        public static PdfPTable JoPagIbigRemittanceHeader()
        {
            float[] headers = { 12, 6, 11, 15, 14, 7, 14, 7, 9, 6, 7 };
            PdfPTable Table = new PdfPTable(11);
            Table.SetWidths(headers);
            Table.WidthPercentage = 100;
            Table.TotalWidth = 400;

            Table.AddCell(new PdfPCell(new Phrase("pag-IBIG ID", new Font(FontFactory.GetFont("HELVETICA", 11, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("ACCOUNT NO", new Font(FontFactory.GetFont("HELVETICA", 11, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("MEMBERSHIP PROGRAM", new Font(FontFactory.GetFont("HELVETICA", 11, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("LASTNAME", new Font(FontFactory.GetFont("HELVETICA", 11, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("FIRST NAME", new Font(FontFactory.GetFont("HELVETICA", 11, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("NAME EXTENSION (JR, II)", new Font(FontFactory.GetFont("HELVETICA", 11, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("MIDDLE NAME", new Font(FontFactory.GetFont("HELVETICA", 11, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("PERCOV (YYYYMM)", new Font(FontFactory.GetFont("HELVETICA", 11, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("EE SHARE", new Font(FontFactory.GetFont("HELVETICA", 11, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("ER SHARE", new Font(FontFactory.GetFont("HELVETICA", 11, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("REMARKS", new Font(FontFactory.GetFont("HELVETICA", 11, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            return Table;
        }

        public static PdfPTable JoPagIbigRemittanceBody(List<PagibigRemittanceModel> list)
        {
            float[] headers = { 12, 6, 11, 15, 14, 7, 14, 7, 9, 6, 7 };
            PdfPTable Table = new PdfPTable(11);
            Table.SetWidths(headers);
            Table.WidthPercentage = 100;
            Table.TotalWidth = 400;

            var newList = list.GroupBy(x => x.UserId);

            foreach(var item in newList)
            {
                Table.AddCell(new PdfPCell(new Phrase(item.First().PagIbigNo, new Font(FontFactory.GetFont("HELVETICA", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
                Table.AddCell(new PdfPCell(new Phrase(item.First().AccountNo, new Font(FontFactory.GetFont("HELVETICA", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
                Table.AddCell(new PdfPCell(new Phrase(item.First().Membership, new Font(FontFactory.GetFont("HELVETICA", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
                Table.AddCell(new PdfPCell(new Phrase(item.First().LastName, new Font(FontFactory.GetFont("HELVETICA", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
                Table.AddCell(new PdfPCell(new Phrase(item.First().FirstName, new Font(FontFactory.GetFont("HELVETICA", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
                Table.AddCell(new PdfPCell(new Phrase(item.First().NameExtension, new Font(FontFactory.GetFont("HELVETICA", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
                Table.AddCell(new PdfPCell(new Phrase(item.First().MiddleName, new Font(FontFactory.GetFont("HELVETICA", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
                Table.AddCell(new PdfPCell(new Phrase(item.First().PerCov, new Font(FontFactory.GetFont("HELVETICA", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });

                Table.AddCell(new PdfPCell(new Phrase(item.Sum(x=>x.EEShare).ToString("0.00"), new Font(FontFactory.GetFont("HELVETICA", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });

                Table.AddCell(new PdfPCell(new Phrase(item.Sum(x=>x.ERShare).ToString("0.00"), new Font(FontFactory.GetFont("HELVETICA", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });

                Table.AddCell(new PdfPCell(new Phrase(item.First().Remarks, new Font(FontFactory.GetFont("HELVETICA", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
            }

            Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER
            });
            Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER
            });
            Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER
            });
            Table.AddCell(new PdfPCell(new Phrase("TOTAL", new Font(FontFactory.GetFont("HELVETICA", 11, Font.BOLDITALIC, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER
            });
            Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER
            });
            Table.AddCell(new PdfPCell(new Phrase("TOTAL", new Font(FontFactory.GetFont("HELVETICA", 11, Font.BOLDITALIC, new BaseColor(255, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER
            });
            Table.AddCell(new PdfPCell(new Phrase(list.Sum(x=>x.EEShare).ToString("0.00"), new Font(FontFactory.GetFont("HELVETICA", 11, Font.BOLDITALIC, new BaseColor(255, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER
            });
            Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", 11, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER
            });
            return Table;
        }
    }

}