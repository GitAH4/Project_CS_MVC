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
    public class ZleceniaController : Controller
    {
        private LogisticDBEntities db = new LogisticDBEntities();
        [Authorize]
        // GET: Zlecenia
        public ActionResult Index()
        {
            var zlecenia = db.Zlecenia.Include(z => z.Ladunki).Include(z => z.Pojazdy).Include(z => z.Trasy);
            return View(zlecenia.ToList());
        }

        // GET: Zlecenia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zlecenia zlecenia = db.Zlecenia.Find(id);
            if (zlecenia == null)
            {
                return HttpNotFound();
            }
            return View(zlecenia);
        }




        // GET: Zlecenia/Create
        public ActionResult Create(DateTime dataZaladunku = default(DateTime), DateTime dataRozladunku = default(DateTime))
        {
            Zlecenia parametrZlecenia = new Zlecenia();
            parametrZlecenia.DataRozladunku = dataRozladunku;
            parametrZlecenia.DataZaladunku = dataZaladunku;
            ViewBag.IDLadunku = new SelectList(db.Ladunki, "ID", "ID");
            ViewBag.IDPojazdu = new SelectList(db.Pojazdy, "ID", "NrRejestracyjny");
            ViewBag.IDTrasy = new SelectList(db.Trasy, "ID", "ID");
            return View(parametrZlecenia);
        }

        // POST: Zlecenia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DataZaladunku,DataRozladunku,IDLadunku,IDPojazdu,IDTrasy")] Zlecenia zlecenia)
        {
            if (ModelState.IsValid)
            {
                db.Zlecenia.Add(zlecenia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDLadunku = new SelectList(db.Ladunki, "ID", "ID", zlecenia.IDLadunku);
            ViewBag.IDPojazdu = new SelectList(db.Pojazdy, "ID", "NrRejestracyjny", zlecenia.IDPojazdu);
            ViewBag.IDTrasy = new SelectList(db.Trasy, "ID", "ID", zlecenia.IDTrasy);
            return View(zlecenia);
        }

        // GET: Zlecenia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zlecenia zlecenia = db.Zlecenia.Find(id);
            if (zlecenia == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDLadunku = new SelectList(db.Ladunki, "ID", "ID", zlecenia.IDLadunku);
            ViewBag.IDPojazdu = new SelectList(db.Pojazdy, "ID", "NrRejestracyjny", zlecenia.IDPojazdu);
            ViewBag.IDTrasy = new SelectList(db.Trasy, "ID", "ID", zlecenia.IDTrasy);
            return View(zlecenia);
        }

        // POST: Zlecenia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DataZaladunku,DataRozladunku,IDLadunku,IDPojazdu,IDTrasy")] Zlecenia zlecenia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zlecenia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDLadunku = new SelectList(db.Ladunki, "ID", "ID", zlecenia.IDLadunku);
            ViewBag.IDPojazdu = new SelectList(db.Pojazdy, "ID", "NrRejestracyjny", zlecenia.IDPojazdu);
            ViewBag.IDTrasy = new SelectList(db.Trasy, "ID", "ID", zlecenia.IDTrasy);
            return View(zlecenia);
        }

        // GET: Zlecenia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zlecenia zlecenia = db.Zlecenia.Find(id);
            if (zlecenia == null)
            {
                return HttpNotFound();
            }
            return View(zlecenia);
        }

        // POST: Zlecenia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zlecenia zlecenia = db.Zlecenia.Find(id);
            db.Zlecenia.Remove(zlecenia);
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
