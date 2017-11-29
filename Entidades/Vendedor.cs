
namespace Entidades
{
    public class Vendedor
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int AnioIngreso { get; set; }
        public long Dni { get; set; }
        public decimal PrecioPorHora { get; set; }
        public decimal HorasTrabajadas { get; set; }
        public decimal SueldoBase = 2000;

        public decimal SueldoTotal { get; set; }
    }
}
