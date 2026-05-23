namespace Backend.DTOs
{
    public class HabitacionDisponibleDTO
    {
        public required int Id { get; set; }

        public required string Tipo_Nombre { get; set; }

        public required int Precio_Noche { get; set; }
    }
}