using CedrosNahuizalquenos.DTOs;

namespace CedrosNahuizalquenos.Aplication.Interfaces
{
    public interface IReporteResumenService
    {
        Task<List<ReporteResumenEmpleadoDto>> ObtenerResumenAsync(int? empleado);
    }
}
