using CedrosNahuizalquenos.Aplication.Interfaces;
using CedrosNahuizalquenos.Infrastructure.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace CedrosNahuizalquenos.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteResumenService _reporteResumenService;

        public ReporteController(IReporteResumenService context)
        {
            _reporteResumenService = context;
        }

        [HttpGet("resumen-empleado")]
        public async Task<IActionResult> GetResumenEmpleado(int? id)
        {
            var resultado = await _reporteResumenService.ObtenerResumenAsync(id);
            return Ok(resultado);
        }
        [HttpGet("resumen/excel")]
        public async Task<IActionResult> ExportarResumenExcel([FromQuery] int? empleadoId)
        {
            var resumen = await _reporteResumenService.ObtenerResumenAsync(empleadoId);

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Resumen Empleados");

            // Encabezados
            var headers = new[]
            {
                "Área", "ID Empleado", "Puesto Funcional", "Fecha Ingreso", "Fecha Corte", "Años en Empresa", "Salario Mensual",
                "HE Diurnas", "HE Nocturnas", "Horas Nocturnas",
                "Pago HE Diurnas", "Pago HE Nocturnas", "Pago Nocturnidad",
                "Vacaciones", "Aguinaldo Total", "Aguinaldo No Gravado", "Aguinaldo Gravado",
                "Monto Gravado Cotizable",
                "ISSS Patronal", "ISSS Empleado", "AFP Patronal", "AFP Empleado",
                "Monto Empleado", "Monto Planilla"
            };

            for (int i = 0; i < headers.Length; i++)
                worksheet.Cell(1, i + 1).Value = headers[i];

            // Datos
            for (int i = 0; i < resumen.Count; i++)
            {
                var r = resumen[i];
                worksheet.Cell(i + 2, 1).Value = r.Area;
                worksheet.Cell(i + 2, 2).Value = r.EmpleadoID;
                worksheet.Cell(i + 2, 3).Value = r.PuestoFuncional;
                worksheet.Cell(i + 2, 4).Value = r.FechaIngreso.ToString("dd/MM/yyyy");
                worksheet.Cell(i + 2, 5).Value = r.FechaCorte.ToString("dd/MM/yyyy");
                worksheet.Cell(i + 2, 6).Value = r.TiempoEnEmpresaAnios;
                worksheet.Cell(i + 2, 7).Value = r.TotalPorPuestoMensual;

                worksheet.Cell(i + 2, 8).Value = r.HorasExtrasDiurnas;
                worksheet.Cell(i + 2, 9).Value = r.HorasExtrasNocturnas;
                worksheet.Cell(i + 2, 10).Value = r.HorasNocturnas;

                worksheet.Cell(i + 2, 11).Value = r.PagoHorasExtrasDiurnas;
                worksheet.Cell(i + 2, 12).Value = r.PagoHorasExtrasNocturnas;
                worksheet.Cell(i + 2, 13).Value = r.PagoHorasNocturnidad;

                worksheet.Cell(i + 2, 14).Value = r.Vacaciones;
                worksheet.Cell(i + 2, 15).Value = r.AguinaldoTotal;
                worksheet.Cell(i + 2, 16).Value = r.AguinaldoNoGravado;
                worksheet.Cell(i + 2, 17).Value = r.AguinaldoGravado;

                worksheet.Cell(i + 2, 18).Value = r.MontoGravadoCotizable;

                worksheet.Cell(i + 2, 19).Value = r.ISSSPatronal;
                worksheet.Cell(i + 2, 20).Value = r.ISSSEmpleado;
                worksheet.Cell(i + 2, 21).Value = r.AFPPatronal;
                worksheet.Cell(i + 2, 22).Value = r.AFPEmpleado;

                worksheet.Cell(i + 2, 23).Value = r.MontoEmpleado;
                worksheet.Cell(i + 2, 24).Value = r.MontoPlanilla;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "ResumenEmpleados.xlsx");
        }

    }
}
