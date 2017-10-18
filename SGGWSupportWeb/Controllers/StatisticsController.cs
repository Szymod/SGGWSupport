using SGGWSupportWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGGWSupportWeb.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult _Statistics(string username,int filterType)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IncidentNo",typeof(string));
            dt.Columns.Add("Username", typeof(string));
            dt.Columns.Add("Priority", typeof(string));
            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("RequestDate", typeof(DateTime));
            dt.Columns.Add("Resolved", typeof(bool));

            dt.Rows.Add("No3245389", "lukasz.ostrowski", "Top", "Printer Problem", new DateTime(2017, 10, 2, 9, 9, 8), false);
            dt.Rows.Add("No9003485", "lukasz.ostrowski", "High", "Desktop Problem", new DateTime(2017, 10, 2, 9, 9, 8), false);

            dt.Rows.Add("No8562701", "lukasz.ostrowski", "Top", "Service Problem", new DateTime(2017, 9,21, 9, 9, 8), false);
            dt.Rows.Add("No7664529", "lukasz.ostrowski", "Low", "Printer Problem", new DateTime(2017, 9, 21, 9, 9, 8), false);
            dt.Rows.Add("No3245353", "lukasz.ostrowski", "Low", "Printer Problem", new DateTime(2017, 9, 22, 9, 9, 8), false);
            dt.Rows.Add("No9003456", "lukasz.ostrowski", "Low", "Desktop Problem", new DateTime(2017, 8, 3, 9, 9, 8), false);

            dt.Rows.Add("No85627014", "lukasz.ostrowski", "Low", "Network Problem", new DateTime(2017, 10, 3, 9, 9, 8), true);

            dt.Rows.Add("No85625327", "lukasz.ostrowski", "High", "Network Problem", new DateTime(2017, 9, 24, 9, 9, 8), true);
            dt.Rows.Add("No78657011", "lukasz.ostrowski", "Medium", "Application Problem", new DateTime(2017, 9, 23, 9, 9, 8), true);
            dt.Rows.Add("No78658511", "lukasz.ostrowski", "Top", "Application Problem", new DateTime(2017, 9, 23, 9, 9, 8), true);
            dt.Rows.Add("No78678931", "lukasz.ostrowski", "Medium", "Application Problem", new DateTime(2017, 9, 22, 9, 9, 8), true);
            dt.Rows.Add("No78728411", "lukasz.ostrowski", "High", "Application Problem", new DateTime(2017, 9, 17, 9, 9, 8), true);
            dt.Rows.Add("No78657011", "lukasz.ostrowski", "Top", "Desktop Problem", new DateTime(2017, 9, 13, 9, 9, 8), true);

            dt.Rows.Add("No78728411", "test.user", "High", "Application Problem", new DateTime(2017, 10, 17, 9, 9, 8), true);
            dt.Rows.Add("No78657011", "test.user", "Top", "Desktop Problem", new DateTime(2017, 10, 15, 9, 9, 8), true);
            var data = dt.AsEnumerable().Select(x => new StatiscticsModel
            {
                IncidentNo = x.Field<string>("IncidentNo"),
                Username = x.Field<string>("Username"),
                Priority = x.Field<string>("Priority"),
                Category = x.Field<string>("Category"),
                RequestDate = x.Field<DateTime>("RequestDate"),
                Resolved = x.Field<bool>("Resolved")
            });
            switch (filterType)
            {
                case 1://not resolved last 7 days
                    data = data.Where(r => r.Resolved == false && r.RequestDate > DateTime.Now.AddDays(-7));
                    break;
                case 2://not resolved older than 7 days
                    data = data.Where(r => r.Resolved == false && r.RequestDate <= DateTime.Now.AddDays(-7));
                    break;
                case 3://resolved last 7 days
                    data = data.Where(r => r.Resolved == true && r.RequestDate > DateTime.Now.AddDays(-7));
                    break;
                case 4://resolved older than 7 days
                    data = data.Where(r => r.Resolved == true && r.RequestDate <= DateTime.Now.AddDays(-7));
                    break;
            }
            if (!string.IsNullOrWhiteSpace(username))
            {
                data = data.Where(r => r.Username.Contains(username));
            }
            return PartialView("_Statistics", data);
        }

    }
}
