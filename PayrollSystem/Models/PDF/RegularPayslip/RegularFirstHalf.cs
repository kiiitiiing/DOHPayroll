using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
    public class RegularFirstHalf
    {
        public decimal BasicSalary { get; set; }
        public decimal Pera { get; set; }
        public decimal Absences { get; set; }
        public decimal Tax { get; set; }
        public decimal Cfi { get; set; }
        public decimal GsisPremium { get; set; }
        public decimal GsisConsoLoan { get; set; }
        public decimal GsisPolicyLoan { get; set; }
        public decimal GsisEml { get; set; }
        public decimal GsisUoli { get; set; }
        public decimal GsisEdu { get; set; }
        public decimal GsisHelp { get; set; }
        public decimal Rel { get; set; }
        public decimal PagibigPremium { get; set; }
        public decimal PagibigLoan { get; set; }
        public decimal PagibigMp2 { get; set; }
        public decimal PhilHealth { get; set; }
        public decimal Simc { get; set; }
        public decimal Hwmpc { get; set; }
        public decimal Dbp { get; set; }
        public decimal Disallowance { get; set; }
        public decimal Subsistence { get; set; }
        public decimal SubsistenceDeduction { get; set; }
        public decimal Longevity { get; set; }
        public decimal LongevityDeduction { get; set; }

        public RegularFirstHalf(decimal BasicSalary, decimal Pera, decimal Absences, decimal  Tax, decimal Cfi, decimal GsisPremium, decimal GsisConsoLoan, decimal GsisPolicyLoan,
            decimal GsisEml, decimal GsisUoli, decimal  GsisEdu, decimal Rel, decimal GsisHelp, decimal  PagibigPremium, decimal  PagibigLoan,decimal PagibigMp2, decimal PhilHealth, 
            decimal Simc, decimal Hwmpc, decimal Disallowance, decimal Subsistence, decimal SubsistenceDeduction, decimal Longevity, decimal LongevityDeduction)
        {
            this.BasicSalary = BasicSalary;
            this.Pera = Pera;
            this.Absences = Absences;
            this.Tax = Tax;
            this.Cfi = Cfi;
            this.GsisPremium = GsisPremium;
            this.GsisConsoLoan = GsisConsoLoan;
            this.GsisPolicyLoan = GsisPolicyLoan;
            this.GsisEml = GsisEml;
            this.GsisUoli = GsisUoli;
            this.GsisEdu = GsisEdu;
            this.Rel = Rel;
            this.GsisHelp = GsisHelp;
            this.PagibigPremium = PagibigPremium;
            this.PagibigLoan = PagibigLoan;
            this.PagibigMp2 = PagibigMp2;
            this.PhilHealth = PhilHealth;
            this.Simc = Simc;
            this.Hwmpc = Hwmpc;
            this.Dbp = Dbp;
            this.Disallowance = Disallowance;
            this.Subsistence = Subsistence;
            this.SubsistenceDeduction = SubsistenceDeduction;
            this.Longevity = Longevity;
            this.LongevityDeduction = LongevityDeduction;
        }
        
        public decimal GetTotalSalary()
        {
            return BasicSalary + Pera;
        }
        public decimal GetTotalDeduction()
        {
            return Absences + Tax + Cfi + GsisPremium + GsisConsoLoan + GsisPolicyLoan + GsisEml + GsisUoli + GsisEdu
                + GsisHelp + Rel + PagibigPremium + PagibigLoan + PagibigMp2 + PhilHealth + Simc + Hwmpc + Dbp + Disallowance;
        }
        public decimal GetNetSalary()
        {
            return (GetTotalSalary() - GetTotalDeduction())/2;
        }
        public decimal GetNetSubsistence()
        {
            return Subsistence - SubsistenceDeduction;
        }
        public decimal GetNetLongevity()
        {
            return Longevity - LongevityDeduction;
        }

        public decimal GetTotalPay()
        {
            return GetNetSalary() + GetNetSubsistence() + GetNetLongevity();
        }
    }
}