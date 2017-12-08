using SGGWSupportWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace SGGWSupportWeb.Controllers
{
    public class TicketController : Controller
    {
        string Baseurl = "http://webservice.adscan.pl:8090/";

        // GET: Ticket
        public async Task<ActionResult> Index()
        {
            List<TicketViewModel> tickets = new List<TicketViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("tasks");

                if (message.IsSuccessStatusCode)
                {
                    var ticketsResponse = message.Content.ReadAsStringAsync().Result;
                    tickets = JsonConvert.DeserializeObject<List<TicketViewModel>>(ticketsResponse);
                }
            }

                return View(tickets);
        }

        public ActionResult AddTicket()
        {
            return View();
        }

        public ActionResult EditTicket()
        {
            return View();
        }

        public ActionResult DeleteTicket()
        {
            return View();
        }

        public ActionResult TicketDetails()
        {
            return View();
        }

        

    }
}