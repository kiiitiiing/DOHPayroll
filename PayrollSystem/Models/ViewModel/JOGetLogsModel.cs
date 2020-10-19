﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollSystem.Models
{
    public partial class JOGetLogsModel
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public DateTime DateIn { get; set; }
        public string DayName { get; set; }
        public string AMIN { get; set; }
        public string AMOUT { get; set; }
        public string PMIN { get; set; }
        public string PMOUT { get; set; }
        public int Late { get; set; }
        public int UnderTime { get; set; }
        public bool Absent { get; set; }
        public bool HalfDay { get; set; }
        //public bool HalfDayPM { get; set; }
    }
}