using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Scrum3.Model;

namespace Scrum3.Controllers
{
    public class KurssitController : Controller
    {
        private ScrumEntities db = new ScrumEntities();

        // GET: Kurssit
        public ActionResult Index()
        {
            if ((Session["UserName"] == null) || (Session["AccessLevel"].ToString() != "1"))
            {
                return RedirectToAction("Index", "Logins");
            }
            else
            {
                ScrumEntities db = new ScrumEntities();
                List<Kurssit> model = db.Kurssit.ToList();
                db.Dispose();
                return View(model);
                
            }
            
        }

        // GET: Kurssit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kurssit kurssit = db.Kurssit.Find(id);
            if (kurssit == null)
            {
                return HttpNotFound();
            }
            return View(kurssit);
        }

        // GET: Kurssit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kurssit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Kurssi,Laajuus,KurssiId")] Kurssit kurssit)
        {
            if (ModelState.IsValid)
            {
                db.Kurssit.Add(kurssit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kurssit);
        }

        // GET: Kurssit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kurssit kurssit = db.Kurssit.Find(id);
            if (kurssit == null)
            {
                return HttpNotFound();
            }
            return View(kurssit);
        }

        // POST: Kurssit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Kurssi,Laajuus,KurssiId")] Kurssit kurssit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kurssit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kurssit);
        }

        // GET: Kurssit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kurssit kurssit = db.Kurssit.Find(id);
            if (kurssit == null)
            {
                return HttpNotFound();
            }
            return View(kurssit);
        }

        // POST: Kurssit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kurssit kurssit = db.Kurssit.Find(id);
            db.Kurssit.Remove(kurssit);
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
                return RedirectToAction("Index", "Logins");
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.LoggedStatus = "Out";
                LoginsModel.LoginIdErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
                return View("Login", LoginsModel);
            }

        }
        //public ActionResult LogOut()
        //{
        //    Session.Abandon();
        //    ViewBag.LoggedStatus = "Out";
        //    return RedirectToAction("Index", "Logins"); //Uloskirjautumisen jälkeen pääsivulle
        //}
    }
}
