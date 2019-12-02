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
    public class LuokkatilatController : Controller
    {
        private ScrumEntities db = new ScrumEntities();

        // GET: Luokkatilat
        public ActionResult Index()
        {
            return View(db.Luokkatilat.ToList());
        }

        // GET: Luokkatilat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Luokkatilat luokkatilat = db.Luokkatilat.Find(id);
            if (luokkatilat == null)
            {
                return HttpNotFound();
            }
            return View(luokkatilat);
        }

        // GET: Luokkatilat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Luokkatilat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Luokka,LuokkaID")] Luokkatilat luokkatilat)
        {
            if (ModelState.IsValid)
            {
                db.Luokkatilat.Add(luokkatilat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(luokkatilat);
        }

        // GET: Luokkatilat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Luokkatilat luokkatilat = db.Luokkatilat.Find(id);
            if (luokkatilat == null)
            {
                return HttpNotFound();
            }
            return View(luokkatilat);
        }

        // POST: Luokkatilat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Luokka,LuokkaID")] Luokkatilat luokkatilat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(luokkatilat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(luokkatilat);
        }

        // GET: Luokkatilat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Luokkatilat luokkatilat = db.Luokkatilat.Find(id);
            if (luokkatilat == null)
            {
                return HttpNotFound();
            }
            return View(luokkatilat);
        }

        // POST: Luokkatilat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Luokkatilat luokkatilat = db.Luokkatilat.Find(id);
            db.Luokkatilat.Remove(luokkatilat);
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
