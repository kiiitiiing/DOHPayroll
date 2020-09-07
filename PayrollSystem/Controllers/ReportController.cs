using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PayrollSystem.Models;
using PayrollSystem.Database;
using PagedList;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PayrollSystem.Controllers
{
     public class ReportController : Controller
     {

          public ActionResult Index(int? page, string search)
          {
               int pageNumber = (page ?? 1);
               int pageSize = 15;

               string searchQuery = (search ?? "");
               ViewBag.Search = searchQuery;

               List<ReportPDF> list = PayrollDatabase.Instance.GetPDF(searchQuery);

               if (TempData["Message"] != null)
               {
                    ViewBag.Message = TempData["Message"];
               }
               return View(list.ToPagedList(pageNumber, pageSize));
          }

          public ActionResult DeletePDF(string id)
          {
               PayrollDatabase.Instance.DeletePDF(id);
               return RedirectToAction("Index", "Report");
          }

          public ActionResult CreateDivisionSummary(string date, string division)
          {


               string document = "ALL";
               string disbursement = "ATM";
               string salary_charge = "";

               int month = int.Parse(date.Split('-')[0].Trim());
               int year = int.Parse(date.Split('-')[1].Trim());

               string from = date.Split('-')[0] + "/01" + date.Split('-')[1];
               string to = date.Split('-')[0] + "/" + DateTime.DaysInMonth(year, month) + "/" + date.Split('-')[1];


               string file_name = month + year + division + ".pdf";
               string division_name = "";
               switch (division)
               {
                    case "3":
                         division_name = "Office of the RD / ARD";
                         break;
                    case "4":
                         division_name = "LHSD - Local Health Support Division";
                         break;
                    case "5":
                         division_name = "RLED - Regulatory and Licensing Enforcement Division";
                         break;
                    case "6":
                         division_name = "MSD - Management Support Division";
                         break;
               }

               float[] widths = { 1, 3, 2, 2, 2, 1 };
               string phyiscal_file_path = Server.MapPath(Url.Content("~/Pdf/" + file_name));

               bool ok = RegularDivisionPDFView.RegularDivisionSummary(phyiscal_file_path, month, year, division);
               if (ok)
               {
                    TempData["Message"] = "Generated Successfully";
                    PayrollDatabase.Instance.InsertPDF(from, to, document, disbursement, salary_charge, division_name, file_name);
               }
               else
               {
                    TempData["Message"] = "Nothing to generate";
               }

               return RedirectToAction("Index", "Report");
          }

          public ActionResult CreateSalarySummary(string date, string salary_charge)
          {

               string document = "ALL";
               string disbursement = "ATM";
               string division = "";

               int month = int.Parse(date.Split('-')[0].Trim());
               int year = int.Parse(date.Split('-')[1].Trim());

               string from = date.Split('-')[0] + "/01" + date.Split('-')[1];
               string to = date.Split('-')[0] + "/" + DateTime.DaysInMonth(year, month) + "/" + date.Split('-')[1];

               string salary_charge_format = salary_charge.Replace('/', '-');

               string file_name = month + year + salary_charge_format + ".pdf";

               string file_path = Server.MapPath(Url.Content("~/Pdf/" + file_name));

               //List<RegularPayrollModel> list = PayrollDatabase.Instance.GenerateSummaryRegularBySalaryCharge(month, year, salary_charge);

               List<SalaryChargeItem> list = PayrollDatabase.Instance.GenerateSalaryChargeSummary(month, year);
               if (list.Count > 0)
               {
                    SalaryChargePDFView.RegularSalaryChargeSummary(month, year, salary_charge, file_path, list);
                    TempData["Message"] = PayrollDatabase.Instance.InsertPDF(from, to, document, disbursement, salary_charge, division, file_name);
               }
               else
               {
                    TempData["Message"] = "Nothing to generate";
               }

               return RedirectToAction("Index", "Report");
          }

          public ActionResult CreateJoSummary(string date, string document, string disbursement, string salary_charge)
          {

               string date_from = date.Split('-')[0].Trim();
               string date_to = date.Split('-')[1].Trim();

               int from_month = int.Parse(date_from.Split('/')[0]);
               int from_day = int.Parse(date_from.Split('/')[1]);
               int from_year = int.Parse(date_from.Split('/')[2]);

               int to_month = int.Parse(date_to.Split('/')[0]);
               int to_day = int.Parse(date_to.Split('/')[1]);
               int to_year = int.Parse(date_to.Split('/')[2]);

               string file_name = from_month + "" + from_day + "" + from_year + "" + to_month + "" + to_day + "" + to_year + "_" + "jo_summary.pdf";

               string file_path = Server.MapPath("~/Pdf/" + file_name);

               string imageURL = Server.MapPath(Url.Content("~/Images/doh.png"));

               bool ok = JoSummaryPDFView.JoSummaryBody(file_path, date_from, date_to, document, disbursement, salary_charge, imageURL);
               if (ok)
               {
                    TempData["Message"] = "Generated Successfully";
                    PayrollDatabase.Instance.InsertPDF(date_from, date_to, document, disbursement, salary_charge, "", file_name);
               }
               else
               {
                    TempData["Message"] = "Nothing to generate";
               }
               return RedirectToAction("Index", "Report");
          }

          public ActionResult CreateJoPayslip(string id, string fullnames, string date_from, string date_to, string designation)
          {

               int month = int.Parse(date_from.Split('/')[0]);
               int year = int.Parse(date_from.Split('/')[2]);

               string file_name = "jo_payslip.pdf";

               string title = DateUtility.GetMonthName(month, year) + " " + int.Parse(date_from.Split('/')[1]) + "-" + date_to.Split('/')[1] + ", " + year;

               string physical_file_path = Server.MapPath(Url.Content("~/Pdf/" + file_name));

               JoPayslipPDFView.JoPayslipContainer(physical_file_path, title, id, fullnames, designation, date_from, date_to);

               return File(physical_file_path, "application/pdf");
          }

          public ActionResult CreateRegularPayslip(string id, string fullname, int month, int year, string designation)
          {

               //  int month = int.Parse(date_from.Split('/')[0]);
               //   int year = int.Parse(date_from.Split('/')[2]);

               string file_name = "regular_payslip.pdf";

               string title = DateUtility.GetMonthName(month, year) + "1-31, " + year;

               string physical_file_path = Server.MapPath(Url.Content("~/Pdf/" + file_name));


               RegularPayslipPDFView.RegularPayslipContainer(id, fullname, designation, month, year, physical_file_path);

               return File(physical_file_path, "application/pdf");
          }



          public ActionResult ViewPDF(string pdf)
          {
               return File(Server.MapPath("~/Pdf/" + pdf), "application/pdf");
          }
     }
}