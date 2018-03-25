using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LogisticsProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Opis naszej aplikacji.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Kontakt z twórcami";

            return View();
        }
    }
}