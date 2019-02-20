using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using PayrollSystem.Models;
using PagedList;
namespace PayrollSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Report()
        {
            return View();
        }

        public ActionResult JobOrder(int? id, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            string action = "ATM";
            switch (id)
            {
                case 1:
                    action = "CASH CARD";
                    break;
                case 2:
                    action = "W/O LBP CARD";
                    break;
                case 3:
                    action = "UNDER VTF";
                    break;

            }
            ViewBag.Action = action;
            List<Employee> list = new List<Employee>()
            {
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
                new Employee("0618","Asnaui", "Pangcatan","Optina","Computer Programmer 1","SA-11231","MSD","CASH_CARD"),
            };
            return View(list.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult RegularPayslipPDF()
        {
            string strAttachment = Server.MapPath(Url.Content("~/Pdf/sample.pdf"));

            MemoryStream workStream = new MemoryStream();

            Document doc = new Document();
            doc.SetMargins(5f, 5f, 45f, 5f);
            doc.SetPageSize(PageSize.A4);

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(strAttachment, FileMode.Create));
            doc.Open();
            doc.Add(RegularViewModelPDF.RegularPayslipTitle("01", "2019"));
            doc.Add(RegularViewModelPDF.RegularPayslipHeader("0618", "ASNAUI O. PANGCATAN", "COMPUTER PROGRAMMER 1"));
            doc.Add(RegularViewModelPDF.GeneratePaySlipBody());
            doc.Close();

            return File(strAttachment, "application/pdf");
        }

        public ActionResult RegularDivisionSummaryPDF()
        {
          
            string strAttachment = Server.MapPath(Url.Content("~/Pdf/sample_division.pdf"));

            float[] widths = { 1, 3, 2, 2, 2, 1 };
            MemoryStream workStream = new MemoryStream();

            Document doc = new Document();
            doc.SetMargins(5f, 5f, 45f, 5f);
            doc.SetPageSize(PageSize.A4);

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(strAttachment, FileMode.Create));
            doc.Open();
            doc.Add(RegularViewModelPDF.RegularDivisionSummaryTitle("01", "2019"));
            doc.Add(RegularViewModelPDF.RegularDivisionSummaryBody("01","2019"));
            doc.Close();

            return File(strAttachment, "application/pdf");
          
        }

        public ActionResult RegularSalaryChargePDF()
        {
            string strAttachment = Server.MapPath(Url.Content("~/Pdf/sample_salary.pdf"));

            float[] widths = { 1, 3, 2, 2, 2, 1 };
            MemoryStream workStream = new MemoryStream();

            RegularViewModelPDF.RegularSalaryChargeSummary(strAttachment);
             
            return File(strAttachment, "application/pdf");
        }
    }
}