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
    public class PojazdyController : Controller
    {
        private LogisticDBEntities db = new LogisticDBEntities();
        [Authorize]
        // GET: Pojazdy
        public ActionResult Index()
        {
            var pojazdy = db.Pojazdy.Include(p => p.Naczepy).Include(p => p.Przewoznicy);
            return View(pojazdy.ToList());
        }

        // GET: Pojazdy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pojazdy pojazdy = db.Pojazdy.Find(id);
            if (pojazdy == null)
            {
                return HttpNotFound();
            }
            return View(pojazdy);
        }

        // GET: Pojazdy/Create
        public ActionResult Create()
        {
            ViewBag.IDNaczepy = new SelectList(db.Naczepy, "ID", "NrRejestracyjny");
            ViewBag.IDPrzewoznika = new SelectList(db.Przewoznicy, "ID", "Nazwa");
            return View();
        }

        // POST: Pojazdy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NrRejestracyjny,MasaCalkowita,Stawka,Kierowca,NrKomorkowyKierowcy,IDPrzewoznika,IDNaczepy")] Pojazdy pojazdy)
        {
            if (ModelState.IsValid)
            {
                db.Pojazdy.Add(pojazdy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDNaczepy = new SelectList(db.Naczepy, "ID", "NrRejestracyjny", pojazdy.IDNaczepy);
            ViewBag.IDPrzewoznika = new SelectList(db.Przewoznicy, "ID", "Nazwa", pojazdy.IDPrzewoznika);
            return View(pojazdy);
        }

        // GET: Pojazdy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pojazdy pojazdy = db.Pojazdy.Find(id);
            if (pojazdy == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDNaczepy = new SelectList(db.Naczepy, "ID", "NrRejestracyjny", pojazdy.IDNaczepy);
            ViewBag.IDPrzewoznika = new SelectList(db.Przewoznicy, "ID", "Nazwa", pojazdy.IDPrzewoznika);
            return View(pojazdy);
        }

        // POST: Pojazdy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NrRejestracyjny,MasaCalkowita,Stawka,Kierowca,NrKomorkowyKierowcy,IDPrzewoznika,IDNaczepy")] Pojazdy pojazdy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pojazdy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDNaczepy = new SelectList(db.Naczepy, "ID", "NrRejestracyjny", pojazdy.IDNaczepy);
            ViewBag.IDPrzewoznika = new SelectList(db.Przewoznicy, "ID", "Nazwa", pojazdy.IDPrzewoznika);
            return View(pojazdy);
        }

        // GET: Pojazdy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pojazdy pojazdy = db.Pojazdy.Find(id);
            if (pojazdy == null)
            {
                return HttpNotFound();
            }
            return View(pojazdy);
        }

        // POST: Pojazdy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pojazdy pojazdy = db.Pojazdy.Find(id);
            db.Pojazdy.Remove(pojazdy);
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
