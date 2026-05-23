namespace Backend.DTOs
{
    public class ReservarEstadiaDTO
    {
        public required DateTime Fecha_Inicio { get; set; }

        public required DateTime Fecha_Finalizacion { get; set; }

        public required int Id_Habitacion { get; set; }

        public required List<string> Documentos_Huespedes { get; set; }
    }
}