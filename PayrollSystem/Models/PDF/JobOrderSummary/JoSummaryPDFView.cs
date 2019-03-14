using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using PayrollSystem.Database;

namespace PayrollSystem.Models
{
    public class JoSummaryPDFView
    {

        private static PdfPTable addFooter(PdfPTable tableLayout, String preparedBy)
        {
            tableLayout.HeaderRows = 0;
            //FOOTER LETTER A
            tableLayout.AddCell(new PdfPCell(new Phrase("A", new Font(FontFactory.GetFont("Times New Roman", 7, Font.BOLD))))
            {
                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("CERTIFIED", new Font(FontFactory.GetFont("Times New Roman", 7, Font.BOLD))))
            {
                Border = 0,
                Padding = 3,
                VerticalAlignment = Element.ALIGN_LEFT,
                HorizontalAlignment = Element.ALIGN_LEFT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Services duly rendered as stated", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL))))
            {

                Border = 0,
                Colspan = 8,
                VerticalAlignment = Element.ALIGN_LEFT,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 3
            });

            //FOOTER LETTER C
            tableLayout.AddCell(new PdfPCell(new Phrase("C", new Font(FontFactory.GetFont("Times New Roman", 7, Font.BOLD))))
            {

                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 3
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("APPROVED FOR PAYMENT:", new Font(FontFactory.GetFont("Times New Roman", 7, Font.BOLD))))
            {

                Border = 0,
                Colspan = 3,
                VerticalAlignment = Element.ALIGN_LEFT,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 3
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("____________________________________________", new Font(FontFactory.GetFont("Times New Roman", 6, Font.NORMAL))))
            {
                Border = 0,
                Colspan = 6,
                VerticalAlignment = Element.ALIGN_LEFT,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 3
            });

            //FOOTER LETTER A - DIVISION
            tableLayout.AddCell(new PdfPCell(new Phrase("d", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("THERESA Q. TRAGICO", new Font(FontFactory.GetFont("Times New Roman", 7, Font.BOLD))))
            {
                PaddingTop = 20,
                Colspan = 5,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("__________________", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {
                PaddingTop = 20,
                Colspan = 4,
                Border = PdfPCell.RIGHT_BORDER,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });


            //FOOTER LETTER C - DIVISION
            tableLayout.AddCell(new PdfPCell(new Phrase("d", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("SOPHIA M. MANCAO,MD,DPSP", new Font(FontFactory.GetFont("Times New Roman", 7, Font.BOLD))))
            {
                PaddingTop = 20,
                Colspan = 5,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("__________________", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {
                PaddingTop = 20,
                Colspan = 4,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });

            //FOOTER LETTER A - DIVISION TITLE

            tableLayout.AddCell(new PdfPCell(new Phrase("d", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Administrative Officer V", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL))))
            {
                Colspan = 5,
                Border = PdfPCell.TOP_BORDER,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Date", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL))))
            {
                Colspan = 4,
                Border = PdfPCell.RIGHT_BORDER,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });

            tableLayout.AddCell(new PdfPCell(new Phrase("d", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("OIC - Asst. Director", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL))))
            {
                Colspan = 5,
                Border = PdfPCell.TOP_BORDER,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Date", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL))))
            {
                Colspan = 4,
                Border = PdfPCell.RIGHT_BORDER,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });


            tableLayout.AddCell(new PdfPCell(new Phrase("d", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Signature over Printed Name of Authorized", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL))))
            {
                PaddingLeft = 20,
                Colspan = 5,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Date", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL, BaseColor.WHITE))))
            {
                Colspan = 4,
                Border = PdfPCell.RIGHT_BORDER,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });


            //FOOTER LETTER C - DIVISION TITLE
            tableLayout.AddCell(new PdfPCell(new Phrase("d", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("(Signature over Printed Name)", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL))))
            {
                PaddingLeft = 20,
                Colspan = 5,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Date", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL, BaseColor.WHITE))))
            {
                Colspan = 4,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });

            tableLayout.AddCell(new PdfPCell(new Phrase("d", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Official", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL))))
            {
                Colspan = 5,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Date", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Colspan = 4,
                Border = PdfPCell.RIGHT_BORDER,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });


            tableLayout.AddCell(new PdfPCell(new Phrase("d", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            //FOOTER LETTER C - DIVISION TITLE
            tableLayout.AddCell(new PdfPCell(new Phrase("Head of Agency/Authorized", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL))))
            {
                Colspan = 5,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Date", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Colspan = 4,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });

            tableLayout.AddCell(new PdfPCell(new Phrase("d", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            //FOOTER LETTER A - DIVISION TITLE
            tableLayout.AddCell(new PdfPCell(new Phrase("Official", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Colspan = 5,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Date", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Colspan = 4,
                Border = PdfPCell.RIGHT_BORDER,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });

            tableLayout.AddCell(new PdfPCell(new Phrase("d", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            //FOOTER LETTER C - DIVISION TITLE
            tableLayout.AddCell(new PdfPCell(new Phrase("Representative", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL))))
            {
                Colspan = 5,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Date", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Colspan = 4,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });


            ////////////////////

            tableLayout.AddCell(new PdfPCell(new Phrase("B", new Font(FontFactory.GetFont("Times New Roman", 7, Font.BOLD))))
            {

                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("CERTIFIED", new Font(FontFactory.GetFont("Times New Roman", 7, Font.BOLD))))
            {
                Padding = 3,
                Border = PdfPCell.TOP_BORDER,
                VerticalAlignment = Element.ALIGN_LEFT,
                HorizontalAlignment = Element.ALIGN_LEFT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Supporting documents complete and proper; and cash available in the amount of P_________________", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL))))
            {

                Border = PdfPCell.TOP_BORDER,
                Colspan = 8,
                VerticalAlignment = Element.ALIGN_LEFT,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 3
            });

            //FOOTER LETTER C
            tableLayout.AddCell(new PdfPCell(new Phrase("D", new Font(FontFactory.GetFont("Times New Roman", 7, Font.BOLD))))
            {

                Border = PdfPCell.RIGHT_BORDER | PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("CERTIFIED", new Font(FontFactory.GetFont("Times New Roman", 7, Font.BOLD))))
            {

                Border = PdfPCell.TOP_BORDER,
                VerticalAlignment = Element.ALIGN_LEFT,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 3
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Each employee whose name appears on the payroll has been paid the amount as indicated opposite his/her name", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL))))
            {
                Border = PdfPCell.TOP_BORDER,
                Colspan = 8,
                VerticalAlignment = Element.ALIGN_LEFT,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 3
            });

            //FOOTER LETTER A - DIVISION
            tableLayout.AddCell(new PdfPCell(new Phrase("d", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("ANGIELINE T. ADLAON,CPA,MBA", new Font(FontFactory.GetFont("Times New Roman", 7, Font.BOLD))))
            {
                PaddingTop = 20,
                Colspan = 5,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("__________________", new Font(FontFactory.GetFont("Times New Roman", 7, Font.BOLD))))
            {
                PaddingTop = 20,
                Colspan = 4,
                Border = PdfPCell.RIGHT_BORDER,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });


            //FOOTER LETTER C - DIVISION
            tableLayout.AddCell(new PdfPCell(new Phrase("d", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("JOSEPHINE D. VERGARA", new Font(FontFactory.GetFont("Times New Roman", 7, Font.BOLD))))
            {
                PaddingTop = 20,
                Colspan = 5,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("__________________", new Font(FontFactory.GetFont("Times New Roman", 7, Font.BOLD))))
            {
                PaddingTop = 20,
                Colspan = 4,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });

            //FOOTER LETTER A - DIVISION TITLE
            tableLayout.AddCell(new PdfPCell(new Phrase("d", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Accountant III", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL))))
            {
                Colspan = 5,
                Border = PdfPCell.TOP_BORDER,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Date", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL))))
            {
                Colspan = 4,
                Border = PdfPCell.RIGHT_BORDER,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });

            tableLayout.AddCell(new PdfPCell(new Phrase("d", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Administrative Office V", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL))))
            {
                Colspan = 5,
                Border = PdfPCell.TOP_BORDER,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Date", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL))))
            {
                Colspan = 4,
                Border = PdfPCell.RIGHT_BORDER,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });


            tableLayout.AddCell(new PdfPCell(new Phrase("d", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("(Signature over Printed Name)", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL))))
            {
                PaddingLeft = 20,
                Colspan = 5,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Date", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL, BaseColor.WHITE))))
            {
                Colspan = 4,
                Border = PdfPCell.RIGHT_BORDER,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });


            //FOOTER LETTER C - DIVISION TITLE
            tableLayout.AddCell(new PdfPCell(new Phrase("d", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("(Signature over Printed Name)", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL))))
            {
                PaddingLeft = 20,
                Colspan = 5,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Date", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL, BaseColor.WHITE))))
            {
                Colspan = 4,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });

            tableLayout.AddCell(new PdfPCell(new Phrase("d", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            //FOOTER LETTER A - DIVISION TITLE
            tableLayout.AddCell(new PdfPCell(new Phrase("Head of Accounting Division/Unit", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL))))
            {
                Colspan = 5,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Date", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Colspan = 4,
                Border = PdfPCell.RIGHT_BORDER,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });


            tableLayout.AddCell(new PdfPCell(new Phrase("d", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            //FOOTER LETTER C - DIVISION TITLE
            tableLayout.AddCell(new PdfPCell(new Phrase("Disbursing Officer", new Font(FontFactory.GetFont("Times New Roman", 7, Font.NORMAL))))
            {
                Colspan = 5,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Date", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Colspan = 4,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });

            tableLayout.AddCell(new PdfPCell(new Phrase("d", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            //FOOTER LETTER A - DIVISION TITLE
            tableLayout.AddCell(new PdfPCell(new Phrase("Official", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Colspan = 5,
                Border = 0,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Date", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
            {
                Colspan = 4,
                Border = PdfPCell.RIGHT_BORDER,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });


         
            return tableLayout;
        }

        private static void addGrandOverall(PdfPTable tableLayout, String month, String half, String adjustment, String absent, String net
              , String other_adjustment, String tax, String disallow, String coop, String pagibig, String phic, String gsis, String excess,
              String total)
        {
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            tableLayout.AddCell(new PdfPCell(new Phrase("Grand Total", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                Colspan = 2,
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(month, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(half, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(adjustment, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(absent, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(net, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.RED))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(other_adjustment, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(tax, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                Colspan = 2,
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });

            tableLayout.AddCell(new PdfPCell(new Phrase(disallow, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(coop, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(pagibig, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(phic, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(gsis, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(excess, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(total, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.RED))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            AddCellToBody(tableLayout, "", "right");
        }

        private static void addSection(PdfPTable tableLayout, String section, String balance)
        {
            AddCellToBody(tableLayout, "", "left");
            tableLayout.AddCell(new PdfPCell(new Phrase(section, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {
                BorderWidth = 0.2f,
                Colspan = 2,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
            {
                BorderWidth = 0.2f,
                Colspan = 2,
                VerticalAlignment = Element.ALIGN_CENTER,
                HorizontalAlignment = Element.ALIGN_CENTER,
            });
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            if (balance.Equals(""))
            {
                AddCellToBody(tableLayout, balance, "right");
            }
            else
            {
                tableLayout.AddCell(new PdfPCell(new Phrase(balance, new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, BaseColor.RED))))
                {

                    BorderWidth = 0.2f,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                });
            }

            AddCellToBody(tableLayout, "", "left");
        }

        private static void addOverall(PdfPTable tableLayout, String overall)
        {
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            tableLayout.AddCell(new PdfPCell(new Phrase(overall, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.RED))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "dual");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
        }


        protected static PdfPTable JoSummaryHeader(PdfPTable tableLayout, string date_range, PdfWriter writer, string disbursment, string imageURL)
        {

            Image jpg = Image.GetInstance(imageURL);
            jpg.ScaleToFit(50f, 50f);
            jpg.Alignment = Element.ALIGN_LEFT;
            //Resize image depend upon your need

            //Give space before image
            //Give some space after the image



            tableLayout.AddCell(new PdfPCell(jpg)
            {
                Rowspan = 3,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            switch (disbursment)
            {
                case "ATM":
                    disbursment = "WITH REG. ATM CARDS";
                    break;
                case "CASH_CARD":
                    disbursment = "WITH CASH CARDS";
                    break;
                case "NO_CARD":
                    disbursment = "W/O LBP CARD";
                    break;
                case "UNDER_VTF":
                    disbursment = "WITH REG. ATM CARDS";
                    break;
            }
            tableLayout.AddCell(new PdfPCell(new Phrase(disbursment, new Font(Font.FontFamily.HELVETICA, 8, Font.UNDERLINE, BaseColor.WHITE)))
            {
                Rowspan = 3,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_MIDDLE,
                VerticalAlignment = Element.ALIGN_MIDDLE
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(disbursment, new Font(Font.FontFamily.HELVETICA, 8, Font.UNDERLINE, BaseColor.RED)))
            {
                Colspan = 3,
                Rowspan = 3,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_MIDDLE,
                VerticalAlignment = Element.ALIGN_MIDDLE
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("PAYROLL FOR JOB PERSONNEL", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 10,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Page " + writer.PageNumber, new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 5,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("DOH-RO 7", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 10,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Page " + writer.PageNumber, new Font(Font.FontFamily.HELVETICA, 9, 1, BaseColor.WHITE)))
            {
                Colspan = 5,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(date_range, new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 10,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_CENTER
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("Page " + writer.PageNumber, new Font(Font.FontFamily.HELVETICA, 9, 1, BaseColor.WHITE)))
            {
                Colspan = 5,
                Border = 0,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("We acknowledge receipt of the sum shown opposite our names as full renumeration for services rendered for the period started:", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 16,
                Border = 0,
                Padding = 3,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_LEFT
            });
            tableLayout.AddCell(new PdfPCell(new Phrase("*NO WORK NO PAY POLICY", new Font(Font.FontFamily.HELVETICA, 9, 1, new BaseColor(0, 0, 0))))
            {
                Colspan = 4,
                Border = 0,
                Padding = 3,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                VerticalAlignment = Element.ALIGN_RIGHT
            });

            AddCellToHeader(tableLayout, "TIN");
            AddCellToHeader(tableLayout, "Name");
            AddCellToHeader(tableLayout, "Position");
            AddCellToHeader(tableLayout, "MO. RATE");
            AddCellToHeader(tableLayout, "HALF MO.");
            AddCellToHeader(tableLayout, "Adjustment");
            AddCellToHeader(tableLayout, "Tardiness");
            AddCellToHeader(tableLayout, "Net Amount");
            AddCellToHeader(tableLayout, "Adjustment");
            AddCellToHeader(tableLayout, "D E D U C T I O N S");
            AddCellToHeader(tableLayout, "Total Amt.");
            AddCellToHeader(tableLayout, "REMARKS");
            AddCellToHeader(tableLayout, "");
            AddCellToHeader(tableLayout, "Firstname");
            AddCellToHeader(tableLayout, "");
            AddCellToHeader(tableLayout, "");
            AddCellToHeader(tableLayout, "");
            AddCellToHeader(tableLayout, "");
            AddCellToHeader(tableLayout, "Absences");
            AddCellToHeader(tableLayout, "Add:");
            AddCellToHeader(tableLayout, "5% EWT");
            AddCellToHeader(tableLayout, "3% Prof.");
            AddCellToHeader(tableLayout, "Coop");
            AddCellToHeader(tableLayout, "Pag-Ibig");
            AddCellToHeader(tableLayout, "PHIC");
            AddCellToHeader(tableLayout, "GSIS");
            AddCellToHeader(tableLayout, "Excess Mobile");
            AddCellToHeader(tableLayout, "");
            AddCellToHeader(tableLayout, "");
            return tableLayout;
       
    }

        private static void AddCellToBody(PdfPTable tableLayout, string cellText, string position)
        {

            if (position.Equals("left"))
            {
                tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
                {
                    BorderWidth = 0.2f,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
            }
            else if (position.Equals("left-remarks"))
            {
                tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 6, Font.NORMAL))))
                {
                    BorderWidth = 0.2f,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
            }
            else if (position.Equals("dual"))
            {
                tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
                {
                    BorderWidth = 0.2f,
                    Colspan = 2,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
            }
            else
            {
                tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 8, Font.NORMAL))))
                {
                    BorderWidth = 0.2f,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    VerticalAlignment = Element.ALIGN_CENTER
                });
            }
        }

        public static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {
            switch (cellText)
            {
                case "Net Amount":
                    tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
                    {
                        BorderWidth = 0.2f,
                        Rowspan = 2,
                        VerticalAlignment = Element.ALIGN_CENTER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    });
                    break;
                case "Name":
                    tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
                    {
                        BorderWidth = 0.2f,
                        Colspan = 2,
                        VerticalAlignment = Element.ALIGN_CENTER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    });
                    break;
                case "Firstname":
                    tableLayout.AddCell(new PdfPCell(new Phrase("", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
                    {
                        BorderWidth = 0.2f,
                        Colspan = 2,
                        VerticalAlignment = Element.ALIGN_CENTER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    });
                    break;
                case "5% EWT":
                    tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
                    {
                        BorderWidth = 0.2f,
                        Colspan = 2,
                        VerticalAlignment = Element.ALIGN_CENTER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    });
                    break;
                case "Tardiness":
                case "Adjustment":
                case "Absences":
                case "Add:":
                case "MO. RATE":
                case "HALF MO.":
                case "Coop":
                case "3% Prof.":
                case "Pag-Ibig":
                case "PHIC":
                case "Position":
                case "GSIS":
                case "Excess Mobile":
                case "TIN":
                case "Total Amt.":
                case "REMARKS":
                case "":
                    tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
                    {
                        BorderWidth = 0.2f,
                        VerticalAlignment = Element.ALIGN_CENTER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                    });
                    break;
                case "D E D U C T I O N S":
                    tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD))))
                    {
                        BorderWidth = 0.2f,
                        Colspan = 8,
                        VerticalAlignment = Element.ALIGN_CENTER,
                        HorizontalAlignment = Element.ALIGN_CENTER,

                    });
                    break;
            }
          
        }

        private static void addPageTotal(PdfPTable tableLayout, String month, String half, String adjustment, String absent, String net
               , String other_adjustment, String tax, String disallow, String coop, String pagibig, String phic, String gsis, String excess,
               String total)
        {
            AddCellToBody(tableLayout, "", "left");
            AddCellToBody(tableLayout, "", "left");
            if (month.Equals(""))
            {
                tableLayout.AddCell(new PdfPCell(new Phrase("haha", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.WHITE))))
                {
                    Colspan = 2,
                    BorderWidth = 0.2f,
                    VerticalAlignment = Element.ALIGN_CENTER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                });
            }
            else
            {
                tableLayout.AddCell(new PdfPCell(new Phrase("Page Total", new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
                {
                    Colspan = 2,
                    BorderWidth = 0.2f,
                    VerticalAlignment = Element.ALIGN_CENTER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                });
            }

            tableLayout.AddCell(new PdfPCell(new Phrase(month, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(half, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(adjustment, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(absent, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(net, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.RED))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(other_adjustment, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(tax, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                Colspan = 2,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });

            tableLayout.AddCell(new PdfPCell(new Phrase(disallow, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(coop, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(pagibig, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(phic, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(gsis, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(excess, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.BLACK))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            tableLayout.AddCell(new PdfPCell(new Phrase(total, new Font(FontFactory.GetFont("Times New Roman", 8, Font.BOLD, BaseColor.RED))))
            {
                BorderWidth = 0.2f,
                VerticalAlignment = Element.ALIGN_RIGHT,
                HorizontalAlignment = Element.ALIGN_RIGHT,
            });
            AddCellToBody(tableLayout, "", "right");
        }

        public static bool JoSummaryBody(string file_path, string date_from, string date_to, string document, string disbursement, string salary_charge,string imageURL)
        {

            List<JobOrderPayrollModel> list = PayrollDatabase.Instance.GenerateJoSummary(date_from, date_to, document, disbursement, salary_charge);

            if(list.Count > 0)
            {
                MemoryStream workStream = new MemoryStream();

                Document doc = new Document();
                doc.SetMargins(5f, 5f, 5f, 5f);
                doc.SetPageSize(PageSize.LEGAL.Rotate());

                PdfPTable outer = new PdfPTable(1);
                outer.TotalWidth = 400;
                outer.WidthPercentage = 100;
                outer.SplitRows = true;
                outer.SplitLate = true;

                float[] headers = { 10, 11, 11, 12, 6.5f, 6.5f, 7, 7, 7, 7, 4, 3, 6, 6, 6, 5, 5, 5, 6, 10 };
                PdfPTable body = new PdfPTable(20);
                body.SetWidths(headers);
                body.TotalWidth = 400;
                body.WidthPercentage = 100;
                body.SplitRows = true;
                body.SplitLate = true;

                PdfPTable footer = new PdfPTable(20);
                footer.TotalWidth = 400;
                footer.WidthPercentage = 100;

                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(file_path, FileMode.Create));
                doc.Open();

                int month = int.Parse(date_from.Split('/')[0]);
                int year = int.Parse(date_from.Split('/')[2]);

                int day_from = int.Parse(date_from.Split('/')[1]);
                string date_range = DateUtility.GetMonthName(month, year) + " ";
                if (day_from == 1)
                {
                    date_range += day_from + "-15, " + year;
                }
                else
                {
                    date_range += "16-" + DateTime.DaysInMonth(year, month) + ", " + year;
                }

                body = JoSummaryHeader(body, date_range, writer, disbursement,imageURL);
                doc.Add(body);
                body.FlushContent();

                string description = "";
                decimal overall_net = 0;
                //PAGE TOTAL
                decimal page_overall_net = 0;
                decimal page_mo_rate = 0;
                decimal page_half_rate = 0;
                decimal page_adjustment = 0;
                decimal page_absences = 0;
                decimal page_tax = 0;
                decimal page_other_adjustment = 0;
                decimal page_coop = 0;
                decimal page_disallow = 0;
                decimal page_pagibig = 0;
                decimal page_phic = 0;
                decimal page_gsis = 0;
                decimal page_excess = 0;
                decimal page_total_amount = 0;
                //GRAND TOTAL
                decimal grand_overall_net = 0;
                decimal grand_mo_rate = 0;
                decimal grand_half_rate = 0;
                decimal grand_adjustment = 0;
                decimal grand_absences = 0;
                decimal grand_tax = 0;
                decimal grand_other_adjustment = 0;
                decimal grand_coop = 0;
                decimal grand_disallow = 0;
                decimal grand_pagibig = 0;
                decimal grand_phic = 0;
                decimal grand_gsis = 0;
                decimal grand_excess = 0;
                decimal grand_total_amount = 0;

                int MAX_ROW_SIZE = 300;

                foreach (var item in list)
                {

                    string TIN = item.Employee.GetTin();
                    string title_salary_charge =  item.Employee.SalaryCharge;

                    if (title_salary_charge.Equals(""))
                    {
                        title_salary_charge = "NO SALARY CHARGE";
                    }
                    if (body.CalculateHeights() >= MAX_ROW_SIZE)
                    {
                        if (!title_salary_charge.Equals(description))
                        {
                            addOverall(body, overall_net.ToString("#,##0.00"));
                            overall_net = 0;
                        }

                        MAX_ROW_SIZE = 390;

                        addPageTotal(body, page_mo_rate.ToString("#,##0.00"), page_half_rate.ToString("#,##0.00"),page_adjustment.ToString("#,##0.00"), page_absences.ToString("#,##0.00"), 
                            page_overall_net.ToString("#,##0.00"), page_other_adjustment.ToString("#,##0.00"),page_tax.ToString("#,##0.00"), page_disallow.ToString("#,##0.00"), 
                            page_coop.ToString("#,##0.00"),page_pagibig.ToString("#,##0.00"), page_phic.ToString("#,##0.00"), page_gsis.ToString("#,##0.00"), page_excess.ToString("#,##0.00"),
                            page_total_amount.ToString("#,##0.00"));

                        doc.Add(body);
                        body.FlushContent();
                        doc.NewPage();

                        JoSummaryHeader(body, date_range, writer, disbursement, imageURL);
                        addSection(body, "Balance forwarded", page_total_amount.ToString("#,##0.00"));

                        description = "";
                        page_overall_net = 0;
                        page_mo_rate = 0;
                        page_half_rate = 0;
                        page_adjustment = 0;
                        page_absences = 0;
                        page_other_adjustment = 0;
                        page_tax = 0;
                        page_disallow = 0;
                        page_coop = 0;
                        page_pagibig = 0;
                        page_phic = 0;
                        page_gsis = 0;
                        page_excess = 0;
                        page_total_amount = 0;
                    }

                    string ID = item.Employee.ID;
                    string fname = item.Employee.Firstname;
                    string lname = item.Employee.Lastname;

                    string position = item.Employee.Designation;
                    string remarks = item.Remarks;
                    decimal salary = decimal.Parse(item.Salary);
                    decimal adjustment = decimal.Parse(item.Adjustment);

                    if (remarks.ToUpper().Equals("NO DTR") || remarks.ToUpper().Equals("SEPARATE DV"))
                    {
                        salary = 0;
                        adjustment = 0;
                    }

                    grand_adjustment += adjustment;
                    page_adjustment += adjustment;
                    grand_mo_rate += salary;
                    page_mo_rate += salary;
                    decimal half_salary = 0;

                    if (int.Parse(date_from.Split('/')[1]) == 1 && int.Parse(date_to.Split('/')[1]) != 15)
                    {
                        half_salary = salary;
                    }
                    else
                    {
                        half_salary = salary / 2;
                    }

                    grand_half_rate += half_salary;
                    page_half_rate += half_salary;
                    int minutes_late = int.Parse(item.MinutesLate);
                    int size = item.DaysAbsent.Split(',').Length;
                    if (size > 0 && !item.DaysAbsent.Split(',')[0].Equals(""))
                    {
                        minutes_late += (480 * size);
                    }
                    int working_days = int.Parse(item.WorkDays);
                    decimal per_day = 0;
                    decimal absences = 0;
                    if (working_days != 0 && salary != 0)
                    {
                        per_day = salary / working_days;
                        absences = Math.Round((minutes_late * (((per_day) / 8) / 60)), 2, MidpointRounding.AwayFromZero);
                    }

                    grand_absences += absences;
                    page_absences += absences;

                    decimal net_amount = 0;
                    if (working_days != 0)
                    {
                        net_amount = (half_salary - absences + adjustment);
                    }

                    grand_overall_net += net_amount;
                    page_overall_net += net_amount;

                    decimal tax = decimal.Parse(item.Tax);
                    grand_tax += tax;
                    page_tax += tax;

                    decimal other_adjustment = decimal.Parse(item.OtherAdjustment);
                    grand_other_adjustment += other_adjustment;
                    page_other_adjustment += other_adjustment;


                    decimal coop = decimal.Parse(item.Coop);
                    grand_coop += coop;
                    page_coop += coop;


                    decimal disallowance = decimal.Parse(item.Disallowance);
                    grand_disallow += disallowance;
                    page_disallow += disallowance;

                    decimal pagibig = decimal.Parse(item.Pagibig);
                    grand_pagibig += pagibig;
                    page_pagibig += pagibig;

                    decimal phic = decimal.Parse(item.Phic);
                    grand_phic += phic;
                    page_phic += phic;

                    decimal gsis = decimal.Parse(item.Gsis);
                    grand_gsis += gsis;
                    page_gsis += gsis;
                    decimal excess = decimal.Parse(item.ExcessMobile);
                    grand_excess += excess;
                    page_excess += excess;

                    decimal total_amount = (net_amount - (tax + coop + disallowance + pagibig + phic + gsis + excess)) + other_adjustment;
                    grand_total_amount += total_amount;
                    page_total_amount += total_amount;

                    /*PayrollPDF pdf = new PayrollPDF(TIN, fname, lname, section_1, position, salary.ToString("#,##0.00"), half_salary.ToString("#,##0.00"), adjustment.ToString("#,##0.00"), absences.ToString("#,##0.00"), net_amount.ToString("#,##0.00"),
                    other_adjustment.ToString("#,##0.00"), tax.ToString("#,##0.00"), disallowance.ToString("#,##0.00"), coop.ToString("#,##0.00"), pagibig.ToString("#,##0.00"), phic.ToString("#,##0.00"), gsis.ToString("#,##0.00"),
                    excess.ToString("#,##0.00"), total_amount.ToString("#,##0.00"), remarks);
                    pdfList.Add(pdf);
                    */

                    if (description.Equals(""))
                    {
                        addSection(body, title_salary_charge, "");
                        overall_net += net_amount;
                    }
                    else
                    {
                        if (!title_salary_charge.Equals(description))
                        {
                            addOverall(body, overall_net.ToString("#,##0.00"));
                            addSection(body, title_salary_charge, "");
                            overall_net = net_amount;
                        }
                        else
                        {
                            overall_net += net_amount;
                        }
                    }

                    description = title_salary_charge;
                    AddCellToBody(body, TIN, "left");
                    AddCellToBody(body, lname, "left");
                    AddCellToBody(body, fname, "left");
                    AddCellToBody(body, position, "left");
                    AddCellToBody(body, salary.ToString("#,##0.00"), "right");
                    AddCellToBody(body, (salary / 2).ToString("#,##0.00"), "right");
                    AddCellToBody(body, adjustment.ToString("#,##0.00"), "right");
                    AddCellToBody(body, absences.ToString("#,##0.00"), "right");
                    AddCellToBody(body, net_amount.ToString("#,##0.00"), "right");
                    AddCellToBody(body, other_adjustment.ToString("#,##0.00"), "right");
                    AddCellToBody(body, tax.ToString("#,##0.00"), "dual");
                    AddCellToBody(body, disallowance.ToString("#,##0.00"), "right");
                    AddCellToBody(body, coop.ToString("#,##0.00"), "right");
                    AddCellToBody(body, pagibig.ToString("#,##0.00"), "right");
                    AddCellToBody(body, phic.ToString("#,##0.00"), "right");
                    AddCellToBody(body, gsis.ToString("#,##0.00"), "right");
                    AddCellToBody(body, excess.ToString("#,##0.00"), "right");
                    AddCellToBody(body, total_amount.ToString("#,##0.00"), "right");
                    AddCellToBody(body, remarks, "left-remarks");

                }

                addOverall(body, overall_net.ToString("#,##0.00"));

                addPageTotal(body, page_mo_rate.ToString("#,##0.00"), page_half_rate.ToString("#,##0.00"),
                page_adjustment.ToString("#,##0.00"), page_absences.ToString("#,##0.00"), page_overall_net.ToString("#,##0.00"), page_other_adjustment.ToString("#,##0.00"),
                page_tax.ToString("#,##0.00"), page_disallow.ToString("#,##0.00"), page_coop.ToString("#,##0.00"),
                page_pagibig.ToString("#,##0.00"), page_phic.ToString("#,##0.00"), page_gsis.ToString("#,##0.00"), page_excess.ToString("#,##0.00"), page_total_amount.ToString("#,##0.00"));

                addPageTotal(body, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

                addGrandOverall(body, grand_mo_rate.ToString("#,##0.00"), grand_half_rate.ToString("#,##0.00"),
                grand_adjustment.ToString("#,##0.00"), grand_absences.ToString("#,##0.00"), grand_overall_net.ToString("#,##0.00"), grand_other_adjustment.ToString("#,##0.00"),
                grand_tax.ToString("#,##0.00"), grand_disallow.ToString("#,##0.00"), grand_coop.ToString("#,##0.00"),
                grand_pagibig.ToString("#,##0.00"), grand_phic.ToString("#,##0.00"), grand_gsis.ToString("#,##0.00"), grand_excess.ToString("#,##0.00"), grand_total_amount.ToString("#,##0.00"));

                //body = addBody(body, filter_dates, imageURL, from_date, to_date, payroll, writer, doc, disbursment);

                footer = addFooter(footer, "");
                outer.AddCell(new PdfPCell(body)
                {
                    Border = 0,
                });
                outer.AddCell(new PdfPCell(footer)
                {
                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.RIGHT_BORDER | PdfPCell.LEFT_BORDER,
                });

                doc.Add(outer);
                doc.Close();
                return true;
            }
            return false;
        }
    }
}
