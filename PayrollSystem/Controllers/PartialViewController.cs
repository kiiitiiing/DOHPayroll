using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PayrollSystem.Models;
using PayrollSystem.Database;

namespace PayrollSystem.Controllers
{
    public class PartialViewController : Controller
    {
        public ActionResult JobOrderList(int? id, int? page, string search)
        {
            int pageNumber = (page ?? 1);
            string searchQuery = (search ?? "");
            int disbursementType = (id ?? 0);

            int pageSize = 10;
            string disbursementQuery = "";

            switch (disbursementType)
            {
                case 0:
                    disbursementQuery = "ATM";
                    break;
                case 1:
                    disbursementQuery = "CASH_CARD";
                    break;
                case 2:
                    disbursementQuery = "NO_CARD";
                    break;
                case 3:
                    disbursementQuery = "UNDER_VTF";
                    break;
            }
            List<JobOrderModel> list = PISDatabase.Instance.GetJoEmployee(searchQuery, disbursementQuery);
            return PartialView(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult JobOrderPayrollList(int? page,string empID, string firstname, string lastname, string middlename)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<JobOrderPayrollModel> list = PayrollDatabase.Instance.GetJobOrderPayrollByID(empID,firstname,lastname,middlename);
            return PartialView(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult RegularPayrollList(int? page, string empID, string firstname, string lastname, string middlename)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<RegularPayrollModel> list = PayrollDatabase.Instance.GetRegularPayrollByID(empID, firstname, lastname, middlename);
            return PartialView(list.ToPagedList(pageNumber, pageSize));
        }
    }
}