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
            throw new NotImplementedException();
        }

        public int Calcular_Costo(int Dias_Estadia, int Precio_Por_Noche)
        {
            if(Dias_Estadia <= 0)
            {
                Dias_Estadia = 1;
            }

            return Dias_Estadia * Precio_Por_Noche;
        }
    }
}