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
    public class NaczepyController : Controller
    {
        private LogisticDBEntities db = new LogisticDBEntities();
        [Authorize]
        // GET: Naczepy
        public ActionResult Index()
        {
            return View(db.Naczepy.ToList());
        }

        // GET: Naczepy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Naczepy naczepy = db.Naczepy.Find(id);
            if (naczepy == null)
            {
                return HttpNotFound();
            }
            return View(naczepy);
        }

        // GET: Naczepy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Naczepy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NrRejestracyjny,Ladownosc,Dlugosc,Szerokosc,Wysokosc,Chlodnia")] Naczepy naczepy)
        {
            if (ModelState.IsValid)
            {
                db.Naczepy.Add(naczepy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(naczepy);
        }

        // GET: Naczepy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Naczepy naczepy = db.Naczepy.Find(id);
            if (naczepy == null)
            {
                return HttpNotFound();
            }
            return View(naczepy);
        }

        // POST: Naczepy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NrRejestracyjny,Ladownosc,Dlugosc,Szerokosc,Wysokosc,Chlodnia")] Naczepy naczepy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(naczepy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(naczepy);
        }

        // GET: Naczepy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Naczepy naczepy = db.Naczepy.Find(id);
            if (naczepy == null)
            {
                return HttpNotFound();
            }
            return View(naczepy);
        }

        // POST: Naczepy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Naczepy naczepy = db.Naczepy.Find(id);
            db.Naczepy.Remove(naczepy);
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
