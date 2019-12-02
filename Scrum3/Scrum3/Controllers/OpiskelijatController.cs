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
    public class OpiskelijatController : Controller
    {
        private ScrumEntities db = new ScrumEntities();

        // GET: Opiskelijat
        public ActionResult Index()
        {
            var opiskelijat = db.Opiskelijat.Include(o => o.Logins);
            return View(opiskelijat.ToList());
        }

        // GET: Opiskelijat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opiskelijat opiskelijat = db.Opiskelijat.Find(id);
            if (opiskelijat == null)
            {
                return HttpNotFound();
            }
            return View(opiskelijat);
        }

        // GET: Opiskelijat/Create
        public ActionResult Create()
        {
            ViewBag.LoginId = new SelectList(db.Logins, "LoginId", "UserName");
            return View();
        }

        // POST: Opiskelijat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Opiskelijanumero,Etunimi,Sukunimi,Puhelin,Sahkoposti,LoginId")] Opiskelijat opiskelijat)
        {
            if (ModelState.IsValid)
            {
                db.Opiskelijat.Add(opiskelijat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoginId = new SelectList(db.Logins, "LoginId", "UserName", opiskelijat.LoginId);
            return View(opiskelijat);
        }

        // GET: Opiskelijat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opiskelijat opiskelijat = db.Opiskelijat.Find(id);
            if (opiskelijat == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoginId = new SelectList(db.Logins, "LoginId", "UserName", opiskelijat.LoginId);
            return View(opiskelijat);
        }

        // POST: Opiskelijat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Opiskelijanumero,Etunimi,Sukunimi,Puhelin,Sahkoposti,LoginId")] Opiskelijat opiskelijat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(opiskelijat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoginId = new SelectList(db.Logins, "LoginId", "UserName", opiskelijat.LoginId);
            return View(opiskelijat);
        }

        // GET: Opiskelijat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opiskelijat opiskelijat = db.Opiskelijat.Find(id);
            if (opiskelijat == null)
            {
                return HttpNotFound();
            }
            return View(opiskelijat);
        }

        // POST: Opiskelijat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Opiskelijat opiskelijat = db.Opiskelijat.Find(id);
            db.Opiskelijat.Remove(opiskelijat);
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
