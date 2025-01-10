using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallengeApplication
{
    public class OrderBusiness
    {
        public List<OrderResult> ProcessarPedidos(List<OrderEntity> pedidos)
        {
            var resultados = new List<OrderResult>();
            var dataLimite = DateTime.Now.AddDays(-90);

            var agrupadosPorCliente = pedidos.GroupBy(p => p.ClienteId);

            foreach (var grupo in agrupadosPorCliente)
            {
                var clientePedidos = grupo.OrderBy(p => p.DataProcedimento).ToList();
                decimal total30Dias = 0;
                int count30Dias = 0;

                foreach (var pedido in clientePedidos)
                {
                    var status = "Aprovado";
                    var valorReembolsado = CalcularReembolso(pedido.TipoProcedimento, pedido.ValorPago);

                   if (pedido.DataProcedimento < dataLimite)
                    {
                        status = "Rejeitado";
                    }

                    var inicioPeriodo = pedido.DataProcedimento.AddDays(-30);
                    count30Dias = clientePedidos.Count(p => p.DataProcedimento >= inicioPeriodo && p.DataProcedimento <= pedido.DataProcedimento);
                    if (count30Dias > 5)
                    {
                        status = "Suspeito de Fraude";
                    }

                    total30Dias = clientePedidos
                        .Where(p => p.DataProcedimento >= inicioPeriodo && p.DataProcedimento <= pedido.DataProcedimento)
                        .Sum(p => CalcularReembolso(p.TipoProcedimento, p.ValorPago));

                    if (total30Dias > 2000)
                    {
                        status = "Suspeito de Fraude";
                    }

                    resultados.Add(new OrderResult
                    {
                        Id = pedido.Id,
                        TipoProcedimento = pedido.TipoProcedimento,
                        DataProcedimento = pedido.DataProcedimento,
                        ValorPago = pedido.ValorPago,
                        ValorReembolsado = status == "Aprovado" ? valorReembolsado : 0,
                        ClienteId = pedido.ClienteId,
                        Status = status
                    });
                }
            }

            return resultados;
        }

        private decimal CalcularReembolso(string tipo, decimal valorPago)
        {
            decimal porcentagem = tipo switch
            {
                "Consulta Médica" => 0.8m,
                "Exame de Imagem" => 0.9m,
                "Exame Laboratorial" => 0.7m,
                _ => 0.5m
            };

            return Math.Min(valorPago * porcentagem, 300);
        }
    }
}