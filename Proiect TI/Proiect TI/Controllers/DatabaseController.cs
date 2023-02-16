using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Proiect_TI.Models;
using Oracle.ManagedDataAccess.Client;
using System.IO;

namespace Proiect_TI.Controllers
{
    public class DatabaseController : Controller
    {
        public ActionResult GetEmployees()
        {
            var employees = new List<EmployeeViewModelGet>();
            string connectionString = "DATA SOURCE=localhost:1521/XE;PASSWORD=STUDENT;PERSIST SECURITY INFO=True;USER ID=STUDENT";

            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                using (var command = new OracleCommand("SELECT ID, NUME, PRENUME, FUNCTIE, SALAR_BAZA, SPOR, PREMII_BRUTE, TOTAL_BRUT, BRUT_IMPOZABIL, IMPOZIT, CAS, CASS, RETINERI, VIRAT_CARD, POZA FROM SALARIATI", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var employee = new EmployeeViewModelGet
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
                                Poza = reader.IsDBNull(reader.GetOrdinal("POZA")) ? null : (byte[])reader["POZA"]
                            };
                            employees.Add(employee);
                        }
                    }
                }
            }
            return Json(employees, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddEmployees(EmployeeViewModelSet employee)
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
                        if (employee.Poza != null && employee.Poza.ContentLength > 0)
                        {
                            byte[] bytes;
                            using (BinaryReader br = new BinaryReader(employee.Poza.InputStream))
                            {
                                bytes = br.ReadBytes(employee.Poza.ContentLength);
                            }
                            command.Parameters.Add(new OracleParameter("POZA", OracleDbType.Blob)).Value = bytes;
                        }
                        else
                        {
                            command.Parameters.Add(new OracleParameter("POZA", OracleDbType.Blob)).Value = null;
                        }
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
        [HttpPost]
        public ActionResult UpdateData(EmployeeViewModelSet employee)
        {
            try
            {
                string connectionString = "DATA SOURCE=localhost:1521/XE;PASSWORD=STUDENT;PERSIST SECURITY INFO=True;USER ID = STUDENT";
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (OracleCommand command = new OracleCommand("UPDATE SALARIATI SET NUME = :Nume, PRENUME = :Prenume, FUNCTIE = :Functie, SALAR_BAZA = :SalarBaza, SPOR = :Spor, PREMII_BRUTE = :PremiiBrute, RETINERI = :Retineri WHERE ID = :Id", connection))
                    {
                        command.Parameters.Add("Nume", OracleDbType.Varchar2).Value = employee.Nume;
                        command.Parameters.Add("Prenume", OracleDbType.Varchar2).Value = employee.Prenume;
                        command.Parameters.Add("Functie", OracleDbType.Varchar2).Value = employee.Functie;
                        command.Parameters.Add("SalarBaza", OracleDbType.Decimal).Value = employee.SalarBaza;
                        command.Parameters.Add("Spor", OracleDbType.Decimal).Value = employee.Spor;
                        command.Parameters.Add("PremiiBrute", OracleDbType.Decimal).Value = employee.PremiiBrute;
                        command.Parameters.Add("Retineri", OracleDbType.Decimal).Value = employee.Retineri;
                        command.Parameters.Add("Id", OracleDbType.Int32).Value = employee.Id;

                        command.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("GestionareAngajati", "Home", new { success = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("GestionareAngajati", "Home", new { success = false });
            }
        }
        [HttpPost]
        public ActionResult UpdatePercentages(Percentages percentages)
        {
            try
            {
                string connectionString = "DATA SOURCE=localhost:1521/XE;PASSWORD=STUDENT;PERSIST SECURITY INFO=True;USER ID = STUDENT";
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (OracleCommand command = new OracleCommand("SELECT PAROLA FROM PROCENTE WHERE PAROLA = :Parola", connection))
                    {
                        command.Parameters.Add("Parola", OracleDbType.Varchar2).Value = percentages.Parola;

                        object password = command.ExecuteScalar();
                        if (password != null && password != DBNull.Value)
                        {
                            var query = "UPDATE PROCENTE SET CAS = " + percentages.CAS + ", CASS = " + percentages.CASS + ", Impozit = " + percentages.Impozit + " WHERE PAROLA = '" + percentages.Parola + "'";

                            using (var updatePercentagesCommand = new OracleCommand(query, connection))
                            {
                                updatePercentagesCommand.ExecuteNonQuery();
                            }

                            using (OracleCommand updateSalariatiCommand = new OracleCommand("UPDATE SALARIATI SET NUME = NUME", connection))
                            {
                                updateSalariatiCommand.ExecuteNonQuery();
                            }

                            return RedirectToAction("ModificareProcente", "Home", new { success = true });
                        }
                        else
                        {
                            return RedirectToAction("ModificareProcente", "Home", new { success = false, message = "Parola invalida" });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ModificareProcente", "Home", new { success = false, message = ex.Message });
            }
        }       
        [HttpPost]
        public ActionResult DeleteEmployee(int id)
        {
            try
            {
                string connectionString = "DATA SOURCE=localhost:1521/XE;PASSWORD=STUDENT;PERSIST SECURITY INFO=True;USER ID = STUDENT";
                using (var connection = new OracleConnection(connectionString))
                {
                    connection.Open();
                    using (OracleCommand command = new OracleCommand("DELETE FROM SALARIATI WHERE ID = :id", connection))
                    {
                        command.Parameters.Add(":id", id);
                        command.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("GestionareAngajati", "Home", new { success = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("GestionareAngajati", "Home", new { success = false });
            }
        }
    }
}