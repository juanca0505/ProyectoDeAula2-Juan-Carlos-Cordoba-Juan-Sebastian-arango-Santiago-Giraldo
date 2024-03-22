using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class EmpresaEnergiaAgua
    {
        private List<Cliente> clientes = new List<Cliente>();

        public void RegistrarCliente(Cliente cliente)
        {
            clientes.Add(cliente);
        }

        public void ActualizarCliente(int cedula, Cliente clienteActualizado)
        {
            Cliente clienteExistente = clientes.Find(c => c.Cedula == cedula);
            if (clienteExistente != null)
            {
                clienteExistente.Estrato = clienteActualizado.Estrato;
                clienteExistente.MetaAhorroEnergia = clienteActualizado.MetaAhorroEnergia;
                clienteExistente.ConsumoActualEnergia = clienteActualizado.ConsumoActualEnergia;
                clienteExistente.PromedioConsumoAgua = clienteActualizado.PromedioConsumoAgua;
                clienteExistente.ConsumoActualAgua = clienteActualizado.ConsumoActualAgua;
                Console.WriteLine("Cliente actualizado correctamente.");
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
        }

        public double CalcularValorPagar(Cliente cliente)
        {
            double valorParcialEnergia = cliente.ConsumoActualEnergia * 850;
            double valorIncentivo = (cliente.MetaAhorroEnergia - cliente.ConsumoActualEnergia) * 850;
            double valorEnergia = valorParcialEnergia - valorIncentivo;
            double valorAgua = CalcularValorAgua(cliente);
            return valorEnergia + valorAgua;
        }


        public void EliminarCliente(int cedula)
        {
            Cliente cliente = clientes.Find(c => c.Cedula == cedula);
            if (cliente != null)
            {
                clientes.Remove(cliente);
                Console.WriteLine("Cliente eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
        }

        public Cliente ObtenerCliente(int cedula)
        {
            return clientes.FirstOrDefault(c => c.Cedula == cedula);
        }

        public Cliente ObtenerClienteMayorDesfase()
        {
            return clientes.OrderByDescending(c => Math.Abs(c.MetaAhorroEnergia - c.ConsumoActualEnergia)).FirstOrDefault();
        }

        public int ObtenerEstratoMayorAhorroAgua()
        {
            var ahorroPorEstrato = clientes.GroupBy(c => c.Estrato)
                                            .ToDictionary(g => g.Key, g => g.Sum(c => c.PromedioConsumoAgua - c.ConsumoActualAgua));
            return ahorroPorEstrato.OrderByDescending(x => x.Value).First().Key;
        }

        public int ObtenerEstratoMayorConsumoEnergia()
        {
            return clientes.GroupBy(c => c.Estrato)
                           .ToDictionary(g => g.Key, g => g.Sum(c => c.ConsumoActualEnergia))
                           .OrderByDescending(x => x.Value)
                           .First().Key;
        }

        public int ObtenerEstratoMenorConsumoEnergia()
        {
            return clientes.GroupBy(c => c.Estrato)
                           .ToDictionary(g => g.Key, g => g.Sum(c => c.ConsumoActualEnergia))
                           .OrderBy(x => x.Value)
                           .First().Key;
        }

        public double ObtenerValorTotal()
        {
            double total = clientes.Sum(c => (c.ConsumoActualEnergia * 850) + CalcularValorAgua(c));
            return total;
        }

        public double CalcularValorAgua(Cliente cliente)
        {
            int excesoAgua = Math.Max(0, cliente.ConsumoActualAgua - cliente.PromedioConsumoAgua);
            double costoExcesoAgua = excesoAgua * (2 * 4600);
            double valorAgua = cliente.ConsumoActualAgua * 4600 + costoExcesoAgua;
            return valorAgua;
        }
    }
}
