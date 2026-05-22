namespace Backend.DTOs
{
    public class VisualizacionDTO
    {
        public int Id { get; set; }

        public DateTime Fecha_Inicio { get; set; }

        public DateTime Fecha_Finalizacion { get; set; }

        public string Estado { get; set; }

        public int Nro_Habitacion { get; set; }

        public int Precio_Total { get; set; }

        public string Nombre_Cliente { get; set; }
    }
}