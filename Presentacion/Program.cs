
using Servicios;
using Servicios.Consultas;
using System;
using Entidades;

namespace Presentacion
{
    public class Program
    {
        static void Main(string[] args)
        {

            string opcion;
            do
            {
                Console.WriteLine("\nIngrese la opcion deseada");
                Console.WriteLine("'S' - Supervisores  'V' - Vendedores   'M' - Mejores pagos   'E' - SALIR");
                opcion = Console.ReadLine();

                switch (opcion.ToLower())
                {
                    case "s":
                        MenuSupervisores();
                        break;

                    case "v":
                        MenuVendedores();
                        break;

                    case "m":
                        MenuMejoresPagos();
                        break;
                }
            } while (opcion != "e");

            Console.WriteLine("Fin del programa, ingrese una tecla para continuar");
            Console.ReadLine();
        }

        private static void MenuMejoresPagos()
        {
            var consultaSup = new ConsultasSupervisor();
            var consultaVen = new ConsultasVendedor();
            var supervisorMejorPago = new Supervisor();
            supervisorMejorPago = consultaSup.MejorPagoSupervisor();
            var vendedorMejorPago = new Vendedor();
            vendedorMejorPago = consultaVen.MejorPagoVendedor();
            Console.WriteLine("LOS MAS PAGOS SON:");
            Deco();
            Console.WriteLine($"SUPERVISOR:\n\tApellido: {supervisorMejorPago.Apellido}\n\t" +
                $"Nombre: {supervisorMejorPago.Nombre}\n\tSueldo total: ${supervisorMejorPago.SueldoTotal}");
            Deco();
            Console.WriteLine($"VENDEDOR:\n\tApellido: {vendedorMejorPago.Apellido}\n\t" +
                $"Nombre: {vendedorMejorPago.Nombre}\n\tSueldo total: ${vendedorMejorPago.SueldoTotal}");
        }

        private static void MenuVendedores()
        {
            var servicio = new ConsultasVendedor();
            Console.WriteLine("'L' Listado - 'C' Consultar - 'A' Agregar - 'M' Modificar - 'E' Eliminar");
            var opcion = Console.ReadLine();

            switch (opcion.ToLower())
            {
                case "l":
                    Console.WriteLine("VENDEDORES:");
                    Deco();
                    var listado = new ConsultasVendedor();
                    var vendedores = listado.ListarVendedores();
                    foreach (var v in vendedores)
                    {
                        Console.WriteLine($"\n\tApellido: {v.Apellido}\n\tNombre: {v.Nombre}\n\tDNI: {v.Dni}" +
                            $"\n\tAño de Ingreso: {v.AnioIngreso}\n\tSueldo total: {v.SueldoTotal}");

                    }
                    break;

                case "c":
                    Console.WriteLine("Ingrese el dni a buscar");
                    long dniParaBuscar;
                    long.TryParse(Console.ReadLine(), out dniParaBuscar);
                    try
                    {
                        var vendedor = servicio.BuscarVendedor(dniParaBuscar);
                        Console.WriteLine($"\tApellido: {vendedor.Apellido}\n\tNombre: {vendedor.Nombre}\n\tDNI: {vendedor.Dni}" +
                                  $"\n\tAño de Ingreso: {vendedor.AnioIngreso}");
                    }

                    catch (InvalidOperationException a)
                    {
                        Console.WriteLine($"No existe el vendedor con el dni: {dniParaBuscar}");
                    }
                    break;

                case "a":
                    var repeat = true;
                    while (repeat)
                    {
                        try
                        {
                            Console.WriteLine("Ingrese nombre");
                            var nuevoVendedorNombre = Console.ReadLine();

                            Console.WriteLine("Ingrese apellido");
                            var nuevoVendedorApellido = Console.ReadLine();

                            Console.WriteLine("Ingrese dni");
                            var nuevoVendedorDni = long.Parse(Console.ReadLine());

                            Console.WriteLine("Ingrese Año de ingreso");
                            var nuevoVendedorAnioIngreso = int.Parse(Console.ReadLine());

                            Console.WriteLine("Ingrese precio por hora");
                            var nuevoVendedorPrecioPorHora = int.Parse(Console.ReadLine());

                            Console.WriteLine("Ingrese horas trabajadas");
                            var nuevoVendedorHorasTrabajadas = int.Parse(Console.ReadLine());

                            var nuevoVendedor = new Vendedor
                            {
                                Apellido = nuevoVendedorApellido,
                                Nombre = nuevoVendedorNombre,
                                AnioIngreso = nuevoVendedorAnioIngreso,
                                Dni = nuevoVendedorDni,
                                PrecioPorHora = nuevoVendedorPrecioPorHora,
                                HorasTrabajadas = nuevoVendedorHorasTrabajadas
                            };

                            servicio.AgregarVendedor(nuevoVendedor);
                            Console.WriteLine("Vendedor agregado correctamente");
                            break;
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Console.WriteLine("Desea volver a intentarlo? S/N");
                        string go = Console.ReadLine();
                        repeat = go.ToLower() == "s";
                    }
                    break;

                case "m":
                    var again = true;
                    while (again)
                    {
                        Console.WriteLine("Ingrese el dni a buscar");
                        long dniParaEditar;
                        long.TryParse(Console.ReadLine(), out dniParaEditar);
                        try
                        {
                            var vendedorParaEditar = servicio.BuscarVendedor(dniParaEditar);

                            Console.WriteLine("Ingrese nuevo nombre");
                            var editarVendedorNombre = Console.ReadLine();

                            Console.WriteLine("Ingrese nuevo apellido");
                            var editarVendedorApellido = Console.ReadLine();

                            Console.WriteLine("Ingrese nuevo Año de ingreso");
                            var editarVendedorAnioIngreso = int.Parse(Console.ReadLine());

                            Console.WriteLine("Ingrese nuevo dni");
                            var editarVendedorDni = long.Parse(Console.ReadLine());

                            Console.WriteLine("Ingrese nuevo precio por hora");
                            var editarVendedorPrecioPorHora = int.Parse(Console.ReadLine());

                            Console.WriteLine("Ingrese nuevas horas trabajadas");
                            var editarVendedorHorasTrabajadas = int.Parse(Console.ReadLine());

                            vendedorParaEditar.Apellido = editarVendedorApellido;
                            vendedorParaEditar.Nombre = editarVendedorNombre;
                            vendedorParaEditar.AnioIngreso = editarVendedorAnioIngreso;
                            vendedorParaEditar.Dni = editarVendedorDni;
                            vendedorParaEditar.PrecioPorHora = editarVendedorPrecioPorHora;
                            vendedorParaEditar.HorasTrabajadas = editarVendedorHorasTrabajadas;

                            Console.WriteLine("Vendedor editado correctamente");
                        }
                        catch (InvalidOperationException a)
                        {
                            Console.WriteLine($"No existe el vendedor con el dni: {dniParaEditar}");
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Console.WriteLine("Desea volver a intentarlo? S/N");
                        var ta = Console.ReadLine();
                        again = ta.ToLower() == "s";

                    }

                    break;

                case "e":
                    Console.WriteLine("Ingrese el dni a buscar");
                    var dniParaRemover = long.Parse(Console.ReadLine());

                    servicio.EliminarVendedor(dniParaRemover);
                    Console.WriteLine("Vendedor eliminado correctamente");

                    break;
            }
            opcion = Console.ReadLine();
        }

        private static void MenuSupervisores()
        {
            var servicio = new ConsultasSupervisor();
            Console.WriteLine("'L' Listado - 'C' Consultar - 'A' Agregar - 'M' Modificar - 'E' Eliminar");
            var opcion = Console.ReadLine();

            switch (opcion.ToLower())
            {

                case "l":
                    Console.WriteLine("SUPERVISORES:");
                    Deco();
                    var listado = new ConsultasSupervisor();
                    var supervisores = listado.ListarSupervisores();
                    foreach (var s in supervisores)
                    {
                        Console.WriteLine($"\n\tApellido: {s.Apellido}\n\tNombre: {s.Nombre}\n\tDNI: {s.Dni}" +
                            $"\n\tAño de Ingreso: {s.AnioIngreso}\n\tCategoria: {s.Categoria}\n\tSueldo total: {s.SueldoTotal}");
                    }
                    break;

                case "c":
                    Console.WriteLine("Ingrese el dni a buscar");
                    var dniParaBuscar = long.Parse(Console.ReadLine());
                    try
                    {
                        var supervisor = servicio.BuscarSupervisor(dniParaBuscar);
                        Console.WriteLine($"\tApellido: {supervisor.Apellido}\n\tNombre: {supervisor.Nombre}" +
                            $"\n\tDNI: {supervisor.Dni}\n\tAño de Ingreso: {supervisor.AnioIngreso}");
                    }
                    catch (InvalidOperationException a)
                    {
                        Console.WriteLine($"No existe el supervisor con el DNI: {dniParaBuscar}");
                    }

                    break;

                case "a":
                    var repeat = true;
                    while (repeat)
                    {
                        try
                        {
                            Console.WriteLine("Ingrese nombre");
                            var nuevoSupervisorNombre = Console.ReadLine();

                            Console.WriteLine("Ingrese apellido");
                            var nuevoSupervisorApellido = Console.ReadLine();

                            Console.WriteLine("Ingrese dni");
                            var nuevoSupervisorDni = long.Parse(Console.ReadLine());
                            string cat1;
                            do
                            {
                                Console.WriteLine("Ingrese categoria");
                                cat1 = Console.ReadLine().ToLower();
                                switch (cat1.ToLower())
                                {
                                    case "a":
                                        cat1 = "a";
                                        break;

                                    case "b":
                                        cat1 = "b";
                                        break;

                                    case "c":
                                        cat1 = "c";
                                        break;

                                    default:
                                        cat1 = "z";
                                        break;
                                }
                            }
                            while (cat1 == "z");
                            var nuevoSupervisorCategoria = cat1;


                            Console.WriteLine("Ingrese Año de ingreso");
                            var nuevoSupervisorAnioIngreso = int.Parse(Console.ReadLine());

                            Console.WriteLine("Ingrese precio por hora");
                            var nuevoSupervisorPrecioPorHora = int.Parse(Console.ReadLine());

                            Console.WriteLine("Ingrese horas trabajadas");
                            var nuevoSupervisorHorasTrabajadas = int.Parse(Console.ReadLine());

                            var nuevoSupervisor = new Supervisor
                            {
                                Apellido = nuevoSupervisorApellido,
                                Nombre = nuevoSupervisorNombre,
                                AnioIngreso = nuevoSupervisorAnioIngreso,
                                Dni = nuevoSupervisorDni,
                                PrecioPorHora = nuevoSupervisorPrecioPorHora,
                                HorasTrabajadas = nuevoSupervisorHorasTrabajadas,
                                Categoria = nuevoSupervisorCategoria
                            };
                            servicio.AgregarSupervisor(nuevoSupervisor);
                            Console.WriteLine("Supervisor agregado correctamente");
                            break;
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);

                        }
                        Console.WriteLine("Desea volver a intentarlo? S/N");
                        var go = Console.ReadLine();
                        repeat = go.ToLower() == "s";

                    }
                    break;

                case "m":
                    var again = true;
                    while (again)
                    {
                        Console.WriteLine("Ingrese el dni a buscar");
                        long dniParaEditar;
                        long.TryParse(Console.ReadLine(), out dniParaEditar);
                        try
                        {
                            var supervisorParaEditar = servicio.BuscarSupervisor(dniParaEditar);

                            string cat;

                            Console.WriteLine("Ingrese nuevo nombre");
                            var editarSupervisorNombre = Console.ReadLine();

                            Console.WriteLine("Ingrese nuevo apellido");
                            var editarSupervisorApellido = Console.ReadLine();

                            Console.WriteLine("Ingrese nuevo Año de ingreso");
                            var editarSupervisorAnioIngreso = int.Parse(Console.ReadLine());

                            Console.WriteLine("Ingrese nuevo dni");
                            var editarSupervisorDni = long.Parse(Console.ReadLine());

                            do
                            {

                                Console.WriteLine("Ingrese categoria");
                                cat = Console.ReadLine().ToLower();
                                switch (cat.ToLower())
                                {
                                    case "a":
                                        cat = "a";
                                        break;

                                    case "b":
                                        cat = "b";
                                        break;

                                    case "c":
                                        cat = "c";
                                        break;

                                    default:
                                        cat = "z";
                                        break;
                                }
                            }
                            while (cat == "z");
                            var editarSupervisorCategoria = cat;

                            Console.WriteLine("Ingrese nuevo precio por hora");
                            var editarSupervisorPrecioPorHora = int.Parse(Console.ReadLine());

                            Console.WriteLine("Ingrese nuevas horas trabajadas");
                            var editarSupervisorHorasTrabajadas = int.Parse(Console.ReadLine());

                            supervisorParaEditar.Apellido = editarSupervisorApellido;
                            supervisorParaEditar.Nombre = editarSupervisorNombre;
                            supervisorParaEditar.AnioIngreso = editarSupervisorAnioIngreso;
                            supervisorParaEditar.Dni = editarSupervisorDni;
                            supervisorParaEditar.PrecioPorHora = editarSupervisorPrecioPorHora;
                            supervisorParaEditar.HorasTrabajadas = editarSupervisorHorasTrabajadas;
                            supervisorParaEditar.Categoria = editarSupervisorCategoria;

                            Console.WriteLine("Supervisor editado correctamente");

                        }
                        catch (InvalidOperationException a)
                        {
                            Console.WriteLine($"No existe el supervisor con el DNI: {dniParaEditar}");
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        Console.WriteLine("Desea volver a intentarlo? S/N");
                        var go = Console.ReadLine();
                        again = go.ToLower() == "s";
                    }
                    break;

                case "e":
                    Console.WriteLine("Ingrese el dni a buscar");
                    long dniParaRemover;
                    long.TryParse(Console.ReadLine(), out dniParaRemover);
                    servicio.EliminarSupervisor(dniParaRemover);
                    Console.WriteLine("Supervisor eliminado correctamente");
                    break;
            }
            opcion = Console.ReadLine();
        }

        private static void Deco()
        {
            Console.WriteLine("--------------------------------------------");
        }
    }

}


