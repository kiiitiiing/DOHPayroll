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
    public class JoPayslipPDFView
    {

        public static void JoPayslipContainer(string physical_file_path, string title, string id, string fullname, string designation, string date_from, string date_to)
        {
            Document doc = new Document();
            doc.SetMargins(5f, 5f, 45f, 5f);
            doc.SetPageSize(PageSize.A4);

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(physical_file_path, FileMode.Create));
            doc.Open();
            doc.Add(JoPayslipPDFView.JoPayslipTitle(title));
            doc.Add(JoPayslipPDFView.JoPayslipHeader(id, fullname, designation));
            doc.Add(JoPayslipPDFView.JoPaySlipBody(id, date_from, date_to)); //implement query JoPayslipModel
            doc.Add(JoPayslipPDFView.JoPayslipFooter());
            doc.Close();
        }

        public static PdfPTable JoPayslipTitle(string date_range)
        {
            PdfPTable Table = new PdfPTable(1);
            Table.TotalWidth = 400;

            List<string> title = new List<string>();
            title.Add("Republic of Philippines");
            title.Add("DEPARTMENT OF HEALTH");
            title.Add("Regional Office VII");
            title.Add("Osmeña Blvd. Cebu City");
            title.Add("Tel. No.: (032) 418-7125");
            title.Add("Job Order Payslip for "+ date_range);

            //Used this type of loop to pinpoint the location of specific item in a list.
            for (int i = 0; i < title.Count; i++)
            {
                int FontStyle = Font.NORMAL;
                int Padding = 0;
                if (i == 5)
                {
                    FontStyle = Font.BOLD;
                    Padding = 20;
                }
                if (i == 1)
                {
                    FontStyle = Font.BOLD;
                }

                Table.AddCell(new PdfPCell(new Phrase(title[i], new Font(FontFactory.GetFont("HELVETICA", 8, FontStyle, new BaseColor(0, 0, 0)))))
                {
                    PaddingBottom = Padding,
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
            }
            return Table;
        }

        public static PdfPTable JoPayslipHeader(string ID, string Name, string Designation)
        {
            float[] Table_Widths = { 1, 2 };

            PdfPTable Table = new PdfPTable(2);
            Table.TotalWidth = 400;
            Table.SetWidths(Table_Widths);

            Table.AddCell(new PdfPCell(new Phrase("ID No.", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BorderWidth = 1,
                Padding = 2,
                BorderColor = new BaseColor(255, 255, 255),
                BackgroundColor = new BaseColor(0, 0, 0),
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase(ID, new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BorderWidth = 1,
                Padding = 2,
                BorderColor = new BaseColor(255, 255, 255),
                BackgroundColor = new BaseColor(255, 255, 255),
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase("Employee name", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BorderWidth = 1,
                Padding = 2,
                BorderColor = new BaseColor(255, 255, 255),
                BackgroundColor = new BaseColor(0, 0, 0),
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase(Name, new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BorderWidth = 1,
                Padding = 2,
                BorderColor = new BaseColor(255, 255, 255),
                BackgroundColor = new BaseColor(255, 255, 255),
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase("Designation", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BorderWidth = 1,
                Padding = 2,
                BorderColor = new BaseColor(255, 255, 255),
                BackgroundColor = new BaseColor(0, 0, 0),
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase(Designation, new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                BorderWidth = 1,
                Padding = 2,
                BorderColor = new BaseColor(255, 255, 255),
                BackgroundColor = new BaseColor(255, 255, 255),
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase("Designation", new Font(FontFactory.GetFont("HELVETICA", 7, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BorderWidth = 1,
                Padding = 2,
                Colspan = 2,
                BorderColor = new BaseColor(255, 255, 255),
                BackgroundColor = new BaseColor(255, 255, 255),
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            return Table;
        }

        public static PdfPTable JoPaySlipBody(string id,string date_from, string date_to)
        {

            PdfPTable Table = new PdfPTable(2);
            Table.TotalWidth = 400;

            JoPayslipModel data =  PayrollDatabase.Instance.GetPayslipByEmployeeID(id, date_from,date_to); // implement query payroll by userid

            if (data != null)
            {
                List<JoPayslipItem> list = JoPayslipItem.Seeder(data);

                int size = 8;

                foreach (JoPayslipItem item in list)
                {

                    Table.AddCell(new PdfPCell(new Phrase(item.LeftDescription, new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, item.LeftBaseColor))))
                    {

                        Border = 0,
                        Padding = 2,
                        BackgroundColor = item.LeftBackground,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        VerticalAlignment = Element.ALIGN_CENTER
                    });
                    Table.AddCell(new PdfPCell(new Phrase(item.LeftValue, new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, item.LeftBaseColor))))
                    {
                        Border = 0,
                        Padding = 2,
                        BackgroundColor = item.LeftBackground,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        VerticalAlignment = Element.ALIGN_CENTER
                    });
                }
            }
          

            return Table;
        }

        public static PdfPTable JoPayslipFooter()
        {
            float[] width = { 1, 3 };
            PdfPTable Table = new PdfPTable(2);
            Table.TotalWidth = 400;
            Table.SetWidths(width);

            List<String> title = new List<String>();
            title.Add("Certified true and correct:");
            title.Add("_________________________");
            title.Add("Theresa Q. Tragico");
            title.Add("Administrative Office V");
            title.Add("Human Resource Management");

            
            //Used this type of loop to pinpoint the location of specific item in a list.
            for (int i = 0; i < title.Count; i++)
            {
                int padding = 0;

                if (i == 0)
                {
                    padding = 20;
                }
                if (i == 1)
                {
                    padding = 10;
                }

                Table.AddCell(new PdfPCell(new Phrase(title[i], new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
                {
                    PaddingTop= padding,
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });

                Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(255, 255, 255)))))
                {
                    PaddingTop = padding,
                    Border = 0,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
            }
        
            return Table;
        }
    }
}