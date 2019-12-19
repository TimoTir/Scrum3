using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Scrum3.Model;
using Scrum3.ViewModels;

namespace Scrum3.Controllers
{
    public class OpettajaController : Controller
    {
        private ScrumEntities1 db = new ScrumEntities1();

        // GET: Opettaja
        public ActionResult Index()
        {
            if ((Session["UserName"] != null) && (Session["AccessLevel"].ToString() == "2"))
            {
                var osallistumiset = from os in db.Osallistumiset
                                     join kt in db.KurssiToteutukset on os.KurssitoteutusID equals kt.KurssitoteutusID
                                     join ku in db.Kurssit on kt.Kurssi equals ku.KurssiId
                                     join op in db.Opiskelijat on os.OppilasID equals op.Opiskelijanumero
                                     select new Class1
                                     {
                                         KurssitoteutusID = kt.KurssitoteutusID,
                                         Kurssi = ku.Kurssi,
                                         Laajuus = (int)ku.Laajuus,
                                         Paivamaara = (DateTime)kt.Paivamaara,
                                         Etunimi = op.Etunimi,
                                         Sukunimi = op.Sukunimi,
                                         OsallistumisetID = os.OsallistumisetID
                                     };

                return View(osallistumiset);
            }

            else
            {
                return RedirectToAction("Index", "Logins");
            }
        }
    }
}
