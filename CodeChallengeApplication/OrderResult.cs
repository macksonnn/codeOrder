using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallengeApplication
{
    public class OrderResult
    {
        public int Id { get; set; }
        public string TipoProcedimento { get; set; }
        public DateTime DataProcedimento { get; set; }
        public decimal ValorPago { get; set; }
        public decimal ValorReembolsado { get; set; }
        public int ClienteId { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return $"{Id},{TipoProcedimento},{DataProcedimento:yyyy-MM-dd},{ValorPago:F2},{ValorReembolsado:F2},{ClienteId},{Status}";
        }

    }
}