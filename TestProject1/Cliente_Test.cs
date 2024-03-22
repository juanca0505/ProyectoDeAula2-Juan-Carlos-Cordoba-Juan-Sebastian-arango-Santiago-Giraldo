using ConsoleApp2;
using System;
using NUnit.Framework;

namespace ClienteTests
{
    [TestFixture]
    public class ClienteTest
    {
        [Test]
        public void TestCliente_PropertiesSetAndGetCorrectly()
        {
            // Arrange
            int cedula = 123456;
            int estrato = 2;
            int metaAhorroEnergia = 1000;
            int consumoActualEnergia = 800;
            int promedioConsumoAgua = 500;
            int consumoActualAgua = 600;

            // Act
            Cliente cliente = new Cliente();
            cliente.Cedula = cedula;
            cliente.Estrato = estrato;
            cliente.MetaAhorroEnergia = metaAhorroEnergia;
            cliente.ConsumoActualEnergia = consumoActualEnergia;
            cliente.PromedioConsumoAgua = promedioConsumoAgua;
            cliente.ConsumoActualAgua = consumoActualAgua;

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(cedula, cliente.Cedula);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(estrato, cliente.Estrato);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(metaAhorroEnergia, cliente.MetaAhorroEnergia);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(consumoActualEnergia, cliente.ConsumoActualEnergia);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(promedioConsumoAgua, cliente.PromedioConsumoAgua);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(consumoActualAgua, cliente.ConsumoActualAgua);
        }
    }
}
