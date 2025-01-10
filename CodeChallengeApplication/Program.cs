using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CodeChallengeApplication;

class Program
{
    static void Main(string[] args)
    {
        var pedidos = new List<OrderEntity>();
        var linhas = Console.In.ReadToEnd().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        foreach (var linha in linhas.Skip(1)) // Ignorar cabeçalho
        {
            var campos = linha.Split(',');
            if (campos.Length != 5) continue;

            pedidos.Add(new OrderEntity
            {
                Id = int.Parse(campos[0]),
                TipoProcedimento = campos[1],
                DataProcedimento = DateTime.Parse(campos[2], CultureInfo.InvariantCulture),
                ValorPago = decimal.Parse(campos[3], CultureInfo.InvariantCulture),
                ClienteId = int.Parse(campos[4])
            });
        }

        var orderBusiness = new OrderBusiness();
        var resultados = orderBusiness.ProcessarPedidos(pedidos);

        Console.WriteLine("Id,TipoProcedimento,DataProcedimento,ValorPago,ValorReembolsado,ClienteId,Status");
        foreach (var resultado in resultados)
        {
            Console.WriteLine(resultado.ToString());
        }
    }
}
