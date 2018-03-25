using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LogisticsProject.Models;
using Newtonsoft.Json.Linq;

namespace LogisticsProject.Controllers
{
    public class TrasyController : Controller
    {
        private LogisticDBEntities db = new LogisticDBEntities();
        [Authorize]
        // GET: Trasy
        public ActionResult Index()
        {
            var trasy = db.Trasy.Include(t => t.CentraLogistyczne).Include(t => t.CentraLogistyczne1);
            return View(trasy.ToList());
        }

        // GET: Trasy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trasy trasy = db.Trasy.Find(id);
            if (trasy == null)
            {
                return HttpNotFound();
            }
            return View(trasy);
        }

        // GET: Trasy/Create
        public ActionResult Create()
        {
            ViewBag.IDCentrum1 = new SelectList(db.CentraLogistyczne, "ID", "Kod");
            ViewBag.IDCentrum2 = new SelectList(db.CentraLogistyczne, "ID", "Kod");
            return View();
        }

        // POST: Trasy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Dlugosc,OplatyDodatkowe,IDCentrum1,IDCentrum2")] Trasy trasy)
        {
            if (ModelState.IsValid)
            {
                if (trasy != null)
                {
                    string a = null, b = null;
                    SqlConnection sqlConnection1 = new SqlConnection("Server=SPIERDOLENIE;Database=LogisticDB;Trusted_Connection=True; ");
                    
                    using (sqlConnection1)
                    {
                        SqlCommand command = new SqlCommand(
                          "SELECT Miasto, Ulica, Numer FROM CentraLogistyczne WHERE ID = " + trasy.IDCentrum1 + ";",
                          sqlConnection1);
                        sqlConnection1.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                a= reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetInt32(2);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No rows found.");
                        }
                        reader.Close();

                        SqlCommand command1 = new SqlCommand(
                          "SELECT Miasto, Ulica, Numer FROM CentraLogistyczne WHERE ID = " + trasy.IDCentrum2 + ";",
                          sqlConnection1);

                        SqlDataReader reader1 = command1.ExecuteReader();

                        if (reader1.HasRows)
                        {
                            while (reader1.Read())
                            {
                                b = reader1.GetString(0) + " " + reader1.GetString(1) + " " + reader1.GetInt32(2);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No rows found.");
                        }
                        reader1.Close();
                        sqlConnection1.Close();
                        trasy.Dlugosc = getDistance(a, b)/1000;
                    }
                    //SqlCommand cmd = new SqlCommand();
                    //SqlCommand cmd1 = new SqlCommand();
                    //SqlDataReader reader;
                    //SqlDataReader reader1;
                    //cmd.CommandText = "SELECT * FROM Trasy" ;
                    //cmd.CommandType = CommandType.Text;
                    //cmd.Connection = sqlConnection1;

                    //sqlConnection1.Open();

                    //reader = cmd.ExecuteReader();
                    //// Data is accessible through the DataReader object here.
                    //string a = reader.GetString(0);
                    //sqlConnection1.Close();
                    //cmd1.CommandText = "SELECT Miasto FROM CentraLogistyczne WHERE ID = '2'";
                    //cmd1.CommandType = CommandType.Text;
                    //cmd1.Connection = sqlConnection1;

                    //sqlConnection1.Open();

                    //reader1 = cmd1.ExecuteReader();
                    //// Data is accessible through the DataReader object here.
                    //string b = reader1.GetString(0);
                    //sqlConnection1.Close();


                    //string centrum1 = trasy.CentraLogistyczne.Miasto + trasy.CentraLogistyczne.Ulica +
                    //                  trasy.CentraLogistyczne.Numer;
                    //string centrum2 = trasy.CentraLogistyczne1.Miasto + trasy.CentraLogistyczne1.Ulica +
                    //                  trasy.CentraLogistyczne1.Numer;
                    
                }
                db.Trasy.Add(trasy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDCentrum1 = new SelectList(db.CentraLogistyczne, "ID", "Kod", trasy.IDCentrum1);
            ViewBag.IDCentrum2 = new SelectList(db.CentraLogistyczne, "ID", "Kod", trasy.IDCentrum2);
            return View(trasy);
        }
        public int getDistance(string origin, string destination)
        {
            System.Threading.Thread.Sleep(1000);
            int distance = 0;
            //string from = origin.Text;
            //string to = destination.Text;
            string url = "http://maps.googleapis.com/maps/api/directions/json?origin=" + origin + "&destination=" + destination + "&sensor=false";
            string requesturl = url;
            //string requesturl = @"http://maps.googleapis.com/maps/api/directions/json?origin=" + from + "&alternatives=false&units=imperial&destination=" + to + "&sensor=false";
            string content = fileGetContents(requesturl);
            JObject o = JObject.Parse(content);
            try
            {
                distance = (int)o.SelectToken("routes[0].legs[0].distance.value");
                return distance;
            }
            catch
            {
                return distance;
            }
            return distance;
            //ResultingDistance.Text = distance;
        }

        protected string fileGetContents(string fileName)
        {
            string sContents = string.Empty;
            string me = string.Empty;
            try
            {
                if (fileName.ToLower().IndexOf("http:") > -1)
                {
                    System.Net.WebClient wc = new System.Net.WebClient();
                    byte[] response = wc.DownloadData(fileName);
                    sContents = System.Text.Encoding.ASCII.GetString(response);

                }
                else
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(fileName);
                    sContents = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch { sContents = "unable to connect to server "; }
            return sContents;
        }
        // GET: Trasy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trasy trasy = db.Trasy.Find(id);
            if (trasy == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCentrum1 = new SelectList(db.CentraLogistyczne, "ID", "Kod", trasy.IDCentrum1);
            ViewBag.IDCentrum2 = new SelectList(db.CentraLogistyczne, "ID", "Kod", trasy.IDCentrum2);
            return View(trasy);
        }

        // POST: Trasy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Dlugosc,OplatyDodatkowe,IDCentrum1,IDCentrum2")] Trasy trasy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trasy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCentrum1 = new SelectList(db.CentraLogistyczne, "ID", "Kod", trasy.IDCentrum1);
            ViewBag.IDCentrum2 = new SelectList(db.CentraLogistyczne, "ID", "Kod", trasy.IDCentrum2);
            return View(trasy);
        }

        // GET: Trasy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trasy trasy = db.Trasy.Find(id);
            if (trasy == null)
            {
                return HttpNotFound();
            }
            return View(trasy);
        }

        // POST: Trasy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trasy trasy = db.Trasy.Find(id);
            db.Trasy.Remove(trasy);
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
