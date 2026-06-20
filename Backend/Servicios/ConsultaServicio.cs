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
    }
}