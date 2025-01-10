using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallengeApplication
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public string TipoProcedimento { get; set; }
        public DateTime DataProcedimento { get; set; }
        public decimal ValorPago { get; set; }
        public int ClienteId { get; set; }
    }
}