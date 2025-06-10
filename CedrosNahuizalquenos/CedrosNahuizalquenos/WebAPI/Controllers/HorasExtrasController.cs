using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CedrosNahuizalquenos.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorasExtrasController : ControllerBase
    {
        private readonly IHorasExtraService _service;

        public HorasExtrasController(IHorasExtraService service)
        {
            _service = service;
        }

        [HttpGet("empleado/{empleadoId}")]
        public async Task<ActionResult<List<HorasExtraDto>>> GetByEmpleado(int empleadoId)
        {
            var horas = await _service.GetByEmpleadoAsync(empleadoId);
            return Ok(horas);
        }
        [HttpGet]
        public async Task<ActionResult<List<HorasExtraDto>>> GetAll()
        {
            var horas = await _service.GetAll();
            return Ok(horas);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Registrar([FromBody] HorasExtraDto dto)
        {
            var id = await _service.RegistrarHorasAsync(dto);
            return Ok(id);
        }
    }
}
