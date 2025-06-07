namespace CedrosNahuizalquenos.DTOs
{
    public class IncidenciaDto
    {
        public int IncidenciaID { get; set; }
        public int EmpleadoID { get; set; }
        public string Tipo { get; set; } = string.Empty; // Vacaciones, Incapacidad, Ausencia
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string? Comentario { get; set; }
    }
}
