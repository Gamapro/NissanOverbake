using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataLayer;
using EntityLayer;
using Google.Protobuf.WellKnownTypes;
using Renci.SshNet.Messages;

namespace NissanOverbake.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Index()
        {
            // return View("PlcLogDashboard", "~/Views/Shared/_Layout.cshtml", null);
            return View();
        }
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Index(string name, string password) //User user)
        {
            Debug.WriteLine("Name", name);
            Debug.WriteLine("Password", password);
            User user = new User();
            string message = "", token = "";
            user.Password = password;
            user.Username = name;
            bool excecuted = AccessApi.ValidateLogin(user, out message, out token);
            if (excecuted)
            {
                System.Web.HttpContext.Current.Session["token"] = token;
                System.Web.HttpContext.Current.Session["username"] = name;
                System.Web.HttpContext.Current.Session["isAdmin"] = UsersApi.IsAdmin(token);
                return RedirectToAction("OverbakeLogCharts", "Home");
            }
            Debug.WriteLine("Message", message);
            System.Web.HttpContext.Current.Session["validation_message"] = message;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ValidateLogin(User user)
        {
            string message = "", token = "";
            bool excecuted = AccessApi.ValidateLogin(user, out message, out token);
            return Json(new { excecuted = excecuted, message = message, token = token }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClearSessionToken()
        {
            // System.Web.HttpContext.Current.Session["token"] = "";
            // System.Web.HttpContext.Current.Session["validation_message"] = "";
            System.Web.HttpContext.Current.Session.Clear();
            // bool excecuted = (System.Web.HttpContext.Current.Session["Token"].ToString() == "");
            // string message = (excecuted ? "Token cleared" : "Token not cleared");
            // return Json(new { excecuted = excecuted, message = message }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Index");
        }
        
    }
}