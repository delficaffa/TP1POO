using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Entidades;


namespace Servicios.Consultas
{
    public class ConsultasVendedor
    {
        public IEnumerable<Vendedor> ListarVendedores()
        {
            var listarVen = BaseDeDatos.Vendedores
                .Select(v => new Vendedor
                {
                    Apellido = v.Apellido,
                    Nombre = v.Nombre,
                    Dni = v.Dni,
                    AnioIngreso = v.AnioIngreso,
                    SueldoTotal = CalculoSueldoTotalVendedor(v)

                });
            return listarVen;
        }

        public Vendedor BuscarVendedor(long dni)
        {

            var buscarVen = BaseDeDatos.Vendedores
                .Where(v => v.Dni.Equals(dni))
                .Select(v => new Vendedor
                {
                    Apellido = v.Apellido,
                    Nombre = v.Nombre,
                    Dni = v.Dni,
                    AnioIngreso = v.AnioIngreso,
                    PrecioPorHora = v.PrecioPorHora,
                    HorasTrabajadas = v.HorasTrabajadas

                }).First();
            return buscarVen;

        }

        public void AgregarVendedor(Vendedor v)
        {
            BaseDeDatos.Vendedores.Add(v);
        }

        public void EliminarVendedor(long dni)
        {
            try
            {
                Vendedor vendedorParaRemover = null;
                foreach (var v in BaseDeDatos.Vendedores)
                {
                    if (v.Dni == dni)
                    {
                        vendedorParaRemover = v;
                        break;
                    }
                }
                if (vendedorParaRemover != null)
                {
                    BaseDeDatos.Vendedores.Remove(vendedorParaRemover);

                }
                else
                {
                    Console.WriteLine($"No existe el vendedor con el dni: {dni}");
                }
            }
            catch (InvalidOperationException a)
            {
                Console.WriteLine($"No existe el vendedor con el dni: {dni}");
            }
        }

        public int CalcularAntiguedadVendedor(Vendedor v)
        {
            return DateTime.Today.Year - v.AnioIngreso;
        }

        public decimal CalcularTasaAntiguedadVendedor(Vendedor v)
        {
            decimal tasaAntiguedad;
            if (CalcularAntiguedadVendedor(v) > 10)
            {
                tasaAntiguedad = v.HorasTrabajadas * (v.HorasTrabajadas * 5m) / 100;
            }
            else if (CalcularAntiguedadVendedor(v) > 5)
            {
                tasaAntiguedad = v.HorasTrabajadas * (v.HorasTrabajadas * 2.5m) / 100;
            }
            else
            {
                tasaAntiguedad = v.HorasTrabajadas;
            }
            return tasaAntiguedad;
        }

        public decimal CalculoSueldoTotalVendedor(Vendedor v)
        {
            return v.SueldoBase + (v.HorasTrabajadas * v.PrecioPorHora) + CalcularTasaAntiguedadVendedor(v);
        }

        public Vendedor MejorPagoVendedor()
        {
            var mejorVen = BaseDeDatos.Vendedores
                .OrderByDescending(a => CalculoSueldoTotalVendedor(a))
                .Select(vendedor => new Vendedor
                {
                    Apellido = vendedor.Apellido,
                    Nombre = vendedor.Nombre,
                    SueldoTotal = CalculoSueldoTotalVendedor(vendedor)

                }).First();
            return mejorVen;
        }
    }
}
