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

        public static void RegularPayslipContainer(string id, string fullname, string designation, int month, int year, string physical_file_path)
        {
            Document doc = new Document();
            doc.SetMargins(5f, 5f, 45f, 5f);
            doc.SetPageSize(PageSize.A4);

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(physical_file_path, FileMode.Create));
            doc.Open();
            doc.Add(RegularPayslipPDFView.RegularPayslipTitle("Salary and Other Benefits Slip For The Month Of "+DateUtility.GetMonthName(month,year)+" 1-"+DateUtility.GetMaximumDayOfMonth(month,year)+", "+year));
            doc.Add(RegularPayslipPDFView.RegularPayslipHeader(id, fullname, designation));
            doc.Add(RegularPayslipPDFView.RegularPayslipBody(id, month, year)); //implement query JoPayslipModel
            doc.Close();
        }
        public static PdfPTable RegularPayslipTitle(string title)
        {
            PdfPTable Table = new PdfPTable(1);
            Table.TotalWidth = 400;

            List<string> titles = new List<string>();
            titles.Add("Republic of Philippines");
            titles.Add("DEPARTMENT OF HEALTH");
            titles.Add("Regional Office VII");
            titles.Add("Osmeña Blvd. Cebu City");
            titles.Add("Tel. No.: (032) 418-7125");
            titles.Add(title);

            //Used this type of loop to pinpoint the location of specific item in a list.
            for (int i = 0; i < titles.Count; i++)
            {
                int FontStyle = Font.NORMAL;

                if (i == 1 || i == 5)
                {
                    FontStyle = Font.BOLD;
                }

                Table.AddCell(new PdfPCell(new Phrase(titles[i], new Font(FontFactory.GetFont("HELVETICA", 7, FontStyle, new BaseColor(0, 0, 0)))))
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

        public static PdfPTable RegularPayslipBody(string userid,int month, int year)
        {

            float[] Table_Widths = { 2, 2, 1, 2, 2 };

            PdfPTable Table = new PdfPTable(5);
            Table.TotalWidth = 400;
            Table.SetWidths(Table_Widths);

            RegularPayslipModel PayslipModel = PayrollDatabase.Instance.GetRegularPayslipById(month,year, userid);
            if (PayslipModel != null)
            {
                List<RegularPayslipItem> list = RegularPayslipItem.Seeder(PayslipModel);

                int size = 7;

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
                Table.AddCell(new PdfPCell(new Phrase(PayslipModel.GetNetPayment().ToString("#,##0.00"), new Font(FontFactory.GetFont("HELVETICA", size, Font.BOLD, new BaseColor(255, 255, 255)))))
                {
                    BorderWidth = 1,
                    Padding = 2,
                    BorderColor = new BaseColor(255, 255, 255),
                    BackgroundColor = new BaseColor(0, 0, 0),
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
            }

            return Table;
        }
    }
}