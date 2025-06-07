namespace CedrosNahuizalquenos.DTOs
{
    public class HorasExtraDto
    {
        public int HoraExtraID { get; set; }
        public int EmpleadoID { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; } = string.Empty; // Diurna, Nocturna, Nocturnidad
        public decimal CantidadHoras { get; set; }
    }
}
