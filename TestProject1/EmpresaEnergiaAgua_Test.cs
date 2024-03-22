using ConsoleApp2;
using NUnit.Framework;

namespace EmpresaEnergiaAguaTests
{
    public class EmpresaEnergiaAguaTest
    {
        private EmpresaEnergiaAgua empresa;

        [SetUp]
        public void SetUp()
        {
            empresa = new EmpresaEnergiaAgua();
        }

        [Test]
        public void TestRegistrarCliente_AñadeNuevoCliente()
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

            empresa.RegistrarCliente(cliente);

            Cliente clienteRegistrado = empresa.ObtenerCliente(cedula);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(cedula, clienteRegistrado.Cedula);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(estrato, clienteRegistrado.Estrato);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(metaAhorroEnergia, clienteRegistrado.MetaAhorroEnergia);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(consumoActualEnergia, clienteRegistrado.ConsumoActualEnergia);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(promedioConsumoAgua, clienteRegistrado.PromedioConsumoAgua);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(consumoActualAgua, clienteRegistrado.ConsumoActualAgua);



        }


        [Test]
        public void TestObtenerEstratoMayorAhorroAgua()
        {
            // Arrange
            empresa.RegistrarCliente(new Cliente
            {
                Cedula = 1,
                Estrato = 1,
                MetaAhorroEnergia = 1000,
                ConsumoActualEnergia = 800,
                PromedioConsumoAgua = 600,
                ConsumoActualAgua = 400
            });
            empresa.RegistrarCliente(new Cliente
            {
                Cedula = 2,
                Estrato = 2,
                MetaAhorroEnergia = 1000,
                ConsumoActualEnergia = 800,
                PromedioConsumoAgua = 500,
                ConsumoActualAgua = 550
            });

            // Act
            int estratoMayorAhorro = empresa.ObtenerEstratoMayorAhorroAgua();

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, estratoMayorAhorro);
        }

        [Test]
        public void TestCalcularValorPagar()
        {
            // Arrange
            Cliente cliente = new Cliente
            {
                Cedula = 123456,
                Estrato = 2,
                MetaAhorroEnergia = 1000,
                ConsumoActualEnergia = 800,
                PromedioConsumoAgua = 500,
                ConsumoActualAgua = 600
            };


            double valorEsperado = (800 * 850) - ((1000 - 800) * 850) + CalcularValorAgua(cliente);

            // Act
            double valorPagar = empresa.CalcularValorPagar(cliente);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(valorEsperado, valorPagar, 0.01);

            double CalcularValorAgua(Cliente cliente)
            {
                int excesoAgua = Math.Max(0, cliente.ConsumoActualAgua - cliente.PromedioConsumoAgua);
                double costoExcesoAgua = excesoAgua * (2 * 4600);
                double valorAgua = cliente.ConsumoActualAgua * 4600 + costoExcesoAgua;
                return valorAgua;
            }
        }

        [Test]
        public void TestEliminarCliente_EliminaClienteExistente()
        {
            // Arrange
            int cedula = 123456;
            Cliente cliente = new Cliente
            {
                Cedula = cedula,
                Estrato = 2,
                MetaAhorroEnergia = 1000,
                ConsumoActualEnergia = 800,
                PromedioConsumoAgua = 500,
                ConsumoActualAgua = 600
            };

            empresa.RegistrarCliente(cliente);
            // Act
            empresa.EliminarCliente(cedula);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNull(empresa.ObtenerCliente(cedula));
        }

        [Test]
        public void TestObtenerEstratoMayorConsumoEnergia_ClientesDiferentesEstratos()
        {
            // Arrange
            empresa.RegistrarCliente(new Cliente { Estrato = 1, ConsumoActualEnergia = 1200 });
            empresa.RegistrarCliente(new Cliente { Estrato = 2, ConsumoActualEnergia = 1500 });
            empresa.RegistrarCliente(new Cliente { Estrato = 3, ConsumoActualEnergia = 800 });

            // Act
            int estratoMayorConsumo = empresa.ObtenerEstratoMayorConsumoEnergia();

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(2, estratoMayorConsumo);
        }

        [Test]
        public void TestObtenerEstratoMenorConsumoEnergia_ClientesDiferentesEstratos()
        {
            // Arrange
            empresa.RegistrarCliente(new Cliente { Estrato = 1, ConsumoActualEnergia = 800 });
            empresa.RegistrarCliente(new Cliente { Estrato = 2, ConsumoActualEnergia = 1500 });
            empresa.RegistrarCliente(new Cliente { Estrato = 3, ConsumoActualEnergia = 1200 });

            // Act
            int estratoMenorConsumo = empresa.ObtenerEstratoMenorConsumoEnergia();

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(1, estratoMenorConsumo);
        }

        [Test]
        public void TestObtenerValorTotal_ListaVacía()
        {
            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(0, empresa.ObtenerValorTotal());
        }


        [Test]
        public void TestCalcularValorAgua_ConsumoPorDebajoPromedio()
        {
            // Arrange
            Cliente cliente = new Cliente
            {
                ConsumoActualAgua = 400,
                PromedioConsumoAgua = 500
            };

            // Act

            double valorAgua = empresa.CalcularValorAgua(cliente);

            // Assert
            double valorEsperado = 400 * 4600;
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(valorEsperado, valorAgua);
        }

        

    }
}
