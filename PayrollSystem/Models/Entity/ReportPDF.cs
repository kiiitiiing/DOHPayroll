using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
    public class ReportPDF
    {
        public string ID { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Document { get; set; }
        public string Disbursement { get; set; }
        public string SalaryCharge { get; set; }
        public string Division { get; set; }
        public string FilePath { get; set; }

        public ReportPDF(string ID, string From, string To,string Document, string Disbursement, string SalaryCharge, string Division, string FilePath)
        {
            this.ID = ID;
            this.From = From;
            this.To = To;
            this.Document = Document;
            this.Disbursement = Disbursement;
            this.SalaryCharge = SalaryCharge;
            this.Division = Division;
            this.FilePath = FilePath;
        }

    }
}