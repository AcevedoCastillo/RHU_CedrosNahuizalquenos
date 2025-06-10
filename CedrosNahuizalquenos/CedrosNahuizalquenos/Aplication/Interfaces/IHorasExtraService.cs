using CedrosNahuizalquenos.DTOs;

namespace CedrosNahuizalquenos.Aplication.Interfaces
{
    public interface IHorasExtraService
    {
        Task<List<HorasExtraDto>> GetByEmpleadoAsync(int empleadoId);
        Task<List<HorasExtraDto>> GetAll();
        Task<int> RegistrarHorasAsync(HorasExtraDto dto);
    }
}
