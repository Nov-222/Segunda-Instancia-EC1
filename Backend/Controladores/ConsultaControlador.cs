using Backend.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/consulta")]
    public class ConsultaControlador : ControllerBase
    {
        private readonly IConsultaServicio servicio;

        public ConsultaControlador(IConsultaServicio service)
        {
            servicio = service;
        }

        [HttpGet("reservas")]
        public IActionResult GetReservas()
        {
            try
            {
                var resultado = servicio.Listar_Reservas_Admin();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("checkin/{id}")]
        public IActionResult CheckIn(int Id)
        {
            if (servicio.Procesar_CheckIn(Id)) return Ok();
            return BadRequest("No se pudo realizar el Check-in");
        }

        [HttpPut("checkout/{Id}")]
        public IActionResult CheckOut(int Id)
        {
            if (servicio.Procesar_CheckOut(Id)) return Ok();
            return BadRequest("No se pudo realizar el Check-Out");
        }
    }
}