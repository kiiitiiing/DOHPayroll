using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
    public class Hazard
    {
        public string ID { get; set; }
        public string PersonnelID { get; set; }
        public string Pay { get; set; }
        public string HWMPC { get; set; }
        public string Mortuary { get; set; }
        public string DigitelBilling { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int DaysLeave { get; set; }
        public int DaysOO { get; set; }

        public Decimal GetNetAmount
        {
            get
            {
                return decimal.Parse(this.Pay) - decimal.Parse(this.HWMPC) - decimal.Parse(this.Mortuary) - decimal.Parse(this.DigitelBilling);
            }
        }

        public Hazard(string ID, string PersonnelID, string Pay, string HWMPC, string Mortuary, string DigitelBilling, int Month, int Year, int DaysLeave, int DaysOO)
        {
            this.ID = ID;
            this.PersonnelID = PersonnelID;
            this.Pay = Pay;
            this.HWMPC = HWMPC;
            this.Mortuary = Mortuary;
            this.DigitelBilling = DigitelBilling;
            this.Month = Month;
            this.Year = Year;
            this.DaysLeave = DaysLeave;
            this.DaysOO = DaysOO;
        }
    }
}