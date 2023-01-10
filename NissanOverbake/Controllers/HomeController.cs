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

        // ############################## VIEWS ###############################

        public ActionResult UsersDashboard()
        {
            return View("UsersDashboard","~/Views/Shared/_Layout.cshtml",null);
        }

        public ActionResult OverbakeLogCharts()
        {
            return View("OverbakeLogCharts", "~/Views/Shared/_Layout.cshtml", null);
        }

        public ActionResult OverbakeLogDashboard()
        {
            return View("OverbakeLogDashboard", "~/Views/Shared/_Layout.cshtml", null);
        }

        public ActionResult PlcConnection()
        {
            return View("PlcConnection", "~/Views/Shared/_Layout.cshtml", null);
        }
        public ActionResult PlcLogDashboard()
        {
            return View("PlcLogDashboard", "~/Views/Shared/_Layout.cshtml", null);
        }

        // ######################## JSON HTTP REQUESTS ########################

        [HttpGet]
        public ActionResult ListUsers()
        {
            List<User> list = new List<User>();
            list = UsersApi.listUsers();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ListPlcLogs()
        {
            List<PlcLog> list = new List<PlcLog>();
            list = PlcLogsApi.listLogs();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ListOverbakeLogs()
        {
            List<Log> list = new List<Log>();
            list = LogApi.listLogs();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertOrUpdateUser(User user)
        {
            string message = "";
            string token = "";
            bool excecuted = UsersApi.registerOrUpdateUser(user, out message, out token);
            return Json(new { excecuted = excecuted, message = message, token = token }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteUser(User user)
        {
            string message = "message";
            bool excecuted = false; //  UsersApi.registerOrUpdateUser(user, out message);
            return Json(new { excecuted = excecuted, message = message }, JsonRequestBehavior.AllowGet);
        }

    }
}