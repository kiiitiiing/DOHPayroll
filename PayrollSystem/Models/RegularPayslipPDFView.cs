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
    public class RegularPayslipPDFView
    {
        public static PdfPTable RegularPayslipTitle(string month, string year)
        {
            PdfPTable Table = new PdfPTable(1);
            Table.TotalWidth = 400;

            List<String> title = new List<String>();
            title.Add("Republic of Philippines");
            title.Add("DEPARTMENT OF HEALTH");
            title.Add("Regional Office VII");
            title.Add("Osmeña Blvd. Cebu City");
            title.Add("Tel. No.: (032) 418-7125");
            title.Add("Salary and Other Benefits Slip For The Month Of January 2019");

            //Used this type of loop to pinpoint the location of specific item in a list.
            for (int i = 0; i < title.Count; i++)
            {
                int FontStyle = Font.NORMAL;

                if (i == 1 || i == 5)
                {
                    FontStyle = Font.BOLD;
                }

                Table.AddCell(new PdfPCell(new Phrase(title[i], new Font(FontFactory.GetFont("HELVETICA", 7, FontStyle, new BaseColor(0, 0, 0)))))
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
        public static PdfPTable RegularPayslipHeader(string ID, string Name, string Designation)
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

        public static PdfPTable GeneratePaySlipBody()
        {

            float[] Table_Widths = { 2, 2, 1, 2, 2 };

            PdfPTable Table = new PdfPTable(5);
            Table.TotalWidth = 400;
            Table.SetWidths(Table_Widths);

            List<RegularPayslipItem> list = RegularPayslipItem.Seeder();

            int size = 7;
            //HEADER


            //LOANS AND SALARY 
            foreach (RegularPayslipItem item in list)
            {

                Table.AddCell(new PdfPCell(new Phrase(item.LeftDescription, new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, item.LeftBaseColor))))
                {
                    BorderWidth = 1,
                    Padding = 2,
                    BorderColor = new BaseColor(255, 255, 255),
                    BackgroundColor = item.LeftBackground,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
                Table.AddCell(new PdfPCell(new Phrase(item.LeftValue, new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, item.LeftBaseColor))))
                {
                    BorderWidth = 1,
                    Padding = 2,
                    BorderColor = new BaseColor(255, 255, 255),
                    BackgroundColor = item.LeftBackground,
                    HorizontalAlignment = item.LeftPosition,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
                Table.AddCell(new PdfPCell(new Phrase("MID BLANK", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
                {

                    BorderWidth = 1,
                    Padding = 2,
                    BorderColor = new BaseColor(255, 255, 255),
                    BackgroundColor = new BaseColor(255, 255, 255),
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
                });

                if (item.RightDescription.Equals("PBB (Performance Based Bonus)") || item.RightDescription.Equals("CELLPHONE COMMUNICATION") || item.RightDescription.Equals("UNIFORM AND CLOTHING ALL"))
                {
                    size = 6;
                }

                Table.AddCell(new PdfPCell(new Phrase(item.RightDescription, new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, item.RightBaseColor))))
                {

                    BorderWidth = 1,
                    Padding = 2,
                    BorderColor = new BaseColor(255, 255, 255),
                    BackgroundColor = item.RightBackground,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
                });

                size = 7;

                Table.AddCell(new PdfPCell(new Phrase(item.RightValue, new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, item.RightBaseColor))))
                {

                    BorderWidth = 1,
                    Padding = 2,
                    BorderColor = new BaseColor(255, 255, 255),
                    BackgroundColor = item.RightBackground,
                    HorizontalAlignment = item.RightPosition,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
            }

            Table.AddCell(new PdfPCell(new Phrase("BLANK", new Font(FontFactory.GetFont("HELVETICA", size, Font.NORMAL, new BaseColor(255, 255, 255)))))
            {
                BorderWidth = 1,
                Colspan = 5,
                Padding = 2,
                BorderColor = new BaseColor(255, 255, 255),
                BackgroundColor = new BaseColor(255, 255, 255),
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_CENTER
            });


            Table.AddCell(new PdfPCell(new Phrase("NET PAYMENT", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BorderWidth = 1,
                Colspan = 4,
                Padding = 2,
                BorderColor = new BaseColor(255, 255, 255),
                BackgroundColor = new BaseColor(0, 0, 0),
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("1,200.00", new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
            {
                BorderWidth = 1,
                Padding = 2,
                BorderColor = new BaseColor(255, 255, 255),
                BackgroundColor = new BaseColor(0, 0, 0),
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_CENTER
            });


            return Table;
        }
    }
}