using Backend.DTOs;
using MySqlConnector;

namespace Backend.Repositorios
{
    public class ReservarEstadia : IReservarEstadia
    {
        private readonly string Configuracion;

        public ReservarEstadia(IConfiguration Configuration)
        {
            Configuracion = Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public List<HabitacionDisponibleDTO> Obtener_Habitaciones(DateTime Inicio, DateTime Fin)
        {
            var Habitaciones = new List<HabitacionDisponibleDTO>();
            using (var Conexion = new MySqlConnection(Configuracion))
            {
                string Query = @"
                    SELECT h.Id, th.Nombre, th.Precio_Noche 
                    FROM Habitacion h
                    INNER JOIN Tipo_Habitacion th ON h.Id_TH = th.Id
                    WHERE h.Id NOT IN (
                        SELECT Id_Habitacion FROM Estadia 
                        WHERE Estado IN ('Reservado', 'Activo') 
                        AND (Fecha_Inicio < @Fin AND Fecha_Finalizacion > @Inicio)
                    )";

                MySqlCommand Comando = new MySqlCommand(Query, Conexion);
                Comando.Parameters.AddWithValue("@Inicio", Inicio);
                Comando.Parameters.AddWithValue("@Fin", Fin);

                Conexion.Open();
                using (var Lector = Comando.ExecuteReader())
                {
                    while (Lector.Read())
                    {
                        Habitaciones.Add(new HabitacionDisponibleDTO
                        {
                            Id = (int)Lector["Id"],
                            Tipo_Nombre = Lector["Nombre"].ToString(),
                            Precio_Noche = (int)Lector["Precio_Noche"]
                        });
                    }
                }
            }
            return Habitaciones;
        }

        public int Guardar_Estadia(ReservarEstadiaDTO Estadia, int PrecioTotal)
        {
            using (var Conexion = new MySqlConnection(Configuracion))
            {
                string QueryId = "SELECT IFNULL(MAX(Id), 0) + 1 FROM Estadia";
                Conexion.Open();

                using (MySqlCommand ComandoId = new MySqlCommand(QueryId, Conexion))
                {
                    int nuevoId = Convert.ToInt32(ComandoId.ExecuteScalar());

                    string QueryInsert = @"
                    INSERT INTO Estadia (Id, Fecha_Inicio, Fecha_Finalizacion, Estado, Id_Habitacion, Precio_Total) 
                    VALUES (@Id, @Ini, @Fin, @Est, @IdH, @Pre)";

                    MySqlCommand Comando = new MySqlCommand(QueryInsert, Conexion);
                    Comando.Parameters.AddWithValue("@Id", nuevoId);
                    Comando.Parameters.AddWithValue("@Ini", Estadia.Fecha_Inicio);
                    Comando.Parameters.AddWithValue("@Fin", Estadia.Fecha_Finalizacion);
                    Comando.Parameters.AddWithValue("@Est", "Reservado");
                    Comando.Parameters.AddWithValue("@IdH", Estadia.Id_Habitacion);
                    Comando.Parameters.AddWithValue("@Pre", PrecioTotal);

                    Comando.ExecuteNonQuery();

                    return nuevoId;
                }
            }
        }

        public void Registrar_Estadia(int IdEstadia, string Documento)
        {
            using (var Conexion = new MySqlConnection(Configuracion))
            {
                string Query = @"
                 INSERT INTO Huesped_Estadia (Id_Estadia, Id_Huesped)
                SELECT @IdE, Id  FROM Huesped WHERE Documento = @Doc";

                MySqlCommand Comando = new MySqlCommand(Query, Conexion);
                Comando.Parameters.AddWithValue("@IdE", IdEstadia);
                Comando.Parameters.AddWithValue("@Doc", Documento);

                Conexion.Open();

                Comando.ExecuteNonQuery();
            }
        }
    }
}