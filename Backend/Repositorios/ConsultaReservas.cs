using Backend.DTOs;
using Microsoft.Data.SqlClient;

namespace Backend.Repositorios
{
    public class ConsultaReservas : IConsultaReservas
    {
        private readonly string Configuracion;

        public ConsultaReservas(IConfiguration Configuration)
        {
            Configuracion = Configuration.GetConnectionString("DefaultConnection");
        }

        public List<VisualizacionDTO> Obtener_Reservas()
        {
            var Reservas = new List<VisualizacionDTO>();
            using (var Conexion = new SqlConnection(Configuracion))
            {
                string Query = @"
                    SELECT 
                        E.Id, E.Fecha_Inicio, E.Fecha_Finalizacion, E.Estado, E.Precio_Total,
                        H.Id AS Nro_Habitacion,
                        (SELECT TOP 1 (Hu.Nombre + ' ' + Hu.Apellido_Paterno) 
                         FROM Huesped_Estadia HE 
                         JOIN Huesped Hu ON HE.Id_Huesped = Hu.Id 
                         WHERE HE.Id_Estadia = E.Id) AS Nombre_Cliente
                    FROM Estadia E
                    JOIN Habitacion H ON E.Id_Habitacion = H.Id
                    ORDER BY E.Fecha_Inicio ASC";

                SqlCommand Comando = new SqlCommand(Query, Conexion);
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
            return Reservas;
        }

        public bool Registrar_CheckIn(int IdEstadia)
        {
            using (var Conexion = new SqlConnection(Configuracion))
            {
                Conexion.Open();

                string QueryActivo = "UPDATE Estadia SET Estado = 'Activo' WHERE Id = @Id AND Estado = 'Reservado'";
                SqlCommand Comando = new SqlCommand(QueryActivo, Conexion);
                Comando.Parameters.AddWithValue("@Id", IdEstadia);

                int afectados = Comando.ExecuteNonQuery();

                if (afectados > 0)
                {
                    string QueryDetalle = "INSERT INTO Detalle_Estadia (Id_Estadia, Registro_Ingreso, Multa) VALUES (@Id, GETDATE(), 0)";
                    SqlCommand Comando2 = new SqlCommand(QueryDetalle, Conexion);
                    Comando2.Parameters.AddWithValue("@Id", IdEstadia);
                    Comando2.ExecuteNonQuery();
                    return true;
                }
                return false;
            }
        }

        public bool Registrar_CheckOut(int IdEstadia)
        {
            using (var Conexion = new SqlConnection(Configuracion))
            {
                Conexion.Open();

                string QueryFinalizado = "UPDATE Estadia SET Estado = 'Finalizada' WHERE Id = @Id AND Estado = 'Activo'";
                SqlCommand Comando = new SqlCommand(QueryFinalizado, Conexion);
                Comando.Parameters.AddWithValue("@Id", IdEstadia);

                if (Comando.ExecuteNonQuery() > 0)
                {
                    string QueryDetalle = "UPDATE Detalle_Estadia SET Registro_Salida = GETDATE() WHERE Id_Estadia = @Id";
                    SqlCommand Comando2 = new SqlCommand(QueryDetalle, Conexion);
                    Comando2.Parameters.AddWithValue("@Id", IdEstadia);
                    Comando2.ExecuteNonQuery();
                    return true;
                }
                return false;
            }
        }
    }
}