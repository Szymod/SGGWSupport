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
using SGGWSupportWeb.Extensions;
using System.Net.Http.Formatting;

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

        public async Task<ActionResult> AddTicket()
        {
            List<CategoryViewModel> categories = new List<CategoryViewModel>();
            List<PriorityViewModel> priorities = new List<PriorityViewModel>();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            List<SelectListItem> priorityList = new List<SelectListItem>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", Session.GetToken().Token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = await client.GetAsync("category");

                if (message.IsSuccessStatusCode)
                {
                    var categoryResponse = message.Content.ReadAsStringAsync().Result;
                    categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(categoryResponse);
                }

                message = await client.GetAsync("priority");

                if (message.IsSuccessStatusCode)
                {
                    var priorityResponse = message.Content.ReadAsStringAsync().Result;
                    priorities = JsonConvert.DeserializeObject<List<PriorityViewModel>>(priorityResponse);
                }

                foreach (var el in categories)
                {
                    categoryList.Add(new SelectListItem { Text = el.Name, Value = (el.id.ToString()) });
                }

                foreach (var el in priorities)
                {
                    priorityList.Add(new SelectListItem { Text = el.Name, Value = (el.id.ToString()) });
                }
            }

            ViewData["categories"] = categoryList;
            ViewData["priorities"] = priorityList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTicket(TicketViewModel ticket)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", Session.GetToken().Token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                HttpResponseMessage message = await client.PostAsJsonAsync("tasks", ticket);
                message.EnsureSuccessStatusCode();

                ModelState.Clear();

                return RedirectToAction("Index", "Ticket");
            }
            else
            {
                return RedirectToAction("Index", "Ticket");
            }
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