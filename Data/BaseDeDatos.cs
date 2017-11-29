using System.Collections.Generic;
using Entidades;

namespace Data
{
    public static class BaseDeDatos
    {
        public static List<Supervisor> Supervisores { get; set; } = new List<Supervisor>
        {
            new Supervisor
            {
                Nombre = "Delfina",
                Apellido = "Caffa",
                AnioIngreso = 2000,
                Dni = 35115451,
                PrecioPorHora = 300,
                HorasTrabajadas = 400,
                Categoria = "a"
            },

            new Supervisor
            {
                Nombre = "Maria",
                Apellido = "Sanchez",
                AnioIngreso = 2010,
                Dni = 39222456,
                PrecioPorHora = 300,
                HorasTrabajadas = 200,
                Categoria = "b"
            }
        };

        public static List<Vendedor> Vendedores { get; set; } = new List<Vendedor>
        {
            new Vendedor
            {
                Nombre = "Marta",
                Apellido = "Martinez",
                AnioIngreso = 2015,
                Dni = 11222333,
                PrecioPorHora = 100,
                HorasTrabajadas = 200
            }
        };
    }
}