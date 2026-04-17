namespace Backend.DTOs
{
    public class ReservarEstadiaDTO
    {
        public DateTime Fecha_Inicio { get; set; }

        public DateTime Fecha_Finalizacion { get; set; }

        public int Id_Habitacion { get; set; }

        public List<string> Documentos_Huespedes { get; set; }
    }
}