using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CedrosNahuizalquenos.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadoService _service;

        public EmpleadosController(IEmpleadoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmpleadoDto>>> GetAll()
        {
            var empleados = await _service.GetAllAsync();
            return Ok(empleados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpleadoDto>> GetById(int id)
        {
            var empleado = await _service.GetByIdAsync(id);
            return empleado == null ? NotFound() : Ok(empleado);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] EmpleadoDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmpleadoDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}
