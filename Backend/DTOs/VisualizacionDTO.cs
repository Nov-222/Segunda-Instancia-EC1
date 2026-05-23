namespace Backend.DTOs
{
    public class VisualizacionDTO
    {
        public required int Id { get; set; }

        public required DateTime Fecha_Inicio { get; set; }

        public required DateTime Fecha_Finalizacion { get; set; }

        public required string Estado { get; set; }

        public required int Nro_Habitacion { get; set; }

        public required int Precio_Total { get; set; }

        public required string Nombre_Cliente { get; set; }
    }
}