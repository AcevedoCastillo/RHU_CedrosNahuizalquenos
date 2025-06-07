namespace CedrosNahuizalquenos.DTOs
{
    public class EmpleadoDto
    {
        public int EmpleadoID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Area { get; set; } = string.Empty;
        public string PuestoFuncional { get; set; } = string.Empty;
        public decimal SalarioMensual { get; set; }
        public bool Activo { get; set; }
    }
}
