using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using DataLayer;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using EntityLayer;

namespace NissanOverbake.Controllers
{
    public class ExportsController : Controller
    {
        // GET: Exports
        public ActionResult Index()
        {
            return View();
        }
        private FileResult CreateExcel(DataTable dt, string title, string fileName)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, title);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + DateTime.Now.ToString() + ".xlsx");
                }
            }
        }
        [HttpPost]
        public FileResult UsersExportToExcel()
        {
            List<User> list = new List<User>();
            list = UsersApi.listUsers();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[6] {
                new DataColumn("Id"),
                new DataColumn("Username"),
                new DataColumn("Password"),
                new DataColumn("Name"),
                new DataColumn("Last Name"),
                new DataColumn("User Type")
            });
            foreach (User user in list)
            {
                dt.Rows.Add(new object[] { user.Id, user.Username, user.Password, user.Name, user.LastName, user.UserTypeId.Usertype });
            }
            Debug.WriteLine("Descargando excel users");
            return CreateExcel(dt, "Users", "RegisteredUsers");
        }
        [HttpPost]
        public FileResult PlcLogsExportToExcel(string fechaInicio, string fechaFin)
        {
            Debug.WriteLine("Fecha inicio PlcLogsExportToExcel: ", fechaInicio);
            Debug.WriteLine("Fecha Fin PlcLogsExportToExcel: ", fechaFin);
            List<PlcLog> list = new List<PlcLog>();
            list = PlcLogsApi.ListLogs(fechaInicio, fechaFin);
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] {
                new DataColumn("Id"),
                new DataColumn("Time"),
                new DataColumn("PlcName"),
                new DataColumn("Message")
            });
            foreach (PlcLog log in list)
            {
                dt.Rows.Add(new object[] { log.Id, log.Time, log.PlcName, log.Message });
            }
            Debug.WriteLine("Descargando excel plcLogs");
            return CreateExcel(dt, "Plc Logs", "PlcLogs");
        }
    }
}