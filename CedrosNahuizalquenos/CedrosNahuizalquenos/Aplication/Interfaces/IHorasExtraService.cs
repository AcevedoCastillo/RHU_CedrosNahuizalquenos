using CedrosNahuizalquenos.DTOs;

namespace CedrosNahuizalquenos.Aplication.Interfaces
{
    public interface IHorasExtraService
    {
        Task<List<HorasExtraDto>> GetByEmpleadoAsync(int empleadoId);
        Task<int> RegistrarHorasAsync(HorasExtraDto dto);
    }
}
