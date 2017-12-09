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
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            FormsAuthentication.SetAuthCookie(model.Login, false);
            return RedirectToAction("Index", "Home");
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
            client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", "kluczAPI");
            var apiModel = new { password = model.ConfirmPassword };

            var content = new StringContent(JsonConvert.SerializeObject(apiModel), Encoding.UTF8, "application/json");
            var response = await client.PatchAsync("Http://api.sggw.pl/sggwsupport/zmianahasla", content);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("","Zmiana hasła nie powiodła się.");
                return View(model);
            }

            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}