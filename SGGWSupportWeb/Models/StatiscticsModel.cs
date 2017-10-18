using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGGWSupportWeb.Models
{
    public class StatiscticsModel
    {
        public string IncidentNo { get; set; }
        public string Username { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }
        public DateTime RequestDate { get; set; }
        public bool Resolved { get; set; }
    }
}