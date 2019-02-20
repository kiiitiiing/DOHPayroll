using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PayrollSystem.Models;

namespace PayrollSystem.Controllers
{
    public class PartialViewController : Controller
    {
        public ActionResult PayrollList(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

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
    }
}