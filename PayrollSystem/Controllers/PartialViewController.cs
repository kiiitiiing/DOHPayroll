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

        public ActionResult RegularList(int? page, string search)
        {

            int pageNumber = (page ?? 1);
            string searchQuery = (search ?? "");
            int pageSize = 10;

            List<RegularModel> list = PISDatabase.Instance.GetRegularEmployee(searchQuery);

            return PartialView(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult JobOrderPayrollList(int? page, string empID, string firstname, string lastname)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<JobOrderPayrollModel> list = PayrollDatabase.Instance.GetJobOrderPayrollByID(empID, firstname, lastname);
            return PartialView(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult RegularPayrollList(int? page, string empID, string firstname, string lastname)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<RegularPayrollModel> list = PayrollDatabase.Instance.GetRegularPayrollByID(empID, firstname, lastname);
            return PartialView(list.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult GetRegularPayroll(string id)
        {
            var payroll = PayrollDatabase.Instance.GetRegularPayroll(id);
            return Json(payroll);
        }

        public ActionResult HazardList(int? page, string empID, string firstname, string lastname)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<HazardListViewModel> list = PayrollDatabase.Instance.GetHazardList(empID, firstname, lastname);
            return PartialView(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult RataList(int? page, string empID, string firstname, string lastname)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<RataListViewModel> list = PayrollDatabase.Instance.GetRataList(empID, firstname, lastname);
            return PartialView(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult CellphoneList(int? page, string empID, string firstname, string lastname)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<CellListViewModel> list = PayrollDatabase.Instance.GetCellList(empID, firstname, lastname);
            return PartialView(list.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult LongevityList(int? page, string empID, string firstname, string lastname)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<LongevityListViewModel> list = PayrollDatabase.Instance.GetLongevityList(empID, firstname, lastname);
            return PartialView(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SubsistenceList(int? page, string empID, string firstname, string lastname)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<SubsistenceListViewModel> list = PayrollDatabase.Instance.GetSubsistenceList(empID, firstname, lastname);
            return PartialView(list.ToPagedList(pageNumber, pageSize));
        }
    }
}