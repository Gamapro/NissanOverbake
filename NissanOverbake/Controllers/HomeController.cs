using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using EntityLayer;
using Renci.SshNet.Messages;

namespace NissanOverbake.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult UsersDashboard()
        {
            return View("UsersDashboard","~/Views/Shared/_Layout.cshtml",null);
        }

        [HttpGet]
        public ActionResult ListUsers()
        {
            List<User> list = new List<User>();
            list = UsersApi.listUsers();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult insertOrUpdateUser(User user)
        {
            string message = "";
            bool excecuted = UsersApi.registerOrUpdateUser(user, out message);
            return Json(new { excecuted = excecuted, message = message }, JsonRequestBehavior.AllowGet);
        }

    }
}