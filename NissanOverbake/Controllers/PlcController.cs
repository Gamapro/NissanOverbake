using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using EntityLayer;

namespace NissanOverbake.Controllers
{
    public class PlcController : Controller
    {
        // GET: Plc
        public ActionResult Index()
        {
            return View();
        }
        //// ################################ PLCS ####################################
        [HttpPost]
        public ActionResult ListPlcs()
        {
            List<Plc> list = new List<Plc>();
            list = PlcApi.ListPlcs();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetLastPlcConection(int id)
        {
            string date = PlcApi.GetPlcLastConection(id);
            return Json(new { datetime = date }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdatePlc(Plc plc)
        {
            int excecuted = 0;
            string message = "";
            try
            {
                PlcApi.UpdatePlc(plc, out message);
                excecuted = 1;
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return Json(new { message = message, excecuted = excecuted }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult InsertPlc(Plc plc)
        {
            int excecuted = 0;
            string message = "";
            try
            {
                PlcApi.InsertPlc(plc, out message);
                excecuted = 1;
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            return Json(new { message = message, excecuted = excecuted }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeletePlc(Plc plc)
        {
            int excecuted = 0;
            string message = "";
            try
            {
                PlcApi.DeletePlc(plc, out message);
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