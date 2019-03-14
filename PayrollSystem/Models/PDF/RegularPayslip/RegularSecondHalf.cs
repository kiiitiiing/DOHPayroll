using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
    public class RegularSecondHalf
    {
        public decimal Salary { get; set; }
        public decimal HazardPay { get; set; }
        public decimal Mortuary { get; set; }
        public decimal Hwmpc { get; set; }
        public decimal OtherPayable { get; set; }
        public decimal CellphoneAllowance { get; set; }
        public decimal CellphoneDeduction { get; set; }
        public decimal MidYearBonus { get; set; }
        public decimal YearEndBonus { get; set; }
        public decimal Rata { get; set; }

        public RegularSecondHalf(decimal Salary, decimal HazardPay, decimal Mortuary, decimal Hwmpc, decimal OtherPayable, 
            decimal  CellphoneAllowance, decimal CellphoneDeduction, decimal MidYearBonus, decimal YearEndBonus, decimal Rata)
        {
            this.Salary = Salary;
            this.HazardPay = HazardPay;
            this.Mortuary = Mortuary;
            this.Hwmpc = Hwmpc;
            this.OtherPayable = OtherPayable;
            this.CellphoneAllowance = CellphoneAllowance;
            this.CellphoneDeduction = CellphoneDeduction;
            this.MidYearBonus = MidYearBonus;
            this.YearEndBonus = YearEndBonus;
            this.Rata = Rata;
        }


        public decimal GetNetHazard()
        {
            return HazardPay - Mortuary - Hwmpc - OtherPayable;
        }

        public decimal GetNetCellphone()
        {
            return CellphoneAllowance - CellphoneDeduction;
        }

        public decimal GetTotalPay()
        {
            return Salary + GetNetHazard() + GetNetCellphone() + MidYearBonus + YearEndBonus + Rata;
        }

    }
}