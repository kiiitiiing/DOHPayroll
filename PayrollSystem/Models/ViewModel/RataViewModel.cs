using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
    public class RataViewModel
    {
        public decimal Ra { get; set; }
        public decimal Ta { get; set; }
        public decimal Deduction { get; set; }

        public RataViewModel(decimal Ra, decimal Ta, decimal Deduction)
        {
            this.Ra = Ra;
            this.Ta = Ta;
            this.Deduction = Deduction;
        }
    }
}