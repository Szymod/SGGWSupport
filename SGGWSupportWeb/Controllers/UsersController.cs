using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGGWSupportWeb.Models;
using SGGWSupportWeb.Extensions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;


namespace SGGWSupportWeb.Controllers
{
    public class UsersController : Controller
    {
        string Baseurl = "http://webservice.adscan.pl:8090/";
        private Permission adminPermission = new Permission
        {
            Id = 10,
            Name = "Admin",
            Code = "ADMIN"

        };
        private Permission userPermission = new Permission
        {
            Id = 11,
            Name = "User",
            Code = "USER"

        };

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<UsersViewModel> users = new List<UsersViewModel>();

            var client = GetInitializedHttpClient();

            var response = await client.GetAsync("users");

                if (response.IsSuccessStatusCode)
                {
                var message = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<dynamic>(message);
                var userList = result.body;
                foreach (var user in userList)
                {
                    UsersViewModel u = new UsersViewModel
                    {
                        Login = user.login,
                        Id = user.id,
                        PhoneNo = user.phone,
                        LastName = user.lastName,
                        FirstName = user.firstName,
                        Email = user.email
                    };

                    users.Add(u);
                }
            }

            return View(users);
        }

        [HttpGet]
        public async Task<ActionResult> UserDetails(int userId)
        {

            UsersViewModel userModel = null;
            var client = GetInitializedHttpClient();
            var response = await client.GetAsync($"users/user/{userId}");
            if (response.IsSuccessStatusCode)
            {
                userModel = initializeUserModel(response);
            }

            return View(userModel);
        }

        private UsersViewModel initializeUserModel(dynamic response)
        {
                var message = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<dynamic>(message);
                var user = result.body;
                // TODO: http get /users/user/{userId}

                List<Permission> permissions = new List<Permission>();
                foreach (var p in user.permissions)
                {
                    Permission permission = new Permission
                    {
                        Id = p.id,
                        Name = p.name,
                        Code = p.code
                    };
                    permissions.Add(permission);
                }

                return new UsersViewModel
                {
                    Login = user.login,
                    Id = user.id,
                    PhoneNo = user.phone,
                    LastName = user.lastName,
                    FirstName = user.firstName,
                    Email = user.email,
                    Permissions = permissions
                };
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: users/user

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UsersViewModel model) {
            
            if (ModelState.IsValid)
            {
                var client = GetInitializedHttpClient();
                var permissions = createPermissions(model);
                var user = new
                {
                    login = model.Login,
                    firstName = model.FirstName,
                    lastName = model.LastName,
                    email = model.Email,
                    phone = model.PhoneNo,
                    password = model.Password,
                    permissions = permissions
                };
                HttpResponseMessage message = await client.PostAsJsonAsync("users/user", user);
                message.EnsureSuccessStatusCode();
                ModelState.Clear();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> Edit(int userId)
        {
            UsersViewModel userModel = null;
            var client = GetInitializedHttpClient();
            var response = await client.GetAsync($"users/user/{userId}");
            if (response.IsSuccessStatusCode)
            {
                userModel = initializeUserModel(response);
            }

            foreach (var p in userModel.Permissions)
            {
                if(p.Id == userPermission.Id)
                {
                    userModel.User = true;
                }
                if (p.Id == adminPermission.Id)
                {
                    userModel.Admin = true;
                }
            }
            return View(userModel);
        }

        // PUT: users/user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UsersViewModel model)
        {
            string userId = this.Request.QueryString["userId"];
            if (ModelState.IsValid)
            {
                var client = GetInitializedHttpClient();
                var permissions = createPermissions(model);
                var user = new
                {
                    id = userId,
                    login = model.Login,
                    firstName = model.FirstName,
                    lastName = model.LastName,
                    email = model.Email,
                    phone = model.PhoneNo,
                    password = model.Password,
                    permissions = permissions
                };
                HttpResponseMessage message = await client.PutAsJsonAsync("users/user", user);
                message.EnsureSuccessStatusCode();
                ModelState.Clear();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> Delete(int userId)
        {
            UsersViewModel userModel = new UsersViewModel
            {
                Id = userId
            };

            return View(userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(UsersViewModel model)
        {
            string userId = this.Request.QueryString["userId"];
            var client = GetInitializedHttpClient();
            var response = await client.DeleteAsync($"users/user/{userId}");

            return RedirectToAction("Index");
        }

        private dynamic createPermissions(UsersViewModel model)
        {
            var permissions = Enumerable.Empty<dynamic>().ToList<dynamic>();

            if (model.User)
            {
                permissions.Add(userPermission);
            }
            if (model.Admin)
            {
                permissions.Add(adminPermission);
            }
            var permissionsArray = new dynamic[permissions.Count];
            for (int i = 0; i < permissions.Count; i++)
            {
                permissionsArray[i] = new
                {
                    id = permissions[i].Id,
                    code = permissions[i].Code,
                    name = permissions[i].Name
                };
            }

            return permissionsArray;
        } 

        private HttpClient GetInitializedHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("X-AUTH-TOKEN", Session.GetToken().Token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
