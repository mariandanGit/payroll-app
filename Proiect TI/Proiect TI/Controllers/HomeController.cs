using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Proiect_TI.Models;

namespace Proiect_TI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetServerTime()
        {
            var currentTime = DateTime.Now.ToString("HH:mm:ss");
            return Content(currentTime);
        }
        public ActionResult AdaugareAngajati(bool ? success)
        {
            ViewBag.Success = success;
            return View();
        }
        public ActionResult GestionareAngajati(bool? success, string message)
        {
            ViewBag.Success = success;
            ViewBag.ErrorMessage = message;

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
        public ActionResult ModificareProcente(bool? success, string message)
        {
            ViewBag.Success = success;
            ViewBag.ErrorMessage = message;
            Percentages percentages = null;
            try
            {
                string connectionString = "DATA SOURCE=localhost:1521/XE;PASSWORD=STUDENT;PERSIST SECURITY INFO=True;USER ID = STUDENT";
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (OracleCommand command = new OracleCommand("SELECT CAS, CASS, IMPOZIT FROM PROCENTE", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                percentages = new Percentages
                                {
                                    CAS = reader.GetString(reader.GetOrdinal("CAS")).Replace(',', '.'),
                                    CASS = reader.GetString(reader.GetOrdinal("CASS")).Replace(',', '.'),
                                    Impozit = reader.GetString(reader.GetOrdinal("IMPOZIT")).Replace(',', '.')
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }

            return View(percentages);
        }
    }
}