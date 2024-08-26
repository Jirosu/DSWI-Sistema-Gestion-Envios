using SistemaGestionEnvios.Models;
using SistemaGestionEnvios.Repository.Contrato;
using System.Data;
using System.Data.SqlClient;

namespace SistemaGestionEnvios.Repository.Implementacion
{
    public class UsuarioRepository : IUsuarioRepository<Usuario>
    {

        private readonly string _cadenaSQL = "";

        public UsuarioRepository(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("cadenaSQL");
        }

        public bool validarCredenciales(string username, string password)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("sp_BuscarUsuarioLogin", conexion);
                cmd.Parameters.AddWithValue("user_usu", username);
                cmd.Parameters.AddWithValue("password_usu", password);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return true;
                }
                else { return false; }
            }
        }
    }
}
