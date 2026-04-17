using Backend.DTOs;
using Backend.Repositorios;

namespace Backend.Servicios
{
    public class ConsultaServicio : IConsultaServicio
    {
        private readonly IConsultaReservas repositorio;

        public ConsultaServicio(IConsultaReservas repo)
        {
            repositorio = repo;
        }

        public List<VisualizacionDTO> Listar_Reservas_Admin()
        {
            return repositorio.Obtener_Reservas();
        }

        public bool Procesar_CheckIn(int Id)
        {
            return repositorio.Registrar_CheckIn(Id);
        }

        public bool Procesar_CheckOut(int Id)
        {
            return repositorio.Registrar_CheckOut(Id);
        }
    }
}