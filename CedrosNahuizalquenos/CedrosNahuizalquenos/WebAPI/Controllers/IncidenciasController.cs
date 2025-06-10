using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CedrosNahuizalquenos.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncidenciasController : ControllerBase
    {
        private readonly IIncidenciaService _service;

        public IncidenciasController(IIncidenciaService service)
        {
            _service = service;
        }

        [HttpGet("empleado/{empleadoId}")]
        public async Task<ActionResult<List<IncidenciaDto>>> GetByEmpleado(int empleadoId)
        {
            var incidencias = await _service.GetByEmpleadoAsync(empleadoId);
            return Ok(incidencias);
        }
        [HttpGet]
        public async Task<ActionResult<List<IncidenciaDto>>> GetAll()
        {
            var incidencias = await _service.GetAll();
            return Ok(incidencias);
        }
        [HttpPost]
        public async Task<ActionResult<int>> Registrar([FromBody] IncidenciaDto dto)
        {
            var id = await _service.RegistrarIncidenciaAsync(dto);
            return Ok(id);
        }
    }
}
