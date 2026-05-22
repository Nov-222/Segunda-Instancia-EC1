using Microsoft.AspNetCore.Mvc;
using Backend.DTOs;
using Backend.Servicios;

namespace Backend.Controladores
{
    [ApiController]
    [Route("api/reserva")]
    public class ReservaControlador : ControllerBase
    {
        private readonly IReservarServicio Servicio;

        public ReservaControlador(IReservarServicio Service)
        {
            Servicio = Service;
        }

        [HttpGet("disponibilidad")]
        public IActionResult Consultar_Disponibilidad([FromQuery] DateTime Inicio, [FromQuery] DateTime Fin)
        {
            var Habitaciones = Servicio.Consultar_Disponibilidad(Inicio, Fin);

            if (Habitaciones.Count == 0)
            {
                return NotFound("No hay habitaciones disponibles para estas fechas.");
            }

            return Ok(Habitaciones);
        }

        [HttpPost("reservar")]
        public IActionResult Confirmar_Reserva([FromBody] ReservarEstadiaDTO Datos)
        {
            var Resultado = Servicio.Confirmar_Reserva(Datos);

            if (Resultado)
            {
                return Ok(new { mensaje = "Reserva realizada con éxito." });
            }

            return BadRequest("No se pudo concretar la reserva. Verifique la disponibilidad.");
        }
    }
}