namespace Backend.Modelos
{
    public class Estadia
    {
        public int Id { get; set; }

        public DateTime Fecha_Inicio { get; set; }

        public DateTime Fecha_Finalizacion { get; set; }

        public string Estado { get; set; } 

        public int Id_Habitacion { get; set; }

        public int Precio_Total { get; set; }
    }
}