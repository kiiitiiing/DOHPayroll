using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
    public class HazardViewModel
    {
        public decimal NetAmount { get; set; }
        public decimal Deductions { get; set; }

        public HazardViewModel(decimal NetAmount, decimal Deductions)
        {
            this.NetAmount = NetAmount;
            this.Deductions = Deductions;
        }
    }
}