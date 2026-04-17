using Backend.DTOs;
using Backend.Repositorios;

namespace Backend.Servicios
{
    public class ReservarServicio : IReservarServicio
    {
        private readonly IReservarEstadia Repositorio;

        public ReservarServicio(IReservarEstadia Repository)
        {
            Repositorio = Repository;
        }

        public List<HabitacionDisponibleDTO> Consultar_Disponibilidad(DateTime Inicio, DateTime Fin)
        {
            if (Inicio.Date < DateTime.Now.Date || Fin.Date <= Inicio.Date)
            {
                return new List<HabitacionDisponibleDTO>();
            }

            return Repositorio.Obtener_Habitaciones(Inicio.Date, Fin.Date);
        }

        public bool Confirmar_Reserva(ReservarEstadiaDTO Datos)
        {
            var HabitacionesDisponibles = Repositorio.Obtener_Habitaciones(Datos.Fecha_Inicio, Datos.Fecha_Finalizacion);

            var HabitacionLibre = HabitacionesDisponibles.Any(h => h.Id == Datos.Id_Habitacion);

            if (!HabitacionLibre) return false;

            var InfoHabitacion = HabitacionesDisponibles.First(h => h.Id == Datos.Id_Habitacion);

            int DiasEstadia = (Datos.Fecha_Finalizacion.Date - Datos.Fecha_Inicio.Date).Days;

            if (DiasEstadia <= 0) DiasEstadia = 1;

            int PrecioTotalCalculado = DiasEstadia * InfoHabitacion.Precio_Noche;

            int IdNuevaEstadia = Repositorio.Guardar_Estadia(Datos, PrecioTotalCalculado);

            if (IdNuevaEstadia > 0)
            {
                foreach (string Documento in Datos.Documentos_Huespedes)
                {
                    Repositorio.Registrar_Estadia(IdNuevaEstadia, Documento);
                }
                return true;
            }

            return false;
        }
    }
}