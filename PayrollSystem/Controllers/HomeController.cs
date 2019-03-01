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
using PayrollSystem.Database;

namespace PayrollSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Report()
        {
            return View();
        }

        public ActionResult Regular(int? id, int? page, string search)
        {

            int pageNumber = (page ?? 1);
            string searchQuery = (search ?? "");
            int pageSize = 10;
        
            ViewBag.Action = "ATM";
            ViewBag.Search = searchQuery;
            List<Employee> list = PISDatabase.Instance.GetEmployee(searchQuery, "Permanent", "ATM");

            return View(list.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public bool InsertJoPayroll()
        {
            string jo_id = Request["jo_id"];
            string start_date = Request["start_date"];
            string end_date = Request["end_date"];
            string adjustment = Request["adjustment"];
            string working_days = Request["working_days"];
            string absent_date_list = Request["absent_date_list"];
            string salary = Request["salary"];
            string minutes_late = Request["minutes_late"];
            string prof_tax = Request["prof_tax"];
            string jo_ewt = Request["jo_ewt"];
            string coop = Request["coop"];
            string other_adjustment = Request["other_adjustment"];
            string pagibig = Request["pagibig"];
            string excess = Request["excess"];
            string phic = Request["phic"];
            string gsis = Request["gsis"];
            string remarks = Request["remarks"];
            
            JobOrder payroll = new JobOrder(jo_id,start_date,end_date,adjustment,working_days,absent_date_list,salary,minutes_late,coop,phic,prof_tax,gsis,pagibig,excess,remarks,jo_ewt,other_adjustment);
            return PayrollDatabase.Instance.InsertPayroll(payroll);
        }

        [HttpPost]
        public bool UpdateRemittance(string table, string ID, string UserID, string MaxCount, string Count, string Amount)
        {
           Remittance remittance = new Remittance(ID,UserID,MaxCount,Count,Amount);
           return PayrollDatabase.Instance.UpdateRemittance(table,remittance);
        }

        public ActionResult JobOrder(int? id, int? page, string search)
        {
            
            int pageNumber = (page ?? 1);
            string searchQuery = (search ?? "");
            int disbursementType = (id ?? 0);

            int pageSize = 10;
            string disbursementQuery = "";

            switch (disbursementType)
            {
                case 0:
                    ViewBag.Action = "ATM";
                    disbursementQuery = "ATM";
                    break;
                case 1:
                    ViewBag.Action = "CASH CARD";
                    disbursementQuery = "CASH_CARD";
                    break;
                case 2:
                    ViewBag.Action = "W/O LBP CARD";
                    disbursementQuery = "NO_CARD";
                    break;
                case 3:
                    ViewBag.Action = "UNDER VTF";
                    disbursementQuery = "UNDER_VTF";
                    break;

            }

            ViewBag.Search = searchQuery;
            List<Employee> list = PISDatabase.Instance.GetEmployee(searchQuery, "Job Order",disbursementQuery);

            return View(list.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public string GetMins(string id, string from, string to, string am_in, string am_out, string pm_in, string pm_out)
        {
            return DTRDatabase.Instance.GetMins(id, from, to, am_in, am_out, pm_in, pm_out);
        }


        /*
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
            doc.Add(RegularViewModelPDF.RegularDivisionSummaryBody("01","2019","3"));
            doc.Close();

            return File(strAttachment, "application/pdf");
          
        }

        public ActionResult RegularSalaryChargePDF()
        {
            string strAttachment = Server.MapPath(Url.Content("~/Pdf/sample_salary.pdf"));

            RegularViewModelPDF.RegularSalaryChargeSummary(strAttachment);
             
            return File(strAttachment, "application/pdf");
        }
        */
    }
}