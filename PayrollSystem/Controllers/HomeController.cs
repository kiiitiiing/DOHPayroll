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
        [Authorize]
        public ActionResult Report()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult JobOrder(int? id)
        {
            switch (id)
            {
                case 0:
                        ViewBag.Action = "Job Order - ATM";
                        break;
                case 1:
                        ViewBag.Action = "Job Order - CASH CARD";
                        break;
                case 2:
                        ViewBag.Action = "Job Order - W/O LBP CARD";
                        break;
                case 3:
                        ViewBag.Action = "Job Order - UNDER VTF";
                        break;
                default:
                        ViewBag.Action = "Job Order - ATM";
                        break;

            }
            return View();
        }

        public ActionResult Regular()
        {      
            return View();
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
            string id = Request["id"];
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

            string type = Request["type"];
            bool isUpdate = false;

            //If it's update, no need to increment the count of paid in loans.
            if (type.Equals("Update")) isUpdate = true;

            JobOrderPayrollModel payroll = new JobOrderPayrollModel(id.CheckValue("0"),jo_id, start_date, end_date, adjustment, working_days, absent_date_list, salary, minutes_late, coop, phic, prof_tax, gsis, pagibig, excess, remarks, jo_ewt, other_adjustment);
            return PayrollDatabase.Instance.InsertJoPayroll(payroll, isUpdate);
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
            string gsis_edu = Request["gsis_edu"];
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

            RegularPayrollModel payroll = new RegularPayrollModel(userid, employee, month, year, absent_date_list, working_days, salary, pera, minutes_late, tax, cfi, gsis_premium, gsis_consoloan, gsis_policyloan, gsis_eml,
                gsis_uoli, gsis_edu, gsis_help, gsis_rel, pagibig_premium, pagibig_loan, disallowance, philhealth, simc, hwmpc, dbp, pagibig_mp2);
            
            return PayrollDatabase.Instance.InsertRegularPayroll(payroll);
        }

        [HttpPost]
        public bool UpdateRemittance(string table, string ID, string UserID, string MaxCount, string Count, string Amount)
        {
            Remittance remittance = new Remittance(ID, UserID, MaxCount, Count, Amount.CheckValue("0"));
            RemittanceLogModel log = new RemittanceLogModel
            {
                UserId = UserID,
                Max = MaxCount,
                Count = Count,
                Amount = Amount.CheckValue("0"),
                RemittanceType = table,
                CreatedAt = DateTime.Now,
                UpdateAt = DateTime.Now
            };

            return PayrollDatabase.Instance.UpdateRemittance(table, remittance);
        }


        [HttpPost]
        public string UpdateSalary()
        {
            string userid = Request["userid"];
            string tranch = Request["tranch"];
            string grade = Request["grade"];
            string step = Request["step"];

            var salary = PISDatabase.Instance.GetSalary(tranch, grade, step);
            if (PISDatabase.Instance.UpdateEmployeeSalary(salary, userid, tranch, grade, step))
                return salary;
            else
                return "";
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
        [HttpPost]
        public int GetWorkingDays(string from, string to)
        {
            return DTRDatabase.Instance.GetWorkingDays(from, to);
        }

        [HttpPost]
        public bool InsertHazard(string id, string userid,decimal pay, decimal hwmpc, decimal mortuary, decimal digitel, int month, int year, int days_leave, int days_oo)
        {
            Hazard hazard = new Hazard(id,userid,pay,hwmpc,mortuary,digitel,month,year,days_leave,days_oo);
            return PayrollDatabase.Instance.InsertHazardPay(hazard);
        }

        [HttpPost]
        public bool InsertRata(string id, string userid, decimal ra, decimal ta , decimal deduction,int month, int year,string remark)
        {
            Rata rata = new Rata(id,userid,ra,ta,deduction,month,year,remark);
            return PayrollDatabase.Instance.InsertRata(rata);
        }
        [HttpPost]
        public bool InsertCell(string id, string userid, decimal amount, decimal less, int month, int year, string remark)
        {
            Cellphone cell = new Cellphone(id, userid, amount, less,  month, year, remark);
            return PayrollDatabase.Instance.InsertCell(cell);
        }
        [HttpPost]
        public bool InsertLongevity(string id, string userid, decimal salary1, decimal salary2, decimal salary3, decimal salary4, decimal salary5, decimal disallowance, int month, int year)
        {
            Longevity longevity = new Longevity(id, userid, salary1, salary2, salary3, salary4, salary5, disallowance, month, year);
            return PayrollDatabase.Instance.InsertLongevity(longevity);
        }
        [HttpPost]
        public bool InsertSubsistence(string id, string userid, decimal subsistence_allowance, decimal laundry_allowance, decimal absences, decimal hwmpc, int no_days, string remarks, int month, int year)
        {
            Subsistence subsistence = new Subsistence(id,userid,subsistence_allowance,laundry_allowance,absences,hwmpc,no_days,remarks,month,year);
            return PayrollDatabase.Instance.InsertSubsistence(subsistence);
        }

        [HttpPost]
        public bool DeleteHazard(string id)
        {
            return PayrollDatabase.Instance.DeleteHazard(id);
        }
        [HttpPost]
        public bool DeleteRata(string id)
        {
            return PayrollDatabase.Instance.DeleteRata(id);
        }
        [HttpPost]
        public bool DeleteCell(string id)
        {
            return PayrollDatabase.Instance.DeleteCell(id);
        }
        [HttpPost]
        public bool DeleteLongevity(string id)
        {
            return PayrollDatabase.Instance.DeleteLongevity(id);
        }
        [HttpPost]
        public bool DeleteSubsistence(string id)
        {
            return PayrollDatabase.Instance.DeleteSubsistence(id);
        }
     }
}