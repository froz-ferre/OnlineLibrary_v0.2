using OnlineLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineLibrary.Controllers
{
    public class AccountController : Controller
    {
        private LibraryDBEntities db = new LibraryDBEntities();

        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public ActionResult Register(User user)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    var cookie = new HttpCookie("AUTH", user.Email);
                    Response.Cookies.Add(cookie);
                    return Redirect("/Home/Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Oops!", ex);
            }
            return View();
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var currentUser = (from user in db.Users
                             where user.Email == email
                             select user).First();
            if (currentUser != null && currentUser.Password == password)
            {
                var cookie = new HttpCookie("AUTH", currentUser.Email);
                Response.Cookies.Add(cookie);
                return Redirect("/Home/Index");
            }
            else return View();
        }


        // GET: Account/Logout
        public ActionResult Logout()
        {
            //var cookie = Response.Cookies["AUTH"];
            //cookie.Expires.AddDays(-1);
            //Response.Cookies.Add(cookie);
            if (Request.Cookies["AUTH"] != null)
            {
                var cookie = new HttpCookie("AUTH")
                {
                    Expires = DateTime.Now.AddDays(-1d)
                };
                Response.Cookies.Add(cookie);
            }
            return Redirect("/Home");
        }
    }
}