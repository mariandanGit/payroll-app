using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Proiect_TI.Models;
using Oracle.ManagedDataAccess.Client;

namespace Proiect_TI.Controllers
{
    public class DatabaseController : Controller
    {
        public ActionResult GetEmployees(EmployeeViewModel Employee)
        {
            var employees = new List<EmployeeViewModel>();
            string connectionString = "DATA SOURCE=localhost:1521/XE;PASSWORD=STUDENT;PERSIST SECURITY INFO=True;USER ID = STUDENT";
            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                using (var command = new OracleCommand("SELECT * FROM SALARIATI", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var employee = new EmployeeViewModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                                Nume = reader.GetString(reader.GetOrdinal("NUME")),
                                Prenume = reader.GetString(reader.GetOrdinal("PRENUME")),
                                Functie = reader.GetString(reader.GetOrdinal("FUNCTIE")),
                                SalarBaza = reader.GetDecimal(reader.GetOrdinal("SALAR_BAZA")),
                                Spor = reader.GetDecimal(reader.GetOrdinal("SPOR")),
                                PremiiBrute = reader.GetDecimal(reader.GetOrdinal("PREMII_BRUTE")),
                                TotalBrut = reader.GetDecimal(reader.GetOrdinal("TOTAL_BRUT")),
                                BrutImpozabil = reader.GetDecimal(reader.GetOrdinal("BRUT_IMPOZABIL")),
                                Impozit = reader.GetDecimal(reader.GetOrdinal("IMPOZIT")),
                                Cas = reader.GetDecimal(reader.GetOrdinal("CAS")),
                                Cass = reader.GetDecimal(reader.GetOrdinal("CASS")),
                                Retineri = reader.GetDecimal(reader.GetOrdinal("RETINERI")),
                                ViratCard = reader.GetDecimal(reader.GetOrdinal("VIRAT_CARD")),
                                Poza = reader.IsDBNull(reader.GetOrdinal("POZA")) ? null : Convert.ToBase64String((byte[])reader.GetValue(reader.GetOrdinal("POZA")))
                            };
                            employees.Add(employee);
                        }
                    }
                }
            }
            return Json(employees, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddEmployees(EmployeeViewModel employee)
        {
            try
            {
                string connectionString = "DATA SOURCE=localhost:1521/XE;PASSWORD=STUDENT;PERSIST SECURITY INFO=True;USER ID = STUDENT";
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (OracleCommand command = new OracleCommand("INSERT INTO SALARIATI (NUME, PRENUME, FUNCTIE, SALAR_BAZA, SPOR, PREMII_BRUTE, RETINERI, POZA) VALUES (:Nume, :Prenume, :Functie, :SalarBaza, :Spor, :PremiiBrute, :Retineri, :Poza)", connection))
                    {
                        command.Parameters.Add("Nume", OracleDbType.Varchar2).Value = employee.Nume;
                        command.Parameters.Add("Prenume", OracleDbType.Varchar2).Value = employee.Prenume;
                        command.Parameters.Add("Functie", OracleDbType.Varchar2).Value = employee.Functie;
                        command.Parameters.Add("SalarBaza", OracleDbType.Decimal).Value = employee.SalarBaza;
                        command.Parameters.Add("Spor", OracleDbType.Decimal).Value = employee.Spor;
                        command.Parameters.Add("PremiiBrute", OracleDbType.Decimal).Value = employee.PremiiBrute;
                        command.Parameters.Add("Retineri", OracleDbType.Decimal).Value = employee.Retineri;
                        if (employee.Poza != null)
                        {
                            byte[] imageBytes = Convert.FromBase64String(employee.Poza);
                            command.Parameters.Add("Poza", OracleDbType.Blob).Value = imageBytes;
                        }
                        else command.Parameters.Add("Poza", OracleDbType.Varchar2).Value = employee.Poza; 
                        command.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("AdaugareAngajati", "Home", new { success = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("AdaugareAngajati", "Home", new { success = false });
            }
        }
    }
}