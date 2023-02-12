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
            string connectionString = "DATA SOURCE=localhost:1521/XE;PASSWORD=STUDENT;PERSIST SECURITY INFO=True;USER ID = STUDENT";
            OracleConnection connection = new OracleConnection(connectionString);

            try
            {
                connection.Open();
                ViewBag.Message = "Connection successful!";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Connection failed: " + ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return View();
        }

        public ActionResult AdaugareAngajati()
        {
            ViewBag.Message = "";

            return View();
        }
        public ActionResult GestionareAngajati()
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