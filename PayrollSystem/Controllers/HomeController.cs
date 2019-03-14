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

        public ActionResult JobOrder()
        {
            /*
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
            List<JobOrderModel> list = PISDatabase.Instance.GetJoEmployee(searchQuery, disbursementQuery);
            */
            return View();
            //return View(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Regular(int? id, int? page, string search)
        {

            int pageNumber = (page ?? 1);
            string searchQuery = (search ?? "");
            int pageSize = 10;
        
            ViewBag.Search = searchQuery;
            List<RegularModel> list = PISDatabase.Instance.GetRegularEmployee(searchQuery);

            return View(list.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public bool DeleteJoPayroll(string id)
        {
            return PayrollDatabase.Instance.DeletePayroll(id);
        }
        [HttpPost]
        public bool DeleteRegularPayroll(string id)
        {
            return PayrollDatabase.Instance.DeleteRegularPayroll(id);
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
            
            JobOrderPayrollModel payroll = new JobOrderPayrollModel(jo_id,start_date,end_date,adjustment,working_days,absent_date_list,salary,minutes_late,coop,phic,prof_tax,gsis,pagibig,excess,remarks,jo_ewt,other_adjustment);
            return PayrollDatabase.Instance.InsertJoPayroll(payroll);
        }

        [HttpPost]
        public bool InsertRegularPayroll()
        {
            string userid = Request["userid"];
            int month = int.Parse(Request["month"]);
            int year = int.Parse(Request["year"]);
            string salary = Request["salary"];
            string pera = Request["pera"];
            string working_days = Request["working_days"];
            int minutes_late = int.Parse(Request["minutes_late"]);
            string absent_date_list = Request["absent_date_list"];
            string tax = Request["tax"];
            string cfi = Request["cfi"];
            string gsis_premium = Request["gsis_premium"];
            string gsis_consoloan = Request["gsis_consoloan"];
            string gsis_policyloan = Request["gsis_policyloan"];
            string gsis_eml = Request["gsis_eml"];
            string gsis_uoli = Request["gsis_uoli"];
            string gsis_edu= Request["gsis_edu"];
            string gsis_help = Request["gsis_help"];
            string gsis_rel = Request["gsis_rel"];
            string pagibig_premium = Request["pagibig_premium"];
            string pagibig_loan = Request["pagibig_loan"];
            string pagibig_mp2 = Request["pagibig_mp2"];
            string philhealth = Request["philhealth"];
            string simc = Request["simc"];
            string hwmpc = Request["hwmpc"];
            string dbp = Request["dbp"];
            string disallowance = Request["disallowance"];

            Employee employee = new Employee();
            employee.ID = userid;

            RegularPayrollModel payroll = new RegularPayrollModel(userid, employee, month,year,absent_date_list,working_days,salary,pera,minutes_late,tax,cfi,gsis_premium,gsis_consoloan,gsis_policyloan,gsis_eml,
                gsis_uoli,gsis_edu,gsis_help,gsis_rel,pagibig_premium,pagibig_loan,disallowance,philhealth,simc,hwmpc,dbp,pagibig_mp2);

            //JobOrderPayrollModel payroll = new JobOrderPayrollModel(jo_id, start_date, end_date, adjustment, working_days, absent_date_list, salary, minutes_late, coop, phic, prof_tax, gsis, pagibig, excess, remarks, jo_ewt, other_adjustment);
            return PayrollDatabase.Instance.InsertRegularPayroll(payroll);
        }

        [HttpPost]
        public bool UpdateRemittance(string table, string ID, string UserID, string MaxCount, string Count, string Amount)
        {
           Remittance remittance = new Remittance(ID,UserID,MaxCount,Count,Amount);
           return PayrollDatabase.Instance.UpdateRemittance(table,remittance);
        }

        [HttpPost]
        public bool UpdateTax(string UserID, string Amount)
        {
            return PayrollDatabase.Instance.UpdateTax(UserID, Amount);
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