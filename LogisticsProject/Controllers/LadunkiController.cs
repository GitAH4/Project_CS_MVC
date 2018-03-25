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
    public class LadunkiController : Controller
    {
        private LogisticDBEntities db = new LogisticDBEntities();
        [Authorize]
        // GET: Ladunki
        public ActionResult Index()
        {
            var ladunki = db.Ladunki.Include(l => l.Klienci);
            return View(ladunki.ToList());
        }

        // GET: Ladunki/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ladunki ladunki = db.Ladunki.Find(id);
            if (ladunki == null)
            {
                return HttpNotFound();
            }
            return View(ladunki);
        }

        // GET: Ladunki/Create
        public ActionResult Create(int masa = 0, float dlugosc = 0, float szerokosc = 0, float wysokosc = 0, bool chlodnia = false)
        {
            Ladunki parameter = new Ladunki();
            parameter.Masa = masa;
            parameter.Dlugosc = dlugosc;
            parameter.Szerokosc = szerokosc;
            parameter.Wysokosc = wysokosc;
            parameter.WymaganaChlodnia = chlodnia;
            ViewBag.IDKlienta = new SelectList(db.Klienci, "ID", "Nazwa");
            return View(parameter);
        }

        // POST: Ladunki/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Masa,Dlugosc,Szerokosc,Wysokosc,WymaganaChlodnia,IDKlienta")] Ladunki ladunki)
        {
            if (ModelState.IsValid)
            {
                db.Ladunki.Add(ladunki);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDKlienta = new SelectList(db.Klienci, "ID", "Nazwa", ladunki.IDKlienta);
            return View(ladunki);
        }

        // GET: Ladunki/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ladunki ladunki = db.Ladunki.Find(id);
            if (ladunki == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDKlienta = new SelectList(db.Klienci, "ID", "Nazwa", ladunki.IDKlienta);
            return View(ladunki);
        }

        // POST: Ladunki/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Masa,Dlugosc,Szerokosc,Wysokosc,WymaganaChlodnia,IDKlienta")] Ladunki ladunki)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ladunki).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDKlienta = new SelectList(db.Klienci, "ID", "Nazwa", ladunki.IDKlienta);
            return View(ladunki);
        }

        // GET: Ladunki/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ladunki ladunki = db.Ladunki.Find(id);
            if (ladunki == null)
            {
                return HttpNotFound();
            }
            return View(ladunki);
        }

        // POST: Ladunki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ladunki ladunki = db.Ladunki.Find(id);
            db.Ladunki.Remove(ladunki);
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
