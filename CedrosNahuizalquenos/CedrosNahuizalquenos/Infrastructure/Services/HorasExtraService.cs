using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.Domain.Entities;
using CedrosNahuizalquenos.DTOs;
using CedrosNahuizalquenos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CedrosNahuizalquenos.Infrastructure.Services
{
    public class HorasExtraService : IHorasExtraService
    {
        private readonly ApplicationDbContext _context;

        public HorasExtraService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<HorasExtraDto>> GetByEmpleadoAsync(int empleadoId)
        {
            return await _context.HorasExtras
                .Where(h => h.EmpleadoId == empleadoId)
                .Select(h => new HorasExtraDto
                {
                    HoraExtraID = h.HoraExtraId,
                    EmpleadoID = h.EmpleadoId,
                    Fecha = h.Fecha,
                    Tipo = h.Tipo,
                    CantidadHoras = h.CantidadHoras
                }).ToListAsync();
        }
        public async Task<List<HorasExtraDto>> GetAll()
        {
            return await _context.HorasExtras
                .Select(h => new HorasExtraDto
                {
                    HoraExtraID = h.HoraExtraId,
                    EmpleadoID = h.EmpleadoId,
                    Fecha = h.Fecha,
                    Tipo = h.Tipo,
                    CantidadHoras = h.CantidadHoras
                }).ToListAsync();
        }

        public async Task<int> RegistrarHorasAsync(HorasExtraDto dto)
        {
            var horas = new HorasExtra
            {
                EmpleadoId = dto.EmpleadoID,
                Fecha = dto.Fecha,
                Tipo = dto.Tipo,
                CantidadHoras = dto.CantidadHoras
            };

            _context.HorasExtras.Add(horas);
            await _context.SaveChangesAsync();

            return horas.HoraExtraId;
        }
    }
}
