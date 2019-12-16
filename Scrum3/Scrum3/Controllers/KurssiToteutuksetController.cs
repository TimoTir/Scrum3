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
    public class KurssiToteutuksetController : Controller
    {
        private ScrumEntities1 db = new ScrumEntities1();

        // GET: KurssiToteutukset
        public ActionResult Index()
        {
            var kurssiToteutukset = db.KurssiToteutukset.Include(k => k.Kurssit).Include(k => k.Luokkatilat).Include(k => k.Opettajat);
            return View(kurssiToteutukset.ToList());
        }

        // GET: KurssiToteutukset/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KurssiToteutukset kurssiToteutukset = db.KurssiToteutukset.Find(id);
            if (kurssiToteutukset == null)
            {
                return HttpNotFound();
            }
            return View(kurssiToteutukset);
        }

        // GET: KurssiToteutukset/Create
        public ActionResult Create()
        {
            ViewBag.Kurssi = new SelectList(db.Kurssit, "KurssiId", "Kurssi");
            ViewBag.Luokka = new SelectList(db.Luokkatilat, "LuokkaID", "Luokka");
            ViewBag.Opettaja = new SelectList(db.Opettajat, "HenkiloID", "Etunimi","Sukunimi");
            return View();
        }

        // POST: KurssiToteutukset/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KurssitoteutusID,Paivamaara,Opettaja,Luokka,Kurssi,Kellonaika")] KurssiToteutukset kurssiToteutukset)
        {
            if (ModelState.IsValid)
            {
                db.KurssiToteutukset.Add(kurssiToteutukset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Kurssi = new SelectList(db.Kurssit, "KurssiId", "Kurssi", kurssiToteutukset.Kurssi);
            ViewBag.Luokka = new SelectList(db.Luokkatilat, "LuokkaID", "Luokka", kurssiToteutukset.Luokka);
            ViewBag.Opettaja = new SelectList(db.Opettajat, "HenkiloID", "Etunimi","Sukunimi", kurssiToteutukset.Opettaja);
            return View(kurssiToteutukset);
        }

        // GET: KurssiToteutukset/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KurssiToteutukset kurssiToteutukset = db.KurssiToteutukset.Find(id);
            if (kurssiToteutukset == null)
            {
                return HttpNotFound();
            }
            ViewBag.Kurssi = new SelectList(db.Kurssit, "KurssiId", "Kurssi", kurssiToteutukset.Kurssi);
            ViewBag.Luokka = new SelectList(db.Luokkatilat, "LuokkaID", "Luokka", kurssiToteutukset.Luokka);
            ViewBag.Opettaja = new SelectList(db.Opettajat, "HenkiloID", "Etunimi", "Sukunimi", kurssiToteutukset.Opettaja);
            return View(kurssiToteutukset);
        }

        // POST: KurssiToteutukset/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KurssitoteutusID,Paivamaara,Opettaja,Luokka,Kurssi,Kellonaika")] KurssiToteutukset kurssiToteutukset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kurssiToteutukset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Kurssi = new SelectList(db.Kurssit, "KurssiId", "Kurssi", kurssiToteutukset.Kurssi);
            ViewBag.Luokka = new SelectList(db.Luokkatilat, "LuokkaID", "Luokka", kurssiToteutukset.Luokka);
            ViewBag.Opettaja = new SelectList(db.Opettajat, "HenkiloID", "Etunimi", "Sukunimi", kurssiToteutukset.Opettaja);
            return View(kurssiToteutukset);
        }

        // GET: KurssiToteutukset/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KurssiToteutukset kurssiToteutukset = db.KurssiToteutukset.Find(id);
            if (kurssiToteutukset == null)
            {
                return HttpNotFound();
            }
            return View(kurssiToteutukset);
        }

        // POST: KurssiToteutukset/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KurssiToteutukset kurssiToteutukset = db.KurssiToteutukset.Find(id);
            db.KurssiToteutukset.Remove(kurssiToteutukset);
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
    }
}
