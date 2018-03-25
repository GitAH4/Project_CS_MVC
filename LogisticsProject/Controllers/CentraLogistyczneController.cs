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
    public class CentraLogistyczneController : Controller
    {
        private LogisticDBEntities db = new LogisticDBEntities();
        [Authorize]
        // GET: CentraLogistyczne
        public ActionResult Index()
        {
            return View(db.CentraLogistyczne.ToList());
        }

        // GET: CentraLogistyczne/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CentraLogistyczne centraLogistyczne = db.CentraLogistyczne.Find(id);
            if (centraLogistyczne == null)
            {
                return HttpNotFound();
            }
            return View(centraLogistyczne);
        }

        // GET: CentraLogistyczne/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CentraLogistyczne/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Kod,Miasto,Ulica,Numer,KodPocztowy")] CentraLogistyczne centraLogistyczne)
        {
            if (ModelState.IsValid)
            {
                db.CentraLogistyczne.Add(centraLogistyczne);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(centraLogistyczne);
        }

        // GET: CentraLogistyczne/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CentraLogistyczne centraLogistyczne = db.CentraLogistyczne.Find(id);
            if (centraLogistyczne == null)
            {
                return HttpNotFound();
            }
            return View(centraLogistyczne);
        }

        // POST: CentraLogistyczne/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Kod,Miasto,Ulica,Numer,KodPocztowy")] CentraLogistyczne centraLogistyczne)
        {
            if (ModelState.IsValid)
            {
                db.Entry(centraLogistyczne).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(centraLogistyczne);
        }

        // GET: CentraLogistyczne/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CentraLogistyczne centraLogistyczne = db.CentraLogistyczne.Find(id);
            if (centraLogistyczne == null)
            {
                return HttpNotFound();
            }
            return View(centraLogistyczne);
        }

        // POST: CentraLogistyczne/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CentraLogistyczne centraLogistyczne = db.CentraLogistyczne.Find(id);
            db.CentraLogistyczne.Remove(centraLogistyczne);
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
