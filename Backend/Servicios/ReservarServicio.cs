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
            var InfoHabitacion = BuscarHabitacionDisponible(Datos);

            if(InfoHabitacion == null) { return false; }

            int DiasEstadia = (Datos.Fecha_Finalizacion.Date - Datos.Fecha_Inicio.Date).Days;


            int PrecioTotalCalculado = Calcular_Costo(DiasEstadia, InfoHabitacion.Precio_Noche);

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

        public int Calcular_Costo(int Dias_Estadia, int Precio_Por_Noche)
        {
            if(Dias_Estadia <= 0)
            {
                Dias_Estadia = 1;
            }

            return Dias_Estadia * Precio_Por_Noche;
        }

        public HabitacionDisponibleDTO? BuscarHabitacionDisponible(ReservarEstadiaDTO Datos)
        {
            var HabitacionesDisponibles = Consultar_Disponibilidad(Datos.Fecha_Inicio, Datos.Fecha_Finalizacion);

            return HabitacionesDisponibles.FirstOrDefault(h => h.Id == Datos.Id_Habitacion);
        }
    }
}