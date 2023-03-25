using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        // ############################## VIEWS ###############################
        public static string getToken()
        {
            try
            {
                if (System.Web.HttpContext.Current.Session["token"] == null) return null;
                return System.Web.HttpContext.Current.Session["token"].ToString();
            }catch (Exception ex) {
                Debug.WriteLine("Error getToken", ex.Message);
                return "";
            }
        }
        private bool checkToken()
        {
            return getToken() != null;
        }
        private bool checkUserCredentials()
        {
            Debug.WriteLine("User token ", getToken());
            return UsersApi.IsAdmin(getToken());
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult UsersDashboard()
        {
            if (!checkToken() || !checkUserCredentials()) return View("~/Views/Shared/AccessDenied.cshtml", "~/Views/Shared/_Layout.cshtml");
            return View("UsersDashboard","~/Views/Shared/_Layout.cshtml",null);
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult OverbakeLogCharts()
        {
            if (!checkToken()) return View("~/Views/Shared/AccessDenied.cshtml", "~/Views/Shared/_Layout.cshtml");
            return View("OverbakeLogCharts", "~/Views/Shared/_Layout.cshtml", null);
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult OverbakeLogDashboard()
        {
            if (!checkToken() || !checkUserCredentials()) return View("~/Views/Shared/AccessDenied.cshtml", "~/Views/Shared/_Layout.cshtml");
            return View("OverbakeLogDashboard", "~/Views/Shared/_Layout.cshtml", null);
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult PlcConnection()
        {
            if (!checkToken()) return View("~/Views/Shared/AccessDenied.cshtml", "~/Views/Shared/_Layout.cshtml");
            return View("PlcConnection", "~/Views/Shared/_Layout.cshtml", null);
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult PlcLogDashboard()
        {
            if (!checkToken() || !checkUserCredentials()) return View("~/Views/Shared/AccessDenied.cshtml", "~/Views/Shared/_Layout.cshtml");
            return View("PlcLogDashboard", "~/Views/Shared/_Layout.cshtml", null);
        }
    }
}