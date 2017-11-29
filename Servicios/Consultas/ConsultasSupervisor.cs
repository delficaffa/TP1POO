using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Entidades;

namespace Servicios
{
    public class ConsultasSupervisor
    {
        public IEnumerable<Supervisor> ListarSupervisores()
        {
            return BaseDeDatos.Supervisores
                .Select(s => new Supervisor
                {
                    Apellido = s.Apellido,
                    Nombre = s.Nombre,
                    Dni = s.Dni,
                    AnioIngreso = s.AnioIngreso,
                    Categoria = s.Categoria,
                    SueldoTotal = CalculoSueldoTotalSupervisor(s)
                });
        }

        public Supervisor BuscarSupervisor(long dni)
        {
            return BaseDeDatos.Supervisores
                .Where(sup => sup.Dni.Equals(dni))
                .Select(sup => new Supervisor
                {
                    Apellido = sup.Apellido,
                    Nombre = sup.Nombre,
                    Dni = sup.Dni,
                    AnioIngreso = sup.AnioIngreso,
                    Categoria = sup.Categoria,
                    PrecioPorHora = sup.PrecioPorHora,
                    HorasTrabajadas = sup.HorasTrabajadas

                }).First();

        }

        public void AgregarSupervisor(Supervisor s)
        {
            BaseDeDatos.Supervisores.Add(s);
        }

        public void EliminarSupervisor(long dni)
        {
            try
            {
                Supervisor supervisorParaRemover = null;
                foreach (var v in BaseDeDatos.Supervisores)
                {
                    if (v.Dni == dni)
                    {
                        supervisorParaRemover = v;
                        break;
                    }
                }
                if (supervisorParaRemover != null)
                {
                    BaseDeDatos.Supervisores.Remove(supervisorParaRemover);
                    
                }
                else
                {
                    Console.WriteLine($"No existe el supervisor con el dni: {dni}");
                }
            }
            catch (InvalidOperationException a)
            {
                Console.WriteLine($"No existe el supervisor con el dni: {dni}");
            }
        }

        public int CalcularAntiguedadSupervisor(Supervisor s)
        {
            return DateTime.Today.Year - s.AnioIngreso;
        }

        public decimal CalcularTasaAntiguedadSupervisor(Supervisor s)
        {
            decimal tasaAntiguedad;
            if (CalcularAntiguedadSupervisor(s) > 10)
            {
                tasaAntiguedad = s.HorasTrabajadas * (s.HorasTrabajadas * 10m) / 100;
            }
            else if (CalcularAntiguedadSupervisor(s) > 5)
            {
                tasaAntiguedad = s.HorasTrabajadas * (s.HorasTrabajadas * 5m) / 100;
            }
            else
            {
                tasaAntiguedad = s.HorasTrabajadas;
            }
            return tasaAntiguedad;
        }

        public decimal CalcularComisionSupervisor(Supervisor s)
        {
            var comision = 0m;
            switch (s.Categoria)
            {
                case "a":
                    return comision = s.HorasTrabajadas * (s.HorasTrabajadas * 10) / 100;

                case "b":
                    return comision = s.HorasTrabajadas * (s.HorasTrabajadas * 5) / 100;

                case "c":
                    return comision = s.HorasTrabajadas * (s.HorasTrabajadas * 2) / 100;

            }
            return default(decimal);
        }

        public decimal CalculoSueldoTotalSupervisor(Supervisor s)
        {
            return s.SueldoBase + (s.HorasTrabajadas * s.PrecioPorHora)
                + CalcularTasaAntiguedadSupervisor(s) + CalcularComisionSupervisor(s);
        }

        public Supervisor MejorPagoSupervisor()
        {
            var MejorSup = BaseDeDatos.Supervisores
                .OrderByDescending(a => CalculoSueldoTotalSupervisor(a))
                .Select(supervisor => new Supervisor
                {
                    Apellido = supervisor.Apellido,
                    Nombre = supervisor.Nombre,
                    SueldoTotal = CalculoSueldoTotalSupervisor(supervisor)

                }).First();
            return MejorSup;

        }

    }
}
