using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
    public class Regular : Employee
    {

        public Regular(string ID, string Firstname, string Lastname, string Middlename, string Designation, string SalaryCharge, string Division, string DisbursementType) :
            base(ID, Firstname, Lastname, Middlename, Designation, SalaryCharge, Division, DisbursementType)
        {

        }
    }
}