using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using EntityLayer;

namespace NissanOverbake.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        // ######################## JSON HTTP REQUESTS ########################
        [HttpPost]
        public ActionResult ListUsers()
        {
            List<User> list = new List<User>();
            list = UsersApi.listUsers();
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult InsertOrUpdateUser(User user)
        {
            string message = "";
            string token = UsersApi.GetUserToken(user);
            user.Token = token;
            int alreadyExists = UsersApi.UserExists(user) ? 1 : 0;
            bool excecuted = UsersApi.RegisterOrUpdateUser(user, out message, out token);
            int id = UsersApi.GetUserId(user);
            return Json(new { excecuted = excecuted, message = message, token = token, alreadyExists = alreadyExists, id = id }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteUser(User user)
        {
            string message = "message";
            bool excecuted = UsersApi.DeleteUser(user, HomeController.getToken(), out message);
            return Json(new { excecuted = excecuted, message = message }, JsonRequestBehavior.AllowGet);
        }
    }
}