using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
    public class Employee
    {
        public string ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Middlename { get; set; }
        public string Designation { get; set; }
        public string Salary { get; set; }
        public string SalaryCharge { get; set; }
        public string Division { get; set; }
        public string DisbursementType { get; set; }
        public string Tin { get; set; }
        public string WorkSchedule { get; set; }

        public Remittance Coop { get; set; }
        public Remittance Pagibig { get; set; }
        public Remittance Gsis { get; set; }
        public Remittance Phic { get; set; }
        public Remittance Excess { get; set; }


        public Employee() { }


        public Employee(string ID, string Firstname, string Lastname , string Middlename , string Designation , string Salary, string SalaryCharge, string Division,  string DisbursementType)
        {
            this.ID = ID;
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.Middlename = Middlename;
            this.Designation = Designation;
            this.Salary = Salary;
            this.SalaryCharge = SalaryCharge;
            this.Division = Division;
            this.DisbursementType = DisbursementType;
        }
        public void SetTin(string Tin)
        {
            this.Tin = Tin;
        }
        public string GetTin()
        {
            return Tin;
        }

        public void SetWorkSchedle(string WorkSchedule)
        {
            this.WorkSchedule = WorkSchedule;
        }
        public string GetWorkSchedle()
        {
            return WorkSchedule;
        }

        public void SetCoop(Remittance Coop)
        {
            this.Coop = Coop;
        }

        public Remittance GetCoop()
        {
            return Coop;
        }

        public void SetPagibig(Remittance Pagibig)
        {
            this.Pagibig = Pagibig;
        }

        public Remittance GetPagibig()
        {
            return Pagibig;
        }

        public void SetPhic(Remittance Phic)
        {
            this.Phic = Phic;
        }

        public Remittance GetPhic()
        {
            return Phic;
        }
        public void SetGsis(Remittance Gsis)
        {
            this.Gsis = Gsis;
        }

        public Remittance GetGsis()
        {
            return Gsis;
        }
        public void SetExcess(Remittance Excess)
        {
            this.Excess = Excess;
        }

        public Remittance GetExcess()
        {
            return Excess;
        }

        public string CoopFormat()
        {
            return Coop.Amount + "/" + Coop.MaxCount + "/" + Coop.Count;
        }

        public string PagibigFormat()
        {
            return Pagibig.Amount + "/" + Pagibig.MaxCount + "/" + Pagibig.Count;
        }
        public string GsisFormat()
        {
            return Gsis.Amount + "/" + Gsis.MaxCount + "/" + Gsis.Count;
        }
        public string PhicFormat()
        {
            return Phic.Amount + "/" + Phic.MaxCount + "/" + Phic.Count;
        }

        public string ExcessFormat()
        {
            return Excess.Amount + "/" + Excess.MaxCount + "/" + Excess.Count;
        }

    }
}