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
                client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", Session.GetToken().Token);

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
            List<StateViewModel> states = new List<StateViewModel>(); 
            List<SelectListItem> categoryList = new List<SelectListItem>();
            List<SelectListItem> priorityList = new List<SelectListItem>();
            List<SelectListItem> statesList = new List<SelectListItem>();

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

                message = await client.GetAsync("statuses");

                if (message.IsSuccessStatusCode)
                {
                    var statesResponse = message.Content.ReadAsStringAsync().Result;
                    states = JsonConvert.DeserializeObject<List<StateViewModel>>(statesResponse);
                }

                foreach (var el in categories)
                {
                    categoryList.Add(new SelectListItem { Text = el.Name, Value = (el.Id.ToString()) });
                }

                foreach (var el in priorities)
                {
                    priorityList.Add(new SelectListItem { Text = el.Name, Value = (el.Id.ToString()) });
                }

                foreach (var el in states)
                {
                    statesList.Add(new SelectListItem { Text = el.Name, Value = (el.Id.ToString()) });
                }
            }

            ViewData["states"] = statesList;
            ViewData["categories"] = categoryList;
            ViewData["priorities"] = priorityList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTicket(TicketViewModel ticket)
        {
            ModelState["Status.Name"].Errors.Clear();
            ModelState["Priority.Name"].Errors.Clear();
            ModelState["Category.Name"].Errors.Clear();

            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", Session.GetToken().Token);

                    ticket.UserData = Session.GetCurrentUser();
                    ticket.TicketAddTime = DateTime.Now;

                    HttpResponseMessage message = await client.PostAsJsonAsync("tasks", ticket);
                    message.EnsureSuccessStatusCode();
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(ticket);
            }
        }

        public async Task<ActionResult> EditTicket(int id)
        {
            TicketViewModel ticket = new TicketViewModel();

            List<CategoryViewModel> categories = new List<CategoryViewModel>();
            List<PriorityViewModel> priorities = new List<PriorityViewModel>();
            List<StateViewModel> states = new List<StateViewModel>();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            List<SelectListItem> priorityList = new List<SelectListItem>();
            List<SelectListItem> statesList = new List<SelectListItem>();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", Session.GetToken().Token);

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

                message = await client.GetAsync("statuses");

                if (message.IsSuccessStatusCode)
                {
                    var statesResponse = message.Content.ReadAsStringAsync().Result;
                    states = JsonConvert.DeserializeObject<List<StateViewModel>>(statesResponse);
                }

                foreach (var el in categories)
                {
                    categoryList.Add(new SelectListItem { Text = el.Name, Value = (el.Id.ToString()) });
                }

                foreach (var el in priorities)
                {
                    priorityList.Add(new SelectListItem { Text = el.Name, Value = (el.Id.ToString()) });
                }

                foreach (var el in states)
                {
                    statesList.Add(new SelectListItem { Text = el.Name, Value = (el.Id.ToString()) });
                }
            

                message = await client.GetAsync("tasks/" + id);



                    if (message.IsSuccessStatusCode)
                    {
                        var ticketResponse = message.Content.ReadAsStringAsync().Result;
                        ticket = JsonConvert.DeserializeObject<TicketViewModel>(ticketResponse);
                    }
                
                }

                ViewBag.Id = id.ToString();
                ViewData["states"] = statesList;
                ViewData["categories"] = categoryList;
                ViewData["priorities"] = priorityList;

                return View(ticket);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditTicket(TicketViewModel ticket)
        {
            ModelState["Status.Name"].Errors.Clear();
            ModelState["Priority.Name"].Errors.Clear();
            ModelState["Category.Name"].Errors.Clear();

            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", Session.GetToken().Token);

                    ticket.UserData = Session.GetCurrentUser();
                    ticket.TicketAddTime = DateTime.Now;

                    HttpResponseMessage message = await client.PostAsJsonAsync("tasks", ticket);
                    message.EnsureSuccessStatusCode();
                }

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> DeleteTicket(int? id)
        {
            TicketViewModel ticket = new TicketViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", Session.GetToken().Token);

                HttpResponseMessage message = await client.GetAsync("tasks/" + id);

                if (message.IsSuccessStatusCode)
                {
                    var ticketResponse = message.Content.ReadAsStringAsync().Result;
                    ticket = JsonConvert.DeserializeObject<TicketViewModel>(ticketResponse);
                }

            }

            ViewBag.Id = id.ToString();

            return PartialView("_DeleteTicket", ticket);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteTicket(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", Session.GetToken().Token);

                HttpResponseMessage message = await client.DeleteAsync("tasks/" + id);
                message.EnsureSuccessStatusCode();

            }

            return RedirectToAction("Index");

        }

        public async Task<ActionResult> TicketDetails(int? id)
        {
            TicketViewModel ticket = new TicketViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", Session.GetToken().Token);

                HttpResponseMessage message = await client.GetAsync("tasks/" + id);

                if (message.IsSuccessStatusCode)
                {
                    var ticketResponse = message.Content.ReadAsStringAsync().Result;
                    ticket = JsonConvert.DeserializeObject<TicketViewModel>(ticketResponse);
                }

            }

            ViewBag.Id = id.ToString();

            return View(ticket);

        }



    }
}