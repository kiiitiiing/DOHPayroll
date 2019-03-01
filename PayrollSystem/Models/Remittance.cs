using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
    public class Remittance
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string MaxCount { get; set; }
        public string Count { get; set; }
        public string Amount { get; set; }

        public Remittance(string ID, string UserID, string MaxCount, string Count, string Amount)
        {
            this.ID = ID;
            this.UserID = UserID;
            this.MaxCount = MaxCount;
            this.Count = Count;
            this.Amount = Amount;
        }
    }
}