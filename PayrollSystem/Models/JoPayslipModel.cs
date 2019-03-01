using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
    public class JoPayslipModel
    {
        public decimal BasicSalary { get; set; }
        public decimal Adjustment { get; set; }
        public decimal Tardiness { get; set; }
        public decimal OtherAdjustment { get; set; }
        public decimal EWT { get; set; }
        public decimal ProfTax { get; set; }
        public decimal Hwmpc { get; set; }
        public decimal Pagibig { get; set; }
        public decimal Phic { get; set; }
        public decimal Gsis { get; set; }
        public decimal Digitel { get; set; }
        

        public JoPayslipModel(decimal BasicSalary, decimal Adjustment, decimal Tardiness, decimal OtherAdjustment,
            decimal EWT, decimal ProfTax, decimal Hwmpc, decimal Pagibig, decimal Phic, decimal Gsis, decimal Digitel) {

            this.BasicSalary = BasicSalary;
            this.Adjustment = Adjustment;
            this.Tardiness = Tardiness;
            this.OtherAdjustment = OtherAdjustment;
            this.EWT = EWT;
            this.ProfTax = ProfTax;
            this.Hwmpc = Hwmpc;
            this.Pagibig = Pagibig;
            this.Phic = Phic;
            this.Gsis = Gsis;
            this.Digitel = Digitel;
        }

        public decimal GetNetIncome()
        {
            return GetNetAmount() - GetTotalDeduction();
        }

        public decimal GetTotalDeduction()
        {
            return (EWT + ProfTax + Hwmpc + Pagibig + Phic + Gsis + Digitel);
        }

        public decimal GetNetAmount()
        {
            return (BasicSalary + Adjustment);
        }
    }
}