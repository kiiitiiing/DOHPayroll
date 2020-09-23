using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
    public partial class LoansModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserId { get; set; }
        public string PagIbigId { get; set; }
        public string ApplicationNo { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string NameExtension { get; set; }
        public string MiddleName { get; set; }
        public string LoanType { get; set; }
        public float Amount { get; set; }
        public string Remarks { get; set; }
    }
}