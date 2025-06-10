using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.Domain.Entities;
using CedrosNahuizalquenos.DTOs;
using CedrosNahuizalquenos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CedrosNahuizalquenos.Infrastructure.Services
{
    public class IncidenciaService : IIncidenciaService
    {
        private readonly ApplicationDbContext _context;

        public IncidenciaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<IncidenciaDto>> GetByEmpleadoAsync(int empleadoId)
        {
            return await _context.Incidencias
                .Where(i => i.EmpleadoId == empleadoId)
                .Select(i => new IncidenciaDto
                {
                    IncidenciaID = i.IncidenciaId,
                    EmpleadoID = i.EmpleadoId,
                    Tipo = i.Tipo,
                    FechaInicio = i.FechaInicio,
                    FechaFin = i.FechaFin,
                    Comentario = i.Comentario
                }).ToListAsync();
        }
        public async Task<List<IncidenciaDto>> GetAll()
        {
            return await _context.Incidencias
                .Select(i => new IncidenciaDto
                {
                    IncidenciaID = i.IncidenciaId,
                    EmpleadoID = i.EmpleadoId,
                    Tipo = i.Tipo,
                    FechaInicio = i.FechaInicio,
                    FechaFin = i.FechaFin,
                    Comentario = i.Comentario
                }).ToListAsync();
        }
        public async Task<int> RegistrarIncidenciaAsync(IncidenciaDto dto)
        {
            var incidencia = new Incidencia
            {
                EmpleadoId = dto.EmpleadoID,
                Tipo = dto.Tipo,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                Comentario = dto.Comentario
            };

            _context.Incidencias.Add(incidencia);
            await _context.SaveChangesAsync();

            return incidencia.IncidenciaId;
        }
    }
}
