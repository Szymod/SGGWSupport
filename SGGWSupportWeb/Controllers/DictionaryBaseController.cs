using Newtonsoft.Json;
using SGGWSupportWeb.Extensions;
using SGGWSupportWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SGGWSupportWeb.Controllers
{
    public abstract class DictionaryBaseController : Controller
    {
        protected async Task<ActionResult> Index<T>(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", Session.GetToken().Token);
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var model = JsonConvert.DeserializeObject<List<T>>(await response.Content.ReadAsStringAsync());
                    return View(model);
                }

                return RedirectToAction("Error");
            }
        }

        protected async Task<ActionResult> Add<T>()
        {
            return await Task.FromResult(View(Activator.CreateInstance<T>()));
        }

        protected async Task<ActionResult> Add<T>(string url, T model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", Session.GetToken().Token);
                var response = await client.PostAsJsonAsync(url, model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Error");
            }
        }

        protected async Task<ActionResult> Edit<T>(string url, int id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", Session.GetToken().Token);
                var response = await client.GetAsync(string.Format("{0}/{1}", url, id));

                if (response.IsSuccessStatusCode)
                {
                    var model = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                    return View(model);
                }

                return RedirectToAction("Error");
            }
        }

        protected async Task<ActionResult> Edit<T>(string url, T model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", Session.GetToken().Token);
                var response = await client.PutAsJsonAsync(url, model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Error");
            }
        }

        protected async Task<ActionResult> Delete(string url, int id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", Session.GetToken().Token);
                var response = await client.DeleteAsync(string.Format("{0}/{1}", url, id));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Error");
            }
        }
    }
}
