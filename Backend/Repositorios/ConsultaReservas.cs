using Backend.DTOs;
using MySqlConnector;

namespace Backend.Repositorios
{
    public class ConsultaReservas : IConsultaReservas
    {
        private readonly string Configuracion;

        public ConsultaReservas(IConfiguration Configuration)
        {
            Configuracion = Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public List<VisualizacionDTO> Obtener_Reservas()
        {
            var Reservas = new List<VisualizacionDTO>();
            using (var Conexion = new MySqlConnection(Configuracion))
            {
                string Query = @"
                    SELECT 
                        E.Id, E.Fecha_Inicio, E.Fecha_Finalizacion, E.Estado, E.Precio_Total,
                        H.Id AS Nro_Habitacion,
                        (SELECT CONCAT(Hu.Nombre, ' ', Hu.Apellido_Paterno) 
                         FROM Huesped_Estadia HE 
                         JOIN Huesped Hu ON HE.Id_Huesped = Hu.Id 
                         WHERE HE.Id_Estadia = E.Id LIMIT 1) AS Nombre_Cliente
                    FROM Estadia E
                    JOIN Habitacion H ON E.Id_Habitacion = H.Id
                    ORDER BY E.Fecha_Inicio ASC";

                using (MySqlCommand Comando = new MySqlCommand(Query, Conexion))
                {
                    Conexion.Open();
                    using (var reader = Comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Reservas.Add(new VisualizacionDTO
                            {
                                Id = (int)reader["Id"],
                                Fecha_Inicio = (DateTime)reader["Fecha_Inicio"],
                                Fecha_Finalizacion = (DateTime)reader["Fecha_Finalizacion"],
                                Estado = reader["Estado"].ToString(),
                                Nro_Habitacion = (int)reader["Nro_Habitacion"],
                                Precio_Total = (int)reader["Precio_Total"],
                                Nombre_Cliente = reader["Nombre_Cliente"]?.ToString() ?? "Sin Huésped"
                            });
                        }
                    }
                }
            }
            return Reservas;
        }

        public bool Registrar_CheckIn(int IdEstadia)
        {
            using (var Conexion = new MySqlConnection(Configuracion))
            {
                Conexion.Open();

                string QueryActivo = "UPDATE Estadia SET Estado = 'Activo' WHERE Id = @Id AND Estado = 'Reservado'";
                MySqlCommand Comando = new MySqlCommand(QueryActivo, Conexion);
                Comando.Parameters.AddWithValue("@Id", IdEstadia);

                int afectados = Comando.ExecuteNonQuery();

                if (afectados > 0)
                {
                    string QueryDetalle = "INSERT INTO Detalle_Estadia (Id_Estadia, Registro_Ingreso, Multa) VALUES (@Id, NOW(), 0)";
                    MySqlCommand Comando2 = new MySqlCommand(QueryDetalle, Conexion);
                    Comando2.Parameters.AddWithValue("@Id", IdEstadia);
                    Comando2.ExecuteNonQuery();
                    return true;
                }
                return false;
            }
        }

        public bool Registrar_CheckOut(int IdEstadia)
        {
            using (var Conexion = new MySqlConnection(Configuracion))
            {
                Conexion.Open();

                string QueryFinalizado = "UPDATE Estadia SET Estado = 'Finalizada' WHERE Id = @Id AND Estado = 'Activo'";
                MySqlCommand Comando = new MySqlCommand(QueryFinalizado, Conexion);
                Comando.Parameters.AddWithValue("@Id", IdEstadia);

                if (Comando.ExecuteNonQuery() > 0)
                {
                    string QueryDetalle = "UPDATE Detalle_Estadia SET Registro_Salida = NOW() WHERE Id_Estadia = @Id";
                    MySqlCommand Comando2 = new MySqlCommand(QueryDetalle, Conexion);
                    Comando2.Parameters.AddWithValue("@Id", IdEstadia);
                    Comando2.ExecuteNonQuery();
                    return true;
                }
                return false;
            }
        }
    }
}