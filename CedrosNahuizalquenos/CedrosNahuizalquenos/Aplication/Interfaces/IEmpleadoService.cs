using CedrosNahuizalquenos.DTOs;

namespace CedrosNahuizalquenos.Aplication.Interfaces
{
    public interface IEmpleadoService
    {
        Task<List<EmpleadoDto>> GetAllAsync();
        Task<EmpleadoDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(EmpleadoDto dto);
        Task<bool> UpdateAsync(int id, EmpleadoDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
