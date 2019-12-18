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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Käyttäjätunnukset()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(Logins LoginsModel)
        {
            ScrumEntities1 db = new ScrumEntities1();

            var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == LoginsModel.UserName && x.PassWord == LoginsModel.PassWord);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successfull login";
                ViewBag.LoggedStatus = "In";
                ViewBag.Acceslevel = LoggedUser.AccessLevel;
                ViewBag.LoginId = LoggedUser.LoginId;
                Session["UserName"] = LoggedUser.UserName;
                Session["Accesslevel"] = LoggedUser.AccessLevel.ToString();
                Session["LoginId"] = LoggedUser.LoginId;
                if (LoggedUser.AccessLevel.ToString() == "1")
                {
                    //Admin
                }
                else if (LoggedUser.AccessLevel.ToString() == "2")
                {
                    //Opettaja
                    int henkiloId;
                    int LoginId = LoggedUser.LoginId;
                    Opettajat opet = new Opettajat();
                    opet = db.Opettajat.Where(o => o.LoginId == LoginId).FirstOrDefault();

                    henkiloId = opet.HenkiloID;
                    Session["henkiloId"] = henkiloId;
                    Session["KirjautunutKayttajaNimi"] = opet.Etunimi + " " + opet.Sukunimi;
                }
                else if (LoggedUser.AccessLevel.ToString() == "3")
                {
                    //Opiskelija
                    int opiskelijaId;
                    int LoginId = LoggedUser.LoginId;
                    Opiskelijat opisk = new Opiskelijat();
                    opisk = db.Opiskelijat.Where(o => o.LoginId == LoginId).FirstOrDefault();
                    opiskelijaId = opisk.Opiskelijanumero;
                    Session["opiskelijaId"] = opiskelijaId;
                    Session["KirjautunutKayttajaNimi"] = opisk.Etunimi + " " + opisk.Sukunimi;
                } else
                {
                    //Vihre
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.LoggedStatus = "Out";
                LoginsModel.LoginIdErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
                return View("Index");
            }

        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Out";
            return RedirectToAction("Index", "Logins"); //Uloskirjautumisen jälkeen pääsivulle
        }
    }
}