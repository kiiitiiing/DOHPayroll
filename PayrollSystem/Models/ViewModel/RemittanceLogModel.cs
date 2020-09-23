using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
    public partial class RemittanceLogModel
    {
        public string RemittanceType { get; set; }
        public string UserId { get; set; }
        public string Max { get; set; }
        public string Count { get; set; }
        public string Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}