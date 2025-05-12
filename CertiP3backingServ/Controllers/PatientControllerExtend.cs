using CertiP3backingServ.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CertiP3backingServ.Controllers
{
    public class PatientControllerExtend : Controller
    {
        [HttpPost("/api/patientcode")]
        public IActionResult CalculateCode([FromBody]Patient input)
        {
            //string code = $"{input.Name[0]}{input.LastName[0]}-{input.CI}";
            //return Ok(code);
            try
            {
                Log.Information("Recibida solicitud para calcular código de paciente con CI: {CI}", input.CI);

                if (string.IsNullOrWhiteSpace(input.Name) || string.IsNullOrWhiteSpace(input.LastName) || string.IsNullOrWhiteSpace(input.CI))
                {
                    Log.Warning("Datos inválidos en la solicitud: {@Input}", input);
                    return BadRequest("Nombre, apellido y CI son obligatorios.");
                }

                string code = $"{input.Name[0]}{input.LastName[0]}-{input.CI}";

                Log.Information("Código generado correctamente: {Code}", code);
                return Ok(code);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error al calcular código para el paciente: {@Input}", input);
                return StatusCode(500, "Error interno al generar el código del paciente.");
            }
        }
    }
}
