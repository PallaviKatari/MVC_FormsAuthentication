using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC_Forms.Models;

namespace MVC_Forms.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            using (MVC_FormsEntities context = new MVC_FormsEntities())
            {
                bool IsValidUser = context.Users.Any(user => user.Username == model.UserName &&
                                                             user.UserPassword == model.UserPassword);
                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Employees");

                }
                ModelState.AddModelError("", "UserName or Password do not Match");
                return View();


            }
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User model)
        {
            using (MVC_FormsEntities context = new MVC_FormsEntities())
            {
                context.Users.Add(model);
                context.SaveChanges();

            }
            return RedirectToAction("Login");


        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}