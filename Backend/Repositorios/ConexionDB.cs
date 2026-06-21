using MySqlConnector;
namespace Backend.Repositorios
{
    public class ConexionDB
    {
        public static MySqlConnection  GenerarConexion(string configuracion)
        {
            var conexion = new MySqlConnection(configuracion);
            conexion.Open();
            return conexion;
        }
    }
}