using System;
using System.Data.Entity;
using System.Linq;

namespace PayrollSystem.Models
{

    public class CookiesModel : Employee
    {
        public string Role { get; set; }
        public string Password { get; set; }

        public CookiesModel() { }
        public CookiesModel(string userid, string fname, string mname, string lname)
        {
            this.ID = userid;
            this.Firstname = fname;
            this.Middlename = mname;
            this.Lastname = lname;

        }
    }
}