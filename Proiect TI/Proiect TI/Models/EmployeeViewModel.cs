using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proiect_TI.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Functie { get; set; }
        public decimal SalarBaza { get; set; }
        public decimal Spor { get; set; }
        public decimal PremiiBrute { get; set; }
        public decimal TotalBrut { get; set; }
        public decimal BrutImpozabil { get; set; }
        public decimal Impozit { get; set; }
        public decimal Cas { get; set; }
        public decimal Cass { get; set; }
        public decimal Retineri { get; set; }
        public decimal ViratCard { get; set; }
        public string Poza { get; set; }
    }
}