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
    public class OsallistumisetController : Controller
    {
        private ScrumEntities1 db = new ScrumEntities1();

        // GET: Osallistumiset
        public ActionResult Index()
        {
            int oppi = (int)Session["LoginId"];
            if ((Session["UserName"] == null) || (Session["AccessLevel"].ToString() != "3"))
                {
                var osallistumiset = db.Osallistumiset.Include(o => o.KurssiToteutukset).Include(o => o.Opiskelijat);
                return View(osallistumiset.ToList()); 
            } else
            {
                var osallistumiset = db.Osallistumiset.Include(o => o.KurssiToteutukset).Include(o => o.Opiskelijat).Where(o => o.OppilasID == oppi);
                return View(osallistumiset.ToList());
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
            ViewBag.KurssitoteutusID = new SelectList(db.KurssiToteutukset, "KurssitoteutusID", "KurssitoteutusID");
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
    }
}
