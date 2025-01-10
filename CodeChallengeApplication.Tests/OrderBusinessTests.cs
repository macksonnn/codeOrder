using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CodeChallengeApplication.Tests
{
    public class OrderBusinessTests
    {
        private readonly OrderBusiness _orderBusiness;

        public OrderBusinessTests()
        {
            _orderBusiness = new OrderBusiness();
        }

        [Fact]
        public void TestReembolsoConsultaMedica()
        {
            // Arrange
            var pedido = new OrderEntity
            {
                Id = 1,
                TipoProcedimento = "Consulta Médica",
                DataProcedimento = DateTime.Now,
                ValorPago = 200.00m,
                ClienteId = 1
            };

            // Act
            var resultado = _orderBusiness.ProcessarPedidos(new List<OrderEntity> { pedido }).First();

            // Assert
            Assert.Equal(200.00m * 0.8m, resultado.ValorReembolsado);
            Assert.Equal("Aprovado", resultado.Status);
        }

        [Fact]
        public void TestReembolsoExameImagem()
        {
            // Arrange
            var pedido = new OrderEntity
            {
                Id = 2,
                TipoProcedimento = "Exame de Imagem",
                DataProcedimento = DateTime.Now,
                ValorPago = 250.00m,
                ClienteId = 1
            };

            // Act
            var resultado = _orderBusiness.ProcessarPedidos(new List<OrderEntity> { pedido }).First();

            // Assert
            Assert.Equal(250.00m * 0.9m, resultado.ValorReembolsado);
            Assert.Equal("Aprovado", resultado.Status);
        }

        [Fact]
        public void TestReembolsoExameLaboratorial()
        {
            // Arrange
            var pedido = new OrderEntity
            {
                Id = 3,
                TipoProcedimento = "Exame Laboratorial",
                DataProcedimento = DateTime.Now,
                ValorPago = 150.00m,
                ClienteId = 1
            };

            // Act
            var resultado = _orderBusiness.ProcessarPedidos(new List<OrderEntity> { pedido }).First();

            // Assert
            Assert.Equal(150.00m * 0.7m, resultado.ValorReembolsado);
            Assert.Equal("Aprovado", resultado.Status);
        }

        [Fact]
        public void TestReembolsoOutroTipoProcedimento()
        {
            // Arrange
            var pedido = new OrderEntity
            {
                Id = 4,
                TipoProcedimento = "Outro",
                DataProcedimento = DateTime.Now,
                ValorPago = 100.00m,
                ClienteId = 1
            };

            // Act
            var resultado = _orderBusiness.ProcessarPedidos(new List<OrderEntity> { pedido }).First();

            // Assert
            Assert.Equal(100.00m * 0.5m, resultado.ValorReembolsado);
            Assert.Equal("Aprovado", resultado.Status);
        }

        [Fact]
        public void TestPedidoForaDoPrazoDe90Dias()
        {
            // Arrange
            var pedido = new OrderEntity
            {
                Id = 5,
                TipoProcedimento = "Consulta Médica",
                DataProcedimento = DateTime.Now.AddDays(-91), 
                ValorPago = 300.00m,
                ClienteId = 1
            };

            // Act
            var resultado = _orderBusiness.ProcessarPedidos(new List<OrderEntity> { pedido }).First();

            // Assert
            Assert.Equal(0, resultado.ValorReembolsado); 
            Assert.Equal("Rejeitado", resultado.Status);
        }

        [Fact]
        public void TestLimiteDePedidosEm30Dias()
        {
            // Arrange
            var pedidos = new List<OrderEntity>
            {
                new OrderEntity { Id = 6, TipoProcedimento = "Consulta Médica", DataProcedimento = DateTime.Now, ValorPago = 200.00m, ClienteId = 2 },
                new OrderEntity { Id = 7, TipoProcedimento = "Consulta Médica", DataProcedimento = DateTime.Now, ValorPago = 200.00m, ClienteId = 2 },
                new OrderEntity { Id = 8, TipoProcedimento = "Exame de Imagem", DataProcedimento = DateTime.Now, ValorPago = 250.00m, ClienteId = 2 },
                new OrderEntity { Id = 9, TipoProcedimento = "Exame Laboratorial", DataProcedimento = DateTime.Now, ValorPago = 150.00m, ClienteId = 2 },
                new OrderEntity { Id = 10, TipoProcedimento = "Outro", DataProcedimento = DateTime.Now, ValorPago = 100.00m, ClienteId = 2 },
                new OrderEntity { Id = 11, TipoProcedimento = "Consulta Médica", DataProcedimento = DateTime.Now, ValorPago = 300.00m, ClienteId = 2 } 
            };

            // Act
            var resultados = _orderBusiness.ProcessarPedidos(pedidos);

            // Assert
            var pedidoExcedente = resultados.Last();
            Assert.Equal("Suspeito de Fraude", pedidoExcedente.Status);
        }

        [Fact]
        public void TestLimiteDeValorEm30Dias()
        {
            // Arrange
            var pedidos = new List<OrderEntity>
            {
                new OrderEntity { Id = 12, TipoProcedimento = "Consulta Médica", DataProcedimento = DateTime.Now, ValorPago = 1000.00m, ClienteId = 3 },
                new OrderEntity { Id = 13, TipoProcedimento = "Exame de Imagem", DataProcedimento = DateTime.Now, ValorPago = 1000.00m, ClienteId = 3 },
                new OrderEntity { Id = 14, TipoProcedimento = "Exame Laboratorial", DataProcedimento = DateTime.Now, ValorPago = 1000.00m, ClienteId = 3 } // Este excede o limite de 2000
            };

            // Act
            var resultados = _orderBusiness.ProcessarPedidos(pedidos);

            // Assert
            var pedidoExcedente = resultados.Last();
            Assert.Equal("Aprovado", pedidoExcedente.Status);
        }
    }
}
