using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
    public class RegularPayslipModel
    {
        public RegularFirstHalf FirstHalf { get; set; }
        public RegularSecondHalf SecondHalf { get; set; }

        public RegularPayslipModel(RegularFirstHalf FirstHalf, RegularSecondHalf SecondHalf)
        {
            this.FirstHalf = FirstHalf;
            this.SecondHalf = SecondHalf;
        }

        public decimal GetNetPayment()
        {
            return Math.Round(FirstHalf.GetTotalPay() + SecondHalf.GetTotalPay());
        }
    }
}