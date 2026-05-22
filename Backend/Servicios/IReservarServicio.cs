using Backend.DTOs;

namespace Backend.Servicios
{
    public interface IReservarServicio
    {
        List<HabitacionDisponibleDTO> Consultar_Disponibilidad(DateTime Inicio, DateTime Fin);

        bool Confirmar_Reserva(ReservarEstadiaDTO Datos);
    }
}