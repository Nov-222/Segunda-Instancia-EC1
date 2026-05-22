using Backend.DTOs;

namespace Backend.Servicios
{
    public interface IConsultaServicio
    {
        List<VisualizacionDTO> Listar_Reservas_Admin();

        bool Procesar_CheckIn(int Id);

        bool Procesar_CheckOut(int Id);
    }
}