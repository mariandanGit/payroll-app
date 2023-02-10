using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proiect_TI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ActualizareDate()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult AdaugareAngajati()
        {
            ViewBag.Message = "";

            return View();
        }
        public ActionResult StergereAngajati()
        {
            ViewBag.Message = "";

            return View();
        }
        public ActionResult CalculSalarii()
        {
            ViewBag.Message = "";

            return View();
        }
        public ActionResult StatPlata()
        {
            ViewBag.Message = "";

            return View();
        }
        public ActionResult Fluturasi()
        {
            ViewBag.Message = "";

            return View();
        }
        public ActionResult ModificareProcente()
        {
            ViewBag.Message = "";

            return View();
        }
    }
}