namespace Backend.Modelos
{
    public class Tipo_Habitacion
    {
        public int Id { get; set; }

        public string Nombre { get; set; } 

        public int Capacidad { get; set; } 

        public string Descripcion { get; set; }

        public string Imagen { get; set; } 

        public int Precio_Noche { get; set; } 
    }
}