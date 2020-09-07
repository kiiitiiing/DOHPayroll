using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
     public class SalaryPageTotal
     {
          public decimal BasicTop { get; set; }
          public decimal BasicDown { get; set; }
          public decimal BenefitsTop { get; set; }
          public decimal BenefitsDown { get; set; }
          public decimal GrossTop { get; set; }
          public decimal GrossDown { get; set; }
          public decimal TaxTop { get; set; }
          public decimal TaxDown { get; set; }
          public decimal GsisTop { get; set; }
          public decimal PagibigTop { get; set; }        
          public decimal OthersTop { get; set; }
          public decimal TotalDeductionTop { get; set; }
          public decimal NetAmountTop { get; set; }
          public decimal NetAmountDown { get; set; }

          public SalaryPageTotal(decimal BasicTop, decimal BasicDown, decimal BenefitsTop, decimal BenefitsDown, decimal GrossTop, decimal GrossDown, decimal TaxTop, decimal TaxDown, decimal GsisTop,
                                 decimal PagibigTop, decimal OthersTop,  decimal TotalDeductionTop, decimal NetAmountTop, decimal NetAmountDown)
          {
               this.BasicTop = BasicTop;
               this.BasicDown = BasicDown;
               this.BenefitsTop = BenefitsTop;
               this.BenefitsDown = BenefitsDown;
               this.GrossTop = GrossTop;
               this.GrossDown = GrossDown;
               this.TaxTop = TaxTop;
               this.TaxDown = TaxDown;
               this.GsisTop = GsisTop;
               this.PagibigTop = PagibigTop;    
               this.OthersTop = OthersTop;        
               this.TotalDeductionTop = TotalDeductionTop;          
               this.NetAmountTop = NetAmountTop;
               this.NetAmountDown = NetAmountDown;
          }
     }
}