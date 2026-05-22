using Backend.DTOs;

namespace Backend.Repositorios
{
    internal interface IReservarEstadia
    {
        List<HabitacionDisponibleDTO> Obtener_Habitaciones(DateTime Inicio, DateTime Fin);

        int Guardar_Estadia(ReservarEstadiaDTO Estadia, int PrecioTotal);

        void Registrar_Estadia(int IdEstadia, string Documento);
    }
}