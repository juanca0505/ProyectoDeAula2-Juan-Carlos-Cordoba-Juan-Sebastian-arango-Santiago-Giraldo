using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{


    class Program
    {
        static EmpresaEnergiaAgua empresa = new EmpresaEnergiaAgua();

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("\n1. Registrar cliente");
                Console.WriteLine("2. Actualizar información del cliente");
                Console.WriteLine("3. Eliminar cliente");
                Console.WriteLine("4. Calcular valor a pagar de un cliente");
                Console.WriteLine("5. Mostrar cliente con mayor desfase de consumo de energía");
                Console.WriteLine("6. Mostrar estrato con mayor ahorro de agua");
                Console.WriteLine("7. Mostrar estrato con mayor consumo de energía");
                Console.WriteLine("8. Mostrar estrato con menor consumo de energía");
                Console.WriteLine("9. Mostrar valor total que pagan los clientes");
                Console.WriteLine("0. Salir");

                int opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        RegistrarCliente();
                        break;

                    case 2:
                        ActualizarCliente();
                        break;

                    case 3:
                        EliminarCliente();
                        break;

                    case 4:
                        CalcularValorAPagar();
                        break;

                    case 5:
                        MostrarMayorDesfaseEnergia();
                        break;

                    case 6:
                        MostrarEstratoMayorAhorroAgua();
                        break;

                    case 7:
                        MostrarEstratoMayorConsumoEnergia();
                        break;

                    case 8:
                        MostrarEstratoMenorConsumoEnergia();
                        break;

                    case 9:
                        MostrarValorTotal();
                        break;

                    case 0:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            }
        }

        static void RegistrarCliente()
        {
            Cliente cliente = new Cliente();

            Console.Write("Ingrese el número de cédula del cliente: ");
            cliente.Cedula = Convert.ToInt32(Console.ReadLine());

            Console.Write("Ingrese el estrato del cliente: ");
            cliente.Estrato = Convert.ToInt32(Console.ReadLine());

            Console.Write("Ingrese la meta de ahorro de energía del cliente: ");
            cliente.MetaAhorroEnergia = Convert.ToInt32(Console.ReadLine());

            Console.Write("Ingrese el consumo actual de energía del cliente: ");
            cliente.ConsumoActualEnergia = Convert.ToInt32(Console.ReadLine());

            Console.Write("Ingrese el promedio de consumo de agua del cliente: ");
            cliente.PromedioConsumoAgua = Convert.ToInt32(Console.ReadLine());

            Console.Write("Ingrese el consumo actual de agua del cliente: ");
            cliente.ConsumoActualAgua = Convert.ToInt32(Console.ReadLine());

            empresa.RegistrarCliente(cliente);
            Console.WriteLine("Cliente registrado correctamente.");
        }

        static void ActualizarCliente()
        {
            Console.Write("Ingrese el número de cédula del cliente a actualizar: ");
            int cedula = Convert.ToInt32(Console.ReadLine());

            Cliente cliente = new Cliente();

            Console.Write("Ingrese el nuevo estrato del cliente: ");
            cliente.Estrato = Convert.ToInt32(Console.ReadLine());

            Console.Write("Ingrese la nueva meta de ahorro de energía del cliente: ");
            cliente.MetaAhorroEnergia = Convert.ToInt32(Console.ReadLine());

            Console.Write("Ingrese el nuevo consumo actual de energía del cliente: ");
            cliente.ConsumoActualEnergia = Convert.ToInt32(Console.ReadLine());

            Console.Write("Ingrese el nuevo promedio de consumo de agua del cliente: ");
            cliente.PromedioConsumoAgua = Convert.ToInt32(Console.ReadLine());

            Console.Write("Ingrese el nuevo consumo actual de agua del cliente: ");
            cliente.ConsumoActualAgua = Convert.ToInt32(Console.ReadLine());

            empresa.ActualizarCliente(cedula, cliente);
        }

        static void EliminarCliente()
        {
            Console.Write("Ingrese el número de cédula del cliente a eliminar: ");
            int cedula = Convert.ToInt32(Console.ReadLine());
            empresa.EliminarCliente(cedula);
        }

        static void CalcularValorAPagar()
        {
            Console.Write("Ingrese el número de cédula del cliente: ");
            int cedula = Convert.ToInt32(Console.ReadLine());
            Cliente clienteSeleccionado = empresa.ObtenerCliente(cedula); // Aquí está el problema
            if (clienteSeleccionado != null)
            {
                double valorAPagar = empresa.CalcularValorPagar(clienteSeleccionado);
                Console.WriteLine($"Valor a pagar para el cliente {cedula}: ${valorAPagar:F2}");
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
        }


        static void MostrarMayorDesfaseEnergia()
        {
            Cliente cliente = empresa.ObtenerClienteMayorDesfase();
            Console.WriteLine($"Cliente con mayor desfase de consumo de energía:");
            Console.WriteLine($"Cédula: {cliente.Cedula}, Desfase: {Math.Abs(cliente.MetaAhorroEnergia - cliente.ConsumoActualEnergia)}");
        }

        static void MostrarEstratoMayorAhorroAgua()
        {
            int estrato = empresa.ObtenerEstratoMayorAhorroAgua();
            Console.WriteLine($"Estrato con mayor ahorro de agua: {estrato}");
        }

        static void MostrarEstratoMayorConsumoEnergia()
        {
            int estrato = empresa.ObtenerEstratoMayorConsumoEnergia();
            Console.WriteLine($"Estrato con mayor consumo de energía: {estrato}");
        }

        static void MostrarEstratoMenorConsumoEnergia()
        {
            int estrato = empresa.ObtenerEstratoMenorConsumoEnergia();
            Console.WriteLine($"Estrato con menor consumo de energía: {estrato}");
        }

        static void MostrarValorTotal()
        {
            double total = empresa.ObtenerValorTotal();
            Console.WriteLine($"Valor total que los clientes le pagan a la empresa: ${total:F2}");
        }
    }
}