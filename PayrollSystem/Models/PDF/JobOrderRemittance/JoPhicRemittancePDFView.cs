using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using PayrollSystem.Database;
using System.Web.Http.Results;

namespace PayrollSystem.Models
{
    public class JoPhicRemittancePDFView
    {
        public static bool JoPhicRemittanceContainer(string physical_file_path, string job_status, string start_date, string end_date)
        {
            List<PhicRemittanceModel> list = PayrollDatabase.Instance.GeneratePhicRemittance(job_status, start_date, end_date);
            if (list.Count > 0)
            {
                Document doc = new Document();
                doc.SetMargins(5f, 5f, 45f, 5f);
                doc.SetPageSize(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(physical_file_path, FileMode.Create));
                doc.Open();
                doc.Add(JoPhicRemittancePDFView.JoPhicRemittanceTitle());
                doc.Add(JoPhicRemittancePDFView.JoPhicRemittanceHeader(start_date, end_date));
                doc.Add(JoPhicRemittancePDFView.JoPhicRemittanceListHeader());
                doc.Add(JoPhicRemittancePDFView.JoPhicRemittanceBody(job_status, start_date, end_date, list));
                doc.Add(JoPhicRemittancePDFView.JoPhicRemittanceFooter(list));
                doc.Close();
                return true;
            }
            else
                return false;
        }

        public static PdfPTable JoPhicRemittanceTitle()
        {
            PdfPTable Table = new PdfPTable(1);
            Table.TotalWidth = 400;

            List<string> title = new List<string>();
            title.Add("Republic of Philippines");
            title.Add("Department of Health");
            title.Add("Regional Office VIICENTRAL VISAYAS CENTER for HEALTH DEVELOPMENT");
            title.Add("Osmeña Boulevard, Sambag II, Cebu City, 6000 Philippines");
            title.Add("Regional Director’s Office Tel. No. (032) 253-6355 Fax No. (032) 254-0109");
            title.Add("Official Website http://www.ro7.doh.gov.ph email Address: dohro7@gmail.com");

            //Used this type of loop to pinpoint the location of specific item in a list.
            for (int i = 0; i < title.Count; i++)
            {
                int FontStyle = Font.NORMAL;
                int Padding = 0;
                if (i == 2)
                {
                    FontStyle = Font.BOLD;
                    Padding = 2;
                }
                if( i == 5)
                {
                    Padding = 10;
                }

                Table.AddCell(new PdfPCell(new Phrase(title[i], new Font(FontFactory.GetFont("HELVETICA", 8, FontStyle, new BaseColor(0, 0, 0)))))
                {
                    PaddingBottom = Padding,
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
            }
            return Table;
        }

        public static PdfPTable JoPhicRemittanceHeader(string start_date, string end_date)
        {
            PdfPTable Table = new PdfPTable(1);
            Table.TotalWidth = 400;

            int FontStyle = Font.NORMAL;
            string startMonth = DateTime.Parse(start_date).ToString("MMMM");
            string endMonth = DateTime.Parse(end_date).ToString("MMMM");
            string startYear = start_date.Split('/')[2];
            string endYear = end_date.Split('/')[2];

            string title = startMonth + " " + startYear + " - " + endMonth + " " + endYear + " REMITTANCE";

            Table.AddCell(new PdfPCell(new Phrase(title.ToUpper(), new Font(FontFactory.GetFont("HELVETICA", 8, FontStyle, new BaseColor(0, 0, 0)))))
            {
                PaddingTop = 10,
                PaddingBottom = 10,
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            return Table;
        }

        public static PdfPTable JoPhicRemittanceListHeader()
        {
            float[] headers = { 3, 12, 15, 18, 15, 13, 7 };
            PdfPTable Table = new PdfPTable(7);
            Table.SetWidths(headers);
            Table.TotalWidth = 400;

            Table.AddCell(new PdfPCell(new Phrase("NO", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("PHIC NO", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("LAST NAME", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("FIRST NAME", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("MIDDLE NAME", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("AMOUNT", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });


            return Table;
        }

        public static PdfPTable JoPhicRemittanceBody(string job_status, string start_date, string end_date, List<PhicRemittanceModel> list)
        {
            float[] headers = { 3, 12, 15, 18, 15, 13, 7 };
            PdfPTable Table = new PdfPTable(7);
            Table.SetWidths(headers);
            Table.TotalWidth = 400;

            var newList = list.GroupBy(x => x.UserId);
            if(list != null)
            {
                int ctr = 0;
                string prevLname = "";
                bool same = false;
                foreach(var item in newList)
                {
                    ctr++;
                    if (item.First().LastName == prevLname)
                        same = true;
                    else
                        same = false;
                    prevLname = item.First().LastName;
                    Table.AddCell(new PdfPCell(new Phrase(ctr+"", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
                    {
                        Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_CENTER
                    });
                    Table.AddCell(new PdfPCell(new Phrase(item.First().PhicNo, new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
                    {
                        Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_CENTER
                    });
                    Table.AddCell(new PdfPCell(new Phrase(item.First().LastName, new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(same? 255 : 0, 0, 0)))))
                    {
                        Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_CENTER
                    });
                    Table.AddCell(new PdfPCell(new Phrase(item.First().FirstName, new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
                    {
                        Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_CENTER
                    });
                    Table.AddCell(new PdfPCell(new Phrase(item.First().MiddleName, new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
                    {
                        Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_CENTER
                    });
                    Table.AddCell(new PdfPCell(new Phrase(item.Sum(x => x.Ammount).ToString("0.00"), new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
                    {
                        Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_CENTER
                    });
                    Table.AddCell(new PdfPCell(new Phrase(TakeType(item.First().Type), new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
                    {
                        Border = PdfPCell.RIGHT_BORDER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_CENTER
                    });
                }
                Table.AddCell(new PdfPCell(new Phrase( "", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });

                Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
                Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
                Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
                Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });


                Table.AddCell(new PdfPCell(new Phrase(list.Sum(x=>x.Ammount).ToString("0.00"), new Font(FontFactory.GetFont("HELVETICA", 8, Font.BOLD, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
                Table.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
                {
                    Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER | PdfPCell.LEFT_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });

            }
            return Table;
        }

        public static PdfPTable JoPhicRemittanceFooter(List<PhicRemittanceModel> list)
        {
            float[] headers = { 3, 12, 15, 18, 15, 13, 7 };
            PdfPTable Table = new PdfPTable(7);
            Table.SetWidths(headers);
            Table.TotalWidth = 400;
            var newList = list.GroupBy(x => x.UserId);

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

            //cashcard
            var ccTotal = list.Where(x => x.Type == "CASH_CARD").Sum(x => x.Ammount);
            var hfepTotal = list.Where(x => x.Type == "HFEP").Sum(x => x.Ammount);
            var atmTotal = list.Where(x => x.Type == "ATM").Sum(x => x.Ammount);

            #region CASH CARD
            Table.AddCell(new PdfPCell(new Phrase(".", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = 0,
                Colspan = 5,
                Rowspan = 6,
            });

            Table.AddCell(new PdfPCell(new Phrase(ccTotal.ToString("0.00") , new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase("with cashcard", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            #endregion
            #region ATM
            Table.AddCell(new PdfPCell(new Phrase(atmTotal.ToString("0.00"), new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase("with atm", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            #endregion
            #region HFEP

            Table.AddCell(new PdfPCell(new Phrase(hfepTotal.ToString("0.00"), new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Rowspan = 2,
                Border = PdfPCell.RIGHT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase("HFEP", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Rowspan = 2,
                Border = PdfPCell.RIGHT_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            #endregion
            //TOTAL

            Table.AddCell(new PdfPCell(new Phrase(list.Sum(x=>x.Ammount).ToString("0.00"), new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.TOP_BORDER + PdfPCell.BOTTOM_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });

            Table.AddCell(new PdfPCell(new Phrase( "", new Font(FontFactory.GetFont("HELVETICA", 8, Font.NORMAL, new BaseColor(0, 0, 0)))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER,
            });

            return Table;
        }

        public static string TakeType(string type)
        {
            switch(type)
            {
                case "CASH_CARD":
                    {
                        return "CC";
                    }
                default:
                    {
                        return "";
                    }
            }
        }
    }
}