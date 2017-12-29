using Newtonsoft.Json;
using SGGWSupportWeb.Extensions;
using SGGWSupportWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SGGWSupportWeb.Controllers
{
    public class AuthorizeController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"http://webservice.adscan.pl:8090/auth?login={model.Login}&password={model.Password}");
            if (response.IsSuccessStatusCode)
            {
                var message = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                if (message.ErrorCode == "0")
                {
                    Session.SetToken(new UserIdentity() { Token = response.Headers.GetValues("X-AUTH-TOKEN").First() });
                    FormsAuthentication.SetAuthCookie(model.Login, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Nieprawidłowy login lub hasło.");
            return View(model);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = HttpContext.User;

            //zmiana hasła użytkownika zgodnie z dostarczonym API
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", Session.GetToken().Token);
            var response = await client.PatchAsync($"http://webservice.adscan.pl:8090/users/user/password?password={model.ConfirmPassword}");

            if (response.IsSuccessStatusCode)
            {
                var message = JsonConvert.DeserializeObject<APIResponse>(await response.Content.ReadAsStringAsync());
                if (message.ErrorCode != "0")
                {
                    ModelState.AddModelError("", "Zmiana hasła nie powiodła się.");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Zmiana hasła nie powiodła się.");
                return View(model);
            }

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", Session.GetToken().Token);
            var response = await client.DeleteAsync($"http://webservice.adscan.pl:8090/auth");
            if (response.IsSuccessStatusCode)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

    }
}