using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LogisticsProject.Models;

namespace LogisticsProject.Controllers
{
    public class PrzewoznicyController : Controller
    {
        private LogisticDBEntities db = new LogisticDBEntities();
        [Authorize]
        // GET: Przewoznicy
        public ActionResult Index()
        {
            return View(db.Przewoznicy.ToList());
        }

        // GET: Przewoznicy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przewoznicy przewoznicy = db.Przewoznicy.Find(id);
            if (przewoznicy == null)
            {
                return HttpNotFound();
            }
            return View(przewoznicy);
        }

        // GET: Przewoznicy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Przewoznicy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nazwa,AdresKontaktowy,Wlasciciel,NrKomorkowy,NrKonta")] Przewoznicy przewoznicy)
        {
            if (ModelState.IsValid)
            {
                db.Przewoznicy.Add(przewoznicy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(przewoznicy);
        }

        // GET: Przewoznicy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przewoznicy przewoznicy = db.Przewoznicy.Find(id);
            if (przewoznicy == null)
            {
                return HttpNotFound();
            }
            return View(przewoznicy);
        }

        // POST: Przewoznicy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nazwa,AdresKontaktowy,Wlasciciel,NrKomorkowy,NrKonta")] Przewoznicy przewoznicy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(przewoznicy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(przewoznicy);
        }

        // GET: Przewoznicy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Przewoznicy przewoznicy = db.Przewoznicy.Find(id);
            if (przewoznicy == null)
            {
                return HttpNotFound();
            }
            return View(przewoznicy);
        }

        // POST: Przewoznicy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Przewoznicy przewoznicy = db.Przewoznicy.Find(id);
            db.Przewoznicy.Remove(przewoznicy);
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
