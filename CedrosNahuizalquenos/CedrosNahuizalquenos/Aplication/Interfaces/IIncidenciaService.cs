using CedrosNahuizalquenos.DTOs;

namespace CedrosNahuizalquenos.Aplication.Interfaces
{
    public interface IIncidenciaService
    {
        Task<List<IncidenciaDto>> GetByEmpleadoAsync(int empleadoId);
        Task<int> RegistrarIncidenciaAsync(IncidenciaDto dto);
    }
}
