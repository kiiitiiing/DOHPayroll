using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
    public partial   class DtrFile
    {
        public string UserId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Event_type { get; set; }
    }
}