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
    public class OverbakeLogsController : Controller
    {
        // GET: OverbakeLogs
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ListOverbakeLogs()
        {
            List<OverbakeLog> list = new List<OverbakeLog>();
            list = LogApi.ListLogs();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ListOverbakeLogsWithDateRange(string fechaInicio, string fechaFin)
        {
            List<OverbakeLog> list = new List<OverbakeLog>();
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
                    list = LogApi.ListLogs(fechaInicio, fechaFin);
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
        [HttpPost]
        public ActionResult FilterOverbakeLogsWithDateRangeAndStatus(string fechaInicio, string fechaFin)
        {
            List<LogCount> list = new List<LogCount>();
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
                    list = LogApi.ListLogCount(fechaInicio, fechaFin);
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
        [HttpPost]
        public ActionResult FilterOverbakeLogsByMonthsWithYear(string year="")
        {
            List<LogCount> list = new List<LogCount>();
            string message = "";
            int excecuted = 0;
            {
                try
                {
                    list = LogApi.ListLogCountByMonthWithYear(year);
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
        [HttpPost]
        public ActionResult GetLogYears(){
            List<int> list = new List<int>();
            string message = "";
            int excecuted = 0;
            {
                try
                {
                    list = LogApi.GetLogYears();
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
        [HttpPost]
        public ActionResult InsertOverbakeLog(OverbakeLog log)
        {
            int excecuted = 0;
            string message = "";
            try
            {
                LogApi.InsertOverbakeLog(log, out message);
                excecuted = 1;
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return Json(new { message = message, excecuted = excecuted }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateOverbakeLog(OverbakeLog log)
        {
            int excecuted = 0;
            string message = "";
            try
            {
                LogApi.UpdateOverbakeLog(log, out message);
                excecuted = 1;
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return Json(new { message = message, excecuted = excecuted }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteOverbakeLog(OverbakeLog log)
        {
            int excecuted = 0;
            string message = "";
            try
            {
                LogApi.DeleteOverbakeLog(log, out message);
                excecuted = 1;
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return Json(new { message = message, excecuted = excecuted }, JsonRequestBehavior.AllowGet);
        }

    }
}