using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Proiect_TI.Models;
using CrystalDecisions.CrystalReports.Engine;

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
        public ActionResult StatDePlataPDF()
        {
            ReportDocument report = new ReportDocument();
            string connectionString = "DATA SOURCE=localhost:1521/XE;PASSWORD=STUDENT;PERSIST SECURITY INFO=True;USER ID=STUDENT";

            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                using (var command = new OracleCommand("SELECT ID, NUME, PRENUME, FUNCTIE, SALAR_BAZA, SPOR, PREMII_BRUTE, TOTAL_BRUT, BRUT_IMPOZABIL, IMPOZIT, CAS, CASS, RETINERI, VIRAT_CARD FROM SALARIATI", connection))
                {
                    using (var adapter = new OracleDataAdapter(command))
                    {
                        var dataSet = new DataSet();
                        adapter.Fill(dataSet);
                        report.Load(Server.MapPath("~/CrystalReports/StatDePlata.rpt"));
                        report.SetDataSource(dataSet.Tables[0]);
                    }
                }
            }

            var stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            var fileName = string.Format("Stat de plata {0}.pdf", DateTime.Now.ToString("dd-MM-yyyy"));
            return new FileStreamResult(stream, "application/pdf")
            {
                FileDownloadName = fileName
            };
        }
        public ActionResult StatDePlataViewer()
        {
            ReportDocument report = new ReportDocument();
            string connectionString = "DATA SOURCE=localhost:1521/XE;PASSWORD=STUDENT;PERSIST SECURITY INFO=True;USER ID=STUDENT";

            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                using (var command = new OracleCommand("SELECT ID, NUME, PRENUME, FUNCTIE, SALAR_BAZA, SPOR, PREMII_BRUTE, TOTAL_BRUT, BRUT_IMPOZABIL, IMPOZIT, CAS, CASS, RETINERI, VIRAT_CARD FROM SALARIATI", connection))
                {
                    using (var adapter = new OracleDataAdapter(command))
                    {
                        var dataSet = new DataSet();
                        adapter.Fill(dataSet);
                        report.Load(Server.MapPath("~/CrystalReports/StatDePlata.rpt"));
                        report.SetDataSource(dataSet.Tables[0]);
                    }
                }
            }

            var stream = report.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            var byteArray = new byte[stream.Length];
            stream.Read(byteArray, 0, (int)stream.Length);
            return new FileContentResult(byteArray, "application/pdf");
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