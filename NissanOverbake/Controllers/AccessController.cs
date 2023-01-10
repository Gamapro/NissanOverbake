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
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidateLogin(User user)
        {
            string message = "";
            bool excecuted = false;
            return Json(new { excecuted = excecuted, message = message }, JsonRequestBehavior.AllowGet);
        }

    }
}