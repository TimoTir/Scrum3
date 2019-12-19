using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Scrum3.Model;
using Scrum3.ViewModels;
namespace Scrum3.Controllers
{
    public class OsallistumisetController : Controller
    {
        private ScrumEntities1 db = new ScrumEntities1();

        // GET: Osallistumiset
        public ActionResult Index()
        {
            if (Session["UserName"] != null)
            {
                if (Session["AccessLevel"].ToString() != "3")
                {
                    var osallistumiset = from os in db.Osallistumiset
                                         join kt in db.KurssiToteutukset on os.KurssitoteutusID equals kt.KurssitoteutusID
                                         join ku in db.Kurssit on kt.Kurssi equals ku.KurssiId
                                         join op in db.Opiskelijat on os.OppilasID equals op.Opiskelijanumero
                                         select new OsallistumisetVM
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
                else //Case acceslevel=3 eli opiskelija
                {
                    int oppi = (int)Session["opiskelijaId"];
                    var osallistumiset = from os in db.Osallistumiset
                                         join kt in db.KurssiToteutukset on os.KurssitoteutusID equals kt.KurssitoteutusID
                                         join ku in db.Kurssit on kt.Kurssi equals ku.KurssiId
                                         join op in db.Opiskelijat on os.OppilasID equals op.Opiskelijanumero
                                         where os.OppilasID == oppi
                                         select new OsallistumisetVM
                                         {
                                             KurssitoteutusID = kt.KurssitoteutusID,
                                             Kurssi = ku.Kurssi,
                                             Laajuus = (int)ku.Laajuus,
                                             Paivamaara = (DateTime)kt.Paivamaara,
                                             Etunimi = op.Etunimi,
                                             Sukunimi = op.Sukunimi,
                                             OsallistumisetID = os.OsallistumisetID
                                         };
                    //var osallistumiset = db.Osallistumiset.Include(o => o.KurssiToteutukset).Include(o => o.Opiskelijat).Where(o => o.OppilasID == oppi);

                    return View(osallistumiset);
                }
            } else
            {
                return RedirectToAction("Index","Logins");
            }

        }

        // GET: Osallistumiset/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osallistumiset osallistumiset = db.Osallistumiset.Find(id);
            if (osallistumiset == null)
            {
                return HttpNotFound();
            }
            return View(osallistumiset);
        }

        // GET: Osallistumiset/Create
        public ActionResult Create()
        {
            var multihaku = db.KurssiToteutukset.Include(k => k.Kurssit);
            List<SelectListItem> kurssitoteutukset = new List<SelectListItem>();
            foreach (var kurssitotteutus in multihaku.ToList())
            {
                kurssitoteutukset.Add(new SelectListItem
                {
                    Value = kurssitotteutus.KurssitoteutusID.ToString(),
                    Text = kurssitotteutus.KurssitoteutusID.ToString() + " " + kurssitotteutus.Kurssit.Kurssi + " - " + kurssitotteutus.Paivamaara.ToString()
                });
            }

            ViewBag.KurssitoteutusID = new SelectList(kurssitoteutukset, "Value", "Text");
            //ViewBag.KurssitoteutusID = new SelectList(db.KurssiToteutukset, "KurssitoteutusID", "KurssitoteutusID");
            ViewBag.OppilasID = new SelectList(db.Opiskelijat, "Opiskelijanumero", "Etunimi");
            return View();
        }

        // POST: Osallistumiset/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OsallistumisetID,KurssitoteutusID,OppilasID")] Osallistumiset osallistumiset)
        {
            if (ModelState.IsValid)
            {
                if ((string)Session["Accesslevel"] == "3")
                {
                    osallistumiset.OppilasID = (int)Session["opiskelijaId"];
                }

                db.Osallistumiset.Add(osallistumiset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KurssitoteutusID = new SelectList(db.KurssiToteutukset, "KurssitoteutusID", "KurssitoteutusID", osallistumiset.KurssitoteutusID);
            ViewBag.OppilasID = new SelectList(db.Opiskelijat, "Opiskelijanumero", "Etunimi", osallistumiset.OppilasID);
            return View(osallistumiset);
        }

        // GET: Osallistumiset/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osallistumiset osallistumiset = db.Osallistumiset.Find(id);
            if (osallistumiset == null)
            {
                return HttpNotFound();
            }
            ViewBag.KurssitoteutusID = new SelectList(db.KurssiToteutukset, "KurssitoteutusID", "KurssitoteutusID", osallistumiset.KurssitoteutusID);
            ViewBag.OppilasID = new SelectList(db.Opiskelijat, "Opiskelijanumero", "Etunimi", osallistumiset.OppilasID);
            return View(osallistumiset);
        }

        // POST: Osallistumiset/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OsallistumisetID,KurssitoteutusID,OppilasID")] Osallistumiset osallistumiset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(osallistumiset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KurssitoteutusID = new SelectList(db.KurssiToteutukset, "KurssitoteutusID", "KurssitoteutusID", osallistumiset.KurssitoteutusID);
            ViewBag.OppilasID = new SelectList(db.Opiskelijat, "Opiskelijanumero", "Etunimi", osallistumiset.OppilasID);
            return View(osallistumiset);
        }

        // GET: Osallistumiset/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osallistumiset osallistumiset = db.Osallistumiset.Find(id);
            if (osallistumiset == null)
            {
                return HttpNotFound();
            }
            return View(osallistumiset);
        }

        // POST: Osallistumiset/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Osallistumiset osallistumiset = db.Osallistumiset.Find(id);
            db.Osallistumiset.Remove(osallistumiset);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //[HttpPost]
        //public ActionResult Authorize(Logins LoginsModel)
        //{
        //    ScrumEntities1 db = new ScrumEntities1();

        //    var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == LoginsModel.UserName && x.PassWord == LoginsModel.PassWord);
        //    if (LoggedUser != null)
        //    {
        //        ViewBag.LoginMessage = "Successfull login";
        //        ViewBag.LoggedStatus = "In";
        //        Session["UserName"] = LoggedUser.UserName;
        //        return RedirectToAction("Index", "Osallistumiset");
        //    }
        //    else
        //    {
        //        ViewBag.LoginMessage = "Login unsuccessfull";
        //        ViewBag.LoggedStatus = "Out";
        //        LoginsModel.LoginIdErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
        //        return View("Login", LoginsModel);
        //    }

        //}
        //public ActionResult LogOut()
        //{
        //    Session.Abandon();
        //    ViewBag.LoggedStatus = "Out";
        //    return RedirectToAction("Index", "Logins"); //Uloskirjautumisen jälkeen pääsivulle
        //}
    }
}
