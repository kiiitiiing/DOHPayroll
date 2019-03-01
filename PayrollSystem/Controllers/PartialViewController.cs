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
        public ActionResult PayrollList(int? page,string empID, string firstname, string lastname, string middlename)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<JobOrder> list = PayrollDatabase.Instance.GetPayrollByID(empID,firstname,lastname,middlename);
            return PartialView(list.ToPagedList(pageNumber, pageSize));
        
        }
    }
}