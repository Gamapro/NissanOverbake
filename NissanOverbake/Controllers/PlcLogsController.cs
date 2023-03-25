using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using EntityLayer;

namespace NissanOverbake.Controllers
{
    public class PlcLogsController : Controller
    {
        // GET: PlcLogs
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ListPlcLogs()
        {
            List<PlcLog> list = new List<PlcLog>();
            list = PlcLogsApi.ListLogs();
            return Json(new { data = list}, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ListPlcLogsWithDateRange(string fechaInicio, string fechaFin)
        {
            List<PlcLog> list = new List<PlcLog>();
            string message = "";
            int excecuted = 0;
            if (String.IsNullOrEmpty(fechaInicio) || String.IsNullOrEmpty(fechaFin))
            {
                message = "Seleccione un rango valido de fechas";
            }
            else
            {
                try
                {
                    list = PlcLogsApi.ListLogs(fechaInicio, fechaFin);
                    excecuted = 1;
                    message = "Logs obtenidos";
                }
                catch (Exception e)
                {
                    excecuted = 0;
                    message = e.Message;
                }
            }
            return Json(new { data = list, message = message, excecuted = excecuted }, JsonRequestBehavior.AllowGet);
        }
    }
}