using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace Proiect_TI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult AdaugareAngajati(bool ? success)
        {
            ViewBag.Success = success;
            return View();
        }
        public ActionResult GestionareAngajati(bool? success)
        {
            ViewBag.Success = success;

            return View();
        }
        public ActionResult StatPlata()
        {

            return View();
        }
        public ActionResult Fluturasi()
        {

            return View();
        }
        public ActionResult ModificareProcente()
        {

            return View();
        }
    }
}