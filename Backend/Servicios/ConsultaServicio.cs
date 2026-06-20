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
            throw new NotImplementedException();
        }

        public bool Procesar_CheckIn(int Id)
        {
            throw new NotImplementedException();
        }

        public bool Procesar_CheckOut(int Id)
        {
            throw new NotImplementedException();
        }
    }
}