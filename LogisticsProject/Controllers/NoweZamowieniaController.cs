using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LogisticsProject.Controllers;

namespace LogisticsProject.Models
{
    public class NoweZamowieniaController : Controller
    {
        private LogisticDBEntities db = new LogisticDBEntities();
        [Authorize]
        // GET: NoweZamowienia
        public ActionResult Index()
        {
            return View(db.NoweZamowienia.ToList());
        }
        [Authorize]
        // GET: NoweZamowienia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoweZamowienia noweZamowienia = db.NoweZamowienia.Find(id);
            parametrydodetali param = new parametrydodetali();
            if (noweZamowienia == null)
            {
                return HttpNotFound();
            }
            string nowy = noweZamowienia.AdresKontaktowy;
            return View(noweZamowienia);
        }

        // GET: NoweZamowienia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NoweZamowienia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NazwaFirmy,AdresKontaktowy,Wlasciciel,Email,MasaLadunku,DlugoscLadunku,SzerokoscLadunku,WysokoscLadunku,WymaganaChlodnia,DataZaladunku,DataRozladunku,MiejsceZaladunku,MiejsceRozladunku")] NoweZamowienia noweZamowienia)
        {
            if (ModelState.IsValid)
            {
                db.NoweZamowienia.Add(noweZamowienia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(noweZamowienia);
        }
        [Authorize]
        // GET: NoweZamowienia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoweZamowienia noweZamowienia = db.NoweZamowienia.Find(id);
            if (noweZamowienia == null)
            {
                return HttpNotFound();
            }
            return View(noweZamowienia);
        }
        [Authorize]
        // POST: NoweZamowienia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NazwaFirmy,AdresKontaktowy,Wlasciciel,Email,MasaLadunku,DlugoscLadunku,SzerokoscLadunku,WysokoscLadunku,WymaganaChlodnia,DataZaladunku,DataRozladunku,MiejsceZaladunku,MiejsceRozladunku")] NoweZamowienia noweZamowienia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(noweZamowienia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(noweZamowienia);
        }
        [Authorize]
        // GET: NoweZamowienia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoweZamowienia noweZamowienia = db.NoweZamowienia.Find(id);
            if (noweZamowienia == null)
            {
                return HttpNotFound();
            }
            return View(noweZamowienia);
        }
        [Authorize]
        // GET: NoweZamowienia/Delete/5
        public ActionResult Delete2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NoweZamowienia noweZamowienia = db.NoweZamowienia.Find(id);
            if (noweZamowienia == null)
            {
                return HttpNotFound();
            }
            return View(noweZamowienia);
        }
        [Authorize]
        // POST: NoweZamowienia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            NoweZamowienia noweZamowienia = db.NoweZamowienia.Find(id);
            Mail mail = new Mail(noweZamowienia.NazwaFirmy, noweZamowienia.Email);
            mail.SendConfirmationMail();
            db.NoweZamowienia.Remove(noweZamowienia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpPost, ActionName("Delete2")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed2(int id)
        {

            NoweZamowienia noweZamowienia = db.NoweZamowienia.Find(id);
            Mail mail = new Mail(noweZamowienia.NazwaFirmy, noweZamowienia.Email);
            mail.SendDeclineMail();
            db.NoweZamowienia.Remove(noweZamowienia);
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
