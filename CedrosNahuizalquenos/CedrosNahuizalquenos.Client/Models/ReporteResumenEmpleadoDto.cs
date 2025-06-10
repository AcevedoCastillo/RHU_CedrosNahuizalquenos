namespace CedrosNahuizalquenos.DTOs
{
    public class ReporteResumenEmpleadoDto
    {
        public string Area { get; set; }
        public string EmpleadoID { get; set; }
        public string PuestoFuncional { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaCorte { get; set; }
        public int TiempoEnEmpresaAnios { get; set; }
        public decimal TotalPorPuestoMensual { get; set; }

        public decimal HorasExtrasDiurnas { get; set; }
        public decimal HorasExtrasNocturnas { get; set; }
        public decimal HorasNocturnas { get; set; }

        public decimal PagoHorasExtrasDiurnas { get; set; }
        public decimal PagoHorasExtrasNocturnas { get; set; }
        public decimal PagoHorasNocturnidad { get; set; }

        public decimal Vacaciones { get; set; }
        public decimal AguinaldoTotal { get; set; }
        public decimal AguinaldoNoGravado { get; set; }
        public decimal AguinaldoGravado { get; set; }

        public decimal MontoGravadoCotizable { get; set; }

        public decimal ISSSPatronal { get; set; }
        public decimal ISSSEmpleado { get; set; }
        public decimal AFPPatronal { get; set; }
        public decimal AFPEmpleado { get; set; }

        public decimal MontoEmpleado { get; set; }
        public decimal MontoPlanilla { get; set; }
    }
}
