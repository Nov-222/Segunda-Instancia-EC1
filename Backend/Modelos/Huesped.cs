namespace Backend.Modelos
{
    public class Huesped
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string Documento { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public DateTime Fecha_Nacimiento { get; set; }
    }
}

