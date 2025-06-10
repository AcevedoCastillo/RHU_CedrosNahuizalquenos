using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.DTOs;
using CedrosNahuizalquenos.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CedrosNahuizalquenos.Infrastructure.Services
{
    public class ReporteResumenEmpleadoService : IReporteResumenService
    {
        private readonly ApplicationDbContext _context;

        public ReporteResumenEmpleadoService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ReporteResumenEmpleadoDto>> ObtenerResumenAsync(int? empleado)
        {
            var clienteIdParam = new SqlParameter("@EmpleadoID", empleado ?? (object)DBNull.Value);

            return await _context.Set<ReporteResumenEmpleadoDto>()
                .FromSqlRaw("EXEC sp_ReporteResumenEmpleado @EmpleadoID",
                            clienteIdParam)
                .ToListAsync();
        }
    }
}
