using Backend.DTOs;

namespace Backend.Repositorios
{
    internal interface IConsultaReservas
    {
        List<VisualizacionDTO> Obtener_Reservas();

        bool Registrar_CheckIn(int IdEstadia);

        bool Registrar_CheckOut(int IdEstadia);
    }
}