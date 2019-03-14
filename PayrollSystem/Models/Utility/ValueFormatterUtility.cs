using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
    public class ValueFormatterUtility
    {
        public static string ConvertToCurrencyFormat(decimal amount,bool deduction)
        {
            if(deduction)
                return "("+amount.ToString("#,##0.00")+")";
            
            return amount.ToString("#,##0.00");
        }
    }
}