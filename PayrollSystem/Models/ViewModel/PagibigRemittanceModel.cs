using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
    public partial class PagibigRemittanceModel
    {
        public string UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PagIbigNo { get; set; }
        public string AccountNo { get; set; }
        public string Membership { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string NameExtension { get; set; }
        public string MiddleName { get; set; }
        public string PerCov { get; set; }
        public float EEShare { get; set; }
        public float ERShare { get; set; }
        public string Remarks { get; set; }
    }
}