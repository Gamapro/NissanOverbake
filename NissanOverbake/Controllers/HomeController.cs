using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using EntityLayer;

namespace NissanOverbake.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UsersDashboard()
        {
            ViewBag.Message = "This is thee messageeeee in viewwss";

            return View();
        }

        [HttpGet]
        public ActionResult ListUsers()
        {
            List<User> list = new List<User>();
            list = UsersApi.listUsers();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

    }
}