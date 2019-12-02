using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Scrum3.Model;

namespace Scrum3.Controllers
{
    public class LoginsController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(Logins LoginsModel)
        {
            ScrumEntities db = new ScrumEntities();

            var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == LoginsModel.UserName && x.PassWord == LoginsModel.PassWord);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successfull login";
                ViewBag.LoggedStatus = "In";
                Session["UserName"] = LoggedUser.UserName;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.LoggedStatus = "Out";
                LoginsModel.LoginIdErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
                return View("Login", LoginsModel);
            }

        }
    }
}