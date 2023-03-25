using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using EntityLayer;

namespace NissanOverbake.Controllers
{
    public class OverbakeLogsController : Controller
    {
        // GET: OverbakeLogs
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ListOverbakeLogs()
        {
            List<Log> list = new List<Log>();
            list = LogApi.listLogs();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}