using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SGGWSupportWeb.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using System.Web.Security;

namespace SGGWSupportWeb.Controllers
{
    public class UsersController : Controller
    {

        public ActionResult Index()
        {
            // TODO: http get /users

            //mock
            var user1 = new { id = 1, login = "JanK" ,firstName = "Jan", lastName = "Kowalski", email = "jkowalski@gmail.com", phone = "411224124" };
            var user2 = new { id = 2, login = "JanK", firstName = "Adam", lastName = "Nowak", email = "nowak@gmail.com", phone = "241215252" };

            var body = new[] { user1, user2 };

            var userList = new List<UserViewModel>();
            foreach(var user in body){
                UserViewModel u = new UserViewModel
                {
                    Id = user.id,
                    Login = user.login,
                    PhoneNo = user.phone,
                    LastName = user.lastName,
                    FirstName = user.firstName,
                    Email = user.email
                };
                userList.Add(u);
            }
        
            return View(userList);
        }


        public ActionResult UserDetails(int userId)
        {
            // TODO: http get /users/user/{userId}

            //mock
            var user = new {
                id = userId,
                login = "JanK",
                firstName = "Adam",
                lastName = "Nowak",
                email = "nowak@gmail.com",
                phone = "241215252",
                permissions = new[] { new {id = 1, name="User", code = "USER" }, new { id = 2, name = "Admin", code = "ADMIN" } }
            };

            List<Permission> permissions = new List<Permission>();
            foreach(var p in user.permissions)
            {
                Permission permission = new Permission
                {
                    Id = p.id,
                    Name = p.name,
                    Code = p.code
                };
                permissions.Add(permission);
            }

            UserViewModel userModel = new UserViewModel
            {
                Id = user.id,
                Login = user.login,
                PhoneNo = user.phone,
                LastName = user.lastName,
                FirstName = user.firstName,
                Email = user.email,
                Permissions = permissions
            };

            return View(userModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: users/user
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int userId)
        {
            // TODO: http get /users/user/{userId}

            //mock
            var user = new
            {
                id = userId,
                login = "JanK",
                firstName = "Adam",
                lastName = "Nowak",
                email = "nowak@gmail.com",
                phone = "241215252",
                permissions = new[] { new { id = 1, name = "User", code = "USER" }, new { id = 2, name = "Admin", code = "ADMIN" } }
            };

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

            UserViewModel userModel = new UserViewModel
            {
                Id = user.id,
                Login = user.login,
                PhoneNo = user.phone,
                LastName = user.lastName,
                FirstName = user.firstName,
                Email = user.email,
                Permissions = permissions
            };

            return View(userModel);
        }

        // PUT: users/user
        [HttpPut]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
  
        public ActionResult Delete(int userId)
        {
            UserViewModel userModel = new UserViewModel
            {
                Id = userId
            };

            return View(userModel);
        }

        // DELETE: users/user/{userId}
        [HttpDelete]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
