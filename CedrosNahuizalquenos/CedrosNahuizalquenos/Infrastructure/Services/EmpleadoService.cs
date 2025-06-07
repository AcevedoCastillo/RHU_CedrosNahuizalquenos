using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.Domain.Entities;
using CedrosNahuizalquenos.DTOs;
using CedrosNahuizalquenos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CedrosNahuizalquenos.Infrastructure.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly ApplicationDbContext _context;

        public EmpleadoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmpleadoDto>> GetAllAsync()
        {
            return await _context.Empleados
                .Where(e => e.Activo)
                .Select(e => new EmpleadoDto
                {
                    EmpleadoID = e.EmpleadoId,
                    Nombre = e.Nombre,
                    Edad = e.Edad,
                    FechaIngreso = e.FechaIngreso,
                    Area = e.Area,
                    PuestoFuncional = e.PuestoFuncional,
                    SalarioMensual = e.SalarioMensual,
                    Activo = e.Activo
                }).ToListAsync();
        }

        public async Task<EmpleadoDto?> GetByIdAsync(int id)
        {
            var e = await _context.Empleados.FindAsync(id);
            if (e == null || !e.Activo) return null;

            return new EmpleadoDto
            {
                EmpleadoID = e.EmpleadoId,
                Nombre = e.Nombre,
                Edad = e.Edad,
                FechaIngreso = e.FechaIngreso,
                Area = e.Area,
                PuestoFuncional = e.PuestoFuncional,
                SalarioMensual = e.SalarioMensual,
                Activo = e.Activo
            };
        }

        public async Task<int> CreateAsync(EmpleadoDto dto)
        {
            var empleado = new Empleado
            {
                Nombre = dto.Nombre,
                Edad = dto.Edad,
                FechaIngreso = dto.FechaIngreso,
                Area = dto.Area,
                PuestoFuncional = dto.PuestoFuncional,
                SalarioMensual = dto.SalarioMensual,
                Activo = true
            };

            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();

            return empleado.EmpleadoId;
        }

        public async Task<bool> UpdateAsync(int id, EmpleadoDto dto)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null || !empleado.Activo) return false;

            empleado.Nombre = dto.Nombre;
            empleado.Edad = dto.Edad;
            empleado.FechaIngreso = dto.FechaIngreso;
            empleado.Area = dto.Area;
            empleado.PuestoFuncional = dto.PuestoFuncional;
            empleado.SalarioMensual = dto.SalarioMensual;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null || !empleado.Activo) return false;

            empleado.Activo = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
